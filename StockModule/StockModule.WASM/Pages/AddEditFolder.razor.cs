
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Radzen;
using StockModule.UI.Data;
using StockModule.UI.Model;
using System;
using System.Net;
using static System.Net.WebRequestMethods;

namespace StockModule.WASM.Pages
{
    public partial class AddEditFolder
    {
        [Parameter] public int? Id { get; set; }
        [Parameter] public int? ParentId { get; set; }
        //[Parameter] public EventCallback<Task> OnSaveClickLoadParentTreeRoots { get; set; }
        FolderHierarchyVM fldrhrchy = new FolderHierarchyVM();
        string uploadedSVG = string.Empty;

        private string title = string.Empty;
        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            // Get the selected file
            var file = e.File;

            // Check if the file is null then return from the method
            if (file == null)
                return;

            // Validate the extension if requried (Client-Side)

            // Set the value of the stream by calling OpenReadStream and pass the maximum number of bytes allowed because by default it only allows 512KB
            // I used the value 5000000 which is about 50MB
            //var trustedFileNameForFileStorage = Path.GetRandomFileName();
            //var path = Path.Combine(Environment.ContentRootPath,
            //        Environment.EnvironmentName, "unsafe_uploads",
            //        trustedFileNameForFileStorage);

            //await using FileStream fs = new(path, FileMode.Create);
            //await file.OpenReadStream(5000000).CopyToAsync(fs);
            uploadedSVG = await new StreamReader(file.OpenReadStream()).ReadToEndAsync();
        }
        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                title = "Edit Folder";
                fldrhrchy = await FolderHierarchyService.GetByIdAsync(Id.Value);

            }
            else if (ParentId.HasValue)
            {
                title = "Add Child Folder";
            }
            else
            {
                title = "Add Root Folder";
            }
        }

        protected async Task OnValidSubmit()
        {
            try
            {
                if (Id == null)
                {
                    if (uploadedSVG != string.Empty)
                    {
                        if (ParentId.HasValue)
                        {
                            fldrhrchy.ParentId = ParentId.Value;
                        }
                        fldrhrchy.Icon = WebUtility.UrlEncode(uploadedSVG);
                        var response = await FolderHierarchyService.CreateAsync(fldrhrchy);
                        var body = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Folder has been saved successfully." });
                        }
                        else
                        {
                            logger.LogError(response.ReasonPhrase + " " + response.StatusCode);
                            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Failed to save folder." });
                        }
                    }
                    else
                    {
                        NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Select an Icon for folder." });
                    }
                }
                else
                {
                    if (uploadedSVG != string.Empty)
                    {
                        fldrhrchy.Icon = WebUtility.UrlEncode(uploadedSVG);
                    }
                    var response = await FolderHierarchyService.EditAsync(fldrhrchy);
                    var body = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Folder has been updated successfully." });
                    }
                    else
                    {
                        logger.LogError(response.ReasonPhrase + " " + response.StatusCode);
                        NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Failed to update folder." });
                    }
                }
            }
            catch (Exception Ex)
            {
                logger.LogError(Ex.Message);
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Failed to update folder." });
            }
        }

        void OnProgress(UploadProgressArgs args, string name)
        {
            //console.Log($"{args.Progress}% '{name}' / {args.Loaded} of {args.Total} bytes.");

            if (args.Progress == 100)
            {
                foreach (var file in args.Files)
                {
                    //console.Log($"Uploaded: {file.Name} / {file.Size} bytes");
                }
            }
        }

        private void clear()
        {
            fldrhrchy = new FolderHierarchyVM();
        }
    }
}
