using Microsoft.AspNetCore.Mvc;
/*using SpotifyAPI.Web;*/

namespace youtubedownloader_vue.Server.Controllers;

/*public sealed class SpotifyController(SpotifyClient spotifyClient) : ApiControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult<string>> UploadTrack(IFormFile audioFile)
    {
        try
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("No audio file uploaded");

            // Note: The Spotify Web API does not directly support uploading audio files
            // You would need to:
            // 1. Have a Spotify for Artists/Label account
            // 2. Use the Spotify Content Delivery API (which requires special access)
            // 3. Follow their content delivery guidelines

            return StatusCode(501, "Direct upload to Spotify is not supported through their public API. " +
                                 "Please use Spotify for Artists or work with a distributor.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error processing request: {ex.Message}");
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<string>> GetUserProfile()
    {
        try
        {
            var profile = await spotifyClient.UserProfile.Current();
            return Ok(profile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving profile: {ex.Message}");
        }
    }
}*/
