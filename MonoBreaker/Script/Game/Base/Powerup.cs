using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;

namespace MonoBreaker.Script.Game.Base;

public class Powerup
{
    public Texture2D image;
    public Vector2 position;
    public Rectangle collisionBox;
    public bool isActive{ set; get; }

    public Powerup(Texture2D image, Vector2 position)
    {
        this.image = image;
        this.position = position;
        collisionBox = new Rectangle((int)this.position.X, (int)this.position.Y, image.Width, image.Height);
    }

    public void Kill()
    {
        isActive = false;
        collisionBox = Rectangle.Empty;
    }

    public virtual void Update()
    {
        // to override
    }
    public virtual void Update(GameTime gameTime)
    {
        // to override
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(isActive)
            spriteBatch.Draw(image, collisionBox, Color.White);
    }
}