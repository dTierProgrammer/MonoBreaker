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
        private static Array commonPowerups = Enum.GetValues(typeof(_CommonPowerups));
        private static Array uncommonPowerups = Enum.GetValues(typeof(_UncommonPowerups));
        private static Array rarePowerups = Enum.GetValues(typeof(_RarePowerups));
        private static Array veryrarePowerups = Enum.GetValues(typeof(_VeryRarePowerups));
        private static int[] powerupGroupProbability = { 70, 90, 98, 100}; // lmao nigga this is wack
        private static Random rng = new Random();
        private static int chance;

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

        public static void CommonPowerup(Vector2 position)
        {
            _CommonPowerups nextPowerup = (_CommonPowerups)rng.Next(0, commonPowerups.Length);

            switch (nextPowerup)
            {
                case _CommonPowerups.SUPERBALL:
                    _ManagePowerups.listSuperBall.Add(new SuperBall(SuperBall.Img, position, SuperBall.flairImg));
                    break;
                case _CommonPowerups.TWINPADDLE:
                    _ManagePowerups.listTwinPaddle.Add(new TwinPaddle(TwinPaddle.Img, position, TwinPaddle.flairImg));
                    break;
                case _CommonPowerups.PADDLEEXTEND:
                    _ManagePowerups.listPaddleExtend.Add(new PaddleExtend(PaddleExtend.Img, position, PaddleExtend.flairImg));
                    break;
            }
        }

        public static void UncommonPowerup(Vector2 position)
        {
            _UncommonPowerups nextPowerup = (_UncommonPowerups)rng.Next(0, uncommonPowerups.Length);

            switch (nextPowerup)
            {
                case _UncommonPowerups.SHOOTING:
                    _ManagePowerups.listShooting.Add(new Shooting(Shooting.Img, position, Shooting.flairImg));
                    break;
                case _UncommonPowerups.MULTIBALL:
                    _ManagePowerups.listMultiBall.Add(new MultiBall(MultiBall.Img, position, MultiBall.flairImg));
                    break;
                case _UncommonPowerups.DEATHBOUNCE:
                    _ManagePowerups.listDeathBounce.Add(new DeathBounce(DeathBounce.Img, position, DeathBounce.flairImg));
                    break;
            }
        }

        public static void RarePowerup(Vector2 position)
        {
            _RarePowerups nextPowerup = (_RarePowerups)rng.Next(0, rarePowerups.Length);

            switch (nextPowerup)
            {
                case _RarePowerups.BALLGUN:
                    _ManagePowerups.listBallGun.Add(new BallGun(BallGun.Img, position, BallGun.flairImg));
                    break;
                case _RarePowerups.GLUEBALL:
                    _ManagePowerups.listGlueBall.Add(new GlueBall(GlueBall.Img, position, GlueBall.flairImg));
                    break;
                case _RarePowerups.PIERCING:
                    _ManagePowerups.listPiercing.Add(new Piercing(Piercing.Img, position, Piercing.flairImg));
                    break;
            }
        }

        public static void VeryRarePowerup(Vector2 position)
        {
            _VeryRarePowerups nextPowerup = (_VeryRarePowerups)rng.Next(0, veryrarePowerups.Length);

            switch (nextPowerup)
            {
                case _VeryRarePowerups.ADDTRY:
                    _ManagePowerups.listAddTry.Add(new AddTry(AddTry.Img, position, AddTry.flairImg));
                    break;
                case _VeryRarePowerups.LOVELY:
                    _ManagePowerups.listLovely.Add(new Lovely(Lovely.Img, position, Lovely.flairImg));
                    break;
            }
        }
        
        public static void RollPowerup(Vector2 position)
        {
            chance = rng.Next(1, 101);
            Console.WriteLine(chance);
            if (chance <= powerupGroupProbability[0]) // common
            {
                CommonPowerup(position);
            }else if (chance > powerupGroupProbability[0] && chance <= powerupGroupProbability[1]) // uncommon
            {
                UncommonPowerup(position);
            } else if (chance > powerupGroupProbability[1] && chance <= powerupGroupProbability[2]) // rare
            {
                RarePowerup(position);
            } else if (chance > powerupGroupProbability[2] && chance <= powerupGroupProbability[3]) // very rare
            {
                VeryRarePowerup(position);
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
