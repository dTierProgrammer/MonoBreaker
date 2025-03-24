// TODO: FINISH SCENE MANAGEMENT
 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoBreaker.Script.Font;
using MonoBreaker.Script.Game;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Scene;
using MonoBreaker.Script.Scene.GameScenes;
using System.Xml.Linq;

namespace MonoBreaker;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D scaledDisp;
    public static Texture2D debug;
    public static Rectangle centerDebug;

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
        centerDebug = new Rectangle(160, 0, 1, trueScreenHeight);
        
        
        GetContent.Initialize(this);
        Title.Initialize(this);
        Reset.Ininitialize(this);
        Playing.Initialize(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        debug = GetContent.GetTexture("Debug/debugRect");
        Title.Load();
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
                Title.Update();
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
                Title.Draw(_spriteBatch);
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
        //_spriteBatch.Draw(debug, new Rectangle(1, 1, 16, 16), Color.Red);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(scaledDisp, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
        _spriteBatch.End();

        _spriteBatch.Begin(samplerState: SamplerState.LinearWrap); // text
        
        switch (SceneController.CurrentScene)
        {
            case Scene.TITLE:
                Title.DrawText();
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
