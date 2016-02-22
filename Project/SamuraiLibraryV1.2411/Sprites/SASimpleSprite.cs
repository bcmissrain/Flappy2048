using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.Sprites
{
    /// <summary>
    /// 只管理一个图像的Sprite
    /// </summary>
    public class SASimpleSprite : SASprite
    {
        public SASimpleSprite() { }
        /// <summary>
        /// 在指定位置绘制整张纹理
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="postion">位置</param>
        public SASimpleSprite(Texture2D texture, Vector2 postion)
        {
            this.texture = texture;
            this.sourceRectangle = new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height);
            this.position = position;
            this.color = Color.White;
        }
        /// <summary>
        /// 直接绘制整张纹理
        /// </summary>
        /// <param name="texture">纹理</param>
        public SASimpleSprite(Texture2D texture)
            : this(texture, Vector2.Zero)
        { }
        /// <summary>
        /// 在指定position绘制指定的sourceRectangle范围内的纹理
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="sourceRectangle">纹理矩形</param>
        /// <param name="position">位置</param>
        /// <param name="color">指定颜色</param>
        public SASimpleSprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Color color)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.position = position;
            this.color = color;
        }
        public SASimpleSprite(string resource, Vector2 position)
            : this(SAGraphicManager.GetImage(resource), position) { }
        public SASimpleSprite(string resource)
            : this(SAGraphicManager.GetImage(resource)) { }
        public SASimpleSprite(string resource, Rectangle sourceRectangle, Vector2 position, Color color)
            : this(SAGraphicManager.GetImage(resource), sourceRectangle, position, color) { }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color);
        }
    }

    /// <summary>
    /// 简单的推荐使用样例
    /// </summary>
    public class TestSASimpleSprite : SASimpleSprite
    {
        static string _resource;
        static Rectangle _sourceRectangle;
        static TestSASimpleSprite()
        {
            _resource = "Template/TestSASimpleSprite";
            _sourceRectangle = new Rectangle(0, 0, 480, 800);
        }
        public TestSASimpleSprite()
            : base(SAGraphicManager.GetImage(_resource))
        {
            sourceRectangle = _sourceRectangle;
            position = Vector2.Zero;
            color = Color.White;
        }
        TestSASimpleSprite(string res) : base(res) { }
    }
}
