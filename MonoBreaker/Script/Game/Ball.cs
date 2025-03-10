using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Game
{
    public class Ball: Sprite
    {
        private Vector2 direction =  Vector2.Zero;
        private Rectangle[] ballBoundaries = new Rectangle[4];
        private Paddle paddle;
        private bool isActive = false;
        int speed;

        public Ball(Texture2D image, Vector2 position, int speed, Rectangle[] ballBoundaries, Paddle paddle) : base(image, position) 
        {
            this.image = image;
            this.position = position;
            this.ballBoundaries = ballBoundaries;
            this.speed = speed;
            direction = new Vector2(0, -speed);
            this.paddle = paddle;
        }

        public void ReverseDirectionX() 
        {
            direction.X *= -1;
        }

        public void ReverseDirectionY()
        {
            direction.Y *= -1;
        }

        public void BounceDownwards()
        {
            direction.Y = Math.Abs(direction.Y);
        }

        public void BounceUpwards()
        {
            direction.Y = -Math.Abs(direction.Y);
        }

        

        public void Launch() 
        {
            isActive = true;
            direction.X = paddle.Velocity.X;
        }

        public void Reset()
        {
            isActive = false;
            direction = new Vector2(0, -speed);
        }

        public void Update() 
        {
            if (isActive)
            {
                position += direction;
                
                // collision problem solved, you're welcome :)
                if (collisionBox.Intersects(ballBoundaries[0]))// || collisionBox.Intersects(ballBoundaries[1]))
                {//right bound
                    direction.X = Math.Abs(direction.X);
                }

                if (collisionBox.Intersects(ballBoundaries[1]))
                {// left bound
                    direction.X = -Math.Abs(direction.X);
                }

                if (collisionBox.Intersects(ballBoundaries[2]))
                {// upper bound
                    direction.Y = Math.Abs(direction.Y);
                }
                if (collisionBox.Intersects(ballBoundaries[3]))
                {// kill
                    Reset();
                }
                if (collisionBox.Intersects(paddle.collisionBox))
                {// add back 'or equal to' flag if somehow nonfucntional
                    if (collisionBox.Left >= paddle.collisionBox.Right)
                    {// leftside collision
                        direction.X = Math.Abs(direction.X);
                    }
                    if (collisionBox.Right <= paddle.collisionBox.Left)
                    {// rightside collision
                        direction.X = -Math.Abs(direction.X);
                    }

                    if (collisionBox.Top > paddle.collisionBox.Bottom)
                    {// if the ball somehow makes it behind the paddle
                        direction.Y = Math.Abs(direction.Y);
                    }
                    if (paddle.Velocity.X != 0) // <TODO: add special collision checks for top of paddle, could prevent future issues>
                    {// maintain ball movement if paddle isn't moving
                        direction.X = paddle.Velocity.X;
                        direction.Y *= -1;
                    }
                    else
                    {
                        direction.Y *= -1;
                    }
                }
            }
            else 
            {
                position.X = paddle.position.X + 14;
                position.Y = paddle.position.Y - 6;
            }
            
        }

        public void Draw(SpriteBatch window) 
        {
            window.Draw(this.image, collisionBox, Color.White);
        }
    }
}
