using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class DeathBounce
{
    private Texture2D image = GetContent.GetTexture("Game/powerup/deathbounce");
    private Vector2 position;
    private bool isActive = true;
    private Rectangle collisionBox;

    public DeathBounce(Vector2 position)
    {
        this.position = position;
        collisionBox =  new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
    }

    public void Update()
    {
        if (isActive)
            collisionBox.Y += 2;
        
        if (collisionBox.Intersects(Playing.player.collisionBox))
        {
            Playing.ball.BallHealth = 4;
            Playing.score += 100;
            collisionBox = Rectangle.Empty;
            Playing.powerUpSound.Play();
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