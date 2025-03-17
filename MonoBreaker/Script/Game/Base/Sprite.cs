using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Game.Base
{
    public class Sprite
    {
        public Texture2D image;
        public Vector2 position;

        public Sprite(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
        }

        public Sprite(Vector2 position)
        {
            this.position = position;
        }
        
        public virtual Rectangle collisionBox
        {
            get
            {
                return new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
            }
        }
    }
}
