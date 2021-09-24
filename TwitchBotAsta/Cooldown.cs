using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBotAsta
{
    class Cooldown
    {
        // ---------- POMO-COMMANDS ----------
        public static Dictionary<Pomodoro, DateTime> globalCooldownsPomos = new Dictionary<Pomodoro, DateTime>()
        {
            { Pomodoro.ADD, DateTime.Now },
            { Pomodoro.DONE, DateTime.Now },
            { Pomodoro.HELP, DateTime.Now }
        };

        public static Dictionary<Pomodoro, int> globalCooldownLengthsPomos = new Dictionary<Pomodoro, int>()
        {
            { Pomodoro.ADD, 30 },
            { Pomodoro.DONE, 30 },
            { Pomodoro.HELP, 30 }
        };

        public static Dictionary<Pomodoro, bool> globalCooldownsRunningPomos = new Dictionary<Pomodoro, bool>()
        {
            { Pomodoro.ADD, false },
            { Pomodoro.ADD, false },
            { Pomodoro.HELP, false }
        };

        public static bool CheckCooldownOffPomodoro(Pomodoro pomo)
        {
            if (DateTime.Now >= globalCooldownsPomos[pomo].AddSeconds(globalCooldownLengthsPomos[pomo]))
            {
                globalCooldownsRunningPomos[pomo] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
