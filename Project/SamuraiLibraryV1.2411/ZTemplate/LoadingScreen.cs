using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.ScreenTemplate
{
    class LoadingScreen : SAScreen
    {
        Color backColor;
        Texture2D backTex;
        Vector2 pos;
        TimeSpan timeSpan;

        public LoadingScreen(ChangeScreenDelegate changeScreenDelegate)
            : base(changeScreenDelegate)
        { }

        public override void LoadContent()
        {
            backColor = new Color(30, 30, 30);
            backTex = SAGlobal.Content.Load<Texture2D>("Images/Logo");
            pos = new Vector2((480 - backTex.Bounds.Width) / 2, (800 - backTex.Bounds.Height) / 2);
        }

        public override void SetupInput()
        {
            SAInput.EnableBackButton(OnBackButton);
        }

        public override void Init()
        {
            timeSpan = TimeSpan.FromSeconds(1.5);
        }
        public override void UnloadContent()
        {
            SAInput.ResetInput();
        }

        public override void Update(GameTime gameTime)
        {
            timeSpan -= gameTime.ElapsedGameTime;
            if (timeSpan <= TimeSpan.Zero)
            {
                ChangeScreenTo(ScreenType.Menu);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SAGlobal.game.GraphicsDevice.Clear(backColor);
            SAGlobal.spriteBatch.Draw(backTex, pos, Color.White);
        }

        private void OnBackButton()
        {
            SAGlobal.game.Exit();
        }
    }
}
