using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBotAsta
{
    class Task
    {
        private string user;
        private string userTask;

        public string User
        {
            get => user;
            set { user = value; }
        }

        public string UserTask
        {
            get => userTask;
            set { userTask = value; }
        }

        public Task(string user, string task)
        {
            this.user = user;
            this.userTask = task;
        }
    }
}
