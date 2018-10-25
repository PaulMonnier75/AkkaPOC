using Core;
using Core.IAdapters.LeftSide;
using Core.Models;

namespace ChatController.Adapters.LeftSide
{
    public class MediaAdapter : IMediaAdapter
    {
        private readonly ICore Core;        
        
        public MediaAdapter(ICore core)
            => Core = core;


        public void PlayMedia()
            => Core.HandleCommand(new PlayMovieCommand());
        

        public void PauseMedia()
            => Core.HandleCommand(new PauseMovieCommand());


        public void CastMedia(string urlToCast)
            => Core.HandleCommand(new CastMovieCommand(urlToCast));

    }
}