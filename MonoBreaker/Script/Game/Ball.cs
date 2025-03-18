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
using System.Security.Cryptography;

namespace MonoBreaker.Script.Game
{
    public class Ball: Sprite
    {
        private Random rng = new Random();
        private Vector2 direction =  Vector2.Zero;
        private Paddle paddle = Playing.player;
        private bool isActive = false;
        private bool isAnimated = true;
        private bool useOldHandling = false;
        private bool isMainBall;
        
        private bool canPierce = false;
        private bool isSuper = false;
        private bool isLovely = false;
        
        private int ballStrength = 1;
        private int ballHealth = 1;
        
        public float speed;
        int prevScore;
        private Color color = Color.DarkGray;
        private readonly SoundEffect bounceSound = GetContent.GetSound("bounce");
        private readonly SoundEffect paddleBounceSound = GetContent.GetSound("paddleBounce");
        private readonly SoundEffect ballLossSound = GetContent.GetSound("ballLoss");
        private readonly SoundEffect ballDownSound = GetContent.GetSound("down");
        private readonly SoundEffect ballLaunchSound = GetContent.GetSound("gameEnd");
        public readonly SoundEffect random = GetContent.GetSound("up");
        
        
        private Vector2 prevPosition;
        Brick _brick;
        private bool hasCollided = false;

        private const float delay = 10;
        private float delayRemainder = delay;

        public Ball(Vector2 position, float speed, bool isMainBall) : base(position) 
        {
            image = GetContent.GetTexture("Game/ball");
            this.position = position;
            this.speed = speed;
            ballHealth = 1;
            
            this.isMainBall = isMainBall;
            if(!isMainBall)
                direction = new Vector2(Math.Clamp(rng.NextSingle(), -speed, speed), -speed);
            else
            {
                direction = new Vector2(0, -speed);
            }
        }

        public Ball(Vector2 position, float speed, bool isMainBall, int ballHealth) : base(position)
        {
            image = GetContent.GetTexture("Game/ball");
            this.position = position;
            this.speed = speed;
            this.ballHealth = ballHealth;
            
            this.isMainBall = isMainBall;
            if(!isMainBall)
                direction = new Vector2(Math.Clamp(rng.NextSingle(), -speed, speed), -speed);
            else
            {
                direction = new Vector2(0, -speed);
            }
        }
        public Ball(Vector2 position, float speed, Vector2 direction, bool isMainBall, int ballHealth) : base(position)
        {
            image = GetContent.GetTexture("Game/ball");
            this.position = position;
            this.speed = speed;
            this.ballHealth = ballHealth;
            
            this.isMainBall = isMainBall;
            if(!isMainBall)
                this.direction = direction;
            else
            {
                direction = new Vector2(0, -speed);
            }
        }

        public int BallHealth
        {
            set{ballHealth=value;}
            get{return ballHealth;}
        }
        
        public int BallStrength
        {
            set{ballStrength=value;}
            get { return ballStrength; }
        }

        public bool IsActive
        {
            get { return isActive; }
        }

        public bool SuperBall 
        {
            set { isSuper = value; }
            get { return isSuper; }
        }

        public bool Lovely
        {
            set { isLovely = value; }
            get { return isLovely; }
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
            if(!isActive && isMainBall)
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

        public void Kill()
        {
            isAnimated = false;
        }

        public void Update(GameTime gameTime) 
        {
            if (isSuper)
            {
                image = GetContent.GetTexture("Game/ballSuper");
                ballStrength = 2;

                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                delayRemainder -= timer;

                if (delayRemainder <= 0)
                {
                    ballDownSound.Play();
                    image = GetContent.GetTexture("Game/ball");
                    ballStrength = 1;
                    isSuper = false;
                }

            }
            if (isLovely) 
            {
                image = GetContent.GetTexture("Test/test");
                canPierce = true;

                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                delayRemainder -= timer;

                if (delayRemainder <= 0)
                {
                    image = GetContent.GetTexture("Game/ball");
                    ballStrength = 1;
                    canPierce = false;
                }
            }

            if (canPierce) 
            {
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                delayRemainder -= timer;

                if (delayRemainder <= 0)
                {
                    ballDownSound.Play();
                    canPierce = false;
                }
            }
            
            if (!isMainBall)
            {
                isActive = true;
            }
            if (isMainBall && isActive)
                Playing.player.canShoot = true;
            if (isActive && isAnimated) // only move ball if active
            {
                // X Start
                position.X += direction.X;

                if (collisionBox.Intersects(Playing.screenBounds[0])) // collision detections have to be done to the right
                {//right bound
                    if (prevPosition.X < Playing.screenBounds[0].Right) // appears to work
                    {
                        position.X = Playing.screenBounds[0].Right;
                    }
                    BounceRight();
                    bounceSound.Play();
                }

                if (collisionBox.Intersects(Playing.screenBounds[1])) // collision detections have to be done to the left
                {// left bound
                    
                    if (prevPosition.X > Playing.screenBounds[1].Left) // appears to work
                    {
                        position.X = Playing.screenBounds[1].Left - collisionBox.Width;
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
                { // ball will only do collision calculations with one brick
                    if (collisionBox.Intersects(brick.Rect)) 
                    {
                        _brick = brick;
                        hasCollided = true;
                        break;
                    }
                }

                if (hasCollided && collisionBox.Intersects(_brick.Rect) ) 
                {
                    if ((collisionBox.Right >= _brick.Rect.Left) && (prevPosition.X >= _brick.Rect.Right) && !canPierce)
                    {// right side collision
                        
                        position.X = prevPosition.X;
                        BounceRight();
                    }
                    if ((collisionBox.Left <= _brick.Rect.Right) && (prevPosition.X <= _brick.Rect.Left) && !canPierce)
                    {// left side collision
                        
                        position.X = prevPosition.X;
                        BounceLeft();
                    }
                    if (canPierce)
                        _brick.Break();
                    else
                        _brick.Weaken(ballStrength);
                    hasCollided = false;
                }
                
                // X End

                // Y Start 
                position.Y += direction.Y;
                if (collisionBox.Intersects(Playing.screenBounds[2]))
                {// upper bound
                    if (prevPosition.Y > Playing.screenBounds[2].Bottom) 
                    {
                        position.Y = Playing.screenBounds[2].Bottom;
                    }
                    BounceDown();
                    bounceSound.Play();
                }
                if (collisionBox.Intersects(Playing.screenBounds[3]))
                {// lower bound / kill
                    if (isMainBall)
                    {
                        if (ballHealth > 1)
                        {
                            bounceSound.Play();
                            BounceUp();
                            ballHealth--;
                        }
                        else
                        {
                            ballLossSound.Play();
                            Playing.tries--;
                            Reset();
                        }
                    }
                    else
                    {if (ballHealth > 1)
                        {
                            bounceSound.Play();
                            BounceUp();
                            ballHealth--;
                        }
                        else
                        {
                            if(isAnimated)
                                ballDownSound.Play();
                            Kill();
                        }
                        position.Y = Playing.screenBounds[3].Top - collisionBox.Height;
                    }
                }
                if (collisionBox.Intersects(paddle.collisionBox)) 
                {
                    if (!useOldHandling)
                    {
                        if ((collisionBox.Top <= paddle.collisionBox.Bottom) && (prevPosition.Y <= paddle.collisionBox.Bottom))
                        { // up (new handling // ball angle is determined by where on the paddle it collides)
                            BounceUp();
                            position.Y = paddle.collisionBox.Top - collisionBox.Height;
                            direction.X = (collisionBox.Center.X - paddle.collisionBox.Center.X) / 10f;
                            Math.Clamp(direction.X, -speed, speed);
                        }
                    }
                    else
                    {
                        if ((collisionBox.Top <= paddle.collisionBox.Bottom) &&
                            (prevPosition.Y <= paddle.collisionBox.Bottom))
                        {
                            BounceUp();
                            position.Y = paddle.collisionBox.Top - collisionBox.Height;
                        }
                        if (paddle.Velocity.X != 0)
                        {// up (old handling // ball angle is set to the direction the paddle is moving, and it remains the same if paddle is stationary)
                            direction.X = paddle.Velocity.X;
                        }
                    }
                    
                    if ((collisionBox.Bottom >= paddle.collisionBox.Top) && (prevPosition.Y >= paddle.collisionBox.Top))
                    { // down (if the ball gets below the paddle)
                        BounceDown();
                        position.Y = paddle.collisionBox.Bottom;
                    }
                    
                    
                    paddleBounceSound.Play();
                }

                foreach (Brick brick in BrickMap.listBricks)
                {
                    if (collisionBox.Intersects(brick.Rect))
                    { // ball will only do collision calculations with one brick
                        _brick = brick;
                        hasCollided = true;
                        break;
                    }
                }

                if (hasCollided && collisionBox.Intersects(_brick.Rect))
                {
                    if ((collisionBox.Top <= _brick.Rect.Bottom) && (prevPosition.Y <= _brick.Rect.Bottom) && !canPierce)
                    { // above collision
                        BounceUp();
                        position.Y = _brick.Rect.Top - collisionBox.Height;

                    }
                    if ((collisionBox.Bottom >= _brick.Rect.Top) && (prevPosition.Y >= _brick.Rect.Top) && !canPierce)
                    { // under collision
                        BounceDown();
                        position.Y = _brick.Rect.Bottom;
                    }

                    if (canPierce)
                        _brick.Break();
                    else
                        _brick.Weaken(ballStrength);
                    hasCollided = false;
                }
                // Y End
                
                prevPosition = position;
            }
            else if(!isActive && isMainBall) // darken ball if not active, and make it hover above paddle
            {
                position.X = paddle.collisionBox.Center.X - (collisionBox.Width / 2f);
                position.Y = paddle.position.Y - collisionBox.Height - 2f;
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
            if(isAnimated)
                window.Draw(this.image, collisionBox, color);
        }
    }
}