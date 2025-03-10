using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Game
{
    public static class BrickMap
    {

        private static int imageIndex;
        private static Texture2D[] images = new Texture2D[4];
        private static int numOfBricks;
        private static int brokenBricks;
        
        private static int offset = 6;
        private static int[,] map =
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
           
            {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4},
            
            {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4}
        };
        
        private static Game1 _game;
        private static List<Brick> listBricks = new List<Brick>();

        public static void Initialize(Game1 game) 
        {
            _game = game;

            for (int index = 0; index < 4; index++) 
            {
                images[index] = GetContent.GetTexture($"Game/brick_final_{index}");
            }

            for (int column = 0; column < map.GetLength(0); column++)
            {
                for (int row = 0; row < map.GetLength(1); row++)
                {
                    if (map[column, row] == 1) 
                    {
                        listBricks.Add(new Brick(images[0], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 4));
                    }
                    if (map[column, row] == 2)
                    {
                        listBricks.Add(new Brick(images[1], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 3));
                    }
                    if (map[column, row] == 3)
                    {
                        listBricks.Add(new Brick(images[2], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 2));
                    }
                    if (map[column, row] == 4)
                    {
                        listBricks.Add(new Brick(images[3], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 1));
                    }

                    numOfBricks++;
                }
            }
        }

        public static void Update()
        {
            
            foreach (Brick brick in listBricks)
            {
                brick.Update();
                if (_game.ball.collisionBox.Intersects(brick.Rect))
                {
                    brick.Weaken();
                    _game.ball.ReverseDirectionY();
                    numOfBricks--;
                    Console.WriteLine(numOfBricks);
                }
            }
        }

        public static int NumOfBricks
        {
            get { return numOfBricks; }
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
