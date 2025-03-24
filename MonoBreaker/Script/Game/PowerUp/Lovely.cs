using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp
{
    public class Lovely : Powerup
    {
        public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/love");
        public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/lovely_flair");
        public Lovely(Texture2D image, Vector2 position, Texture2D flair) : base(image, position, flair)
        {
            this.image = image;
            this.position = position;
            this.flair = flair;
            isActive = true;
        }

        public override void Action()
        {
            Playing.ball.random.Play();
            Playing.score += 5000;
            if (!Playing.ball.Lovely) 
            {
                Playing.ball.Lovely = true;
                Playing.score += 5000;
            }
            else 
            {
                Playing.score += 5000;
            }

                base.Action();
        }
    }
}
