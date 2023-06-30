using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PlayerDAO
    {
        private static PlayerDAO instance = null;
        private static readonly object instanceLock = new object();
        private PlayerDAO() { }


        public static PlayerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PlayerDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    players = context.Players.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllPlayers: " + ex.Message);
            }
            return players;
        }

        public Player GetPlayerByID(Guid id)
        {
            Player player = new Player();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    player = context.Players.SingleOrDefault(a => a.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetPlayerByID: " + ex.Message);
            }
            return player;
        }

        public Player AddNewPlayer(Guid userid)
        {
            Player player = new Player
            {
                Id = new Guid(),
                UserId = userid,
                TotalPoint = 0,
                TotalTime = 0
            };

            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Players.Add(player);
            }
            catch (Exception ex)
            {
                throw new Exception("Error at CreateItem: " + ex.Message);
            }
            return player;
        }
    }
}
