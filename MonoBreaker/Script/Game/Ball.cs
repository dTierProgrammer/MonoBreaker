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
using System.Diagnostics;

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
        private Point collidePoint = new Point(1, 1); // doin stuff
        private Vector2 prevPosition;
        Brick _brick;
        private bool hasCollided = false;
        
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

        public void BounceLeft() 
        {
            direction.X = -Math.Abs(direction.X);
        }

        public void BounceRight() 
        {
            direction.X = Math.Abs(direction.X);
        }

        public void BounceUp() 
        {
            direction.Y = -Math.Abs(direction.Y);
        }

        public void BounceDown() 
        {
            direction.Y = Math.Abs(direction.Y);
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

        public void Update(GameTime gameTime) 
        {
            // projected movement, I THINK
            projectedMovement = new Vector2 (position.X += direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds, position.Y += direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (isActive) // only move ball if active
            {
                // X Start
                position.X += direction.X;

                if (collisionBox.Intersects(ballBoundaries[0])) // collision detections have to be done to the right
                {//right bound
                    if (prevPosition.X < ballBoundaries[0].Right) // appears to work
                    {
                        position.X = ballBoundaries[0].Right;
                    }
                    BounceRight();
                    bounceSound.Play();
                }

                if (collisionBox.Intersects(ballBoundaries[1])) // collision detections have to be done to the left
                {// left bound
                    
                    if (prevPosition.X > ballBoundaries[1].Left) // appears to work
                    {
                        position.X = ballBoundaries[1].Left - collisionBox.Width;
                    }
                    BounceLeft();  
                    bounceSound.Play();
                }

                if (collisionBox.Intersects(paddle.collisionBox))
                {// add back 'or equal to' if somehow nonfucntional
                    if ((collisionBox.Right <= paddle.collisionBox.Left) && (prevPosition.X <= paddle.collisionBox.Right))
                    {// right side collision
                        position.X = paddle.collisionBox.Right;
                        BounceRight();
                    }
                    if ((collisionBox.Left >= paddle.collisionBox.Right) && (prevPosition.X >= paddle.collisionBox.Left))
                    {// left side collision
                        position.X = paddle.collisionBox.Left - paddle.collisionBox.Width;
                        BounceLeft();
                    }
                    
                }

                foreach (Brick brick in BrickMap.listBricks) 
                {
                    if (collisionBox.Intersects(brick.Rect)) 
                    {
                        _brick = brick;
                        hasCollided = true;
                        break;
                    }
                }

                if (hasCollided && collisionBox.Intersects(_brick.Rect) ) 
                {
                    if ((collisionBox.Right >= _brick.Rect.Left) && (prevPosition.X >= _brick.Rect.Right))
                    {// right side collision
                        
                        position.X = prevPosition.X;
                        BounceRight();
                    }
                    if ((collisionBox.Left <= _brick.Rect.Right) && (prevPosition.X <= _brick.Rect.Left))
                    {// left side collision
                        
                        position.X = prevPosition.X;
                        BounceLeft();
                    }
                    _brick.Weaken();
                    hasCollided = false;
                }
                
                // X End

                // Y Start 
                position.Y += direction.Y;
                if (collisionBox.Intersects(ballBoundaries[2]))
                {// upper bound
                    if (prevPosition.Y > ballBoundaries[2].Bottom) 
                    {
                        position.Y = ballBoundaries[2].Bottom;
                    }
                    BounceDown();
                    bounceSound.Play();
                }
                if (collisionBox.Intersects(ballBoundaries[3]))
                {// kill
                    ballLossSound.Play();
                    Playing.tries--;
                    Reset();
                }
                if (collisionBox.Intersects(paddle.collisionBox)) 
                {
                    if ((collisionBox.Top <= paddle.collisionBox.Bottom) && (prevPosition.Y <= paddle.collisionBox.Bottom))
                    { // up
                        BounceUp();
                        position.Y = paddle.collisionBox.Top - collisionBox.Height;
                    }
                    if ((collisionBox.Bottom >= paddle.collisionBox.Top) && (prevPosition.Y >= paddle.collisionBox.Top))
                    { // down (if the ball gets below the paddle)
                        BounceDown();
                        position.Y = paddle.collisionBox.Bottom;
                    }
                    
                    if (paddle.Velocity.X != 0)
                    {// only set X velocity to paddle X velocity if paddle is moving
                        direction.X = paddle.Velocity.X;
                    }
                    paddleBounceSound.Play();


                }

                foreach (Brick brick in BrickMap.listBricks)
                {
                    if (collisionBox.Intersects(brick.Rect))
                    {
                        _brick = brick;
                        hasCollided = true;
                        break;
                    }
                }

                if (hasCollided && collisionBox.Intersects(_brick.Rect))
                {
                    if ((collisionBox.Top <= _brick.Rect.Bottom) && (prevPosition.Y <= _brick.Rect.Bottom))
                    { // above collision
                        BounceUp();
                        position.Y = _brick.Rect.Top - collisionBox.Height;

                    }
                    if ((collisionBox.Bottom >= _brick.Rect.Top) && (prevPosition.Y >= _brick.Rect.Top))
                    { // under collision
                        BounceDown();
                        position.Y = _brick.Rect.Bottom;
                    }
                    _brick.Weaken();
                    hasCollided = false;
                }
                // Y End

                prevPosition = position;
            }
            else // darken ball if not active, and make it hover above paddle
            {
                position.X = paddle.position.X + 14;
                position.Y = paddle.position.Y - 6;
                color = Color.DarkGray;
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
