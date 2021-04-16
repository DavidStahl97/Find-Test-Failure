using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UserFiles
{
    public partial class UserFiles : ComponentBase
    {
        private TablePagination<GetUserFileDto> _pagination;

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private async Task<SearchResult<GetUserFileDto>> Search(SearchQuery query)
        {
            var page = await Api.SendAsync(x => x.GetUserFilePageAsync(query.PageIndex, 
                query.PageSize, query.SearchString));
            return new SearchResult<GetUserFileDto>
            {
                Items = page.Data,
                Total = page.Count
            };
        }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            var userFiles = e.GetMultipleFiles()
                .Select(x => new FileParameter(x.OpenReadStream(maxAllowedSize: 512 * 1000 * 1000), x.Name, x.ContentType))
                .ToList();

            await Api.SendAsync(x => x.PostUserFileAsync(userFiles));

            await _pagination.SearchAsync();
        }

        private async Task DeleteUserFile(GetUserFileDto userFile)
        {
            Task deleteFunc(swaggerClient x) => x.DeleteUserFileAsync(userFile.Id);

            await DialogService.ShowDeleteDialog("File", userFile.FileName, deleteFunc).Result;
            await _pagination.SearchAsync();
        }

        private static string CreateSizeString(GetUserFileDto file)
        {
            const int kbSize = 1000;
            const int mbSize = kbSize * 1000;

            return file.FileSize switch
            {
                >= mbSize => $"{file.FileSize / mbSize} MB",
                _ => $"{file.FileSize / kbSize} KB"
            };
        }
    }
}
