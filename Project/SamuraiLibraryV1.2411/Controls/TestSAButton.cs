using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai.Sprites
{
    /// <summary>
    /// Button的子类，使用Button的样例
    /// 不过实际使用的时候也可以不这样用
    /// </summary>
    public class TestSAButton : SAButton
    {
        static string init_resource;
        static Rectangle[] init_sourceRectangles;
        static Vector2 init_position;
        static TestSAButton()
        {
            init_resource = "Template/TestSAButton";
            init_sourceRectangles = new Rectangle[2];
            init_sourceRectangles[0] = new Rectangle(0, 0, 148, 36);
            init_sourceRectangles[1] = new Rectangle(200, 0, 148, 36);
            init_position = new Vector2(136, 300);
        }
        public TestSAButton(ClickDelegate clickDel)
            : base(init_resource, init_sourceRectangles, init_position, clickDel)
        {
        }//记住，在基类中已经注册了
    }
}
