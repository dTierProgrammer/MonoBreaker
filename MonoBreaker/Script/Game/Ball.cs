using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MonoBreaker.Script.Game
{
    public class Ball: Sprite
    {
        private Vector2 direction =  Vector2.Zero;
        private Rectangle[] ballBoundaries = new Rectangle[4];
        private Paddle paddle;
        private bool isActive = false;
        float speed;
        int prevScore;
        private Color color = Color.DarkGray;

        public Ball(Texture2D image, Vector2 position, float speed, Rectangle[] ballBoundaries, Paddle paddle) : base(image, position) 
        {
            this.image = image;
            this.position = position;
            this.ballBoundaries = ballBoundaries;
            this.speed = speed;
            direction = new Vector2(0, -speed);
            this.paddle = paddle;
        }

        public bool isBallActive
        {
            get { return isActive; }
        }

        public void CheckCollision()
        {
            // DO SHIT IN THIS METHOD !!!
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
            color = Color.White;
        }

        public void Reset()
        {
            isActive = false;
            direction = new Vector2(0, -speed);
        }

        public void Update() 
        {
            // collision problem (somewhat?) solved
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
                Game1.tries--;
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

            if (isActive)
            {
                position += direction;

            }
            else
            {
                position.X = paddle.position.X + 14;
                position.Y = paddle.position.Y - 6;
                color = Color.DarkGray;
            }

            if (Game1.score % Game1.speedUpThreshold == 0 && Game1.score != 0 && Game1.score != prevScore)
            {
                speed += Game1.speedIncrement;
                //direction.X = speed * Math.Sign(direction.X);
                direction.Y = speed * Math.Sign(direction.Y);
            }
            prevScore = Game1.score;
        }
        public void Draw(SpriteBatch window) 
        {
            window.Draw(this.image, collisionBox, color);
        }
    }
}
