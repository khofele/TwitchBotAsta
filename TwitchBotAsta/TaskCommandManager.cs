using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace TwitchBotAsta
{
    class TaskCommandManager
    {
        public List<Task> tasks = new List<Task>();
        private int counter = 0;
        private bool notAdded = true;
        private Task task;

        private FileManager fileManager = new FileManager();

        public bool NotAdded
        {
            get => notAdded;
            set
            {
                notAdded = value;
            }
        }

        public string AddTaskCommand(OnChatCommandReceivedArgs e)
        {
            string chatMessage = e.Command.ChatMessage.Message.ToString();
            string taskMessage = chatMessage.Replace("!addtodo", " -");
            string response = CheckAndAddTask(taskMessage, User.GetUser(e), e);

            return response;
        }

        public string TaskDoneCommand(OnChatCommandReceivedArgs e)
        {
            if (fileManager.FindTask(User.GetUser(e)) != null)
            {
                string finishedTask = fileManager.FindTask(User.GetUser(e)).ToUpper();
                RemoveTask(User.GetUser(e));
                fileManager.DeleteTaskInFile(User.GetUser(e));
                return "nice! done already " + User.GetUser(e) + "!? so whats next on the list? Kappa";
            }
            else
            {
                return User.GetUser(e) + " you have to add a task to finish a task!";
            }
        }

        public string HelpCommand()
        {
            return "break it down, identify your first step and let's start with that!";
        }

        private void AddTask(Task task)
        {
            tasks.Add(task);
        }

        private void RemoveTask(string user)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].User == user)
                {
                    tasks.Remove(tasks[i]);
                }
            }
        }

        private void CheckTaskUser(string user)
        {
            ResetCounter();
            ResetNotAdded();
            if (tasks.Count != 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].User == user)
                    {
                        counter++;
                    }
                }

                if (counter >= 1)
                {
                    notAdded = false;
                    return;
                }
                else if (counter <= 0)
                {
                    notAdded = true;
                    return;
                }
            }
        }

        private void ResetCounter()
        {
            counter = 0;
        }

        private void ResetNotAdded()
        {
            notAdded = true;
        }

        private string CheckAndAddTask(string taskMessage, string user, OnChatCommandReceivedArgs e)
        {
            task = new Task(user, taskMessage);
            CheckTaskUser(task.User);

            if (NotAdded == true)
            {
                fileManager.WriteToFile(User.GetUser(e) + task.UserTask);
                AddTask(task);
                return "It's on the list so you cant back out now.." + User.GetUser(e) + "lets get started! <3";
            }
            else
            {
                return User.GetUser(e) + " you need to finish or remove your last todo before adding another!";
            }
        }
    }
}
