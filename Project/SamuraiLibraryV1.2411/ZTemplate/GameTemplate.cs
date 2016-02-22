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

namespace Samurai
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Director
        SADirector director;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            //Config È«ÆÁ
            graphics.IsFullScreen = true;
            //Config ÊúÆÁ
            SAGraphicManager.SetVertical(graphics);
        }

        protected override void Initialize()
        {
            //TODO
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Director
            //director = new SADirector(this, graphics, spriteBatch);
        }

        protected override void UnloadContent()
        {
            //Director
            director.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            //Director
            director.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Director
            director.Draw(gameTime);
            base.Draw(gameTime);
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            //Director
            director.OnActivated();
            base.OnActivated(sender, args);
        }
    }
}
