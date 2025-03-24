using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using MonoBreaker.Script.Game.PowerUp;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

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

        private Random rng = new Random();
        private int chance;
        private int rollPowerup;
        private int prevVal;
        public int initStrength;
        
        public Brick(Texture2D image, Vector2 position, int toughness)
        {
            this.image = image;
            this.position = position;
            isActive = true;
            Rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.image.Width, this.image.Height);
            strength = toughness;
            initStrength = strength;
        }

        public int BrickHealth
        {
            get{return strength;}
        }

        public void Weaken(int diminishingValue)
        {
            strength -= diminishingValue;
            brickWeaken.Play();
        }

        public void Break() 
        {
            if (isActive)
            {
                BrickMap.inactiveBricks.Add(this);
                brickBreak.Play();
                Playing.brokenBricks++;
                chance = rng.Next(1, 101);
                if(chance < 15) // 10 % chance
                    _GeneratePowerup.RollPowerup(new Vector2(Rect.X + 2, Rect.Y  + 2));
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
            strength = initStrength;
        }

        public void Draw(SpriteBatch window) 
        {
            if(isActive)
                window.Draw(this.image, Rect, color);
        }
    }
}
