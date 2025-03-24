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
            Playing.score = 0;
            Playing.round = 1;
            Playing.tries = 5;
            Playing.ball.Reset();
            Playing.otherBalls.Clear();
            Playing.player.ResetAll();
            _ManagePowerups.ClearLists();
            BrickMap.HardReset();
            Playing.brokenBricks = 0;
            Playing.ball.speed = Playing.startingGameSpeed;
            Playing.player.maxVelocity = Playing.startingGameSpeed;
            //BrickMap.listBricks.Clear();
            //Playing.Initialize(_game); // most likely will crash the game (it doesn't :o )
            //BrickMap.Initialize(_game); // most likely will crash the game
            //BrickMap.ResetMap();
        }

        public static void RoundReset() 
        {
            Playing.ball.RoundReset();
            Playing.player.RoundReset();
            Playing.otherBalls.Clear();
            _ManagePowerups.ClearLists();
            BrickMap.HardReset();
            Playing.brokenBricks = 0;
            //BrickMap.listBricks.Clear();
            //Playing.Initialize(_game);
        }
    }
}
