using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Game
{
    public static class BrickMap
    {

        static int imageIndex;
        private static Texture2D[] images = new Texture2D[4];

        private static int offset = 5;
        private static int[,] map =
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
            {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4}
        };
        private static Brick[,] bricks = new Brick[8, 31];

        private static Game1 _game;
        public static List<Brick> listBricks = new List<Brick>();

        public static void Initialize(Game1 game) 
        {
            _game = game;

            for (int index = 0; index < 4; index++) 
            {
                images[index] = GetContent.GetTexture($"Game/brick_pretty_{index}");
            }

            for (int column = 0; column < map.GetLength(0); column++)
            {
                for (int row = 0; row < map.GetLength(1); row++)
                {
                    if (map[column, row] == 1) 
                    {
                        listBricks.Add(new Brick(images[0], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6)));
                    }
                    if (map[column, row] == 2)
                    {
                        listBricks.Add(new Brick(images[1], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6)));
                    }
                    if (map[column, row] == 3)
                    {
                        listBricks.Add(new Brick(images[2], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6)));
                    }
                    if (map[column, row] == 4)
                    {
                        listBricks.Add(new Brick(images[3], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6)));
                    }
                }
            }
        }

        public static void Update() 
        {

            foreach (Brick brick in listBricks) 
            {
                if (_game.ball.collisionBox.Intersects(brick.Rect))
                {
                    brick.Break();
                    _game.ball.ReverseDirectionY();
                }
            }
        }

        public static void Draw(SpriteBatch window) 
        {
            
            foreach (Brick brick in listBricks) 
            {
                brick.Draw(window);
            }
            
        }
    }
}
