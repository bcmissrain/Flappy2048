using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.ScreenTemplate
{
    public class ScreenManager : SADirector
    {
        public ScreenManager(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
            : base(game, graphics, spriteBatch)
        {
            ChangeScreenTo(ScreenType.Loading);
        }

        public override void ChangeScreenTo(ScreenType screenType)
        {
            switch (screenType)
            {
                case ScreenType.Loading:
                    ChangeScreenTo(screenType, CreateLoadingScreen);
                    break;
                //case ScreenType.MainMenu:
                //    ChangeScreenTo(screenType, CreateMainMenuScreen);
                //    break;
                //case ScreenType.Game:
                //    ChangeScreenTo(screenType, CreateGameScreen);
                //    break;
            }
        }

        public SAScreen CreateLoadingScreen()
        {
            return new LoadingScreen(ChangeScreenTo);
        }

        //public SAScreen CreateMainMenuScreen()
        //{
        //    return new MainMenuScreen(ChangeScreenTo);
        //}

        //public SAScreen CreateGameScreen()
        //{
        //    return new GameScreen(ChangeScreenTo);
        //}
    }
}
