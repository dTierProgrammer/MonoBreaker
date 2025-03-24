using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;
using System;

namespace MonoBreaker.Script.Game.PowerUp;

public class AddPoints:Powerup
{
    Texture2D Img = GetContent.GetTexture("Gamme/powerup/Generic");
    Texture2D flairImg = GetContent.GetTexture("Gamme/powerup/flair/addpts");
    int weight;
    
    public AddPoints(Texture2D image, Vector2 position, Texture2D flair, int weight) : base(image, position, flair) 
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        this.weight = weight;
    }

    public override void Action()
    {
        switch (weight) 
        {
            case 1:
                Playing.score += 100;
                break;
            case 2:
                Playing.score += 200;
                break;
            case 3:
                Playing.score += 300;
                break;
            case 4:
                Playing.score += 400;
                break;
            case 5:
                Playing.score += 500;
                break;
            case 6:
                Playing.score += 600;
                break;
            case 7:
                Playing.score += 700;
                break;
            case 8:
                Playing.score += 800;
                break;
            case 9:
                Playing.score += 900;
                break;
            default:
                Playing.score += 50;
                break;
        }
    }

    public override void Draw(SpriteBatch sprieBatch)
    {

        
    }
}