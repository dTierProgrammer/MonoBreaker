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
    public class Brick
    {
        public bool isActive;
        public Rectangle Rect;
        private int strength;
        private Texture2D image;
        private Vector2 position;
        private static int colorVal = 255;
        Color color = new Color(colorVal, colorVal, colorVal);
        public Brick(Texture2D image, Vector2 position, int toughness)
        {
            this.image = image;
            this.position = position;
            isActive = true;
            Rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
            strength = toughness;
        }

        public void Weaken()
        {
            strength--;
            color = new Color(colorVal - 60, colorVal - 60, colorVal - 60);
        }

        public void Break() 
        {
            isActive = false;
            Rect = Rectangle.Empty;
            
        }

        public void Update()
        {
            if (strength <= 0)
            {
                if(isActive)
                    Game1.score++;
                Break();
            }
        }

        public void Draw(SpriteBatch window) 
        {
            if(isActive)
                window.Draw(this.image, Rect, color);
        }
    }
}
