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

        public static void RandomPowerup(Vector2 position)
        {
            _Powerups nextPowerup = (_Powerups)rng.Next(0, powerups.Length);
            
            switch(nextPowerup)
            {
                case _Powerups.ADDTRY:
                    _ManagePowerups.listAddTry.Add(new AddTry(AddTry.Img, position, AddTry.flairImg));
                    break;
                /*case _Powerups.LOVELY:
                    _ManagePowerups.listLovely.Add(new Lovely(loveImg, position));
                    break;*/
                case _Powerups.PIERCING:
                    _ManagePowerups.listPiercing.Add(new Piercing(Piercing.Img, position, Piercing.flairImg));
                    break;
                case _Powerups.SHOOTING:
                    _ManagePowerups.listShooting.Add(new Shooting(Shooting.Img, position, Shooting.flairImg));
                    break;
                case _Powerups.SUPERBALL:
                    _ManagePowerups.listSuperBall.Add(new SuperBall(SuperBall.Img, position, SuperBall.flairImg));
                    break;
                case _Powerups.DEATHBOUNCE:
                    _ManagePowerups.listDeathBounce.Add(new DeathBounce(DeathBounce.Img, position, DeathBounce.flairImg));
                    break;
                case _Powerups.PADDLEEXTEND:
                    _ManagePowerups.listPaddleExtend.Add(new PaddleExtend(PaddleExtend.Img, position, PaddleExtend.flairImg));
                    break;
                case _Powerups.MULTIBALL:
                    _ManagePowerups.listMultiBall.Add(new MultiBall(MultiBall.Img, position, MultiBall.flairImg));
                    break;
                case _Powerups.TWINPADDLE:
                    _ManagePowerups.listTwinPaddle.Add(new TwinPaddle(TwinPaddle.Img, position, TwinPaddle.flairImg));
                    break;
                case _Powerups.BALLGUN:
                    _ManagePowerups.listBallGun.Add(new BallGun(BallGun.Img, position, BallGun.flairImg));
                    break;
                case _Powerups.GLUEBALL:
                    _ManagePowerups.listGlueBall.Add(new GlueBall(GlueBall.Img, position, GlueBall.flairImg));
                    break;
            }
        }

        public static void NewAddTry(Vector2 position)
        {
            _ManagePowerups.listAddTry.Add(new AddTry(AddTry.Img, position, AddTry.flairImg));
        }
        public static void NewLovely(Vector2 position)
        {
            _ManagePowerups.listLovely.Add(new Lovely(Lovely.Img, position, Lovely.flairImg));
        }
        public static void NewPiercing(Vector2 position)
        {
            _ManagePowerups.listPiercing.Add(new Piercing(Piercing.Img, position, Piercing.flairImg));
        }
        public static void NewShooting(Vector2 position)
        {
            _ManagePowerups.listShooting.Add(new Shooting(Shooting.Img, position, Shooting.flairImg));
        }
        public static void NewSuperBall(Vector2 position)
        {
            _ManagePowerups.listSuperBall.Add(new SuperBall(SuperBall.Img, position, SuperBall.flairImg));
        }
        public static void NewDeathBounce(Vector2 position)
        {
            _ManagePowerups.listDeathBounce.Add(new DeathBounce(DeathBounce.Img, position, DeathBounce.flairImg));
        }
        public static void NewPaddleExtend(Vector2 position)
        {
            _ManagePowerups.listPaddleExtend.Add(new PaddleExtend(PaddleExtend.Img, position, PaddleExtend.flairImg));
        }
        public static void NewMultiBall(Vector2 position)
        {
            _ManagePowerups.listMultiBall.Add(new MultiBall(MultiBall.Img, position, MultiBall.flairImg));
        }
        public static void NewTwinPaddle(Vector2 position)
        {
            _ManagePowerups.listTwinPaddle.Add(new TwinPaddle(TwinPaddle.Img, position, TwinPaddle.flairImg));
        }
        public static void NewBallGun(Vector2 position)
        {
            _ManagePowerups.listBallGun.Add(new BallGun(BallGun.Img, position, BallGun.flairImg));
        }
        public static void NewGlueBall(Vector2 position)
        {
            _ManagePowerups.listGlueBall.Add(new GlueBall(GlueBall.Img, position, GlueBall.flairImg));
        }
    }
}
