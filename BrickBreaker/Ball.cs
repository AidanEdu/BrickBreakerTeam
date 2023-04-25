﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrickBreaker
{
    public class Ball
    {
        public int x, y, xSpeed, ySpeed, size;
        public Color colour;
        public static Random rand = new Random();
        public bool canCollide = true;
        public int collisionTimer;

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed, int _ballSize)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;

        }

        public void Move()
        {
            x = x + xSpeed;
            y = y + ySpeed;
        }

        public bool BlockCollision(Block b)
        {
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }

            return blockRec.IntersectsWith(ballRec);
        }

        public void PaddleCollision(Paddle p)
        {
            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle leftSideRec = new Rectangle(p.x - 3, p.y, 3, p.height);
            Rectangle rightSideRec = new Rectangle(p.x + p.width + 3, p.y, 3, p.height);
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);

            #region Paddle side collision code
            if (canCollide == true)
            {
                if (ballRec.IntersectsWith(paddleRec) && ballRec.Y <= 617)
                {
                    ySpeed *= -1;
                    canCollide = false;
                }
                if (ballRec.IntersectsWith(leftSideRec))
                {
                    xSpeed *= -1;
                    canCollide = false;
                }
                if (ballRec.IntersectsWith(rightSideRec))
                {
                    xSpeed *= -1;
                    canCollide = false;
                }
            }
            else if (canCollide == false)
            {
                collisionTimer++;
            }
            #endregion

            #region Collision timer
            if (collisionTimer >= 100)
            {
                canCollide = true;
                collisionTimer = 0;
            }
            #endregion
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                xSpeed *= -1;
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed *= -1;
            }
            // Collision with top wall
            if (y <= 2)
            {
                ySpeed *= -1;
            }
        }

        public bool BottomCollision(UserControl UC)
        {
            Boolean didCollide = false;

            if (y >= UC.Height)
            {
                didCollide = true;
            }

            return didCollide;
        }

    }
}
