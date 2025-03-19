using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene.GameScenes;
using MonoBreaker.Script.Game.Base;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.CompilerServices;

namespace MonoBreaker.Script.Game.PowerUp;

public class AddTry:Powerup
{
    private static string name = "1up";
    public static readonly Texture2D Img = GetContent.GetTexture($"Game/powerup/{name}");
    public static readonly Texture2D flairImg = GetContent.GetTexture($"Game/powerup/flair/{name}_flair");
    public AddTry(Texture2D image, Vector2 position, Texture2D flair):base(image, position, flair)
    {
        this.position = position;
        this.image = image;
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
                Playing.tries++;
                Playing.score += 100;
                Playing.addTrySound.Play();
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

    public override void Draw(SpriteBatch spriteBatch) 
    {
        base.Draw(spriteBatch);
    }
}