using MineGameSpweeper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Seokho Han
 * 700657609
 * MineSweeperGame v1.0
 **/

namespace MineG2
{
    public partial class Form1 : Form
    {
       
        private Game game;
   

        public Form1()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            game = new Game(this.panel1, 10, 20, 15);
            game.Tick += new EventHandler(GameTick);
            game.DismantledMinesChanged += new EventHandler(GameDismantledMinesChanged);
            game.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            label2.Text = game.Time.ToString();
        }

        private void GameDismantledMinesChanged(object sender, EventArgs e)
        {
            label1.Text = (game.Mines - game.DismantledMines).ToString();
        }

    }
}
