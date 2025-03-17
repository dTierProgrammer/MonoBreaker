using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoBreaker.Script.Font;
using MonoBreaker.Script.Game;
using MonoBreaker.Script.Global;
using static System.Formats.Asn1.AsnWriter;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using MonoBreaker.Script.Game.PowerUp;
using System.Diagnostics;

namespace MonoBreaker.Script.Scene.GameScenes;

public static class Playing
{
    private static KeyboardState priorKBState;

    private static Texture2D playfield;

    public static Paddle player;
    public static  Ball ball;
    public static List<Ball>otherBalls = new List<Ball>();
    public static List<Bullet> bullets = new List<Bullet>();
    public readonly static Rectangle[] screenBounds = new Rectangle[4];

    public static SoundEffect powerUpSound = GetContent.GetSound("powerUp");
    public static SoundEffect addTrySound = GetContent.GetSound("1up");

    private static Texture2D leftoverTriesCounter;
    private static Texture2D testBullet;

    public static int score = 0;
    private static int prevScore;
    public static readonly int speedUpThreshold = BrickMap.RowLength;
    private static readonly int addTryThreshold = 130;

    public static float startingGameSpeed = 1.5f;

    public static float speedIncrement = .1f;
    public static float tries = 5;
    public static float round = 1;
    private static AddTry oneUp;
    private static DeathBounce deathBounce;
    private static MultiBall multiBall;

    public static Random rng = new Random();

    private static Game1 _game;
    
    public static void Initialize(Game1 game)
    {
        _game = game;
        BrickMap.Initialize(_game);

        screenBounds[0] = new Rectangle(0, 0, 4, Game1.trueScreenHeight); // r
        screenBounds[1] = new Rectangle(Game1.trueScreenWidth - 4, 0, 4, Game1.trueScreenHeight); // l
        screenBounds[2] = new Rectangle(0, 0, Game1.trueScreenWidth, 4); // u
        screenBounds[3] = new Rectangle(0, Game1.trueScreenHeight - 4, Game1.trueScreenWidth, 4); // d

        player = new Paddle(new Vector2(Game1.trueScreenWidth / 2f - 17f, Game1.trueScreenHeight - 16), startingGameSpeed);
        ball = new Ball(new Vector2(100, 100), startingGameSpeed, true);

        oneUp = new AddTry(new Vector2(100, 0));
        deathBounce = new DeathBounce(new Vector2(200, 0));
        multiBall = new MultiBall(GetContent.GetTexture("Game/powerup/multiball"), new Vector2(150, 0));
        leftoverTriesCounter = GetContent.GetTexture("Game/ball");
    }

    public static void Load()
    {
        playfield = GetContent.GetTexture("Game/playField");
        leftoverTriesCounter = GetContent.GetTexture("Game/ball");
        testBullet = GetContent.GetTexture("Game/bullet");
    }

    public static void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
            player.isMoving[0] = true;
        else
            player.isMoving[0] = false;
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
            player.isMoving[1] = true;
        else
            player.isMoving[1] = false;

        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !priorKBState.IsKeyDown(Keys.Space) && ball.IsActive == false)
            ball.Launch();
        if (Keyboard.GetState().IsKeyDown(Keys.Up) & !priorKBState.IsKeyDown(Keys.Up) && ball.IsActive == true)
            player.ShootBullet();
        priorKBState = Keyboard.GetState();
        
        if (score % addTryThreshold == 0 && score != 0 && score != prevScore) // 1up after a certain amount of points is added to score
        {
            tries++;
            addTrySound.Play();
        }
        prevScore = score;
        
        if(ball.collisionBox.Intersects(Playing.screenBounds[3]))
        {
            foreach (Ball ball in otherBalls)
            {
                ball.Kill();
            }
        }

        player.Update();
        ball.Update(gameTime);
        foreach (Ball ball in otherBalls)
        {
            ball.Update(gameTime);
        }
        foreach (Bullet bullet in bullets)
        {
            bullet.Update();
        }
        BrickMap.Update();
        oneUp.Update();
        deathBounce.Update();
        multiBall.Update();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(playfield, Vector2.Zero, Color.White);
        spriteBatch.Draw(leftoverTriesCounter, new Vector2(6, 25), Color.White);
        player.Draw(spriteBatch);
        ball.Draw(spriteBatch);
        foreach (Ball ball in otherBalls)
        {
            ball.Draw(spriteBatch);
        }
        foreach (Bullet bullet in bullets)
        {
            bullet.Draw(spriteBatch);
        }
        BrickMap.Draw(spriteBatch);
        oneUp.Draw(spriteBatch);
        deathBounce.Draw(spriteBatch);
        multiBall.Draw(spriteBatch);
        spriteBatch.Draw(testBullet, new Vector2(100, 100), Color.White);
    }

    public static void DrawText(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.titleFont, $"Score: {score}\n" +
                                                 $"Round {round}\n" +
                                                 $"   x {tries}\n"
                                                 , new Vector2(25, 24), Color.White);
    }
}