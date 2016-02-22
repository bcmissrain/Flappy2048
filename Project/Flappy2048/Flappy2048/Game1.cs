using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Samurai.Sprites;
using Samurai;

namespace Flappy2048
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SASimpleSprite background;
        SASimpleSprite winSprite;
        SASimpleSprite frame;
        Bird bird;
        List<Block> blocks;
        SpriteFont font;
        Vector2[] scorePositions;
        public static bool ifWin;
        Rectangle[] buttons;
        SASimpleSprite soundOn;
        SASimpleSprite soundOff;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 480;
        }

        protected override void Initialize()
        {
            // TODO: 在此处添加初始化逻辑
            TouchPanel.EnabledGestures = GestureType.Tap;
            ifWin = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // 创建新的 SpriteBatch，可将其用于绘制纹理。
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //注册 Global
            SAGlobal.Setup(this, spriteBatch, graphics);
            //注册MusicManager
            SAMusicManager.Setup();
            winSprite = new SASimpleSprite("Images/win");
            font = Content.Load<SpriteFont>("Font/ScoreFont");
            background = new SASimpleSprite("Images/background");
            frame = new SASimpleSprite("Images/frame");
            soundOn = new SASimpleSprite("Images/tile",new Rectangle(10,340,66,54),new Vector2(390,720),Color.White);
            soundOff = new SASimpleSprite("Images/tile",new Rectangle(90,340,66,54),new Vector2(390,720),Color.White);

            scorePositions = new Vector2[2];
            scorePositions[0] = new Vector2(292,62);
            scorePositions[1] = new Vector2(381,62);
            bird = new Bird();
            blocks = new List<Block>();
            Block block0 = new Block();
            block0.Reset();
            block0.SetLeft(0);
            Block block1 = new Block();
            block1.Reset();
            block1.SetLeft(333);
            blocks.Add(block0);
            blocks.Add(block1);
            buttons = new Rectangle[2];
            buttons[0] = new Rectangle(80,380,124,54);
            buttons[1] = new Rectangle(278,380,124,54);
        }

        protected override void UnloadContent()
        {
            // TODO: 在此处取消加载任何非 ContentManager 内容
        }

        protected override void Update(GameTime gameTime)
        {
            // 允许游戏退出
            if (ifWin == false)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    this.Exit();
                }
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    SAMusicManager.PlaySoundEffect("Sound/Coin");
                    ifWin = false;
                    bird.Clean();
                }
            }
            
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.Tap)
                {
                    Rectangle temp = new Rectangle((int)(gesture.Position.X), (int)(gesture.Position.Y), 1, 1);
                    if (ifWin == false)
                    {
                        if (soundOn.IfCollide(temp))
                        {
                            SAMusicManager.IfPlaySound = !SAMusicManager.IfPlaySound;
                        }
                        else
                        {
                            bird.Fly();
                        }
                    }
                    else
                    {
                        if (buttons[0].Intersects(temp))
                        {
                            SAMusicManager.PlaySoundEffect("Sound/Coin");
                            ifWin = false;
                            bird.Clean();
                        }
                        else if (buttons[1].Intersects(temp))
                        {
                            SAMusicManager.PlaySoundEffect("Sound/Coin");
                            GameData.RateGame();
                            ifWin = false;
                            bird.Clean();
                        }
                    }
                }
            }
            if (ifWin == false)
            {
                bird.Update();
                // TODO: 在此处添加更新逻辑
                foreach (Block b in blocks)
                {
                    b.Update(bird);
                }

                if (bird.IfWin)
                {
                    ifWin = true;
                    SAMusicManager.PlaySoundEffect("Sound/win");
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: 在此处添加绘图代码
            spriteBatch.Begin();
            background.Draw();
            foreach (Block b in blocks)
            {
                b.Draw();
            }
            bird.Draw();
            frame.Draw();
            spriteBatch.DrawString(font, bird.Score.ToString(), scorePositions[0], Color.White);
            spriteBatch.DrawString(font, GameData.gBestScore.ToString(), scorePositions[1], Color.White);
            if (SAMusicManager.IfPlaySound)
            {
                soundOn.Draw();
            }
            else
            {
                soundOff.Draw();
            }
            if(ifWin)
            {
                winSprite.Draw();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
