// TODO: FIX ISSUES, ADD SCENE MANAGEMENT, ADD SOUNDS
/* Issues:
 * - Ball will phase through bricks and instantly destroy bricks if edge/side is hit, or if 2 are hit at the same time
 * - Ball will still get stuck in paddle if hit at specific angle
 * - If ball is moving fast enough, it can phase through collision boxes
 * */
 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoBreaker.Script.Font;
using MonoBreaker.Script.Game;
using MonoBreaker.Script.Global;

namespace MonoBreaker;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Rectangle[] screenBounds = new Rectangle[4];

    private RenderTarget2D scaledDisp;
    private KeyboardState priorKBState;

    private Texture2D playfield;

    public Paddle player;
    public Ball ball;

    private Texture2D debug;
    private Texture2D leftoverTriesCounter;

    private static int offset = 1;

    public static int score;
    public static readonly int speedUpThreshold = BrickMap.RowLength;

    public static float startingGameSpeed = 1.5f;

    public static float speedIncrement = .25f;
    public static float tries = 5;
    public static float round = 1;

    public static int trueScreenWidth = 320; public static int trueScreenHeight = 240; // instance for all
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.Title = "MonoBreaker Demo";

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 960;
        _graphics.ApplyChanges();
        scaledDisp = new RenderTarget2D(GraphicsDevice, GraphicsDevice.DisplayMode.Width / 4, GraphicsDevice.DisplayMode.Height / 4);

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        GetContent.Initialize(this);
        screenBounds[0] = new Rectangle(0, 0, 4, trueScreenHeight); // r
        screenBounds[1] = new Rectangle(trueScreenWidth - 4, 0, 4, trueScreenHeight); // l
        screenBounds[2] = new Rectangle(0, 0, trueScreenWidth, 4); // u
        screenBounds[3] = new Rectangle(0 ,trueScreenHeight - 4, trueScreenWidth, 4); // d
        BrickMap.Initialize(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        playfield = GetContent.GetTexture("Game/playField");

        player = new Paddle(GetContent.GetTexture("Game/paddle"), new Vector2(trueScreenWidth / 2 - 17, trueScreenHeight - 10), startingGameSpeed, screenBounds);
        ball = new Ball(GetContent.GetTexture("Game/ball"), new Vector2(100, 100), startingGameSpeed, screenBounds, player);
        debug = GetContent.GetTexture("Game/ballSuper");
        leftoverTriesCounter = GetContent.GetTexture("Game/ball");
    }

    protected override void Update(GameTime gameTime)
    {
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        // Exit();
            
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

        // TODO: Add your update logic here
        player.Update();
        ball.Update();
        BrickMap.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        GraphicsDevice.SetRenderTarget(scaledDisp);

        _spriteBatch.Begin();
        _spriteBatch.Draw(playfield, Vector2.Zero, Color.White);
        player.Draw(_spriteBatch);
        ball.Draw(_spriteBatch);
        BrickMap.Draw(_spriteBatch);
        _spriteBatch.Draw(leftoverTriesCounter, new Vector2(6, 25), Color.White);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(scaledDisp, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
        _spriteBatch.End();

        _spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
        _spriteBatch.DrawString(Fonts.titleFont, $"Score: {score}\n" +
                                                 $"Round {round}\n" +
                                                 $"   x {tries}\n"
                                                 , new Vector2(25, 24), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
