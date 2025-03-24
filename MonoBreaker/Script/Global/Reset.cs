using MonoBreaker.Script.Game;
using MonoBreaker.Script.Game.PowerUp;
using MonoBreaker.Script.Scene.GameScenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Global
{
    public static class Reset
    {
        private static Game1 _game;
        public static void Ininitialize(Game1 game) 
        {
            _game = game;
        }
        public static void HardReset() 
        {
            BrickMap.mapID = 0;
            Playing.score = 0;
            Playing.round = 0;
            Playing.tries = 5;
            Playing.ball.ResetSpeed();
            Playing.player.ResetSpeed();
            Playing.ball.Reset();
            Playing.otherBalls.Clear();
            Playing.player.ResetAll();
            _ManagePowerups.ClearLists();
            BrickMap.HardReset();
            Playing.brokenBricks = 0;
            Playing.ball.speed = Playing.startingGameSpeed;
            Playing.player.maxVelocity = Playing.startingGameSpeed;
        }

        public static void RoundReset()
        {
            Playing.ball.RoundReset();
            Playing.player.RoundReset();
            Playing.otherBalls.Clear();
            _ManagePowerups.ClearLists();
            BrickMap.RoundReset();
            Playing.brokenBricks = 0;
        }

        public static void SpeedReset()
        {
            Playing.ball.ResetSpeed();
            Playing.player.ResetSpeed();
        }
    }
}