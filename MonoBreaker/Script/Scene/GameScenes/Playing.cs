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

namespace MonoBreaker.Script.Scene.GameScenes;

public static class Playing
{
    private static KeyboardState priorKBState;

    private static Texture2D playfield;

    public static Paddle player;
    public static  Ball ball;
    private static List<Ball>otherBalls = new List<Ball>();
    public readonly static Rectangle[] screenBounds = new Rectangle[4];

    public static SoundEffect powerUpSound = GetContent.GetSound("powerUp");

    private static Texture2D leftoverTriesCounter;

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

        player = new Paddle(new Vector2(Game1.trueScreenWidth / 2f - 17f, Game1.trueScreenHeight - 16), startingGameSpeed, screenBounds);
        ball = new Ball(new Vector2(100, 100), startingGameSpeed, true);
        
        // test multiball
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));
        otherBalls.Add(new Ball(new Vector2(rng.NextInt64(180, 320), rng.NextInt64(100, 320)), startingGameSpeed, false, 5));

        oneUp = new AddTry(new Vector2(100, 0));
        deathBounce = new DeathBounce(new Vector2(200, 0));
        leftoverTriesCounter = GetContent.GetTexture("Game/ball");
    }

    public static void Load()
    {
        playfield = GetContent.GetTexture("Game/playField");
        leftoverTriesCounter = GetContent.GetTexture("Game/ball");
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
        priorKBState = Keyboard.GetState();
        
        if (score % addTryThreshold == 0 && score != 0 && score != prevScore) // 1up after a certain amount of points is added to score
        {
            tries++;
        }
        prevScore = score;

        player.Update();
        ball.Update(gameTime);
        foreach (Ball ball in otherBalls)
        {
            ball.Update(gameTime);
        }
        BrickMap.Update();
        oneUp.Update();
        deathBounce.Update();
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
        oneUp.Draw(spriteBatch);
        deathBounce.Draw(spriteBatch);
    }
    
    public static void DrawText(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.titleFont, $"Score: {score}\n" +
                                                 $"Round {round}\n" +
                                                 $"   x {tries}\n"
                                                 , new Vector2(25, 24), Color.White);
    }
}