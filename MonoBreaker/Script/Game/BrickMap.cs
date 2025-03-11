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

        public static int GetRowLength() 
        {
            int rowLength = map.GetLength(1);
            return rowLength;
        }

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
                    // nigga how the fuck do i make it not phase through the damn brick if it bounces on the side ??????
                    // and why will it immediately destroy them if it collides with 2 at a time ??????
                    
                    // <TODO: maybe make ball collisions with bricks a method within the ball class...???>
                    // CONSIDERING COLLISIONS WITH shallow axis METHOD !!!!!!!!!!
                    // research shallow axis
                    
                    brick.Weaken();
                    _game.ball.ReverseDirectionY();
                    
                    /*
                    if (_game.ball.collisionBox.Right > brick.Rect.Left)
                    {
                        _game.ball.ReverseDirectionX();
                    }
                    else if (_game.ball.collisionBox.Left < brick.Rect.Right)
                    {
                        _game.ball.ReverseDirectionX();
                    }
                    */
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
