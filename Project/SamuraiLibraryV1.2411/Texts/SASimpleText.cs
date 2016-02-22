using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public class SASimpleText
    {
        protected int BORDER = 3;
        public Vector2 position;
        protected SpriteFont font;
        protected string textString;

        public SASimpleText(string fontStr, int scoreNum, Vector2 position)
            : this(fontStr, scoreNum.ToString(), position) { }
        public SASimpleText(string fontStr, string scoreStr, Vector2 position)
        {
            this.textString = scoreStr;
            this.position = position;
            this.font = SAGlobal.Content.Load<SpriteFont>(fontStr);
        }
        //绘制
        public void Draw()
        {
            Draw(SAGlobal.spriteBatch);
        }
        protected void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, textString, position + new Vector2(0, BORDER), Color.White); //下
            spriteBatch.DrawString(font, textString, position - new Vector2(0, BORDER), Color.White);  //上
            spriteBatch.DrawString(font, textString, position + new Vector2(BORDER, 0), Color.White);  //右
            spriteBatch.DrawString(font, textString, position - new Vector2(BORDER, 0), Color.White);   //左
            spriteBatch.DrawString(font, textString, position + new Vector2(BORDER, BORDER), Color.White); //右下
            spriteBatch.DrawString(font, textString, position - new Vector2(BORDER, BORDER), Color.White); //左上
            spriteBatch.DrawString(font, textString, position + new Vector2(-BORDER, BORDER), Color.White); //左下
            spriteBatch.DrawString(font, textString, position - new Vector2(-BORDER, BORDER), Color.White); //右上
            spriteBatch.DrawString(font, textString, position, Color.Black);
        }
    }
}
