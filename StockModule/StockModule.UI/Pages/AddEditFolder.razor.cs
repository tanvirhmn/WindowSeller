using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Radzen;
using StockModule.UI.Model;
using System;
using System.Net;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace StockModule.UI.Pages
{
    public partial class AddEditFolder
    {
        [Parameter] public int? Id { get; set; }
        [Parameter] public int? ParentId { get; set; }

        [Inject]
        protected DialogService ds { get; set; }

        private void OnRefresh()
        {
            StateHasChanged();
        }

        private void OnClose()
        {
            StateHasChanged();
        }

        //[Parameter] public EventCallback<Task> OnSaveClickLoadParentTreeRoots { get; set; }
        FolderHierarchyVM fldrhrchy = new FolderHierarchyVM();
        string uploadedSVG = string.Empty;

        //private string title = string.Empty;
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
                //title = "Edit Folder";
                fldrhrchy = await FolderHierarchyService.GetByIdAsync(Id.Value);

            }
            ds.OnRefresh += OnRefresh;
            //else if (ParentId.HasValue)
            //{
            //    title = "Add Child Folder";
            //}
            //else
            //{
            //    title = "Add Root Folder";
            //}
        }

        protected async Task OnValidSubmit()
        {
            try
            {
                if (Id == null)
                {
                    if (uploadedSVG == string.Empty)
                    {
                        uploadedSVG = "<svg xmlns =\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-folder\"><path d=\"M22 19a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h5l2 3h9a2 2 0 0 1 2 2z\"></path></svg>";
                    }
                    if (uploadedSVG != string.Empty)
                    {
                        if(!uploadedSVG.Substring(0, uploadedSVG.IndexOf('>') + 1).Contains("width"))
                        {
                            string[] splitImges = uploadedSVG.Split(new char[] { ' ' }, 2);
                            uploadedSVG = splitImges[0]+ " style=\"width:24; height: 24;\"" + splitImges[1];
                        }
                        if (ParentId.HasValue)
                        {
                            fldrhrchy.ParentId = ParentId.Value;
                        }
                        fldrhrchy.Icon = uploadedSVG;//WebUtility.UrlEncode(uploadedSVG);
                        var response = await FolderHierarchyService.CreateAsync(fldrhrchy);
                        var body =  response.Content.ReadAsStringAsync();
                        Id = Convert.ToInt32(body.Result.Split(',')[0].Split(':')[1]);

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
                    //if (uploadedSVG == string.Empty)
                    //{
                    //    uploadedSVG = "<svg xmlns =\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-folder\"><path d=\"M22 19a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h5l2 3h9a2 2 0 0 1 2 2z\"></path></svg>";
                    //}

                    if (uploadedSVG != string.Empty)
                    {
                        if (!uploadedSVG.Substring(0, uploadedSVG.IndexOf('>') + 1).Contains("width"))
                        {
                            string[] splitImges = uploadedSVG.Split(new char[] { ' ' }, 2);
                            uploadedSVG = splitImges[0] + " style=\"width:24; height: 24;\"" + splitImges[1];
                        }
                        fldrhrchy.Icon = uploadedSVG;//WebUtility.UrlEncode(uploadedSVG);
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
                OnRefresh();
                DialogService.Close(Id);
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
            //fldrhrchy = new FolderHierarchyVM();
            DialogService.Close();
        }
    }
}
