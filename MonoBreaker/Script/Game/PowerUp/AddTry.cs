using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class AddTry
{
    private Texture2D image = GetContent.GetTexture("Game/powerup/1up");
    private Vector2 position;
    private bool isActive = true;
    private Rectangle collisionBox;
    
    public AddTry(Vector2 position)
    {
        this.position = position;
        collisionBox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
    }

    public void Update()
    {
        if (isActive)
            collisionBox.Y += 2;
        
        if (collisionBox.Intersects(Playing.player.collisionBox))
        {
            Playing.tries++;
            collisionBox = Rectangle.Empty;
            Playing.addTrySound.Play();
            isActive = false;
        }

        if (collisionBox.Intersects(Playing.screenBounds[3]))
        {
            collisionBox = Rectangle.Empty;
            isActive = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(isActive)
            spriteBatch.Draw(image, collisionBox, Color.White);
    }
}