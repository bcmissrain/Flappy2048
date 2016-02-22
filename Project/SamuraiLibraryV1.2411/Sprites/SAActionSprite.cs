using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.Sprites
{
    public class SAActionSprite : SASimpleSprite
    {
        /// <summary>
        /// 推荐的做法是：
        /// 在子类中的静态方法中将这些数据进行初始化（当然要另外设置相应的静态变量）
        /// 在构造方法中直接就为_sourceRectangles & _sizes赋值
        /// </summary>
        public Rectangle[] _sourceRectangles;
        public Vector2[] _sizes;
        public int index;
        public int Count { get { return _sourceRectangles.Length; } }
        /// <summary>
        /// 直到此时，我才真正清楚原来Prop是如此的方便，还可以像方法一样被重写。
        /// </summary>
        public override Vector2 Size { get { return _sizes[index]; } } //获取index帧的Sprite的大小
        public override Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y); } }//获取index帧的Sprite的碰撞矩形
        public override Rectangle sourceRectangle { get { return _sourceRectangles[index]; } } //截取绘制图像位置
        /// <summary>
        /// 不怕麻烦就用默认构造函数
        /// </summary>
        public SAActionSprite() { }
        /// <summary>
        /// 默认动作第零帧的构造函数
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="sourceRectangles"></param>
        /// <param name="sizes"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public SAActionSprite(Texture2D texture, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color)
            : this(texture, sourceRectangles, sizes, position, color, 0) { }
        public SAActionSprite(Texture2D texture, Rectangle[] sourceRectangles, Vector2 position, Color color, int index)
        {
            this.texture = texture;
            this._sourceRectangles = sourceRectangles;
            this._sizes = new Vector2[this._sourceRectangles.Length];
            for (int i = 0; i < this._sourceRectangles.Length; i++)
            {
                this._sizes[i] = new Vector2(this._sourceRectangles[i].Width, this._sourceRectangles[i].Height);
            }
            this.position = position;
            this.color = color;
            this.index = index;
        }
        /// <summary>
        /// 最全的构造函数了
        /// </summary>
        /// <param name="texture">纹理</param>
        /// <param name="sourceRectangles">纹理矩形数组</param>
        /// <param name="sizes">大小数组</param>
        /// <param name="position">绘制位置</param>
        /// <param name="color">绘制颜色</param>
        /// <param name="index">动作的第几帧</param>
        public SAActionSprite(Texture2D texture, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color, int index)
        {
            this.texture = texture;
            this._sourceRectangles = sourceRectangles;
            this._sizes = sizes;
            this.position = position;
            this.color = color;
            this.index = index;
        }
        public SAActionSprite(string resource, Rectangle[] sourceRectangles, Vector2 position, Color color, int index)
        {
            this.texture = SAGraphicManager.GetImage(resource);
            this._sourceRectangles = sourceRectangles;
            this._sizes = new Vector2[this._sourceRectangles.Length];
            for (int i = 0; i < this._sourceRectangles.Length; i++)
            {
                this._sizes[i] = new Vector2(this._sourceRectangles[i].Width, this._sourceRectangles[i].Height);
            }
            this.position = position;
            this.color = color;
            this.index = index;
        }
        public SAActionSprite(string resource, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color, int index)
            : this(SAGraphicManager.GetImage(resource), sourceRectangles, sizes, position, color, index) { }
        public SAActionSprite(string resource, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color)
            : this(SAGraphicManager.GetImage(resource), sourceRectangles, sizes, position, color, 0) { }
        //因为属性被Override，Draw方法反而不用重写了
    }
}
