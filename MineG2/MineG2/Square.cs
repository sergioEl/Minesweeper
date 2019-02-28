using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineGameSpweeper
{
    class Square
    {
        public EventHandler Dismantle;
        public EventHandler Explode;

        private Button button;
        private bool dismantled = false;
        private Game game;
        private bool mined = false;
        private bool opened = false;
        private int x;
        private int y;

        public Square(Game game, int x, int y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            button = new Button();
            Button.Text = "";

            int w = game.Panel.Width / game.Width;
            int h = game.Panel.Height / game.Height;

            button.Width = w + 1;
            button.Height = h + 1;
            button.Left = w * X;
            button.Top = h * Y;
            button.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            button.Click += new EventHandler(Click);
            button.MouseDown += new MouseEventHandler(DismantleClick);

            game.Panel.Controls.Add(Button);


        }

        public Button Button
        {
            get { return (this.button); }
        }

        private void Click(object sender, System.EventArgs e)
        {
            if (!Dismantled)
            {
                if (mined)
                {
                    Button.BackColor = Color.Red;
                    OnExplode();
                }
                else
                {
                    this.Open();
                }
            }
        }

        private void DismantleClick(object sender, MouseEventArgs e)
        {
            if (!opened && e.Button == MouseButtons.Right)
            {
                if (Dismantled)
                {
                    dismantled = false;
                    Button.BackColor = SystemColors.Control;
                    Button.Text = "?";
                }
                else
                {
                    dismantled = true;
                    Button.ForeColor = Color.Red;
                    Button.Text = "¶";
                }
                OnDismantle();
            }
        }

        public bool Dismantled
        {
            get { return (this.dismantled); }
        }

        public bool Mined
        {
            get { return (this.mined); }
            set { this.mined = value; }
        }

        protected void OnDismantle()
        {
            if (Dismantle != null)
            {
                Dismantle(this, new EventArgs());
            }
        }

        protected void OnExplode()
        {
            if (Explode != null)
            {
                Explode(this, new EventArgs());
            }
        }

        public void Open()
        {
            if (!Opened && !Dismantled)
            {
                opened = true;

                int c = 0;
                if (game.IsBomb(X - 1, Y - 1)) c++;
                if (game.IsBomb(X - 0, Y - 1)) c++;
                if (game.IsBomb(X + 1, Y - 1)) c++;
                if (game.IsBomb(X - 1, Y - 0)) c++;
                if (game.IsBomb(X - 0, Y - 0)) c++;
                if (game.IsBomb(X + 1, Y - 0)) c++;
                if (game.IsBomb(X - 1, Y + 1)) c++;
                if (game.IsBomb(X - 0, Y + 1)) c++;
                if (game.IsBomb(X + 1, Y + 1)) c++;

                if (c > 0)
                {
                    Button.Text = c.ToString();
                    switch (c)
                    {
                        case 1:
                            Button.ForeColor = Color.Blue;
                            break;
                        case 2:
                            Button.ForeColor = Color.Green;
                            break;
                        case 3:
                            Button.ForeColor = Color.Red;
                            break;
                        case 4:
                            Button.ForeColor = Color.DarkBlue;
                            break;
                        case 5:
                            Button.ForeColor = Color.DarkRed;
                            break;
                        case 6:
                            Button.ForeColor = Color.LightBlue;
                            break;
                        case 7:
                            Button.ForeColor = Color.Orange; 
                            break;
                        case 8:
                            Button.ForeColor = Color.Ivory;
                            break;
                    }
                }
                else
                {
                    Button.BackColor = SystemColors.ControlLight;
                    Button.FlatStyle = FlatStyle.Standard;
                    Button.Enabled = false;

                    game.OpenSpot(X - 1, Y - 1);
                    game.OpenSpot(X - 0, Y - 1);
                    game.OpenSpot(X + 1, Y - 1);
                    game.OpenSpot(X - 1, Y - 0);
                    game.OpenSpot(X - 0, Y - 0);
                    game.OpenSpot(X + 1, Y - 0);
                    game.OpenSpot(X - 1, Y + 1);
                    game.OpenSpot(X - 0, Y + 1);
                    game.OpenSpot(X + 1, Y + 1);


                }
            }
        }

        public bool Opened
        {
            get { return (this.opened); }
        }

        public int X
        {
            get { return (this.x); }
        }

        public int Y
        {
            get { return (this.y); }
        }

        public void RemoveEvents()
        {
            button.Click -= new EventHandler(Click);
            button.MouseDown -= new MouseEventHandler(DismantleClick);
        }


    }
}
