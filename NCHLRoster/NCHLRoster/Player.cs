using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NCHLRoster.Enums;

namespace NCHLRoster
{
    class Player
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public PlayerPosition Pos { get; set; }
        public NCHLTeam NCHLTeam { get; set; }
    }
}
