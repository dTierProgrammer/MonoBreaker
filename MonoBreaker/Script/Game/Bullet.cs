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
    private bool isActive;
    private Brick _brick;
    public Bullet(Vector2 posititon)
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
            collisionBox.X += 2;
            if (collisionBox.Intersects(Playing.screenBounds[2])) 
            {
                Kill();
            }

            foreach(Brick brick in BrickMap.listBricks) 
            {
                if (collisionBox.Intersects(brick.Rect))
                    _brick = brick;
            }

            if (collisionBox.Intersects(_brick.Rect)) 
            {
                _brick.Weaken(1);
            }
        }
    }
}