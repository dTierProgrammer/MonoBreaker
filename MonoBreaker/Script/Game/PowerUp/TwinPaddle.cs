using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;

public class TwinPaddle:Powerup
{
    private static string name = "twinpaddle";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");
    
    public TwinPaddle(Texture2D image, Vector2 position, Texture2D flair): base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }

    public override void Action()
    {
        Playing.score += 100;
        if (!Playing.player.isTwinActive)
            Playing.player.isTwinActive = true;
        else
            Playing.player.twinTimeLeft += 5;
        Playing.powerUpSound.Play();
        base.Action();
    }
}