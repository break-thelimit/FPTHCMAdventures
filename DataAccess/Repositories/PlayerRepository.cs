using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public Player AddNewPlayer(Guid userid) => PlayerDAO.Instance.AddNewPlayer(userid);

        public Player GetPlayerByID(Guid id) => PlayerDAO.Instance.GetPlayerByID(id);

        public IEnumerable<Player> GetPlayers() => PlayerDAO.Instance.GetPlayers();
    }
}
