using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using static System.Formats.Asn1.AsnWriter;
using MonoBreaker.Script.Scene.GameScenes;

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
        private readonly SoundEffect bounceSound = GetContent.GetSound("bounce");
        private readonly SoundEffect paddleBounceSound = GetContent.GetSound("paddleBounce");
        private readonly SoundEffect ballLossSound = GetContent.GetSound("ballLoss");
        private readonly SoundEffect ballLaunchSound = GetContent.GetSound("gameEnd");
        private Vector2 projectedMovement; // doin stuff
        private Point collidePoint; // doin stuff

        public Ball(Texture2D image, Vector2 position, float speed, Rectangle[] ballBoundaries, Paddle paddle) : base(image, position) 
        {
            this.image = image;
            this.position = position;
            this.ballBoundaries = ballBoundaries;
            this.speed = speed;
            direction = new Vector2(0, -speed);
            this.paddle = paddle;
        }

        public bool IsActive
        {
            get { return isActive; }
        }

        public void ReverseDirectionX() 
        {
            direction.X *= -1;
        }

        public void ReverseDirectionY()
        {
            direction.Y *= -1;
        }

        public void Launch() 
        {
            if(!isActive)
                ballLaunchSound.Play();
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
            // no good
            if (collisionBox.Intersects(ballBoundaries[0]))
            {//right bound
                direction.X = Math.Abs(direction.X);
                bounceSound.Play();
            }

            if (collisionBox.Intersects(ballBoundaries[1]))
            {// left bound
                direction.X = -Math.Abs(direction.X);
                bounceSound.Play();
            }

            if (collisionBox.Intersects(ballBoundaries[2]))
            {// upper bound
                direction.Y = Math.Abs(direction.Y);
                bounceSound.Play();
            }
            if (collisionBox.Intersects(ballBoundaries[3]))
            {// kill
                ballLossSound.Play();
                Playing.tries--;
                Reset();
            }
            if (collisionBox.Intersects(paddle.collisionBox))
            {// add back 'or equal to' if somehow nonfucntional
                if (collisionBox.Left >= paddle.collisionBox.Right)
                {// leftside collision
                    direction.X = Math.Abs(direction.X);
                }
                if (collisionBox.Right <= paddle.collisionBox.Left)
                {// rightside collision
                    direction.X = -Math.Abs(direction.X);
                }

                if (collisionBox.Top > paddle.collisionBox.Bottom) // doesn't do shit
                {// if the ball somehow makes it behind the paddle
                    direction.Y = Math.Abs(direction.Y);
                }
                if (paddle.Velocity.X != 0)
                {// maintain ball movement if paddle isn't moving
                    direction.X = paddle.Velocity.X;
                    direction.Y *= -1;
                }
                else
                {
                    direction.Y *= -1;
                }

                paddleBounceSound.Play();
            }

            if (isActive) // only move ball if active
            {
                position += direction;
            }
            else // darken ball if not active, and make it hover above paddle
            {
                position.X = paddle.position.X + 14;
                position.Y = paddle.position.Y - 6;
                color = Color.DarkGray;
            } // ts pmo pmo pmo cuzz
            
            foreach(Brick brick in BrickMap.listBricks)
            {
                if (collisionBox.Intersects(brick.Rect))
                {
                    if (collisionBox.Right <= brick.Rect.Left || collisionBox.Left >= brick.Rect.Right)
                    {
                        ReverseDirectionX();
                    }
                    if (collisionBox.Top <= brick.Rect.Bottom || collisionBox.Bottom >= brick.Rect.Top)
                    {
                        ReverseDirectionY();
                    } 
                    brick.Weaken();
                    break; // better, but ball still phases through the sides of the bricks
                }
            }

            if (Playing.score % Playing.speedUpThreshold == 0 && Playing.score != 0 && Playing.score != prevScore) // speed up if score equals a certain value
            {
                speed += Playing.speedIncrement;
                direction.Y = speed * Math.Sign(direction.Y);
            }
            prevScore = Playing.score;
        }
        public void Draw(SpriteBatch window) 
        {
            window.Draw(this.image, collisionBox, color);
        }
    }
}
