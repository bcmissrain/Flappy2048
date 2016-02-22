using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Samurai.Sprites
{
    /// <summary>
    /// 开关按钮
    /// 有四种状态：On、OnTouch、Off和OffTouch，默认On
    /// </summary>
    public class SASwitchButton : SAControl
    {
        /// <summary>
        /// 最全的,想偷懒的话也有技巧
        /// sourceRects长度为2，则开关各一个
        /// 为3，则开关为0,2,1为Touch
        /// 为4，你懂的
        /// </summary>
        public SASwitchButton(string resource, Rectangle[] sourceRects, Vector2 pos, SwitchDelegate switchDel)
            : this(resource, sourceRects, pos, switchDel, true) { }
        public SASwitchButton(string resource, Rectangle[] sourceRects, Vector2 pos, SwitchDelegate switchDel, bool ifOn)
        {
            Init();
            //TODO
            if (ifOn)
            {
                index = 0;
                this.controlStatus = ControlStatus.On;
            }
            else
            {
                index = 2;
                this.controlStatus = ControlStatus.Off;
            }
            this.texture = SAGraphicManager.GetImage(resource);
            this._sourceRectangles = new Rectangle[4];
            this._sizes = new Vector2[4];
            this.position = pos;
            this.switchHandler = switchDel;
            #region 处理不同的数量
            if (sourceRects.Length == 2)
            {
                this._sourceRectangles[1] = this._sourceRectangles[0] = sourceRects[0];
                this._sourceRectangles[3] = this._sourceRectangles[2] = sourceRects[1];
                for (int i = 0; i < 4; i++)
                {
                    this._sizes[i] = new Vector2(_sourceRectangles[i].Width, _sourceRectangles[i].Height);
                }
            }
            else if (sourceRects.Length == 3)
            {
                this._sourceRectangles[0] = sourceRects[0];
                this._sourceRectangles[1] = this._sourceRectangles[3] = sourceRects[1];
                this._sourceRectangles[2] = sourceRects[2];
                for (int i = 0; i < 4; i++)
                {
                    this._sizes[i] = new Vector2(_sourceRectangles[i].Width, _sourceRectangles[i].Height);
                }
            }
            else if (sourceRects.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    this._sourceRectangles[i] = sourceRects[i];
                    this._sizes[i] = new Vector2(_sourceRectangles[i].Width, _sourceRectangles[i].Height);
                }
            }
            #endregion
            Add();
        }

        #region 流程控制
        public override void Update()
        {
            if (Enable)
            {
                if (controlStatus == ControlStatus.On || controlStatus == ControlStatus.OnTouch)
                {
                    controlStatus = ControlStatus.On;
                    index = 0;
                }
                else if (controlStatus == ControlStatus.Off || controlStatus == ControlStatus.OffTouch)
                {
                    controlStatus = ControlStatus.Off;
                    index = 2;
                }
            }
        }
        public override void OnClick()
        {
            //TODO
            if (this.Enable)
            {
                if (switchHandler != null)
                {
                    //根据当前的状态进行处理
                    switchHandler.Invoke(controlStatus);
                    SwitchStatus();
                }
            }
        }
        public override void OnTouch(Vector2 touchPos)
        {
            //TODO
            if (this.Enable)
            {
                if (IfOnTouch(touchPos))
                {
                    if (controlStatus == ControlStatus.On)
                    {
                        this.controlStatus = ControlStatus.OnTouch;
                        index = 1;
                    }
                    else if (controlStatus == ControlStatus.Off)
                    {
                        this.controlStatus = ControlStatus.OffTouch;
                        index = 3;
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// 点击时转换状态
        /// </summary>
        protected void SwitchStatus()
        {
            if (controlStatus == ControlStatus.On || controlStatus == ControlStatus.OnTouch)
            {
                this.controlStatus = ControlStatus.Off;
                index = 2;
            }
            else if (controlStatus == ControlStatus.Off || controlStatus == ControlStatus.OffTouch)
            {
                this.controlStatus = ControlStatus.On;
                index = 0;
            }
        }
    }
}
