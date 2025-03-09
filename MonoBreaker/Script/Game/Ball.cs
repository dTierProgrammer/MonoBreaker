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

        public Rectangle collisionBox
        {

            get
            {
                return new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
            }

        }

        public void Launch() 
        {
            isActive = true;
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

                if (collisionBox.Intersects(ballBoundaries[0]) || collisionBox.Intersects(ballBoundaries[1]))
                {
                    direction.X *= -1;
                }
                if (collisionBox.Intersects(ballBoundaries[2]))
                {
                    direction.Y *= -1;
                }
                if (collisionBox.Intersects(ballBoundaries[3]))
                {
                    Reset();
                }
                if (collisionBox.Intersects(paddle.collisionBox))
                {
                    if (paddle.Velocity.X != 0)
                    {
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
