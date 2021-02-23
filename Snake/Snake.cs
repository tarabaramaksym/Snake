using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public enum Movement
    {
        UP, RIGHT, DOWN, LEFT
    }
    public class Snake
    {
        class Part 
        {
            static int id = 0;
            // didn't knew about GUI+ when I wrote this, would definetly use it over picturebox in this case
            PictureBox box;
            Movement movement;
            public Part(Form parent,int top,int left) 
            {
               
                this.box = new PictureBox();
                this.movement = Movement.LEFT;
                parent.SuspendLayout();

                box.Anchor = System.Windows.Forms.AnchorStyles.None;
                box.BackColor = System.Drawing.Color.DarkRed;
                box.Location = new System.Drawing.Point(top,left);
                box.Name = $"snakePart{id++}";
                box.Size = new System.Drawing.Size(20, 20);
                box.TabIndex = 0;
                box.TabStop = false;

                parent.Controls.Add(box);
                parent.ResumeLayout(false);
                parent.PerformLayout();
            }

            public PictureBox Box { get => box; set => box = value; }
            public Movement Movement { get => movement; set => movement = value; }
        }

        List<Part> tail;
        Queue<Movement> history;
        int moveFrames;
        Movement lastMove = Movement.LEFT;
        Form parent;
        public Snake(Form parent,int moveFrames)
        {
            // generating base tail
            this.tail = new List<Part>() { new Part(parent, 360, 240), new Part(parent,380,240),new Part(parent,400, 240),
             new Part(parent, 420, 240), new Part(parent,440,240),new Part(parent,460, 240)};
            this.history = new Queue<Movement>();
            history.Enqueue(Movement.LEFT);
            history.Enqueue(Movement.LEFT);
            history.Enqueue(Movement.LEFT);
            history.Enqueue(Movement.LEFT);
            history.Enqueue(Movement.LEFT);
            history.Enqueue(Movement.LEFT);
            // ---------------------------

            this.parent = parent;
            this.moveFrames = moveFrames;
        }

        public void SendMove(Movement move)
        {
            // get movement from form every time timer ticks
            Movement compare = history.ElementAt(history.Count - 1);
            if (IllegalMove(move, compare))
                return;
            
            // if move is legal move queue and update movement for every part of the tail;
            lastMove = move;
            history.Dequeue();
            history.Enqueue(move);
            for (int i = 0, j = history.Count - 1; j >= 0; j--, i++)
                tail[i].Movement = history.ElementAt(j);
            
        }

        public bool IllegalMove(Movement move,Movement compare)
        {
            // returns true if move is ILLEGAL (!) otherwise false
            switch (compare)
            {
                case Movement.UP:
                    if (move == compare || move == Movement.DOWN) return true;
                    break;
                case Movement.RIGHT:
                    if (move == compare || move == Movement.LEFT) return true;
                    break;
                case Movement.DOWN:
                    if (move == compare || move == Movement.UP) return true;
                    break;
                case Movement.LEFT:
                    if (move == compare || move == Movement.RIGHT) return true;
                    break;
                default:
                    break;
            }
            return false;
        }
       
        public void Move(int width, int height)
        {
            foreach(Part p in tail)
            {
                switch (p.Movement)
                {
                    case Movement.UP:
                        if(p.Box.Top - moveFrames < 0)
                            p.Box.Top = height;
                        else
                            p.Box.Top -= moveFrames;
                        
                        break;
                    case Movement.RIGHT:
                        if (p.Box.Left + moveFrames > width)
                            p.Box.Left = 0;
                        else
                            p.Box.Left += moveFrames;

                        break;
                    case Movement.DOWN:
                        if (p.Box.Top + moveFrames > height)
                            p.Box.Top = 0;
                        else
                            p.Box.Top += moveFrames;
                        break;
                    case Movement.LEFT:
                        if (p.Box.Left - moveFrames < 0)
                            p.Box.Left = width;
                        else
                            p.Box.Left -= moveFrames;
                        break;
                    default:
                        break;
                }
            }
        }
        public void ChangeDirection(Movement move)
        {
            if(move == lastMove)
            {
                history.Dequeue();
                history.Enqueue(history.ElementAt(history.Count - 1));
                for (int i = 0, j = history.Count - 1; j >= 0; j--, i++)
                    tail[i].Movement = history.ElementAt(j);
            }
            else
            {
                SendMove(move);
            }
        }
        public bool GameOver()
        {
            for (int i = 0, j = 1; j < tail.Count; j++)
            {
                if(tail[i].Box.Location == tail[j].Box.Location)
                {
                    return true;
                }
            }
            return false;
        }
        public Point GetPoint()
        {
            // returns head of snake
            return tail.ElementAt(0).Box.Location;
        }
        public void IncreaseTail()
        {
            // increase tail, invoke when snake ate objective
            Part p = tail.ElementAt(tail.Count - 1);
            Point point = new Point();
            Movement move = Movement.UP;
            switch (p.Movement)
            {
                case Movement.UP:
                    point.X = p.Box.Location.X;
                    point.Y = p.Box.Location.Y+20;
                    move = Movement.UP;
                    break;
                case Movement.RIGHT:
                    point.X = p.Box.Location.X - 20;
                    point.Y = p.Box.Location.Y;
                    move = Movement.RIGHT;
                    break;
                case Movement.DOWN:
                    point.X = p.Box.Location.X;
                    point.Y = p.Box.Location.Y - 20;
                    move = Movement.DOWN;
                    break;
                case Movement.LEFT:
                    point.X = p.Box.Location.X + 20;
                    point.Y = p.Box.Location.Y;
                    move = Movement.LEFT;
                    break;
                default:
                    break;
            }

            this.tail.Add(new Part(parent, point.X,point.Y));

            List<Movement> moves = this.history.ToList();
            moves.Insert(0, move);
            this.history = new Queue<Movement>(moves);
        }
    }
}
