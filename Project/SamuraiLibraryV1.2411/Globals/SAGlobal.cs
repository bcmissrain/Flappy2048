using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public static class SAGlobal
    {
        public static Game game { get; set; }
        //内存中永久驻留的资源
        public static ContentManager Content { get; set; }
        //以页面为生命周期驻留的资源
        public static ContentManager TemporalContent { get; set; }
        public static SpriteBatch spriteBatch { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }

        public static void Setup(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            SAGlobal.game = game;
            SAGlobal.Content = game.Content;
            SAGlobal.TemporalContent = CreateContentManager();
            SAGlobal.spriteBatch = spriteBatch;
            SAGlobal.graphics = graphics;
        }

        public static ContentManager CreateContentManager()
        {
            return new ContentManager(Content.ServiceProvider, Content.RootDirectory);
        }

        public static void CleanTemporalContent()
        {
            if (TemporalContent != null)
            {
                TemporalContent.Unload();
            }
        }
    }
}
