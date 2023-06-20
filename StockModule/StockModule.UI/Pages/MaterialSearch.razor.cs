using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Radzen;
using Radzen.Blazor;
using StockModule.UI.Data;
using StockModule.UI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;

namespace StockModule.UI.Pages
{
    public partial class MaterialSearch
    {
        bool isLoading = false;
        int count;
        FolderHierarchyVM? selectedFldrhrchy;
        FolderHierarchyVM? selectedFldrhrchyMaterial;
        IEnumerable<FolderHierarchyVM>? folderHierarchyVMs;
        IEnumerable<FolderHierarchyVM>? parentfolderHierarchyVMs;

        IEnumerable<StockSettingsMaterial_FolderHierarchyVM>? stockSettingsMaterial_FolderHierarchyVMs;
        IEnumerable<StockSettingsMaterial_FolderHierarchyVM>? filtered_StockSettingsMaterial_FolderHierarchyVMs;
        IList<StockSettingsMaterial_FolderHierarchyVM>? selectedstockSettingsMaterial_Folders;
        StockSettingsMaterial_FolderHierarchyVM? doubleClickedstockSettingsMaterial_Folders;
        bool sidebar4Expanded = true;

        RadzenDataGrid<StockSettingsMaterial_FolderHierarchyVM> materialGrid;
        RadzenDataFilter<StockSettingsMaterial_FolderHierarchyVM> dataFilter;
        //RadzenTree hirechyTree;

        IList<StockSettingsMaterial_FolderHierarchyVM> selectedStockSettingsMaterial_FolderHierarchyVMs;
        bool hasChildren;

        IEnumerable<int> selectedIds;
        IEnumerable<int> materialIds;

        bool auto = true;

        #region DataFilter
        void OnSelectedIdsChange(object value)
        {
            if (selectedIds != null && !selectedIds.Any())
            {
                selectedIds = null;
            }
        }
        #endregion

        #region Page
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await base.OnInitializedAsync();
            await LoadTreeRootsAsync();
            await LoadGridAsync();
            isLoading = false;
        }

        private async Task LoadGridAsync()
        {
            stockSettingsMaterial_FolderHierarchyVMs = await StockSettingsMaterial_FolderHierarchyService.GetAllAsync();
            count = stockSettingsMaterial_FolderHierarchyVMs.Count();
            materialIds = stockSettingsMaterial_FolderHierarchyVMs.Select(x => x.Id).Distinct();
        }
        private async void LoadGrid()
        {
            stockSettingsMaterial_FolderHierarchyVMs = await StockSettingsMaterial_FolderHierarchyService.GetAllAsync();
            count = stockSettingsMaterial_FolderHierarchyVMs.Count();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await dataFilter.AddFilter(new CompositeFilterDescriptor()
                {
                    Property = "StockSettingsMaterial_FolderHierarchyVM.Code",
                    FilterValue = "AZF",
                    FilterOperator = FilterOperator.Contains
                });
            }
        }
        #endregion

        #region Grid

        void ClearRowSelection()
        {
            selectedstockSettingsMaterial_Folders = null;
        }

        void OnGroup(DataGridColumnGroupEventArgs<StockSettingsMaterial_FolderHierarchyVM> args)
        {
            //console.Log($"DataGrid {(args.GroupDescriptor != null ? "grouped" : "ungrouped")} by {args.Column.GetGroupProperty()}");
        }

        void OnRender(DataGridRenderEventArgs<StockSettingsMaterial_FolderHierarchyVM> args)
        {
            //if (args.FirstRender)
            //{
            //    args.Grid.Groups.Add(new GroupDescriptor() { Property = "Type", SortOrder = SortOrder.Descending });
            //    StateHasChanged();
            //}
        }

        async void LoadData(LoadDataArgs args)
        {
            isLoading = true;
            stockSettingsMaterial_FolderHierarchyVMs = await StockSettingsMaterial_FolderHierarchyService.GetCustomVirtualization(args.Filter, args.OrderBy, args.Skip, args.Top);
            count = await StockSettingsMaterial_FolderHierarchyService.GetCustomVirtualizationCount(args.Filter);


            isLoading = false;

            await InvokeAsync(() => { StateHasChanged(); });
        }

        void OnCellContextMenu(DataGridCellMouseEventArgs<StockSettingsMaterial_FolderHierarchyVM> args)
        {
            if (selectedFldrhrchy != null)
            {
                selectedStockSettingsMaterial_FolderHierarchyVMs = new List<StockSettingsMaterial_FolderHierarchyVM>() { args.Data };
                if (selectedstockSettingsMaterial_Folders == null)
                {
                    selectedstockSettingsMaterial_Folders = new List<StockSettingsMaterial_FolderHierarchyVM>();
                }

                if (selectedstockSettingsMaterial_Folders != null && !selectedstockSettingsMaterial_Folders.Contains(selectedStockSettingsMaterial_FolderHierarchyVMs[0]))
                {
                    selectedstockSettingsMaterial_Folders.Add(selectedStockSettingsMaterial_FolderHierarchyVMs[0]);
                }

                ContextMenuService.Open(args,
                    new List<ContextMenuItem> {
                        new ContextMenuItem(){ Text = "Add to the selected folder", Value = 1 }
                            }, OnGridMenuItemClick);
            }
        }

        async void OnGridMenuItemClick(MenuItemEventArgs e)
        {
            if (e.Value.Equals(1))
            {
                List<int> ids = selectedstockSettingsMaterial_Folders.Select(x => x.Id).ToList();

                var response = await StockSettingsService.UpdateStockSetting(ids, selectedFldrhrchy.Id);

                if (response.IsSuccessStatusCode)
                {
                    parentfolderHierarchyVMs = null;// new List<FolderHierarchyVM>() { selectedFldrhrchy };

                    ContextMenuService.Close();

                    await LoadChildrenAsync();
                    NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Selected Material/s has been added to the selected Folder has been saved successfully." });
                    await materialGrid.SelectRow(selectedstockSettingsMaterial_Folders[0]);
                    //uriHelper.NavigateTo(uriHelper.Uri, forceLoad: true);
                    await InvokeAsync(() => { materialGrid.Reset(); });
                    await InvokeAsync(() => { materialGrid.Reload(); });
                    await InvokeAsync(() => { StateHasChanged(); });
                }
                else
                {
                    ContextMenuService.Close();
                    NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Failed to add the selected Material/s to the selected folder." });
                }
            }
        }
        async Task OnRowDoubleClickAsync(DataGridRowMouseEventArgs<StockSettingsMaterial_FolderHierarchyVM> args)
        {
            selectedStockSettingsMaterial_FolderHierarchyVMs = new List<StockSettingsMaterial_FolderHierarchyVM>() { args.Data };

            doubleClickedstockSettingsMaterial_Folders = selectedStockSettingsMaterial_FolderHierarchyVMs[0] ;


            parentfolderHierarchyVMs = await FolderHierarchyService.GetAllParentFoldersByMaterialId(selectedStockSettingsMaterial_FolderHierarchyVMs[0].Id);
            if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.ToList().Count > 0)
            {
                hasChildren = await FolderHierarchyService.HasChiledrenAsync(parentfolderHierarchyVMs.ToList()[0].Id);

                selectedFldrhrchy = null;
                selectedFldrhrchyMaterial = new FolderHierarchyVM()
                {
                    HierarchyType = (int)EnumHierarchyType.Material,
                    Icon = "",
                    Id = selectedStockSettingsMaterial_FolderHierarchyVMs[0].Id,
                    Name = selectedStockSettingsMaterial_FolderHierarchyVMs[0].Code + " " + selectedStockSettingsMaterial_FolderHierarchyVMs[0].Description,
                    ParentId = selectedStockSettingsMaterial_FolderHierarchyVMs[0].FolderHierarchyId
                };

                await LoadTreeRootsAsync();
            }
            else
            {
                doubleClickedstockSettingsMaterial_Folders = null;
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Info, Summary = "Info", Detail = "Not mapped to any folder" });
            }

        }
        #endregion

        #region TextBox
        async Task OnChange(string value, string name)
        {
            parentfolderHierarchyVMs = null;
            if (value == string.Empty)
            {
                parentfolderHierarchyVMs = null;
                await LoadTreeRootsAsync();
            }
            else
            {

                parentfolderHierarchyVMs = await FolderHierarchyService.GetAllParentFoldersByFolderName(value);
                if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.ToList().Count > 0)
                {
                    hasChildren = await FolderHierarchyService.HasChiledrenAsync(parentfolderHierarchyVMs.ToList()[0].Id);

                    await LoadTreeRootsAsync();
                }
                else
                {
                    NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Info, Summary = "Info", Detail = "Not matched to any folder" });
                }
            }
        }
        #endregion

        #region TreeView


        protected async Task LoadTreeRootsAsync()
        {
            //folderHierarchyVMs = await FolderHierarchyService.GetAllRootsAsync();
            folderHierarchyVMs = await FolderHierarchyService.GetAllNodes();
        }

        protected async void LoadTreeRootsForAddRoot(dynamic? result)
        {
            isLoading = true;
            selectedFldrhrchy = null;
            selectedFldrhrchy = await FolderHierarchyService.GetByIdAsync(result);
            folderHierarchyVMs = await FolderHierarchyService.GetAllNodes();
            await InvokeAsync(() => { StateHasChanged(); });
            isLoading = false;
        }

        async Task OnExpandAsync(TreeExpandEventArgs args)
        {
            var folderHierarchy = args.Value as FolderHierarchyVM;

            EnumHierarchyType enmHirchy = (EnumHierarchyType)folderHierarchy.HierarchyType;
            if (enmHirchy == EnumHierarchyType.Folder)
            {
                var children = await FolderHierarchyService.GetAllChiledrenAsync(folderHierarchy.Id);
                args.Children.Data = children;
                args.Children.TextProperty = "Name";


                hasChildren = await FolderHierarchyService.HasChiledrenAsync(folderHierarchy.Id);
                args.Children.HasChildren = (abcd) => hasChildren;
                args.Children.Expanded = (abcd) => ShouldExpand(folderHierarchy);
            }

            args.Children.Template = FileOrFolderTemplate;
        }

        void OnTreeSelectionChange(TreeEventArgs args)
        {
            if (args != null && args.Value != null)
            {
                var folderhererchy = args.Value as FolderHierarchyVM;
                if (folderhererchy!= null && folderhererchy.HierarchyType == (int)EnumHierarchyType.Folder)
                {
                    selectedFldrhrchy = args.Value as FolderHierarchyVM;
                    doubleClickedstockSettingsMaterial_Folders = null;
                    selectedFldrhrchyMaterial = null;
                }
                else
                {
                    selectedFldrhrchyMaterial = args.Value as FolderHierarchyVM;
                    doubleClickedstockSettingsMaterial_Folders = null;
                    selectedFldrhrchy = null;
                }
            }
            else
            {
                selectedFldrhrchy = null;
            }
        }

        async void OnMenuItemClick(MenuItemEventArgs args)
        {
            await OpenHierarchy(args.Value);
        }

        void ShowContextMenuWithItems(MouseEventArgs args)
        {
            if (selectedFldrhrchy != null && selectedFldrhrchy.HierarchyType == 1)
            {
                ContextMenuService.Open(args,
                    new List<ContextMenuItem> {
                    new ContextMenuItem(){ Text = "Add Root Folder", Value = 1 },
                    new ContextMenuItem(){ Text = "Add Child Folder", Value = 2 },
                    new ContextMenuItem(){ Text = "Edit Selected Folder", Value = 3 },
                     }, OnMenuItemClick);
            }
            else
            {

                ContextMenuService.Open(args,
                    new List<ContextMenuItem> {
                    new ContextMenuItem(){ Text = "Add Root Folder", Value = 1 },
                         }, OnMenuItemClick);
            }
        }

        RenderFragment<RadzenTreeItem> FileOrFolderTemplate = (context) =>  builder =>
        {
            FolderHierarchyVM fldrhrchy = context.Value as FolderHierarchyVM;

            EnumHierarchyType enmHirchy = (EnumHierarchyType)fldrhrchy.HierarchyType;


            if (enmHirchy == EnumHierarchyType.Folder)
            {
                builder.AddContent(1, (MarkupString)fldrhrchy.Icon);
                builder.AddContent(2, (MarkupString)fldrhrchy.Name);
                if (context.Selected)
                {
                    builder.AddContent(3, (MarkupString)" ");

                }
            }
            else
            {
                builder.AddContent(1, (MarkupString)fldrhrchy.Name);
                if (context.Selected)
                {
                    builder.AddContent(2, (MarkupString)" ");

                }

            }
        };

        protected async void ReloadEdit()
        {
            if (selectedFldrhrchy != null && folderHierarchyVMs != null)
            {
                isLoading = true;
                var children = await FolderHierarchyService.GetByIdAsync(selectedFldrhrchy.Id);

                if (folderHierarchyVMs.Any(fldehky => fldehky.Id == selectedFldrhrchy.Id && fldehky.HierarchyType == selectedFldrhrchy.HierarchyType))
                {

                    folderHierarchyVMs.Single(fldehky => fldehky.Id == selectedFldrhrchy.Id && fldehky.HierarchyType == selectedFldrhrchy.HierarchyType).Name = children.Name;
                    folderHierarchyVMs.Single(fldehky => fldehky.Id == selectedFldrhrchy.Id && fldehky.HierarchyType == selectedFldrhrchy.HierarchyType).Icon = children.Icon;
                }
                else
                {
                    foreach (var rootNode in folderHierarchyVMs)
                    {
                        if (FindParentFolder(rootNode, selectedFldrhrchy.Id) != null)
                        {
                            FindParentFolder(rootNode, selectedFldrhrchy.Id).Name = children.Name;
                            FindParentFolder(rootNode, selectedFldrhrchy.Id).Icon = children.Icon;
                            break;
                        }
                    }
                }

                await InvokeAsync(() => { StateHasChanged(); });
                isLoading = false;
            }

        }

        protected async Task LoadChildrenAsync()
        {
            //if (selectedFldrhrchy != null && folderHierarchyVMs != null)
            //{
            //    isLoading = true;
            //    var children = await FolderHierarchyService.GetAllChiledrenAsync(selectedFldrhrchy.Id);
            //    if (children != null)
            //    {
            //        parentfolderHierarchyVMs = null;
            //        parentfolderHierarchyVMs = await GetParentsOfFolders(selectedFldrhrchy);
            //        if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.Any())
            //        {
            //            hasChildren = true;
            //        }
            //    }
            //    if (folderHierarchyVMs.Any(fldehky => fldehky.Id == selectedFldrhrchy.Id && fldehky.HierarchyType == selectedFldrhrchy.HierarchyType))
            //    {

            //        folderHierarchyVMs.Single(fldehky => fldehky.Id == selectedFldrhrchy.Id && fldehky.HierarchyType == selectedFldrhrchy.HierarchyType).Children = children;
            //    }
            //    else
            //    {
            //        foreach (var rootNode in folderHierarchyVMs)
            //        {
            //            if (FindParentFolder(rootNode, selectedFldrhrchy.Id) != null)
            //            {
            //                FindParentFolder(rootNode, selectedFldrhrchy.Id).Children = children;
            //                break;
            //            }
            //        }
            //    }

            //    await InvokeAsync(() => { StateHasChanged(); });
            //    isLoading = false;
            //}
            await GetChildrenBase();
        }

        protected async void LoadChildren()
        {
            await GetChildrenBase();

        }

        private async Task GetChildrenBase()
        {
            if (selectedFldrhrchy != null && folderHierarchyVMs != null)
            {
                isLoading = true;

                parentfolderHierarchyVMs = null;
                if (selectedFldrhrchy.ParentId != null)
                {
                    parentfolderHierarchyVMs = await GetParentsOfFolders(selectedFldrhrchy);
                }
                else
                {
                    parentfolderHierarchyVMs = new List<FolderHierarchyVM>() { selectedFldrhrchy };
                }

                if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.Any())
                {
                    hasChildren = true;
                }

                var children = await FolderHierarchyService.GetAllChiledrenAsync(selectedFldrhrchy.Id);
                if (children != null)
                {
                    EnumHierarchyType enmHirchy = EnumHierarchyType.None;
                    foreach (var rootNode in folderHierarchyVMs)
                    {
                        if (FindParentFolder(rootNode, selectedFldrhrchy.Id) != null)
                        {
                            foreach (var newChild in children)
                            {

                                if (FindParentFolder(rootNode, selectedFldrhrchy.Id) != null &&
                                    FindParentFolder(rootNode, selectedFldrhrchy.Id).Children != null &&
                                    !FindParentFolder(rootNode, selectedFldrhrchy.Id).Children.Any(chld => chld.Id == newChild.Id && chld.HierarchyType == newChild.HierarchyType))
                                {
                                     enmHirchy = (EnumHierarchyType)newChild.HierarchyType;


                                    if (enmHirchy == EnumHierarchyType.Folder)
                                    {
                                        selectedFldrhrchy = newChild;
                                    }
                                    else
                                    {
                                        selectedFldrhrchyMaterial = newChild;

                                    }
                                    //FindParentFolder(rootNode, selectedFldrhrchy.Id).Children.Add(newChild);
                                }
                            }
                        }
                    }

                    if (enmHirchy == EnumHierarchyType.Folder)
                    {
                        selectedFldrhrchyMaterial = null;
                    }
                    else if(enmHirchy == EnumHierarchyType.Material)
                    {
                        selectedFldrhrchy = null;

                    }

                }

                await LoadTreeRootsAsync();

                await InvokeAsync(() => { StateHasChanged(); });
                isLoading = false;
            }
        }

        FolderHierarchyVM? FindParentFolder(FolderHierarchyVM rootNode, int id)
        {
            if (rootNode.Id == id && rootNode.HierarchyType == (int)EnumHierarchyType.Folder) 
            { 
                return rootNode; 
            }
            if (rootNode.Children != null)
            {
                foreach (var child in rootNode.Children)
                {
                    var node = FindParentFolder(child, id);
                    if (node != null && node.HierarchyType == (int)EnumHierarchyType.Folder)
                    {
                        return node;
                    }
                }
            }
            return null;
        }


        private async Task<List<FolderHierarchyVM>> GetParentsOfFolders(FolderHierarchyVM lookFor)
        {
           return await FolderHierarchyService.GetAllParentFoldersByFolderId(lookFor.Id);
        }
        protected Action<object> DialogClose(dynamic? result)
        {
            LoadChildren();
     
            //uriHelper.NavigateTo(uriHelper.Uri, forceLoad: true);
            //materialGrid.Reset();
            //materialGrid.Reload(); 
            //StateHasChanged();
            DialogService.Refresh();
            StateHasChanged();
            isLoading = false;
            Action<object> action = null;
            return action;
        }
        protected Action<object> DialogEditClose(dynamic? result)
        {
            ReloadEdit();

            //uriHelper.NavigateTo(uriHelper.Uri, forceLoad: true);
            //materialGrid.Reset();
            //materialGrid.Reload();
            //StateHasChanged();
            DialogService.Refresh();
            StateHasChanged();
            isLoading = false;
            Action<object> action = null;
            return action;
        }
        protected Action<object> DialogRootClose(dynamic? result)
        {
            LoadTreeRootsForAddRoot(result);


            //materialGrid.Reset();
            //materialGrid.Reload();
            //
            DialogService.Refresh();
            StateHasChanged();
            isLoading = false;
            Action<object> action = null;
            return action;
        }
        public async Task OpenHierarchy(object menuValue)
        {
            if (selectedFldrhrchy != null)
            {
                //parentfolderHierarchyVMs = new List<FolderHierarchyVM>() { selectedFldrhrchy };
                if (menuValue.Equals(2))
                {
                    var result=await DialogService.OpenAsync<AddEditFolder>($"Add Child Folder to {selectedFldrhrchy.Name}",
                       new Dictionary<string, object>() { { "ParentId", selectedFldrhrchy.Id }},
                       new DialogOptions() { Width = "700px",  Resizable = false, Draggable = true,CloseDialogOnEsc=true});
                    DialogService.OnClose += DialogClose(result);
                }
                else if (menuValue.Equals(3))
                {
                    var result=await DialogService.OpenAsync<AddEditFolder>($"Edit Folder {selectedFldrhrchy.Name}",
                       new Dictionary<string, object>() { { "Id", selectedFldrhrchy.Id } },
                       new DialogOptions() { Width = "700px",  Resizable = false, Draggable = true });
                    DialogService.OnClose += DialogEditClose(result);
                }
                else
                {
                    var result=await DialogService.OpenAsync<AddEditFolder>($"Add Root Folder",
                        null,
                        new DialogOptions() { Width = "700px", Resizable = false, Draggable = true });
                    DialogService.OnClose +=  DialogClose(result);
                }
            }
            else
            {
                var result=await DialogService.OpenAsync<AddEditFolder>($"Add Root Folder",
                    null,
                    new DialogOptions() { Width = "700px",Resizable = false, Draggable = true });
                DialogService.OnClose += DialogRootClose(result);
            }
        }

        bool ShouldExpand( object data)
        {
            FolderHierarchyVM? fldrhrchy = data as FolderHierarchyVM;


            if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.ToList().Count > 0 && fldrhrchy != null && (EnumHierarchyType)fldrhrchy.HierarchyType == EnumHierarchyType.Folder &&
                hasChildren && parentfolderHierarchyVMs.Any(ph => ph.Id == fldrhrchy.Id && ph.HierarchyType == (int)EnumHierarchyType.Folder))
            {
                return true;
            }
            else { return false; }
        }
        bool ShouldSelect(object data)
        {
            FolderHierarchyVM? fldrhrchy = data as FolderHierarchyVM;
            if (selectedFldrhrchyMaterial != null && selectedFldrhrchyMaterial.ParentId.HasValue 
                && fldrhrchy != null && (EnumHierarchyType)fldrhrchy.HierarchyType == EnumHierarchyType.Material
                && selectedFldrhrchyMaterial.Id == fldrhrchy.Id && selectedFldrhrchyMaterial.HierarchyType == fldrhrchy.HierarchyType)
            {
                return true;
            }
            else if (selectedFldrhrchy != null && folderHierarchyVMs != null && fldrhrchy != null &&
                (EnumHierarchyType)fldrhrchy.HierarchyType == EnumHierarchyType.Folder && 
                hasChildren && selectedFldrhrchy.Id == fldrhrchy.Id && selectedFldrhrchy.HierarchyType == fldrhrchy.HierarchyType)
            {
                return true;
            }
            else if (selectedFldrhrchy != null && folderHierarchyVMs != null && fldrhrchy != null &&
                (EnumHierarchyType)fldrhrchy.HierarchyType == EnumHierarchyType.Folder &&
                selectedFldrhrchy.ParentId is null && selectedFldrhrchy.Id == fldrhrchy.Id 
                && selectedFldrhrchy.HierarchyType == fldrhrchy.HierarchyType)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
        bool HasChildren(object data)
        {
            FolderHierarchyVM? folderHierarchy = data as FolderHierarchyVM;
            if (folderHierarchy != null) {
                EnumHierarchyType enmHirchy = (EnumHierarchyType)folderHierarchy.HierarchyType;

                if (enmHirchy == EnumHierarchyType.Folder)
                {
                    //bool hasChildren = FolderHierarchyService.HasChiledrenAsync(folderHierarchy.Id);
                    //return true;
                    if(folderHierarchy.Children == null)
                    {
                        return false;
                    }
                    return folderHierarchy.Children.Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
