using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.PowerUp;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;
using System;

namespace MonoBreaker.Script.Game.Base;

public class Powerup
{
    public Texture2D image;
    public Vector2 position;
    public Rectangle collisionBox;
    public bool isActive{ set; get; }

    public Texture2D flair;

    public Powerup(Texture2D image, Vector2 position, Texture2D flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        collisionBox = new Rectangle((int)this.position.X, (int)this.position.Y, image.Width, image.Height);
    }
    
    public Powerup(Vector2 position)
    {
        this.position = position;
        collisionBox = new Rectangle((int)this.position.X, (int)this.position.Y, image.Width, image.Height); // lmao nigga idc
    }

    
    public void AnimateFlair()
    {
        _ManagePowerups.powerupFlairs.Add(new Flair(flair, new Vector2(Playing.player.collisionBox.Center.X - (flair.Width / 2), Playing.player.position.Y - 15)));
    }

    public virtual void Action() 
    {
        AnimateFlair();
        Kill();
    }
    

    public void Kill()
    {
        isActive = false;
        collisionBox = Rectangle.Empty;
    }

    public virtual void Update()
    {
        // to override
        collisionBox.X = (int)position.X;
        collisionBox.Y = (int)position.Y;

        position.Y += .5f;
        if (isActive)
        {
            if (collisionBox.Intersects(Playing.player.collisionBox))
            {
                Action();
            }else if (collisionBox.Intersects(Playing.player.twinCollisionBox) && Playing.player.isTwinActive)
            {
                Action();
            }

            if (collisionBox.Intersects(Playing.screenBounds[3]))
            {
                Kill();
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if(isActive)
            spriteBatch.Draw(image, collisionBox, Color.White);
    }
}