using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NCHLRoster.Enums;

namespace NCHLRoster
{
    class Utilities
    {
        public static NCHLTeam GetNCHLTeamFromString(string teamName)
        {
            return (NCHLTeam)Enum.Parse(typeof(NCHLTeam), teamName);
        }

        public static PlayerPosition GetPlayerPositionFromString(string position)
        {
            return (PlayerPosition)Enum.Parse(typeof(PlayerPosition), position);
        }
    }
}
