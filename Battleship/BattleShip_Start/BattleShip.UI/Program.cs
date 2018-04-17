using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            GameInput gi = new GameInput();
            GameView gv = new GameView();
            Player p1 = new Player();
            Player p2 = new Player();
            p1.EnemyPlayerBoard = p2.PlayerBoard;
            p2.EnemyPlayerBoard = p1.PlayerBoard;
            GameManager gm = new GameManager(gi, gv, p1, p2);
            gm.Start();
        }
    }
}
