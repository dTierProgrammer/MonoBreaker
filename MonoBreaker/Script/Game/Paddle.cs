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

        private Rectangle[] playerBoundaries = new Rectangle[4];
        int prevScore;

        private int score;
        public Paddle(Texture2D image, Vector2 position, float moveSpeed, Rectangle[] playerBoundaries) : base(image, position) 
        {
            this.image = image;
            this.position = position;
            maxVelocity = moveSpeed;
            this.playerBoundaries = playerBoundaries;
        }

        public Vector2 Velocity 
        {
            get { return velocity; }
        }

        public void Update() 
        {
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

            if (collisionBox.Intersects(playerBoundaries[0])) // collide with left wall
            {
                velocity.X = 0;
                if (position.X <= playerBoundaries[0].Right) 
                {
                    position.X = playerBoundaries[0].Right;
                }
            }

            if (collisionBox.Intersects(playerBoundaries[1])) // colide with right wall
            {
                velocity.X = 0;
                if (position.X >= playerBoundaries[1].Left - this.image.Width) 
                {
                    position.X = playerBoundaries[1].Left - this.image.Width;
                }
            }
            if (Game1.score % Game1.speedUpThreshold == 0 && Game1.score != 0 && Game1.score != prevScore) // speed up if score reaches specific value
            {
                maxVelocity += Game1.speedIncrement;
                acceleration += Game1.speedIncrement;
                speedUpSound.Play();
            }
            prevScore = Game1.score;
        }

        public void Draw(SpriteBatch window)
        {
            window.Draw(this.image, collisionBox, Color.White);
        }
    }
}
