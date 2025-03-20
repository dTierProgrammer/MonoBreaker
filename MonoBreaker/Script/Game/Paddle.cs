using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using MonoBreaker.Script.Global;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game
{
    public class Paddle:Sprite
    {
        public bool[] isMoving = new bool[2]; // 0-Right, 1-Left

        private Vector2 velocity = Vector2.Zero;
        public float maxVelocity;
        private static float acceleration = .35f;
        private static float friction = acceleration * .5f;
        private static float tolerance = friction * .9f;
        private SoundEffect speedUpSound = GetContent.GetSound("speedUp");
        private SoundEffect bulletShootSound = GetContent.GetSound("bulletshoot");
        private SoundEffect powerDownSound = GetContent.GetSound("down");

        public static List<Bullet> bullets = new List<Bullet>();

        public bool isSuper = false;
        public bool canShoot = false;
        private int ammo = 0;
        public bool isTwinActive = false;
        public Vector2 twinPosition;
        //public Rectangle twinCollisionBox;
        private int offset = 144;
        int prevValue;

        private const float delay = 10;
        private float delayRemainder = delay;
        private float timeLeftSuper = delay;
        private float twinTimeLeft = delay;

        public float distFromCenter { get; private set; }
        public float collBoxOriginDistFromCenter { get; private set; }
        private int score;
        public Paddle(Vector2 position, float moveSpeed) : base(position) 
        {
            image = GetContent.GetTexture("Game/paddle");
            this.position = position;
            twinPosition = new Vector2(this.position.X, this.position.Y);
            maxVelocity = moveSpeed;
        }

        public bool SuperPaddle 
        {
            set { isSuper = value; }
            get { return isSuper; }
        }
        
        public int Ammo 
        {
            set { ammo = value; }
            get { return ammo; }
        }
        
        public void ShootBullet() 
        {
            if(ammo > 0 && canShoot) 
            {
                bulletShootSound.Play();
                ammo -= 1;
                bullets.Add(new Bullet(new Vector2(collisionBox.Left, collisionBox.Top - 3)));
                bullets.Add(new Bullet(new Vector2(collisionBox.Right - 3, collisionBox.Top - 3)));
                if (isTwinActive)
                {
                    bullets.Add(new Bullet(new Vector2(twinCollisionBox.Left, twinCollisionBox.Top - 3)));
                    bullets.Add(new Bullet(new Vector2(twinCollisionBox.Right - 3, twinCollisionBox.Top - 3)));
                }
            } 
        }
        
        
        public Rectangle twinCollisionBox
        {
            get { return new Rectangle(-(int)distFromCenter + offset, (int)twinPosition.Y, image.Width, image.Height);}
        }
        

        public Vector2 Velocity 
        {
            get { return velocity; }
        }

        public void Update(GameTime gameTime) 
        {
            distFromCenter = collisionBox.Center.X - Playing.centerPt.X;
            collBoxOriginDistFromCenter = collisionBox.X - Playing.centerPt.X;
            
            if (isSuper)
            {
                image = GetContent.GetTexture("Game/paddleSuper");
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timeLeftSuper -= timer;
                if(isTwinActive)
                    offset = 136;
                
                if (timeLeftSuper <= 0)
                {
                    offset = 144;
                    powerDownSound.Play();
                    image = GetContent.GetTexture("Game/paddle");
                    isSuper = false;
                    timeLeftSuper = delay;
                }
            }

            if (isTwinActive)
            {
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                twinTimeLeft -= timer;

                if (twinTimeLeft <= 0)
                {
                    powerDownSound.Play();
                    isTwinActive = false;
                }
            }

            if (isMoving[0]) // Right
            {
                isMoving[1] = false;
                velocity.X = (velocity.X + acceleration);
                velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            }
            if (isMoving[1]) // Left
            {
                isMoving[0] = false;
                velocity.X = (velocity.X - acceleration);
                velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            }
            if (!isMoving[0] || !isMoving[1]) // neither
            {
                velocity.X += -Math.Sign(velocity.X) * friction;
                if (Math.Abs(velocity.X) <= tolerance) 
                {
                    velocity.X = 0;
                }
            }
            this.position.X += velocity.X;

            if (collisionBox.Intersects(Playing.screenBounds[0])) // collide with left wall
            {
                velocity.X = 0;
                if (position.X <= Playing.screenBounds[0].Right) 
                {
                    position.X = Playing.screenBounds[0].Right;
                }
            }

            if (collisionBox.Intersects(Playing.screenBounds[1])) // colide with right wall
            {
                velocity.X = 0;
                if (position.X >= Playing.screenBounds[1].Left - this.image.Width) 
                {
                    position.X = Playing.screenBounds[1].Left - this.image.Width;
                }
            }
            if (Playing.brokenBricks % Playing.speedUpThreshold == 0 && Playing.brokenBricks != 0 && Playing.brokenBricks != prevValue) // speed up if score reaches specific value
            {
                maxVelocity += Playing.speedIncrement;
                acceleration += Playing.speedIncrement;
                speedUpSound.Play();
            }
            prevValue = Playing.brokenBricks;

            foreach(Bullet bullet in bullets) 
            {
                bullet.Update();
            }
        }

        public void Draw(SpriteBatch window)
        {
            if(isTwinActive)
                window.Draw(this.image, twinCollisionBox, Color.DarkGray);
            window.Draw(this.image, collisionBox, Color.White);
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(window);
            }
        }
    }
}
