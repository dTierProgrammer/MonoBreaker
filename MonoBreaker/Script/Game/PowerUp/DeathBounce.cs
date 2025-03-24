using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;
using MonoBreaker.Script.Game.Base;

namespace MonoBreaker.Script.Game.PowerUp;

public class DeathBounce:Powerup
{
    private static string name = "deathbounce";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");
    public DeathBounce(Texture2D image, Vector2 position, Texture2D flair):base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }

    public override void Action() 
    {
        Playing.ball.BallHealth = 4;
        Playing.score += 200;
        Playing.powerUpSound.Play();
        base.Action();
    }
}