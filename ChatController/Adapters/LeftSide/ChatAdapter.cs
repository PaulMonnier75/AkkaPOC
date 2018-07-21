using Core;
using Core.IAdapters.LeftSide;
using Core.Models;

namespace ChatController.Adapters.LeftSide
{
    public class ChatAdapter : IChatAdapter
    {
        private ICore Core;        
        
        public ChatAdapter(ICore core)
            => Core = core;

        public void RetriveMessage()
            => Core.HandleCommand(new RetrieveMessage());
    }
}