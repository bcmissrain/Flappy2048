using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai.Sprites
{
    /// <summary>
    /// Sprite的基类
    /// 绘制整个texture
    /// </summary>
    public abstract class SASprite
    {
        public Texture2D texture;
        public virtual Color color { get; set; }
        public Vector2 position;

        public virtual Vector2 Size { get { return new Vector2(rectangle.Width, rectangle.Height); } } //获取Sprite的大小
        public virtual Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height); } }//获取Sprite的碰撞矩形
        public virtual Rectangle sourceRectangle { get; set; } //截取绘制图像位置

        public SASprite() { }
        public SASprite(Texture2D texture) : this(texture, Vector2.Zero) { }
        public SASprite(Texture2D texture, Vector2 positon) : this(texture, (int)positon.X, (int)positon.Y) { }
        public SASprite(Texture2D texture, int pos_x, int pos_y)
        {
            this.texture = texture;
            this.position = new Vector2(pos_x, pos_y);
            this.color = Color.White;
        }
        //判断是否有相交部分
        public virtual bool IfCollide(Rectangle rect)
        {
            return rectangle.Intersects(rect);
        }
        //外部调用的Draw接口
        public void Draw()
        {
            Draw(SAGlobal.spriteBatch);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
