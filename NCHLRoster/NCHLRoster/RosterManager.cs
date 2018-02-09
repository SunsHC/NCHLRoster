using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NCHLRoster.Enums;

namespace NCHLRoster
{
    class RosterManager
    {
        public List<Player> Players { get; protected set; }

        public RosterManager()
        {
            Players = new List<Player>();
        }

        internal void LoadNCHLDB()
        {
            using (StreamReader sr = new StreamReader("DB NCHL.csv"))
            {
                do
                {
                    string[] line = sr.ReadLine().Split(',');

                    Players.Add(new Player()
                    {
                        Name = line[0],
                        NCHLTeam = Utilities.GetNCHLTeamFromString(line[1]),
                        Pos = Utilities.GetPlayerPositionFromString(line[2]),
                        Id = Convert.ToInt32(line[3])
                    });
                } while (sr.Peek() >= 0);
            }
        }

        internal void GenerateAlertString(NCHLTeam team)
        {
            string path = $"AlertStrings/{team}.txt";
            if (!Directory.Exists("AlertStrings"))
                Directory.CreateDirectory("AlertStrings");

            using (StreamWriter sw = new StreamWriter(path))
            {
                var playersList = Players.Where(pla => pla.NCHLTeam == team).OrderBy(pla => pla.Name.Split(' ')[1]);

                for (int i = 0; i < playersList.Count(); i++)
                {
                    sw.Write($"\"{playersList.ElementAt(i).Name}\"");

                    if (i == playersList.Count() - 1 || (i != 0 && i % 10 == 0))
                    {
                        sw.Write($" site:cbssports.com");
                        sw.WriteLine();
                    }
                    else
                        sw.Write($" OR ");
                }
            }            
        }

        internal void GenerateTeamFile(NCHLTeam team)
        {
            using (StreamWriter sw = new StreamWriter(string.Format("Roster{0}.csv", team)))
            {
                var goalies = Players.Where(pla => pla.NCHLTeam == team && pla.Pos == PlayerPosition.G).OrderBy(pla => pla.Name.Split(' ')[1]);
                var defencemans = Players.Where(pla => pla.NCHLTeam == team && pla.Pos == PlayerPosition.D).OrderBy(pla => pla.Name.Split(' ')[1]);
                var centers = Players.Where(pla => pla.NCHLTeam == team && pla.Pos == PlayerPosition.C).OrderBy(pla => pla.Name.Split(' ')[1]);
                var rightWings = Players.Where(pla => pla.NCHLTeam == team && pla.Pos == PlayerPosition.R).OrderBy(pla => pla.Name.Split(' ')[1]);
                var leftWings = Players.Where(pla => pla.NCHLTeam == team && pla.Pos == PlayerPosition.L).OrderBy(pla => pla.Name.Split(' ')[1]);

                int maxCount = Math.Max(goalies.Count(), Math.Max(defencemans.Count(), Math.Max(centers.Count(), Math.Max(rightWings.Count(), leftWings.Count()))));
                for(int i = 0; i < maxCount; i++)
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                        i < goalies.Count() ? goalies.ElementAt(i).Name : string.Empty,
                        i < defencemans.Count() ? defencemans.ElementAt(i).Name : string.Empty,
                        i < centers.Count() ? centers.ElementAt(i).Name : string.Empty,
                        i < rightWings.Count() ? rightWings.ElementAt(i).Name : string.Empty,
                        i < leftWings.Count() ? leftWings.ElementAt(i).Name : string.Empty));
                }
            }
        }

        internal void GenerateLeagueFile()
        {
            using (StreamWriter sw = new StreamWriter("RosterNCHL.csv"))
            {
                var goalies = Players.Where(pla => pla.NCHLTeam != NCHLTeam.AGL && pla.Pos == PlayerPosition.G).OrderBy(pla => pla.Name.Split(' ')[1]);
                var defencemans = Players.Where(pla => pla.NCHLTeam != NCHLTeam.AGL && pla.Pos == PlayerPosition.D).OrderBy(pla => pla.Name.Split(' ')[1]);
                var centers = Players.Where(pla => pla.NCHLTeam != NCHLTeam.AGL && pla.Pos == PlayerPosition.C).OrderBy(pla => pla.Name.Split(' ')[1]);
                var rightWings = Players.Where(pla => pla.NCHLTeam != NCHLTeam.AGL && pla.Pos == PlayerPosition.R).OrderBy(pla => pla.Name.Split(' ')[1]);
                var leftWings = Players.Where(pla => pla.NCHLTeam != NCHLTeam.AGL && pla.Pos == PlayerPosition.L).OrderBy(pla => pla.Name.Split(' ')[1]);

                int maxCount = Math.Max(goalies.Count(), Math.Max(defencemans.Count(), Math.Max(centers.Count(), Math.Max(rightWings.Count(), leftWings.Count()))));
                for (int i = 0; i < maxCount; i++)
                {
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                       i < goalies.Count() ? string.Format("{1} [{0}]", goalies.ElementAt(i).NCHLTeam, goalies.ElementAt(i).Name) : string.Empty,
                       i < defencemans.Count() ? string.Format("{1} [{0}]", defencemans.ElementAt(i).NCHLTeam, defencemans.ElementAt(i).Name) : string.Empty,
                       i < centers.Count() ? string.Format("{1} [{0}]", centers.ElementAt(i).NCHLTeam, centers.ElementAt(i).Name) : string.Empty,
                       i < rightWings.Count() ? string.Format("{1} [{0}]", rightWings.ElementAt(i).NCHLTeam, rightWings.ElementAt(i).Name) : string.Empty,
                       i < leftWings.Count() ? string.Format("{1} [{0}]", leftWings.ElementAt(i).NCHLTeam, leftWings.ElementAt(i).Name) : string.Empty));

                    //sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                    //    i < goalies.Count() ? string.Format("[{0}] {1}", goalies.ElementAt(i).NCHLTeam, goalies.ElementAt(i).Name) : string.Empty,
                    //    i < defencemans.Count() ? string.Format("[{0}] {1}", defencemans.ElementAt(i).NCHLTeam, defencemans.ElementAt(i).Name) : string.Empty,
                    //    i < centers.Count() ? string.Format("[{0}] {1}", centers.ElementAt(i).NCHLTeam, centers.ElementAt(i).Name) : string.Empty,
                    //    i < rightWings.Count() ? string.Format("[{0}] {1}", rightWings.ElementAt(i).NCHLTeam, rightWings.ElementAt(i).Name) : string.Empty,
                    //    i < leftWings.Count() ? string.Format("[{0}] {1}", leftWings.ElementAt(i).NCHLTeam, leftWings.ElementAt(i).Name) : string.Empty));
                }
            }
        }

    }
}
