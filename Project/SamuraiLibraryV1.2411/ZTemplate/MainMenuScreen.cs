using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.ScreenTemplate
{
    //class MainMenuScreen:SAScreen
    //{
    //    Texture2D backTex;
    //    SASimpleTextButton[] buttons;
    //    SpriteFont font;

    //    public MainMenuScreen(ChangeScreenDelegate changeScreenDelegate)
    //        : base(changeScreenDelegate) 
    //    { 

    //    }

    //     public override void LoadContent()
    //     {
    //         backTex = SAGlobal.Content.Load<Texture2D>(@"Images/back");
    //         font = SAGlobal.Content.Load<SpriteFont>(@"Font/ButtonFont");
    //         buttons = new SASimpleTextButton[3]; 
    //     }

    //     public override void SetupInput()
    //     {
    //         SAInput.EnableBackButton(OnBackButton);
    //         Color normalColor = new Color(153, 201, 3);
    //         Color clickColor = new Color(114, 182, 1);
    //         buttons[1] = new SASimpleTextButton(font, "Play", Color.Black, 200, 350, 140, 60, Click1, normalColor, clickColor);
    //     }

    //     public override void Draw(GameTime gameTime)
    //     {
    //         SAGlobal.spriteBatch.Draw(backTex, Vector2.Zero, Color.White);
    //         buttons[1].Draw(SAGlobal.spriteBatch);
    //     }

    //     public override void UnloadContent()
    //     {
    //         SAInput.ResetInput();
    //     }

    //     private void OnBackButton()
    //     {
    //         SAGlobal.game.Exit();
    //     }

    //     public void Click1()
    //     {
    //         ChangeScreenTo(ScreenType.Game);
    //     }

    //     public void Click2()
    //     { 

    //     }

    //     public void Click3()
    //     { 

    //     }
    //}
}
