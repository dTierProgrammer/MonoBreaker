using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp
{
    public class Lovely:Powerup
    {
        public Lovely(Texture2D image, Vector2 position): base(image, position) 
        {
            this.image = image;
            this.position = position;
            isActive = true;
        }

        public override void Update()
        {
            collisionBox.Y += 2;
            if (collisionBox.Intersects(Playing.player.collisionBox))
            {
                Playing.ball.random.Play();
                Playing.score += 1000;
                Playing.ball.Lovely = true;
                Kill();
            }
            if (collisionBox.Intersects(Playing.screenBounds[3]))
            {
                Kill();
            }
            base.Update();
        }
    }
}
