using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBotAsta
{
    enum Pomodoro
    {
        ADD, EDIT, FINISHED, REMOVE, HELP
    }

    class Bot
    {
        private ConnectionCredentials creds = new ConnectionCredentials("astas_assistant", "oauth:a9znkcoafd7ql6eehud40jsvvqm1p2");
        private TwitchClient client;
        private string channel = "Asta_Francesca";
        private string response;
        private TaskCommandManager taskCommandManager = new TaskCommandManager();

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
                // POMO
                case "addtodo":
                    DisplayPomodoroCommand(Pomodoro.ADD, e);
                    break;

                case "finishedtodo":
                    DisplayPomodoroCommand(Pomodoro.FINISHED, e);
                    break;

                case "help":
                    DisplayPomodoroCommand(Pomodoro.HELP, e);
                    break;

                case "edittodo":
                    DisplayPomodoroCommand(Pomodoro.EDIT, e);
                    break;

                case "removetodo":
                    DisplayPomodoroCommand(Pomodoro.REMOVE, e);
                    break;
            }
        }

        private void DisplayPomodoroCommand(Pomodoro pomo, OnChatCommandReceivedArgs e)
        {
            if (CheckModerator(e) == true || CheckBroadcaster(e) == true)
            {
                GetResponsePomodoro(pomo, e);
                if(response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }
            else if (Cooldown.CheckCooldownOffPomodoro(pomo) == true)
            {
                GetResponsePomodoro(pomo, e);
                Cooldown.globalCooldownsPomos[pomo] = DateTime.Now;
                Cooldown.globalCooldownsRunningPomos[pomo] = true;
                if(response != null)
                {
                    SendChatMessage(response);
                    return;
                }
            }

        }

        private void SendChatMessage(string response)
        {
            client.SendMessage(channel, response);
            Console.WriteLine($"[Bot]: {response}");
        }

        private bool CheckModerator(OnChatCommandReceivedArgs e)
        {
            if (e.Command.ChatMessage.IsModerator == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBroadcaster(OnChatCommandReceivedArgs e)
        {
            if (e.Command.ChatMessage.IsBroadcaster == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetResponsePomodoro(Pomodoro pomo, OnChatCommandReceivedArgs e)
        {
            switch (pomo)
            {
                case Pomodoro.ADD:
                    response = taskCommandManager.AddTaskCommand(e);
                    break;

                case Pomodoro.EDIT:
                    response = taskCommandManager.EditTaskCommand(e);
                    break;

                case Pomodoro.FINISHED:
                    response = taskCommandManager.TaskDoneCommand(e);
                    break;

                case Pomodoro.HELP:
                    response = taskCommandManager.HelpCommand();
                    break;

                case Pomodoro.REMOVE:
                    response = taskCommandManager.RemoveTaskCommand(e);
                    break;
            }
            return response;
        }
    }
}
