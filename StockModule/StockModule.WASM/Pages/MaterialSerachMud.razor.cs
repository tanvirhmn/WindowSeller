using MudBlazor;
using StockModule.UI.Model;

namespace StockModule.WASM.Pages
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
            return item.Grouping.Key?.ToString().Length > 0
                    ? "mud-theme-warning"
                    : string.Empty;
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
    }
}
