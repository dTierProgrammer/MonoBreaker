using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Scene.GameScenes;
using System;
using System.Diagnostics;

namespace MonoBreaker.Script.Game.PowerUp;

public class SuperBall:Powerup
{
    private const float delay = 10;
    private float delayRemainder = delay;

    public SuperBall(Texture2D image, Vector2 position):base(image, position) 
    {
        this.image = image;
        this.position = position;
        isActive = true;
    }

    public override void Update(GameTime gameTime)
    {
        if (isActive) 
        {
            collisionBox.Y += 2;

            if (collisionBox.Intersects(Playing.player.collisionBox))
            {
                Playing.score += 100;
                Playing.ball.SuperBall = true;
                Playing.powerUpSound.Play();
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