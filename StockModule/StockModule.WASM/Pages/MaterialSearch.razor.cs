
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;
using Radzen;
using Radzen.Blazor;
using StockModule.UI.Data;
using StockModule.UI.Model;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;

namespace StockModule.WASM.Pages
{
    public partial class MaterialSearch
    {
        bool isLoading = false;
        int count;
        FolderHierarchyVM? selectedFldrhrchy;
        IEnumerable<StockSettingsMaterial_FolderHierarchyVM>? stockSettingsMaterial_FolderHierarchyVMs;
        IEnumerable<FolderHierarchyVM>? folderHierarchyVMs;
        IEnumerable<FolderHierarchyVM>? parentfolderHierarchyVMs;
        IList<StockSettingsMaterial_FolderHierarchyVM>? selectedstockSettingsMaterial_Folders;

        RadzenTree materialSearchTree;

        IList<StockSettingsMaterial_FolderHierarchyVM> selectedStockSettingsMaterial_FolderHierarchyVMs;
        bool hasChildren;

        #region Page
        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await base.OnInitializedAsync();
            await LoadTreeRoots();
            stockSettingsMaterial_FolderHierarchyVMs = await StockSettingsMaterial_FolderHierarchyService.GetAllAsync();
            count = stockSettingsMaterial_FolderHierarchyVMs.Count();
            isLoading = false;
        }
        #endregion

        #region Grid

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

            InvokeAsync(() => { StateHasChanged(); });
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
                    parentfolderHierarchyVMs = null;
                    //await LoadTreeRoots();
                    NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Selected Material/s has been added to the selected Folder has been saved successfully." });

                }
                else
                {
                    NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Failed to add the selected Material/s to the selected folder." });
                }
            }
        }
        async Task OnRowDoubleClickAsync(DataGridRowMouseEventArgs<StockSettingsMaterial_FolderHierarchyVM> args)
        {
            selectedStockSettingsMaterial_FolderHierarchyVMs = new List<StockSettingsMaterial_FolderHierarchyVM>() { args.Data };

            parentfolderHierarchyVMs = await FolderHierarchyService.GetAllParentFoldersByMaterialId(selectedStockSettingsMaterial_FolderHierarchyVMs[0].Id);
            if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.ToList().Count > 0)
            {
                hasChildren = await FolderHierarchyService.HasChiledrenAsync(parentfolderHierarchyVMs.ToList()[0].Id);

                await LoadTreeRoots();
            }
            else
            {
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
                await LoadTreeRoots();
            }
            else
            {
                folderHierarchyVMs = await FolderHierarchyService.GetAllNodes(value);
            }
        }
        #endregion

        #region TreeView


        protected async Task LoadTreeRoots()
        {
            //folderHierarchyVMs = await FolderHierarchyService.GetAllRootsAsync();
            folderHierarchyVMs = await FolderHierarchyService.GetAllNodes();
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
                selectedFldrhrchy = args.Value as FolderHierarchyVM;
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

        RenderFragment<RadzenTreeItem> FileOrFolderTemplate = (context) => builder =>
        {
            FolderHierarchyVM fldrhrchy = context.Value as FolderHierarchyVM;

            EnumHierarchyType enmHirchy = (EnumHierarchyType)fldrhrchy.HierarchyType;

            if (enmHirchy == EnumHierarchyType.Folder)
            {
                builder.AddContent(1, (MarkupString)fldrhrchy.Icon);
                builder.AddContent(2, (MarkupString)fldrhrchy.Name);
            }
            else
            {
                builder.AddContent(1, (MarkupString)fldrhrchy.Name);
            }
        };

        protected async void LoadChildren()
        {
            if (selectedFldrhrchy != null)
            {
                var children = await FolderHierarchyService.GetAllChiledrenAsync(selectedFldrhrchy.Id);


                folderHierarchyVMs.Single(fldehky => fldehky.Id == selectedFldrhrchy.Id && fldehky.HierarchyType == selectedFldrhrchy.HierarchyType).Children = children;
            }

        }

        protected Action<object> DialogClose(dynamic? result)
        {
            LoadChildren();
            InvokeAsync(() => { StateHasChanged(); });
            Action<object> action = null;
            return action;
        }

        public async Task OpenHierarchy(object menuValue)
        {
            if (selectedFldrhrchy != null)
            {
                if (menuValue.Equals(2))
                {
                    var result = await DialogService.OpenAsync<AddEditFolder>($"Add Child Folder to {selectedFldrhrchy.Name}",
                       new Dictionary<string, object>() { { "ParentId", selectedFldrhrchy.Id } },
                       new DialogOptions() { Width = "700px", Resizable = false, Draggable = true });
                    DialogService.OnClose += DialogClose(result);
                }
                else if (menuValue.Equals(3))
                {
                    var result = await DialogService.OpenAsync<AddEditFolder>($"Edit Folder {selectedFldrhrchy.Name}",
                       new Dictionary<string, object>() { { "Id", selectedFldrhrchy.Id } },
                       new DialogOptions() { Width = "700px", Resizable = false, Draggable = true });
                    DialogService.OnClose += DialogClose(result);
                }
                else
                {
                    var result = await DialogService.OpenAsync<AddEditFolder>($"Add Root Folder",
                        null,
                        new DialogOptions() { Width = "700px", Resizable = false, Draggable = true });
                    DialogService.OnClose += DialogClose(result);
                }
            }
            else
            {
                var result = await DialogService.OpenAsync<AddEditFolder>($"Add Root Folder",
                    null,
                    new DialogOptions() { Width = "700px", Resizable = false, Draggable = true });
                DialogService.OnClose += DialogClose(result);
            }
        }

        bool ShouldExpand(object data)
        {
            FolderHierarchyVM? fldrhrchy = data as FolderHierarchyVM;
            if (parentfolderHierarchyVMs != null && parentfolderHierarchyVMs.ToList().Count > 0 && fldrhrchy != null && (EnumHierarchyType)fldrhrchy.HierarchyType == EnumHierarchyType.Folder && hasChildren)
            {
                return parentfolderHierarchyVMs.Any(ph => ph.Id == fldrhrchy.Id && ph.HierarchyType == (int)EnumHierarchyType.Folder);
            }
            else { return false; }
        }

        bool HasChildren(object data)
        {
            FolderHierarchyVM? folderHierarchy = data as FolderHierarchyVM;
            if (folderHierarchy != null)
            {
                EnumHierarchyType enmHirchy = (EnumHierarchyType)folderHierarchy.HierarchyType;

                if (enmHirchy == EnumHierarchyType.Folder)
                {
                    //bool hasChildren = FolderHierarchyService.HasChiledrenAsync(folderHierarchy.Id);
                    //return true;
                    if (folderHierarchy.Children == null)
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
