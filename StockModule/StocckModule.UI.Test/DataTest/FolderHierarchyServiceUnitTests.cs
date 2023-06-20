using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using StockModule.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocckModule.UI.Test.DataTest
{
    public class FolderHierarchyServiceUnitTests
    {
        FolderHierarchyService? _folderHierarchyService = new(new HttpClient { BaseAddress = new Uri("http://localhost:5096/api/") });

        [Fact]
        public async Task GetByIdAsyncTest()
        {
            //await Assert.ThrowsAsync<Exception>(async () => await _folderHierarchyService.GetByIdAsync(1));
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetByIdAsync(1));

            Assert.Null(exception);           
        }

        [Fact]
        public async Task GetAllRootsAsyncTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllRootsAsync());

            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllChiledrenAsyncTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllChiledrenAsync(1));

            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllNodesByNamesTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllNodes("Folder"));

            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllNodesTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllNodes());

            Assert.Null(exception);
        }

        [Fact]
        public async Task HasChiledrenAsyncTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.HasChiledrenAsync(1));

            Assert.Null(exception);
        }

        //[Fact]
        //public async Task CreateAsyncTest()
        //{
        //    var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.CreateAsync(1));

        //    Assert.Null(exception);
        //}

        //[Fact]
        //public async Task EditAsyncTest()
        //{
        //}

        [Fact]
        public async Task GetAllParentFoldersByMaterialIdTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllParentFoldersByMaterialId(1));

            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllParentFoldersByFolderNameTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllParentFoldersByFolderName("Folder"));

            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAllParentFoldersByFolderIdTest()
        {
            var exception = await Record.ExceptionAsync(async () => await _folderHierarchyService!.GetAllParentFoldersByFolderId(1));

            Assert.Null(exception);
        }
    }
}
