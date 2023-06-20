using Microsoft.AspNetCore.Components.Web;
using Microsoft.Graph;
using MudBlazor;
using Radzen;
using StockModule.UI.Model;  

namespace StockModule.UI.Pages
{
    public partial class MaterialSearchBlazorise
    {
        List<StockSettingsMaterial_FolderHierarchyVM>? stockSettingsMaterial_FolderHierarchyVMs = new List<StockSettingsMaterial_FolderHierarchyVM>();

        StockSettingsMaterial_FolderHierarchyVM? selectedstockSettingsMaterial_Folders;

        #region Page
        protected override async Task OnInitializedAsync()
        {
            stockSettingsMaterial_FolderHierarchyVMs = await StockSettingsMaterial_FolderHierarchyService.GetAllAsync();
            await base.OnInitializedAsync();
        }
        #endregion
    }
}
