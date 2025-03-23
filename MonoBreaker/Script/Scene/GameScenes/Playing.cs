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

public enum GameState 
{
    PLAY,
    ROUNDCOMPLETE,
    PAUSE,
    GAMEOVER
}

public static class Playing
{
    private static KeyboardState priorKBState;
    private static GameState currentState = GameState.PLAY;


    private static Texture2D playfield;
    private static Texture2D pauseOverlay;
    private static Texture2D pauseText;
    private static Texture2D roundclearText;
    private static Texture2D gameoverText;

    public static Paddle player;
    public static  Ball ball;
    public static List<Ball>otherBalls = new List<Ball>();
    public readonly static Rectangle[] screenBounds = new Rectangle[4];

    public static SoundEffect powerUpSound = GetContent.GetSound("powerUp");
    public static SoundEffect addTrySound = GetContent.GetSound("1up");

    private static Texture2D leftoverTriesCounter;


    public static int score = 0;
    public static int brokenBricks = 0;
    private static int prevScore;
    public static readonly int speedUpThreshold = BrickMap.RowLength;
    private static readonly int addTryThreshold = 1000;
    
    public static bool showDebugInfo { get; private set; }
    public static float startingGameSpeed = 1.4f;

    public static float speedIncrement = .1f;
    public static float tries = 5;
    public static float round = 1;

    public static Random rng = new Random();

    public readonly static Vector2 centerPt = new Vector2(160, 0);
    
    
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
        pauseOverlay = GetContent.GetTexture("UI/Pause/overlay");
        pauseText = GetContent.GetTexture("UI/Pause/paused");
        roundclearText = GetContent.GetTexture("UI/Pause/roundclear");
        gameoverText = GetContent.GetTexture("UI/Pause/gameover");
    }

    public static void Update(GameTime gameTime)
    {
        switch (currentState) 
        {
            case GameState.PLAY:
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    player.isMoving[0] = true;
                else
                    player.isMoving[0] = false;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    player.isMoving[1] = true;
                else
                    player.isMoving[1] = false;

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !priorKBState.IsKeyDown(Keys.Space) && ball.IsActive == false ||
                    Keyboard.GetState().IsKeyDown(Keys.Space) && !priorKBState.IsKeyDown(Keys.Space) && (ball.isStuck || ball.isStuckToTwin))
                    ball.Launch();
                if (Keyboard.GetState().IsKeyDown(Keys.Space) & !priorKBState.IsKeyDown(Keys.Space) && ball.IsActive == true && player.canShoot)
                    player.ShootBullet();

                if (showDebugInfo)
                {
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
                    if (Keyboard.GetState().IsKeyDown(Keys.L) & !priorKBState.IsKeyDown(Keys.L))
                        _GeneratePowerup.NewTwinPaddle(player.position);
                    if (Keyboard.GetState().IsKeyDown(Keys.OemSemicolon) & !priorKBState.IsKeyDown(Keys.OemSemicolon))
                        _GeneratePowerup.NewBallGun(player.position);
                    if (Keyboard.GetState().IsKeyDown(Keys.OemQuotes) & !priorKBState.IsKeyDown(Keys.OemQuotes))
                        _GeneratePowerup.NewGlueBall(player.position);
                    if (Keyboard.GetState().IsKeyDown(Keys.R) & !priorKBState.IsKeyDown(Keys.R))
                        _GeneratePowerup.RollPowerup(player.position);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Z) & !priorKBState.IsKeyDown(Keys.Z))
                    if (!showDebugInfo)
                        showDebugInfo = true;
                    else
                        showDebugInfo = false;
                if (Keyboard.GetState().IsKeyDown(Keys.Back) & !priorKBState.IsKeyDown(Keys.Back))
                    currentState = GameState.PAUSE;
                    

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

                if(tries <=0) 
                {
                    currentState = GameState.GAMEOVER;
                }

                BrickMap.Update();
                _ManagePowerups.Update(gameTime);
                break;
            case GameState.PAUSE:
                if (Keyboard.GetState().IsKeyDown(Keys.Back) & !priorKBState.IsKeyDown(Keys.Back))
                    currentState = GameState.PLAY;
                if(Keyboard.GetState().IsKeyDown(Keys.Escape) & !priorKBState.IsKeyDown(Keys.Escape))
                    SceneController.GoToTitle();
                break;
        }
        priorKBState = Keyboard.GetState();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(playfield, Vector2.Zero, Color.White);
        player.Draw(spriteBatch);
        ball.Draw(spriteBatch);
        foreach (Ball ball in otherBalls)
        {
            ball.Draw(spriteBatch);
        }
        BrickMap.Draw(spriteBatch);
        _ManagePowerups.Draw(spriteBatch);
        if(showDebugInfo)
            spriteBatch.Draw(Game1.debug,Game1.centerDebug, Color.Yellow );
        if(!showDebugInfo)
            spriteBatch.Draw(leftoverTriesCounter, new Vector2(6, 25), Color.White);
        
        if(ball.SuperBall)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/superball_icon"), new Vector2 (307, 6), Color.White);
        if(ball.Piercing)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/piercing_icon"), new Vector2(307, 15), Color.White);
        if(ball.BallHealth >= 2)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/deathbounce_icon"), new Vector2(307, 25), Color.White);
        if(ball.Lovely)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/lovely_icon"), new Vector2(307, 34), Color.White);
        if(ball.GlueBall)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/glueball_icon"), new Vector2(307, 43), Color.White);
        if(player.isSuper)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/paddleextend_icon"), new Vector2(307, 52), Color.White);
        if(player.isTwinActive)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/twinpaddle_icon"), new Vector2(307, 61), Color.White);
        if(player.gunIsActive)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/shooting_icon"), new Vector2(307, 71), Color.White);
        if(player.ballGunIsActive)
            spriteBatch.Draw(GetContent.GetTexture("UI/Playing/ballshooting_icon"), new Vector2(307, 80), Color.White);

        switch (currentState)
        {
            case GameState.PAUSE:
                spriteBatch.Draw(pauseOverlay, Vector2.Zero, Color.White);
                spriteBatch.Draw(pauseText, new Rectangle(160, 90, pauseText.Width, pauseText.Height), null, Color.White, 0f, new Vector2(pauseText.Width / 2, pauseText.Height / 2), SpriteEffects.None, 0);
                string p_menuElementOne = "Press Backspace to Resume";
                spriteBatch.DrawString(Fonts.subTitleFont, p_menuElementOne, new Vector2(160, 120), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(p_menuElementOne).X / 2, 0), 1f, SpriteEffects.None, 0f);
                string p_menuElementTwo = "Press Escape to Quit";
                spriteBatch.DrawString(Fonts.subTitleFont, p_menuElementTwo, new Vector2(160, 135), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(p_menuElementTwo).X / 2, 0), 1f, SpriteEffects.None, 0f);
                string p_score = $"Score: {score}";
                spriteBatch.DrawString(Fonts.subTitleFont, p_score, new Vector2(160, 150), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(p_score).X / 2, 0), 1f, SpriteEffects.None, 0f);
                string p_tries = $"Tries: {tries}";
                spriteBatch.DrawString(Fonts.subTitleFont, p_tries, new Vector2(160, 165), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(p_tries).X / 2, 0), 1f, SpriteEffects.None, 0f);
                break;
            case GameState.ROUNDCOMPLETE:
                spriteBatch.Draw(pauseOverlay, Vector2.Zero, Color.White);
                break;
            case GameState.GAMEOVER:
                spriteBatch.Draw(pauseOverlay, Vector2.Zero, Color.White);
                spriteBatch.Draw(gameoverText, new Rectangle(158, 90, gameoverText.Width, gameoverText.Height), null, Color.White, 0f, new Vector2(gameoverText.Width / 2, gameoverText.Height / 2), SpriteEffects.None, 0);
                string g_menuElementOne = "Press Enter to Retry";
                spriteBatch.DrawString(Fonts.subTitleFont, g_menuElementOne, new Vector2(160, 120), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(g_menuElementOne).X / 2, 0), 1f, SpriteEffects.None, 0f);
                string g_menuElementTwo = "Press Escape to Quit";
                spriteBatch.DrawString(Fonts.subTitleFont, g_menuElementTwo, new Vector2(160, 135), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(g_menuElementTwo).X / 2, 0), 1f, SpriteEffects.None, 0f);
                string g_score = $"Final Score: {score}";
                spriteBatch.DrawString(Fonts.subTitleFont, g_score, new Vector2(160, 150), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(g_score).X / 2, 0), 1f, SpriteEffects.None, 0f);
                string g_round = $"Out at: Round {round}";
                spriteBatch.DrawString(Fonts.subTitleFont, g_round, new Vector2(160, 165), Color.White, 0, new Vector2(Fonts.subTitleFont.MeasureString(g_round).X / 2, 0), 1f, SpriteEffects.None, 0f);
                break;
        }
    }

    public static void DrawText(SpriteBatch spriteBatch)
    {
        switch (currentState) 
        {
            case GameState.PLAY:
                if (!showDebugInfo)
                {
                    spriteBatch.DrawString(Fonts.titleFont, $"Score: {score}\n" +
                                                            $"Round {round}\n" +
                                                            $"   x {tries}\n"
                        , new Vector2(25, 24), Color.White);
                }

                if (showDebugInfo)
                {
                    spriteBatch.DrawString(Fonts.titleFont, $"Center Dist from Center: {player.distFromCenter}" +
                                                            $"\nOrigin Dist from Center: {player.collBoxOriginDistFromCenter}" +
                                                            $"\nX Velocity: {player.Velocity.X}" +
                                                            $"\nXY Coords: {player.collisionBox.X}, {player.collisionBox.Y}" +
                                                            $"\nBallDist from Paddle Origin: {ball.stickPointX}", new Vector2(25, 18), Color.Red);
                }
                break;
        }
    }

    public static void DrawOverlay(SpriteBatch spriteBatch) 
    {
        
    }
}