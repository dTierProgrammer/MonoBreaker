using System;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker.Script.Game.PowerUp;
using MonoBreaker.Script.Game.Base;

public class MultiBall:Powerup
{
    private static string name = "multiball";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");

    private static Random rng = new Random();
    private Vector2 spawnPos;
    long cap;
    public MultiBall(Texture2D image, Vector2 position, Texture2D flair): base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }

    public override void Action() 
    {
        cap = rng.NextInt64(5, 10);
        for (int iteration = 0; iteration < cap; iteration++)
        {
            Playing.otherBalls.Add(new Ball(new Vector2(rng.NextInt64(8, Game1.trueScreenWidth - 8), rng.NextInt64(200, (long)Playing.player.position.Y - 10)), Playing.startingGameSpeed, false, 5));
        }
        Playing.score += 200;
        Playing.powerUpSound.Play();
        base.Action();
    }
}