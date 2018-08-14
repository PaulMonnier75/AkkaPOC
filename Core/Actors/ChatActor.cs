using System;
using Akka.Actor;
using Akka.DI.Core;
using Core.Models;
using Core.Services;

namespace Core.Actors
{
    public class ChatActor : ReceiveActor
    {
        private readonly IChatService ChatService;
        
        public ChatActor(IChatService chatService)
        {
            ChatService = chatService;
            
            Receive<RetrieveMessageCommand>(command => HandleRetrieveMessageCommand(command));
            ReceiveAny(command => throw new Exception("ChatActor: Undefined Command"));
        }

        private void HandleRetrieveMessageCommand(RetrieveMessageCommand command)
        {
            var result = ChatService.GetChatMessage();
            
            Console.WriteLine("Command : " + command);
        }    
    }
}