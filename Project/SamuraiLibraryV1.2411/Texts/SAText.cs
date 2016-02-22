using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai//.Texts
{
    public class SAText
    {
        public enum Type
        {
            Horizental,
            Vertical,
            Both,
            None
        }
        //默认边框1px
        protected int BORDER = 1;

        #region 基本属性
        public Vector2 Position { get; set; }
        public Color TextColor { get; set; }
        public SpriteFont Font { get; set; }
        public string TextString { get; set; }
        public Vector2 TextSize { get; set; }
        public Color BorderColor { get; set; }
        bool IfHaveBorder { get; set; }
        #endregion

        #region 构造函数 多态
        public SAText() { }
        public SAText(SpriteFont font, string text, Color textColor, Vector2 pos) : this(font, text, textColor, Color.White, false, Type.None, pos, Rectangle.Empty) { } //无边框，直接放
        public SAText(SpriteFont font, string text, Color textColor, Rectangle rec) : this(font, text, textColor, Color.White, true, Type.Both, Vector2.Zero, rec) { }//右边框，参考位置
        public SAText(SpriteFont font, string text, Color textColor, Color borderColor, bool ifHaveBorder, Type type, Vector2 pos, Rectangle displayArea)
        {
            this.Font = font;
            this.TextString = text;
            this.Position = pos;
            this.TextColor = textColor;
            this.BorderColor = borderColor;
            this.IfHaveBorder = ifHaveBorder;
            CenterText(type, displayArea);
        }
        #endregion
        //文字对齐
        private void CenterText(Type type, Rectangle displayArea)
        {
            //ATTENTION（在此设置文字的大小）
            TextSize = Font.MeasureString(TextString);
            int x = (int)Position.X;
            int y = (int)Position.Y;
            switch (type)
            {
                case Type.None:
                    break;
                case Type.Both:
                    x = (int)((displayArea.Width - TextSize.X) / 2) + displayArea.X;
                    y = (int)((displayArea.Height - TextSize.Y) / 2) + displayArea.Y;
                    break;
                case Type.Horizental:
                    x = (int)((displayArea.Width - TextSize.X) / 2) + displayArea.X;
                    break;
                case Type.Vertical:
                    x = (int)((displayArea.Width - TextSize.X) / 2) + displayArea.X;
                    break;
            }
            this.Position = new Vector2(x, y);
        }
        //绘制
        public void Draw()
        {
            Draw(SAGlobal.spriteBatch);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //绘制边框
            if (IfHaveBorder)
            {
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(0, BORDER), BorderColor); //下
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(0, BORDER), BorderColor);  //上
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(BORDER, 0), BorderColor);  //右
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(BORDER, 0), BorderColor);   //左
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(BORDER, BORDER), BorderColor); //右下
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(BORDER, BORDER), BorderColor); //左上
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(-BORDER, BORDER), BorderColor); //左下
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(-BORDER, BORDER), BorderColor); //右上
            }
            spriteBatch.DrawString(Font, TextString, Position, TextColor);
        }
        //文字矩形
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)TextSize.X, (int)TextSize.Y);
        }
    }
}
