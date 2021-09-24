using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBotAsta
{
    class Bot
    {
        private ConnectionCredentials creds = new ConnectionCredentials("astas_assistant", "oauth:a9znkcoafd7ql6eehud40jsvvqm1p2");
        private TwitchClient client;
        private string channel = "Asta_Francesca";
        private string response;

        public Bot()
        {
            client = new TwitchClient();
            client.Initialize(creds, channel);

            client.OnLog += Client_OnLog;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;

            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            switch (e.Command.CommandText.ToLower())
            {
                // EVERYONE
                case "test":
                    response = User.GetUser(e) + " hey this is a text";
                    SendChatMessage(response);
                    break;   
            }
        }

        private void SendChatMessage(string response)
        {
            client.SendMessage(channel, response);
            Console.WriteLine($"[Bot]: {response}");
        }
    }
}
