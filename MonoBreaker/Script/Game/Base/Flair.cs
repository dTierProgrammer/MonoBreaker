using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Game.Base
{
    public class Flair
    {
        private Texture2D image;
        private Vector2 position;
        private Rectangle Rect;
        private bool isActive = true;

        private const float timeAnimated = 1;
        private float timeLeft = timeAnimated;

        public Flair(Texture2D image, Vector2 position) 
        {
            this.image = image;
            this.position = position;
            Rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
        }

        public void Update(GameTime gameTime) 
        {
            Rect.X = (int)position.X;
            Rect.Y = (int)position.Y;
            if (isActive) 
            {
                position.Y -= .35f;
                var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

                timeLeft -= timer;

                if(timeLeft <= 0)
                {
                    Rect = Rectangle.Empty;
                    isActive = false;
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            if (isActive)
                spriteBatch.Draw(this.image, Rect, Color.White);
        }
    }
}
