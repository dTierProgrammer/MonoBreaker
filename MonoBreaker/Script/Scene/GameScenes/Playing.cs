using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoBreaker.Script.Font;
using MonoBreaker.Script.Game;
using MonoBreaker.Script.Global;
using static System.Formats.Asn1.AsnWriter;
using System.Text;

namespace MonoBreaker.Script.Scene.GameScenes;

public static class Playing
{
    private static KeyboardState priorKBState;

    private static Texture2D playfield;

    public static Paddle player;
    public static  Ball ball;
    private static Rectangle[] screenBounds = new Rectangle[4];

    private static Texture2D leftoverTriesCounter;

    public static int score;
    public static readonly int speedUpThreshold = BrickMap.RowLength;

    public static float startingGameSpeed = 1.5f;

    public static float speedIncrement = .1f;
    public static float tries = 5;
    public static float round = 1;

    private static Game1 _game;
    public static void Initialize(Game1 game)
    {
        _game = game;
        BrickMap.Initialize(_game);

        screenBounds[0] = new Rectangle(0, 0, 4, Game1.trueScreenHeight); // r
        screenBounds[1] = new Rectangle(Game1.trueScreenWidth - 4, 0, 4, Game1.trueScreenHeight); // l
        screenBounds[2] = new Rectangle(0, 0, Game1.trueScreenWidth, 4); // u
        screenBounds[3] = new Rectangle(0, Game1.trueScreenHeight - 4, Game1.trueScreenWidth, 4); // d

        player = new Paddle(GetContent.GetTexture("Game/paddle"), new Vector2(Game1.trueScreenWidth / 2 - 17, Game1.trueScreenHeight - 10), startingGameSpeed, screenBounds);
        ball = new Ball(GetContent.GetTexture("Game/ball"), new Vector2(100, 100), startingGameSpeed, screenBounds, player);
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

        player.Update();
        ball.Update(gameTime);
        BrickMap.Update();

        
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(playfield, Vector2.Zero, Color.White);
        spriteBatch.Draw(leftoverTriesCounter, new Vector2(6, 25), Color.White);
        player.Draw(spriteBatch);
        ball.Draw(spriteBatch);
        BrickMap.Draw(spriteBatch);
    }

    public static void DrawText(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Fonts.titleFont, $"Score: {score}\n" +
                                                 $"Round {round}\n" +
                                                 $"   x {tries}\n"
                                                 , new Vector2(25, 24), Color.White);
    }
}