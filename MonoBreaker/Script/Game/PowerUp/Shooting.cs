using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class Shooting:Powerup
{
    private static string name = "shooting";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair_alt");
    public Shooting(Texture2D image, Vector2 position, Texture2D flair) :base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }

    public override void Action() 
    {
        if (!Playing.player.gunIsActive)
            Playing.player.gunIsActive = true;
        else
            Playing.player.gunTimeLeft += 5;
        Playing.powerUpSound.Play();
        base.Action();
    }
}