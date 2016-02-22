using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Samurai
{
    //ATTENTION after Global.Setup
    public static class SAGraphicManager
    {
        #region 图像资源管理
        public static Texture2D GetImage(string name)
        {
            return SAGlobal.Content.Load<Texture2D>(name);
        }
        #endregion

        #region 内存中绘制图像
        public static RenderTarget2D GetButtonTexture(int width, int height, Color color)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(SAGlobal.game.GraphicsDevice, width, height);
            SAGlobal.game.GraphicsDevice.SetRenderTarget(renderTarget);
            SAGlobal.game.GraphicsDevice.Clear(color);
            SAGlobal.game.GraphicsDevice.SetRenderTarget(null);
            return renderTarget;
        }
        public static RenderTarget2D GetButtonTexture(int width, int height, int border, Color backColor, Color borderColor)
        {
            RenderTarget2D backTarget = new RenderTarget2D(SAGlobal.game.GraphicsDevice, width - 2 * border, height - 2 * border);
            SAGlobal.game.GraphicsDevice.SetRenderTarget(backTarget);
            SAGlobal.game.GraphicsDevice.Clear(backColor);
            SAGlobal.game.GraphicsDevice.SetRenderTarget(null);
            RenderTarget2D result = new RenderTarget2D(SAGlobal.game.GraphicsDevice, width, height);
            SAGlobal.game.GraphicsDevice.SetRenderTarget(backTarget);
            SAGlobal.game.GraphicsDevice.Clear(borderColor);
            SAGlobal.spriteBatch.Begin();
            SAGlobal.spriteBatch.Draw(backTarget, new Vector2(border, border), Color.White);
            SAGlobal.spriteBatch.End();
            SAGlobal.game.GraphicsDevice.SetRenderTarget(null);
            return result;
        }
        public static RenderTarget2D GetTextButtonTexture(SpriteFont font, string text, Color textColor, int width, int height, Color backColor)
        {
            SAText Text = new SAText(font, text, textColor, new Rectangle(0, 0, width, height));
            RenderTarget2D renderTarget = new RenderTarget2D(SAGlobal.game.GraphicsDevice, width, height);
            SAGlobal.game.GraphicsDevice.SetRenderTarget(renderTarget);
            SAGlobal.game.GraphicsDevice.Clear(backColor);
            SAGlobal.spriteBatch.Begin();
            Text.Draw(SAGlobal.spriteBatch);
            SAGlobal.spriteBatch.End();
            SAGlobal.game.GraphicsDevice.SetRenderTarget(null);
            return renderTarget;
        }
        #endregion

        #region 设置竖屏（独立无依赖项）
        public static void SetVertical(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 480;
        }
        #endregion
    }
}
