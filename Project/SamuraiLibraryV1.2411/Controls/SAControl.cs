using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai.Sprites
{
    public enum ControlStatus
    {
        //For SAButton
        Normal,
        Touch,
        Release,

        //For SASwitchButton
        On,
        OnTouch,
        Off,
        OffTouch
    }
    /// <summary>
    /// 控件基类，继承自ActionSprite
    /// </summary>
    public abstract class SAControl : SAActionSprite
    {
        //控件的状态，初始为Normal
        protected ControlStatus controlStatus;
        //关于响应点击的委托
        public delegate void ClickDelegate();
        protected ClickDelegate ClickHandler;
        public delegate void SwitchDelegate(ControlStatus switchStatus);
        protected SwitchDelegate switchHandler;
        //控件是否开启，默认开启
        public bool Enable { get; set; }

        #region 初始化
        public virtual void Init()
        {
            this.index = 0;
            this.Enable = true;
            this.color = Color.White;
        }
        #endregion

        #region 注册和注销（注意不能在响应方法中调用，因为Foreach中不可以增减）
        public void Add()
        {
            SAInput.AddButton(this);
        }
        public void Remove()
        {
            SAInput.RemoveButton(this);
        }
        #endregion

        #region 与SAInput之间的接口
        /// <summary>
        /// 更新时，先重置状态为Normal,不过子类要重写（index的修改）
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// 更新时发现被触碰了
        /// </summary>
        public virtual void OnTouch(Vector2 touchPos) { }
        /// <summary>
        /// 辅助判断是否被触碰到
        /// </summary>
        /// <param name="touchPosition"></param>
        /// <returns></returns>
        public bool IfOnTouch(Vector2 touchPosition)
        {
            if (touchPosition.X >= position.X && touchPosition.X <= position.X + Size.X
                && touchPosition.Y >= position.Y && touchPosition.Y <= position.Y + Size.Y)
            {
                return true;
            }
            return false;
        }
        public bool IfOnTouch(GestureSample gesture)
        {
            return IfOnTouch(gesture.Position);
        }
        #endregion

        #region 响应点击事件
        public virtual void OnClick() { }
        #endregion
    }
}
