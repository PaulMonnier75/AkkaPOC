namespace Core.IAdapters.LeftSide
{
    public interface IMediaAdapter
    {
        void PlayMedia();
        void PauseMedia();
        void CastMedia(string urlToCast);
    }
}