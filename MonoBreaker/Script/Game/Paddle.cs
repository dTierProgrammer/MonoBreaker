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

        private bool isSuper = false;
        private int ammo = 0;
        
        int prevScore;

        private int score;
        public Paddle(Vector2 position, float moveSpeed) : base(position) 
        {
            image = GetContent.GetTexture("Game/paddle");
            this.position = position;
            maxVelocity = moveSpeed;
        }

        public int Ammo 
        {
            set { ammo = value; }
            get { return ammo; }
        }

        public void ShootBullet() 
        {
            if(ammo > 0) 
            {
                bulletShootSound.Play();
                ammo -= 1;
                Playing.bullets.Add(new Bullet(new Vector2(collisionBox.Left, collisionBox.Top - 3)));
                Playing.bullets.Add(new Bullet(new Vector2(collisionBox.Right - 3, collisionBox.Top - 3)));
            } 
        }

        public Vector2 Velocity 
        {
            get { return velocity; }
        }

        public void Update() 
        {
            if (isSuper)
                image = GetContent.GetTexture("Game/paddleSuper");
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
            if (Playing.score % Playing.speedUpThreshold == 0 && Playing.score != 0 && Playing.score != prevScore) // speed up if score reaches specific value
            {
                maxVelocity += Playing.speedIncrement;
                acceleration += Playing.speedIncrement;
                speedUpSound.Play();
            }
            prevScore = Playing.score;
        }

        public void Draw(SpriteBatch window)
        {
            window.Draw(this.image, collisionBox, Color.White);
        }
    }
}
