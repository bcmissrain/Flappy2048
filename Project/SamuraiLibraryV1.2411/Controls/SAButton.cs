using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Samurai.Sprites
{
    /// <summary>
    /// 所有Button的基类
    /// </summary>
    public class SAButton : SAControl
    {
        /// <summary>
        /// 最简单，一张图片搞定所有
        /// </summary>
        public SAButton(string resource, Rectangle sourceRect, Vector2 pos, ClickDelegate clickDel)
        {
            Init();
            //TODO
            this.texture = SAGraphicManager.GetImage(resource);
            this._sourceRectangles = new Rectangle[3];
            this._sourceRectangles[2] = this._sourceRectangles[1] = this._sourceRectangles[0] = sourceRect;
            this._sizes = new Vector2[3];
            this._sizes[2] = this._sizes[1] = this._sizes[0] = new Vector2(sourceRect.Width, sourceRect.Height);
            this.position = pos;
            this.ClickHandler = clickDel;
            Add();
        }
        /// <summary>
        /// 最全面
        /// </summary>
        public SAButton(string resource, Rectangle[] sourceRects, Vector2 pos, ClickDelegate clickDel)
        {
            Init();
            //TODO
            this.texture = SAGraphicManager.GetImage(resource);
            this._sourceRectangles = new Rectangle[3];
            this._sizes = new Vector2[3];
            this.position = pos;
            this.ClickHandler = clickDel;
            #region 处理不同的数量
            if (sourceRects.Length == 3)
            {
                this._sourceRectangles = sourceRects;
                for (int i = 0; i < 3; i++)
                {
                    this._sizes[i] = new Vector2(_sourceRectangles[i].Width, _sourceRectangles[i].Height);
                }
            }
            else if (sourceRects.Length == 2)
            {
                this._sourceRectangles[2] = this._sourceRectangles[0] = sourceRects[0];
                this._sourceRectangles[1] = sourceRects[1];
                for (int i = 0; i < 3; i++)
                {
                    this._sizes[i] = new Vector2(_sourceRectangles[i].Width, _sourceRectangles[i].Height);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    this._sourceRectangles[i] = sourceRects[0];
                    this._sizes[i] = new Vector2(_sourceRectangles[i].Width, _sourceRectangles[i].Height);
                }
            }
            #endregion
            Add();
        }

        #region 流程控制
        public override void Init()
        {
            this.controlStatus = ControlStatus.Normal;
            base.Init();
        }
        public override void Update()
        {
            if (this.Enable)
            {
                //TODO
                index = 0;
                this.controlStatus = ControlStatus.Normal;
            }
        }
        public override void OnClick()
        {
            //TODO
            if (this.Enable)
            {
                if (ClickHandler != null)
                {
                    index = 2;
                    this.controlStatus = ControlStatus.Release;
                    ClickHandler.Invoke();
                }
            }
        }
        public override void OnTouch(Vector2 touchPos)
        {
            if (this.Enable)
            {
                //TODO
                if (IfOnTouch(touchPos))
                {
                    this.controlStatus = ControlStatus.Touch;
                    index = 1;
                }
            }
        }
        /// <summary>
        /// 由于状态的改变以及index的变化进行了修改，所以没有必要重写Draw了
        /// </summary>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        #endregion
    }
}
