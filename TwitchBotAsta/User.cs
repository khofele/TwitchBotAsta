using System;
using TwitchLib.Client.Events;

namespace TwitchBotAsta
{
    class User
    {
        public static string GetUser(OnChatCommandReceivedArgs e)
        {
            return e.Command.ChatMessage.DisplayName.ToString();
        }
    }
}
