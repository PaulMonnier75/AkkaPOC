using System;
using System.Diagnostics;
using Akka.Actor;
using Akka.DI.Core;
using Core.Models;
using Core.Services;

namespace Core.Actors
{
    public class ChatActor : ReceiveActor
    {    
        private readonly IChatService ChatService;
        private readonly ILuisService LuisService;

        public ChatActor(IChatService chatService, ILuisService luisService)
        {
            ChatService = chatService;
            LuisService = luisService;

            Receive<RetrieveMessageCommand>(command => HandleRetrieveMessagesCommand(command));
            Receive<SendMessageCommand>(command => HandleSendMessageCommand(command));
            ReceiveAny(command => throw new Exception("ChatActor: Undefined Command"));
        }

        private void HandleRetrieveMessagesCommand(RetrieveMessageCommand command)
            => ChatService.GetChatMessage();

        private void HandleSendMessageCommand(SendMessageCommand command)
            => DispatchUserCommand(command.Message);

        private async void DispatchUserCommand(ChatMessage chatMessage)
        {
            var topScoringIntent = await LuisService.GetIntentFromMessage(chatMessage.Text);
            
            
            Debug.WriteLine("");
        }       
    }
}