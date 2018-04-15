using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleshiptop1
{
    public delegate void PanelDelegate();

    class GameLogic
    {
        public PlayerPanel p1, p2;
        public GameLogic()
        {
            p1 = new PlayerPanel(PanelPosition.Left, PlayerType.Human, MakeBotTurn, PanelEnabled);
            p2 = new PlayerPanel(PanelPosition.Right, PlayerType.Bot, MakeBotTurn, PanelEnabled);
        }

        void MakeBotTurn()
        {
            Random rnd = new Random();
            int i = rnd.Next(1, 11);
            int j = rnd.Next(1, 11);
            while (p1.brain.Process2(string.Format("{0}_{1}", i, j)))
            {
                Thread.Sleep(1000);
                i = rnd.Next(1, 11);
                j = rnd.Next(1, 11);
            }
        }

        void PanelEnabled()
        {
            p1.Enabled = false;
            p2.Enabled = true;
        }
    }
}
