using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Game.PowerUp;
using MonoBreaker.Script.Global;

namespace MonoBreaker.Script.Game.PowerUp
{
    
    
    public static class _GeneratePowerup
    {
        private static Array powerups = Enum.GetValues(typeof(_Powerups));
        private static Random rng = new Random();
        
        private static Texture2D multiballImg = GetContent.GetTexture("Game/powerup/multiball"); // it's because I'm stupid
        private static Texture2D superballImg = GetContent.GetTexture("Game/powerup/superball");
        private static Texture2D shootingImg = GetContent.GetTexture("Game/powerup/shooting");
        private static Texture2D piercingImg = GetContent.GetTexture("Game/powerup/piercing");
        private static Texture2D deathbounceImg = GetContent.GetTexture("Game/powerup/deathbounce");
        private static Texture2D paddleextendImg = GetContent.GetTexture("Game/powerup/paddleextend");
        private static Texture2D addtryImg = GetContent.GetTexture("Game/powerup/1up");
        private static Texture2D loveImg = GetContent.GetTexture("Game/powerup/love");

        public static void RandomPowerup(Vector2 position)
        {
            _Powerups nextPowerup = (_Powerups)rng.Next(0, powerups.Length);
            
            switch(nextPowerup)
            {
                case _Powerups.ADDTRY:
                    _ManagePowerups.listAddTry.Add(new AddTry(position));
                    break;
                case _Powerups.LOVELY:
                    _ManagePowerups.listLovely.Add(new Lovely(loveImg, position));
                    break;
                case _Powerups.PIERCING:
                    _ManagePowerups.listPiercing.Add(new Piercing(piercingImg, position));
                    break;
                case _Powerups.SHOOTING:
                    _ManagePowerups.listShooting.Add(new Shooting(shootingImg, position));
                    break;
                case _Powerups.SUPERBALL:
                    _ManagePowerups.listSuperBall.Add(new SuperBall(superballImg, position));
                    break;
                case _Powerups.DEATHBOUNCE:
                    _ManagePowerups.listDeathBounce.Add(new DeathBounce(position));
                    break;
                case _Powerups.PADDLEEXTEND:
                    _ManagePowerups.listPaddleExtend.Add(new PaddleExtend(paddleextendImg, position));
                    break;
            }
        }

        public static void NewAddTry(Vector2 position)
        {
            _ManagePowerups.listAddTry.Add(new AddTry(position));
        }
        public static void NewLovely(Vector2 position)
        {
            _ManagePowerups.listLovely.Add(new Lovely(loveImg, position));
        }
        public static void NewPiercing(Vector2 position)
        {
            _ManagePowerups.listPiercing.Add(new Piercing(piercingImg, position));
        }
        public static void NewShooting(Vector2 position)
        {
            _ManagePowerups.listShooting.Add(new Shooting(shootingImg, position));
        }
        public static void NewSuperBall(Vector2 position)
        {
            _ManagePowerups.listSuperBall.Add(new SuperBall(superballImg, position));
        }
        public static void NewDeathBounce(Vector2 position)
        {
            _ManagePowerups.listDeathBounce.Add(new DeathBounce(position));
        }
        public static void NewPaddleExtend(Vector2 position)
        {
            _ManagePowerups.listPaddleExtend.Add(new PaddleExtend(paddleextendImg, position));
        }
    }
}
