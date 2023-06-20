using Microsoft.AspNetCore.Components.Web;
using Microsoft.Graph;
using MudBlazor;
using Radzen;
using StockModule.UI.Model;
using static MudBlazor.CategoryTypes;

namespace StockModule.UI.Pages
{
    public partial class MaterialSerachMud
    {
        List<StockSettingsMaterial_FolderHierarchyVM>? stockSettingsMaterial_FolderHierarchyVMs = new List<StockSettingsMaterial_FolderHierarchyVM>();
        MudDataGrid<StockSettingsMaterial_FolderHierarchyVM> dataGrid;

        #region Page
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            stockSettingsMaterial_FolderHierarchyVMs = await StockSettingsMaterial_FolderHierarchyService.GetAllAsync();
        }
        #endregion

        Func<StockSettingsMaterial_FolderHierarchyVM, object> _groupByType = x =>
        {
            return x.Type;
        };
        private string GroupClassFunc(GroupDefinition<StockSettingsMaterial_FolderHierarchyVM> item)
        {
            return item.Grouping.Key?.ToString().Length>0
                    ? "mud-theme-warning"
                    : string.Empty;
        }
        void ShowContextMenuWithContent(MouseEventArgs args)
        {
            

                ContextMenuService.Open(args,
                    new List<ContextMenuItem> {
                    new ContextMenuItem(){ Text = "Add Root Folder", Value = 1 },
                         }, OnMenuItemClick);
        }
        async void OnMenuItemClick(MenuItemEventArgs args)
        {
        }

        void ExpandAllGroups()
        {
            dataGrid?.ExpandAllGroups();
        }

        void CollapseAllGroups()
        {
            dataGrid?.CollapseAllGroups();
        }

        void CustomizeByGroupChanged(bool isChecked)
        {
            dataGrid.GroupItems();
        }

        // events
        void RowClicked(DataGridRowClickEventArgs<StockSettingsMaterial_FolderHierarchyVM> args)
        {
            Console.WriteLine(dataGrid ?.FilterDefinitions);
            Console.WriteLine(args.Item);
        }

        void SelectedItemsChanged(HashSet<StockSettingsMaterial_FolderHierarchyVM> items)
        {
        }
    }
}
