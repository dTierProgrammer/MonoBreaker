using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class Piercing:Powerup
{
    private static string name = "piercing";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");
    public Piercing(Texture2D image, Vector2 position, Texture2D flair) : base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }

    public override void Update()
    {
        if (isActive) 
        {
            position.Y += .5f;
            if (collisionBox.Intersects(Playing.player.collisionBox))
            {
                Playing.score += 100;
                Playing.powerUpSound.Play();
                Playing.ball.Piercing = true;
                AnimateFlair();
                Kill();
            }
            if (collisionBox.Intersects(Playing.screenBounds[3]))
            {
                Kill();
            }
        }
        base.Update();
    }
}
