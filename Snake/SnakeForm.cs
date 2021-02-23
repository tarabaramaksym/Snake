using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    // This is my first project using winforms.
    // I did this project just to get a hand at WinForms and understand how it works better

    /* 
     * There is 2 bugs (as far as I know) which I'm to lazy to fix.
     * 1 - If you will start pressing arrows keys very fast in all directions snake breaks in parts
     * 
     * 2 - Right as you come outside the screen, press arrow key to the other direction 
     * (if you going LEFT, press UP or DOWN) and your snake will stay outside the screen
     */



    public partial class SnakeForm : Form
    {
        Snake tail;
        Movement controlMove = Movement.LEFT;

        Random rand;
        public SnakeForm()
        {
            InitializeComponent();
            tail = new Snake(this,20);
            rand = new Random();
            SpawnObjective();
        }

 
        public void SpawnObjective()
        {
            // there should be a better way to do this, but I don't know how
            int r1 = rand.Next(60, this.Width - 60);
            int r2 = rand.Next(60, this.Height - 60);
            while (r1 % 20 != 0 || r2 % 20 != 0)
            {
                r1 = rand.Next(60, this.Width - 60);
                r2 = rand.Next(60, this.Height - 60);
            }

            this.objective.Location = new Point(r1, r2);
        }
        private void SnakeForm_Load(object sender, EventArgs e)
        {
            frameTimer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            if (tail.GameOver())
            {
                // ate its own tail
                this.Close();
                return;
            }

            tail.Move(this.ClientSize.Width, this.ClientSize.Height);

            if (tail.GetPoint().X == objective.Location.X &&
                tail.GetPoint().Y == objective.Location.Y )
            {
                // ate objective
                tail.IncreaseTail();
                SpawnObjective();
            }
            tail.ChangeDirection(controlMove);
        }
        
        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (tail.IllegalMove(Movement.UP,controlMove)) return; 
                    controlMove = Movement.UP; break;
                case Keys.Right:
                    if (tail.IllegalMove(Movement.RIGHT, controlMove)) return;
                    controlMove = Movement.RIGHT; break;
                case Keys.Down:
                    if (tail.IllegalMove(Movement.DOWN, controlMove)) return;
                    controlMove = Movement.DOWN; break;
                case Keys.Left:
                    if (tail.IllegalMove(Movement.LEFT, controlMove)) return;
                    controlMove = Movement.LEFT; break;
                default: break;
            }

        }

       
    }
}

