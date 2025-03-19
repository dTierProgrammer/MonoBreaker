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
using MonoBreaker.Script.Game.Base;

namespace MonoBreaker.Script.Scene.GameScenes;

public static class Playing
{
    private static KeyboardState priorKBState;

    private static Texture2D playfield;

    public static Paddle player;
    public static  Ball ball;
    public static List<Ball>otherBalls = new List<Ball>();
    public readonly static Rectangle[] screenBounds = new Rectangle[4];

    public static SoundEffect powerUpSound = GetContent.GetSound("powerUp");
    public static SoundEffect addTrySound = GetContent.GetSound("1up");

    private static Texture2D leftoverTriesCounter;
    private static Texture2D testBullet;

    public static int score = 0;
    public static int brokenBricks = 0;
    private static int prevScore;
    public static readonly int speedUpThreshold = BrickMap.RowLength;
    private static readonly int addTryThreshold = 1000;

    public static float startingGameSpeed = 1.5f;

    public static float speedIncrement = .1f;
    public static float tries = 5;
    public static float round = 1;

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
        if (Keyboard.GetState().IsKeyDown(Keys.Space) & !priorKBState.IsKeyDown(Keys.Space) && ball.IsActive == true && player.canShoot)
            player.ShootBullet();
        if (Keyboard.GetState().IsKeyDown(Keys.A) & !priorKBState.IsKeyDown(Keys.A))
            _GeneratePowerup.NewAddTry(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.S) & !priorKBState.IsKeyDown(Keys.S))
            _GeneratePowerup.NewDeathBounce(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.D) & !priorKBState.IsKeyDown(Keys.D))
            _GeneratePowerup.NewMultiBall(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.F) & !priorKBState.IsKeyDown(Keys.F))
            _GeneratePowerup.NewPaddleExtend(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.G) & !priorKBState.IsKeyDown(Keys.G))
            _GeneratePowerup.NewPiercing(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.H) & !priorKBState.IsKeyDown(Keys.H))
            _GeneratePowerup.NewShooting(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.J) & !priorKBState.IsKeyDown(Keys.J))
            _GeneratePowerup.NewSuperBall(player.position);
        if (Keyboard.GetState().IsKeyDown(Keys.K) & !priorKBState.IsKeyDown(Keys.K))
            _GeneratePowerup.NewLovely(player.position);
        priorKBState = Keyboard.GetState();
        
        if (score % addTryThreshold == 0 && score != 0 && score != prevScore) // 1up after a certain amount of points is added to score
        {
            tries++;
            addTrySound.Play();
        }
        prevScore = score;

        player.Update(gameTime);
        ball.Update(gameTime);
        foreach (Ball ball in otherBalls)
        {
            ball.Update(gameTime);
        }
        BrickMap.Update();
        _ManagePowerups.Update(gameTime);
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
        BrickMap.Draw(spriteBatch);
        _ManagePowerups.Draw(spriteBatch);
    }

    public static void DrawText(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.titleFont, $"Score: {score}\n" +
                                                 $"Round {round}\n" +
                                                 $"   x {tries}\n"
                                                 , new Vector2(25, 24), Color.White);
    }
}