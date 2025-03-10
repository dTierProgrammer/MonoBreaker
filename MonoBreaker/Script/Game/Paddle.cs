using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoBreaker.Script.Game
{
    public class Paddle:Sprite
    {
        public Boolean[] isMoving = new bool[4]; // 0-Right, 1-Left, 2-Up, 3-Down

        private Vector2 velocity = Vector2.Zero;
        public float maxVelocity;
        private const float acceleration = .35f;
        private const float friction = acceleration * .5f;
        private const float tolerance = friction * .9f;

        private Rectangle[] playerBoundaries = new Rectangle[4];

        private int score;
        public Paddle(Texture2D image, Vector2 position, float moveSpeed, Rectangle[] playerBoundaries) : base(image, position) 
        {
            this.image = image;
            this.position = position;
            maxVelocity = moveSpeed;
            this.playerBoundaries = playerBoundaries;
        }

        public int Score
        {
            set { score = value; }
            get { return score; }
        }

        public Vector2 Velocity 
        {
            get { return velocity; }
        }

        public void Update() 
        {
            if (isMoving[0]) // Right
            {
                velocity.X = (velocity.X + acceleration);
                velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            }
            if (isMoving[1]) // Left
            {
                velocity.X = (velocity.X - acceleration);
                velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            }
            if (!isMoving[0] || !isMoving[1]) 
            {
                velocity.X += -Math.Sign(velocity.X) * friction;
                if (Math.Abs(velocity.X) <= tolerance) 
                {
                    velocity.X = 0;
                }
            }
            this.position.X += velocity.X;

            if (collisionBox.Intersects(playerBoundaries[0])) 
            {
                velocity.X = 0;
                if (position.X <= playerBoundaries[0].Right) 
                {
                    position.X = playerBoundaries[0].Right;
                }
            }

            if (collisionBox.Intersects(playerBoundaries[1])) 
            {
                velocity.X = 0;
                if (position.X >= playerBoundaries[1].Left - this.image.Width) 
                {
                    position.X = playerBoundaries[1].Left - this.image.Width;
                }
            }
        }

        public void Draw(SpriteBatch window)
        {
            window.Draw(this.image, collisionBox, Color.White);
        }
    }
}
