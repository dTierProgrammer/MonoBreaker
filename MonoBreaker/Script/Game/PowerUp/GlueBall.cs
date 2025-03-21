using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class GlueBall:Powerup
{
    private static string name = "glueball";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");
    public GlueBall(Texture2D image, Vector2 position, Texture2D flair) : base(image, position, flair)
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
                Playing.ball.GlueBall = true;
                Playing.score += 100;
                Playing.powerUpSound.Play();
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