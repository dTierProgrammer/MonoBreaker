using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class BallGun:Powerup
{
    private static string name = "ballshooting";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");
    public BallGun(Texture2D image, Vector2 position, Texture2D flair) : base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }

    public override void Action() 
    {
        if (!Playing.player.ballGunIsActive)
            Playing.player.ballGunIsActive = true;
        else
            Playing.player.ballGunTimeLeft += 5;
        Playing.score += 100;
        Playing.powerUpSound.Play();
        base.Action();
    }
}