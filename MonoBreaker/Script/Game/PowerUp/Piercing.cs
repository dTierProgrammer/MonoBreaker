using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class Piercing:Powerup
{
    public Piercing(Texture2D image, Vector2 position) : base(image, position)
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
            Playing.score += 100;
            Playing.powerUpSound.Play();
            Playing.ball.Piercing = true;
            Kill();
        }
        if (collisionBox.Intersects(Playing.screenBounds[3])) 
        {
            Kill();
        }
        base.Update();
    }
}
