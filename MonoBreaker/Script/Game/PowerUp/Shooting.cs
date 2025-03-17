using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class Shooting:Powerup
{
    public Shooting(Texture2D image, Vector2 position) :base(image, position)
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
            Playing.player.Ammo = 35;
            Playing.powerUpSound.Play();
            Kill();
        }

        if (collisionBox.Intersects(Playing.screenBounds[3])) 
        {
            Kill();
        }
    }
}