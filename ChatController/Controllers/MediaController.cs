using Core.IAdapters.LeftSide;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{   
    [Route("api/[controller]")]
    public class MediaController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediaAdapter MediaAdapter;

        public MediaController(IMediaAdapter mediaAdapter)
        {
            MediaAdapter = mediaAdapter;
        }
        
        [HttpPost("PauseMedia")]
        public IActionResult PauseMedia()
        {
            MediaAdapter.PauseMedia();

            return Ok();
        }

        [HttpPost("PlayMedia")]
        public IActionResult PlayMedia()
        {
            MediaAdapter.PlayMedia();
            
            return Ok();
        }
        
        [HttpPost("CastMedia")]
        public IActionResult CastMedia([FromBody] CastRequestModel request)
        {
            MediaAdapter.CastMedia(request.UrlToCast);
            
            return Ok();
        }
    }
}