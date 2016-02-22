using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public abstract class SAScreen
    {
        protected static Random random { get; set; }
        public bool IfReady { get; set; } //页面资源是否加载完毕
        public virtual bool Releasable { get; set; } //页面是否回收利用
        public ContentManager ScreenContent { get; set; }
        public delegate void ChangeScreenDelegate(ScreenType screenType);
        public static ChangeScreenDelegate ChangeScreenTo; //暂时获得上下文的管理权限,记住，这里可是static的，好用极了~
        static SAScreen()
        {
            random = new Random();
        }
        public SAScreen() { }
        public SAScreen(ChangeScreenDelegate changeScreenDelegate)
        {
            SAScreen.ChangeScreenTo = changeScreenDelegate;
        }

        #region 与SADirector的接口 不可复写
        public void BaseLoadContent()
        {
            CreateContentManager();
            LoadContent();
            SetupInput();
            Init();
            Ready();
        }

        public void RegistCotent()
        {
            CreateContentManager();
            LoadContent();
            //
            Init();
            Ready();
        }

        public void BaseUpdate(GameTime gameTime)
        {
            //Input的更新隐藏在了Screen基类中的BaseUpdate中
            SAInput.UpdateInput();
            Update(gameTime);
        }

        public void BaseDraw(GameTime gameTime)
        {
            //所有页面中的绘制再也不用SpriteBatch.Begin和End了
            SAGlobal.spriteBatch.Begin();
            Draw(gameTime);
            SAGlobal.spriteBatch.End();
        }

        public void BaseUnloadContent()
        {
            //会自动调用Input的清空
            UnloadContent();
            if (Releasable)
            {
                ReleaseResource();
            }
        }

        public void ReInit()
        {
            SetupInput();
            Init();
        }

        protected void Ready()
        {
            this.IfReady = true;
        }
        #endregion

        #region 子类复写
        protected void CreateContentManager()
        {
            ScreenContent = SAGlobal.CreateContentManager();
        }

        public virtual void LoadContent()
        {
            //TODO
        }

        public virtual void Init()
        {
            //TODO
        }

        public virtual void Update(GameTime gameTime)
        {
            //TODO
        }

        //只管画，不管Begin和End
        public virtual void Draw(GameTime gameTime)
        {
            //TODO
        }

        public virtual void UnloadContent()
        {
            //如果子类override就更好了，不过base.UnloadContent
            SAInput.ResetInput();
        }

        public virtual void SetupInput()
        {
            //TODO
        }

        /// <summary>
        /// 释放素材资源
        /// 不自动调用
        /// </summary>
        public virtual void ReleaseResource()
        {
            ScreenContent.Unload();
            //TODO
        }
        #endregion

        #region 辅助类
        /// <summary>
        /// 加载图像资源，页面销毁时释放
        /// </summary>
        public void LoadTemporalImage(string name)
        {
            ScreenContent.Load<Texture2D>(name);
        }
        /// <summary>
        /// 获取图像资源，页面销毁时释放
        /// </summary>
        public Texture2D GetTemporalImage(string name)
        {
            return ScreenContent.Load<Texture2D>(name);
        }
        #endregion
    }
}
