using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game;

public class Bullet
{
    private Texture2D image = GetContent.GetTexture("Game/bullet");
    private Vector2 position;
    private Rectangle collisionBox;
    private bool isActive = true;
    private Brick _brick;
    private bool hasCollided;
    public Bullet(Vector2 position)
    {
        this.position = position;
        collisionBox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
    }

    public void Kill() 
    {
        isActive = false;
        collisionBox = Rectangle.Empty;
    }

    public void Update() 
    {
        if (isActive) 
        {
            collisionBox.Y -= 5;

            
            if (collisionBox.Intersects(Playing.screenBounds[2])) 
            {
                Kill();
            }
            
            foreach(Brick brick in BrickMap.listBricks) 
            {
                if (collisionBox.Intersects(brick.Rect)) 
                {
                    _brick = brick;
                    hasCollided = true;
                    break;
                }
                    

              
            }
            if (hasCollided && collisionBox.Intersects(_brick.Rect) )
            {
                if (_brick.BrickHealth > 1)
                {
                    Playing.score += 10;
                }else if (_brick.BrickHealth == 1)
                {
                    Playing.score += 100;
                }
                _brick.Weaken(1);
                Kill();
                hasCollided = false;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) 
    {
        if (isActive)
            spriteBatch.Draw(image, collisionBox, Color.White);
    }
}