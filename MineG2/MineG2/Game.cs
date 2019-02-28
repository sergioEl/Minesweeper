using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineGameSpweeper
{
    class Game
    {
        public event EventHandler DismantledMinesChanged;
        public event EventHandler Tick;

        private int dismantledMines;
        private int height;
        private int incorrectdismantledMines;
        private int mines;
        private Panel panel;
        private Square[,] squares;
        private Timer timer;
        private int width;

        public int Time;

        public Game(Panel panel, int width, int height, int mines)
        {
            this.panel = panel;
            this.width = width;
            this.height = height;
            this.mines = mines;
        }

        private void Dismantle(object sender, EventArgs e)
        {
            Square s = (Square)sender;
            if (s.Dismantled)
            {
                if (s.Mined)
                {
                    dismantledMines++;
                }
                else
                {
                    incorrectdismantledMines++;
                }
            }
            else
            {
                if (s.Mined)
                {
                    dismantledMines--;
                }
                else
                {
                    incorrectdismantledMines--;
                }
            }

            OnDismantledMinesChanged();

            if (dismantledMines == Mines)
            {
                timer.Enabled = false;
                panel.Enabled = false;
                MessageBox.Show("You win!!", "Alter");
            }
        }

        public int DismantledMines
        {
            get { return dismantledMines + incorrectdismantledMines; }
        }

        private void Explode(object sender, EventArgs e)
        {
            timer.Enabled = false;

            foreach (Square s in squares)
            {
                s.RemoveEvents();
                if (s.Mined)
                {
                    s.Button.Text = "*";
                    s.Button.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                    s.Button.ForeColor = Color.Black;
                }
            }
        }

        public int Height
        {
            get { return (this.height); }
        }

        public bool IsBomb(int x, int y)
        {
            if (x >= 0 && x < Width)
            {
                if (y >= 0 && y < Height)
                {
                    return squares[x, y].Mined;
                }
            }
            return false;
        }

        public int Mines
        {
            get { return (this.mines); }
        }

        protected void OnDismantledMinesChanged()
        {
            if (DismantledMinesChanged != null)
            {
                DismantledMinesChanged(this, new EventArgs());
            }
        }

        protected void OnTick()
        {
            if (Tick != null)
            {
                Tick(this, new EventArgs());
            }
        }

        public void OpenSpot(int x, int y)
        {
            if (x >= 0 && x < width)
            {
                if (y >= 0 && y < Height)
                {
                    squares[x, y].Open();
                }
            }
        }

        public Panel Panel
        {
            get { return (this.panel); }
        }

        public void Start()
        {
            Time = 0;
            dismantledMines = 0;
            incorrectdismantledMines = 0;
            OnTick();
            Panel.Enabled = true;
            Panel.Controls.Clear();

            squares = new Square[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Square s = new Square(this, x, y);
                    s.Explode += new EventHandler(Explode);
                    s.Dismantle += new EventHandler(Dismantle);
                    squares[x, y] = s;
                }
            }

            int b = 0;
            Random r = new Random();
            while (b < Mines)
            {
                int x = r.Next(Width);
                int y = r.Next(Height);

                Square s = squares[x, y];
                if (!s.Mined)
                {
                    s.Mined = true;
                    b++;
                }
            }

            OnDismantledMinesChanged();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerTick);
            timer.Enabled = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Time++;
            OnTick();
        }

        public int Width
        {
            get { return (this.width); }
        }



    }
}
