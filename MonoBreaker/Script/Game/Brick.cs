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
    public class Brick: Sprite
    {
        public bool isActive;
        public Rectangle Rect;
        public Brick(Texture2D image, Vector2 position): base(image, position) 
        {
            this.image = image;
            this.position = position;
            isActive = true;
            Rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
        }

        public void Break() 
        {
            isActive = false;
            Rect = Rectangle.Empty;
        }

        public void Draw(SpriteBatch window) 
        {
            if(isActive)
                window.Draw(this.image, Rect, Color.White);
        }
    }
}
