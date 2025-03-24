using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using MonoBreaker.Script.Font;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using MonoBreaker.Script.Game;

namespace MonoBreaker.Script.Scene.GameScenes;

public static class Title
{
    private static Texture2D title;
    private static Texture2D titleshadow;
    private static Texture2D titlebg;
    private static Texture2D caption;
    private static Game1 _game;

    public static void Initialize(Game1 game)
    {
        _game = game;
    }

    public static void Load()
    {
        title = GetContent.GetTexture("UI/Title/title");
        titleshadow = GetContent.GetTexture("UI/Title/titleshadow");
        caption = GetContent.GetTexture("UI/Title/supertitle");
        titlebg = GetContent.GetTexture("UI/Title/bgclear");
    }

    public static void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter)) 
        {
            SceneController.StartGame();
            
        }
               
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //_game.Exit();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(titlebg, Vector2.Zero, Color.White);
        //spriteBatch.Draw(title, new Rectangle(160, 20, (int)title.Width / 2, (int)title.Height / 2), null, Color.White, 0f, new Vector2(196, 16), 1f, SpriteEffects.None, 0);
        spriteBatch.Draw(titleshadow, new Rectangle(161, 91, titleshadow.Width / 2, titleshadow.Height / 2), null, Color.White, 0f, new Vector2(titleshadow.Width / 2, titleshadow.Height / 2), SpriteEffects.None, 0);
        spriteBatch.Draw(title, new Rectangle(160, 90, title.Width / 2, title.Height / 2), null, Color.White, 0f, new Vector2(title.Width / 2, title.Height / 2), SpriteEffects.None, 0);
        spriteBatch.Draw(caption, new Vector2(62, 76), Color.White);
        string menuElementOne = "ENTER - Start Game";
        spriteBatch.DrawString(Fonts.subTitleFont, menuElementOne,new Vector2(160, 120), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(menuElementOne).X / 2, 0), 1f, SpriteEffects.None, 0f);
        string menuElementTwo = "ESC - Exit";
        //spriteBatch.DrawString(Fonts.subTitleFont, menuElementTwo, new Vector2(160, 135), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(menuElementTwo).X / 2, 0), 1f, SpriteEffects.None, 0f);


        spriteBatch.DrawString(Fonts.subTitleFont, "Version 1.0\n" +
                                                   "By: DTierProgrammer", new Vector2(1, 218), Color.White);
    }

    public static void DrawText()
    {
        
    }
}