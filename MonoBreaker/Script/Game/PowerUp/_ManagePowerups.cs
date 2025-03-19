using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.Base;

namespace MonoBreaker.Script.Game.PowerUp;

public static class _ManagePowerups
{ // i got tears in my eyes nigga this shit ASS
    public static List<AddTry> listAddTry = new List<AddTry>();
    public static List<DeathBounce> listDeathBounce = new List<DeathBounce>();
    public static List<Lovely> listLovely = new List<Lovely>();
    public static List<MultiBall> listMultiBall = new List<MultiBall>();
    public static List<PaddleExtend> listPaddleExtend = new List<PaddleExtend>();
    public static List<Piercing> listPiercing = new List<Piercing>();
    public static List<Shooting> listShooting = new List<Shooting>();
    public static List<SuperBall> listSuperBall = new List<SuperBall>();
    
    private static List<List<string>> allPowerups = new List<List<string>>();

    public static List<Flair> powerupFlairs = new List<Flair>();
    
    public static void Update(GameTime gameTime) // holy shit code
    {
        foreach (AddTry item in listAddTry)
        {
            item.Update(gameTime);
        }

        foreach (DeathBounce item in listDeathBounce)
        {
            item.Update();
        }

        foreach (Lovely item in listLovely)
        {
            item.Update();
        }

        foreach (MultiBall item in listMultiBall)
        {
            item.Update();
        }

        foreach (PaddleExtend item in listPaddleExtend)
        {
            item.Update();
        }

        foreach (Piercing item in listPiercing)
        {
            item.Update();
        }

        foreach (Shooting item in listShooting)
        {
            item.Update();
        }

        foreach (SuperBall item in listSuperBall)
        {
            item.Update();
        }

        foreach (Flair item in powerupFlairs) 
        {
            item.Update(gameTime);
        }
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (AddTry item in listAddTry)
        {
            item.Draw(spriteBatch);
        }

        foreach (DeathBounce item in listDeathBounce)
        {
            item.Draw(spriteBatch);
        }

        foreach (Lovely item in listLovely)
        {
            item.Draw(spriteBatch);
        }

        foreach (MultiBall item in listMultiBall)
        {
            item.Draw(spriteBatch);
        }

        foreach (PaddleExtend item in listPaddleExtend)
        {
            item.Draw(spriteBatch);
        }

        foreach (Piercing item in listPiercing)
        {
            item.Draw(spriteBatch);
        }

        foreach (Shooting item in listShooting)
        {
            item.Draw(spriteBatch);
        }

        foreach (SuperBall item in listSuperBall)
        {
            item.Draw(spriteBatch);
        }

        foreach (Flair item in powerupFlairs)
        {
            item.Draw(spriteBatch);
        }
    }
}