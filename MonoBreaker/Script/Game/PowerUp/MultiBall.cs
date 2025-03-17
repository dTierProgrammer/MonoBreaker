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
    private Random rng = new Random();
    private Vector2 spawnPos;
    public MultiBall(Texture2D image, Vector2 position): base(image, position)
    {
        this.image = image;
        this.position = position;
        isActive = true;
    }

    public override void Update()
    {
        spawnPos = new Vector2(Playing.player.position.X, Playing.player.position.Y);
        if (isActive)
        {
            collisionBox.Y += 2;
            
            if (collisionBox.Intersects(Playing.player.collisionBox))
            {
                for (int iteration = 0; iteration < rng.NextInt64(10, 10); iteration++)
                {
                    Playing.otherBalls.Add(new Ball(new Vector2(rng.NextInt64(10, 310), Playing.player.position.Y - 10), Playing.startingGameSpeed, false, 5));
                }
                Playing.powerUpSound.Play();
                Kill();
            }
            if (collisionBox.Intersects(Playing.screenBounds[3]))
            {
                Kill();
            }
        }
    }
}