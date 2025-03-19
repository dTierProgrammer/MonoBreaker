using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;
using System;
using System.Diagnostics;

namespace MonoBreaker.Script.Game.PowerUp;

public class SuperBall:Powerup
{
    private static string name = "superball";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");

    private const float delay = 10;
    private float delayRemainder = delay;

    public SuperBall(Texture2D image, Vector2 position, Texture2D flair):base(image, position, flair) 
    {
        this.image = image;
        this.position = position;
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
                Playing.ball.SuperBall = true;
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