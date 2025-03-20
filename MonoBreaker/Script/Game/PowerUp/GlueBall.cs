using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;

namespace MonoBreaker.Script.Game.PowerUp;

public class GlueBall:Powerup
{
    public GlueBall(Texture2D image, Vector2 position, Texture2D flair) : base(image, position, flair)
    {
        this.image = image;
        this.position = position;
        this.flair = flair;
        isActive = true;
    }
}