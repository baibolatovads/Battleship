using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleshiptop1
{
    enum PanelPosition
    {
        Left,
        Right
    }

    enum PlayerType
    {
        Human,
        Bot
    }

    class PlayerPanel: Panel
    {
        public Brain brain;
        int cellW = 20;
        PanelPosition panelPosition;
        PlayerType playerType;
        PanelDelegate tDelegate;
        PanelDelegate enDelegate;

        public PlayerPanel(PanelPosition panelPosition, PlayerType playerType, PanelDelegate tDelegate, PanelDelegate enDelegate)
        {
            this.panelPosition = panelPosition;
            this.playerType = playerType;
            this.tDelegate = tDelegate;
            this.enDelegate = enDelegate;

            Initialize();  //What does this mean?
            Random rnd1 = new Random(Guid.NewGuid().GetHashCode()); // What is guid?
            Random rnd2 = new Random(Guid.NewGuid().GetHashCode());

            /*
            if (playerType == PlayerType.Human)
            {
                while (brain.stIndex < brain.st.Length - 1) // ???
                {
                    int row = rnd1.Next(1, 11);
                    int column = rnd1.Next(1, 11);
                    string msg = string.Format("{0}_{1}", row, column);
                    brain.Process(msg);
                }
            }
            */

            if (playerType == PlayerType.Bot)
            {
                this.Enabled = false;
                while (brain.stIndex < brain.st.Length - 1)
                {
                    int row = rnd2.Next(1, 11);
                    int column = rnd2.Next(1, 11);
                    string msg = string.Format("{0}_{1}", row, column);
                    brain.Process(msg);
                }
            }
        }

        private void Initialize()
        {
            this.Location = new System.Drawing.Point(cellW + 10, cellW + 10);

            if (panelPosition == PanelPosition.Right)
            {
                this.Location = new System.Drawing.Point(cellW * 12 + cellW + 20, cellW + 10);
            }

            this.BackColor = SystemColors.ActiveCaption;
            this.Size = new System.Drawing.Size(cellW * 12, cellW * 12);

            for (int i = 1; i <= 10; ++i)
            {
                for (int j = 1; j <= 10; ++j)
                {
                    Button btn = new Button();
                    btn.Name = i + "_" + j;
                    btn.Click += Btn_Click;
                    btn.Size = new Size(cellW, cellW);
                    btn.Location = new Point(i * cellW, j * cellW);
                    this.Controls.Add(btn);
                }
            }

            brain = new Brain(ChangeButton);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (brain.stIndex < brain.st.Length - 1)
            {
                brain.Process(btn.Name);
            }
            else
            {
                if (!brain.Process2(btn.Name))
                {
                    tDelegate.Invoke();
                }
            }

            if(brain.stIndex == brain.st.Length - 1)
            {
                enDelegate.Invoke();
            }
        }

        private void FillMe(CellState[,] map)
        {

            for (int i = 1; i < 11; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    Color colorToFill = Color.White;
                    bool isEnabled = true;

                    switch (map[i, j])
                    {
                        case CellState.empty:
                            colorToFill = Color.White;
                            break;
                        case CellState.busy:
                            colorToFill = Color.Blue;
                            break;
                        case CellState.striked:
                            colorToFill = Color.Yellow;
                            isEnabled = false;
                            break;
                        case CellState.missed:
                            colorToFill = Color.Gray;
                            isEnabled = false;
                            break;
                        case CellState.killed:
                            colorToFill = Color.Red;
                            isEnabled = false;
                            break;
                        default:
                            break;
                    }

                    this.Controls[10 * (i-1) + j-1].BackColor = colorToFill; //??????
                    this.Controls[10 * (i-1) + j-1].Enabled = isEnabled;
                }
            }
        }

        private void MakeMasked(CellState[,] map)
        {
            for (int i = 1; i < 11; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    Color colorToFill = Color.White;
                    bool isEnabled = true;

                    switch (map[i, j])
                    {
                        case CellState.empty:
                            colorToFill = Color.White;
                            break;
                        case CellState.busy:
                            colorToFill = Color.White;
                            break;
                        case CellState.striked:
                            colorToFill = Color.Yellow;
                            isEnabled = false;
                            break;
                        case CellState.missed:
                            colorToFill = Color.Gray;
                            isEnabled = false;
                            break;
                        case CellState.killed:
                            colorToFill = Color.Red;
                            isEnabled = false;
                            break;
                        default:
                            break;
                    }

                    this.Controls[10 * (i - 1) + j - 1].BackColor = colorToFill; //??????
                    this.Controls[10 * (i - 1) + j - 1].Enabled = isEnabled;
                }
            }
        }
        private void ChangeButton(CellState[,] map)
        {
            if (playerType == PlayerType.Human)
            {
                FillMe(map);
            }
            else
            {
                MakeMasked(map);
            }
        }
    }
}
