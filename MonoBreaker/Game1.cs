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
using MonoBreaker.Script.Scene;
using MonoBreaker.Script.Scene.GameScenes;

namespace MonoBreaker;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D scaledDisp;

    public static int trueScreenWidth = 320; public static int trueScreenHeight = 240; // instance for all
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.Title = "MonoBreaker";

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 960;
        _graphics.ApplyChanges();
        scaledDisp = new RenderTarget2D(GraphicsDevice, GraphicsDevice.DisplayMode.Width / 4, GraphicsDevice.DisplayMode.Height / 4);
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        GetContent.Initialize(this);
        Playing.Initialize(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        Playing.Load();
    }

    protected override void Update(GameTime gameTime)
    {
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        // Exit();
            
        

        // TODO: Add your update logic here
        base.Update(gameTime);

        
        switch (SceneController.CurrentScene)
        {
            case Scene.TITLE:
                break;
            case Scene.MENU:
                break;
            case Scene.PLAYING:
                Playing.Update(gameTime);
                break;
            case Scene.PAUSE:
                break;
            case Scene.NEXTROUND:
                break;
            case Scene.GAMEOVER:
                break;
        }
        
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        GraphicsDevice.SetRenderTarget(scaledDisp);

        _spriteBatch.Begin();

        switch (SceneController.CurrentScene)
        {
            case Scene.TITLE:
                break;
            case Scene.MENU:
                break;
            case Scene.PLAYING:
                Playing.Draw(_spriteBatch);
                break;
            case Scene.PAUSE:
                break;
            case Scene.NEXTROUND:
                break;
            case Scene.GAMEOVER:
                break;
        }

        
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(scaledDisp, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
        _spriteBatch.End();

        _spriteBatch.Begin(samplerState: SamplerState.LinearWrap); // text

        switch (SceneController.CurrentScene)
        {
            case Scene.TITLE:
                break;
            case Scene.MENU:
                break;
            case Scene.PLAYING:
                Playing.DrawText(_spriteBatch);
                break;
            case Scene.PAUSE:
                break;
            case Scene.NEXTROUND:
                break;
            case Scene.GAMEOVER:
                break;
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
