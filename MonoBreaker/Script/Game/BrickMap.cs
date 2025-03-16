using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using System;
using System.Collections.Generic;


namespace MonoBreaker.Script.Game
{
    public static class BrickMap
    {
        private static Texture2D[] images = new Texture2D[4];
        private static int numOfBricks;

        private static int offset = 6;
        private static int[,] map = // map of bricks
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
        public static List<Brick> listBricks = new List<Brick>(); // every brick instance

        public static void Initialize(Game1 game)
        {
            _game = game;
            for (int index = 0; index < 4; index++) // load brick images (fake automation)
            {
                images[index] = GetContent.GetTexture($"Game/brick_final_{index}");
            }

            for (int column = 0; column < map.GetLength(0); column++) // iterate through map array, for every number create new brick and add to brick list
            {
                for (int row = 0; row < map.GetLength(1); row++)
                {
                    if (map[column, row] == 1)
                    {
                        listBricks.Add(new Brick(images[0], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 2));
                    }
                    if (map[column, row] == 2)
                    {
                        listBricks.Add(new Brick(images[1], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 2));
                    }
                    if (map[column, row] == 3)
                    {
                        listBricks.Add(new Brick(images[2], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 1));
                    }
                    if (map[column, row] == 4)
                    {
                        listBricks.Add(new Brick(images[3], new Vector2((row * images[0].Width + offset), column * images[0].Height + offset * 6), 1));
                    }
                }
            }
        }

        public static int RowLength
        {
            get { return map.GetLength(1); }
        }

        public static int NumOfBricks
        {
            set 
            {
                numOfBricks = 112; 
            }

            get 
            {
                return numOfBricks; 
            }
        }

        public static void Update() // iterate through brick list and call each brick's update function
        {
            foreach (Brick brick in listBricks)
            {
                brick.Update();
                // ball will immediately destroy brick if it hits its corner or side
                // side collision checks obvs but any code I write for it does jack shit
                // how do I do tilemap collisions l;asd;asjkfopiasdjfiodwgjLKSDCVJoiflgrj
                // better idea: don't check for collisions in the fucken brickmap class
            }
        }

        public static void ResetMap()
        {
            foreach (Brick brick in listBricks) 
            {
                brick.Reset();
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
