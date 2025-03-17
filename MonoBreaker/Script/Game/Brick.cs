using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;
using System.Linq.Expressions;

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
        private Color color = new Color(colorVal, colorVal, colorVal);
        private SoundEffect brickWeaken = GetContent.GetSound("brickBreak");
        private SoundEffect brickBreak = GetContent.GetSound("brickDestroy");
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
            strength -= Playing.ball.BallStrength;
            color = Color.DarkGray;
            brickWeaken.Play();
        }

        public void Break() 
        {
            if (isActive)
            {
                brickBreak.Play();
                Playing.score++;
            }
            
            isActive = false;
            Rect = Rectangle.Empty;
           
        }

        public void Update()
        {
            if (strength <= 0)
            {
                Break();
            }
        }

        public void Reset() 
        {
            if (!isActive && Rect.IsEmpty) 
            {
                isActive = true;
                Rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
            }
        }

        public void Draw(SpriteBatch window) 
        {
            if(isActive)
                window.Draw(this.image, Rect, color);
        }
    }
}
