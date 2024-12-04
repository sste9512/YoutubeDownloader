using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.IO;
using Microsoft.Graph.Models;

namespace youtubedownloader_vue.Server.Controllers;

public sealed class OneDriveController : ApiControllerBase
{
    private readonly GraphServiceClient _graphClient;
    private readonly ILogger<OneDriveController> _logger;

    public OneDriveController(GraphServiceClient graphClient, ILogger<OneDriveController> logger)
    {
        _graphClient = graphClient;
        _logger = logger;
    }


    [HttpPost("[action]")]
    public async Task<ActionResult<string>> UploadFile(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            await using var stream = file.OpenReadStream();
            var driveItem = await _graphClient.Me.Drive.GetAsync();

            driveItem.Root.Children.Add(new DriveItem
            {
                Name = file.FileName,

                File = new FileObject
                {
                    AdditionalData = new Dictionary<string, object>(),
                    Hashes = new Hashes(),
                    MimeType = "application/octet-stream",
                    OdataType = "microsoft.graph.driveItem",
                    ProcessingMetadata = false,
                }
            });


            // .ItemWithPath(file.FileName)
            // .Content
            // .Request()
            // .PutAsync<DriveItem>(stream);

            return Ok(driveItem.Id);
        }
        catch (ServiceException ex)
        {
            return StatusCode(500, $"Error uploading file: {ex.Message}");
        }
    }

    /*[HttpGet("[action]/{fileId}")]
    public async Task<ActionResult> DownloadFile(string fileId)
    {
        try
        {
            var driveItem = await _graphClient.Me.Drive.Items[fileId]
                .Request()
                .GetAsync();

            var stream = await _graphClient.Me.Drive.Items[fileId]
                .Content
                .Request()
                .GetAsync();

            return File(stream, "application/octet-stream", driveItem.Name);
        }
        catch (ServiceException ex)
        {
            return StatusCode(500, $"Error downloading file: {ex.Message}");
        }
    }*/

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<DriveItem>>> ListFiles()
    {
        try
        {
            var items = await _graphClient.Me.Drive.GetAsync();


            return Ok(items?.Root);
        }
        catch (ServiceException ex)
        {
            return StatusCode(500, $"Error listing files: {ex.Message}");
        }
    }
}