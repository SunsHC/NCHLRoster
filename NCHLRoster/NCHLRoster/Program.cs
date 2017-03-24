using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NCHLRoster.Enums;

namespace NCHLRoster
{
    class Program
    {
        static void Main(string[] args)
        {
            RosterManager manager = new RosterManager();
            manager.LoadNCHLDB();

            foreach (NCHLTeam team in Enum.GetValues(typeof(NCHLTeam)))
            {
                if (team == NCHLTeam.AGL)
                    continue;
                manager.GenerateTeamFile(team);
            }

            manager.GenerateLeagueFile();
        }
    }
}
