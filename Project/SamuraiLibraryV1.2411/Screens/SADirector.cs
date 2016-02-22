using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace Samurai
{
    public abstract class SADirector
    {
        //常用的引用
        protected static Game game;
        protected static GraphicsDeviceManager graphics;
        protected static SpriteBatch spriteBatch;
        protected static ContentManager content;
        //前一个屏幕
        protected static ScreenType previousScreenType;
        protected static SAScreen previousScreen;
        //当前屏幕
        protected static ScreenType currentScreenType;
        protected static SAScreen currentScreen;
        //下一个屏幕
        protected static ScreenType nextScreenType;
        protected static SAScreen nextScreen;
        public static SAScreen NextScreen { get { return nextScreen; } }
        public static ScreenType NextScreenType { get { return nextScreenType; } }
        //内存缓存
        protected static Dictionary<ScreenType, SAScreen> screenDictionary;
        //屏幕工厂委托
        public delegate SAScreen CreateScreen();
        public SADirector(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            SADirector.game = game;
            SADirector.content = game.Content;
            SADirector.graphics = graphics;
            SADirector.spriteBatch = spriteBatch;
            screenDictionary = new Dictionary<ScreenType, SAScreen>();
            //注册 Global
            SAGlobal.Setup(game, spriteBatch, graphics);
            //注册MusicManager
            SAMusicManager.Setup();
            //ATTENTION 加载第一个页面
            LoadFirstScreen();
        }

        #region 程序资源初始化
        /// <summary>
        /// 加载全局的页面资源
        /// 这里的加载是单线程的
        /// </summary>
        public virtual void LoadFirstScreen()
        {
            //TODO
        }
        #endregion

        #region 与Game的接口
        public void Update(GameTime gameTime)
        {
            currentScreen.BaseUpdate(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            //game.GraphicsDevice.Clear(Color.CadetBlue);
            //TODEL
            if (previousScreen != null)
            {
                previousScreen.BaseDraw(gameTime);
            }
            if (nextScreen != null)
            {
                //if (nextScreen.IfReady) 不仅仅是Ready
                if (CanDrawNextScreen())
                {
                    nextScreen.BaseDraw(gameTime);
                }
            }
            currentScreen.BaseDraw(gameTime);
        }

        public virtual void UnloadContent() { }

        public virtual void OnActivated() { }
        #endregion

        #region 切换页面（SAScreen需要知道）
        /// <summary>
        /// 暴露给Screen的方法，Screen通过ChangeScreenTo实现状态模式
        /// </summary>
        /// <param name="screenType"></param>
        public virtual void ChangeScreenTo(ScreenType screenType) { }
        /// <summary>
        /// 用于回收再利用
        /// 默认调用了LoadContent以及Reinit
        /// 注意的是：screenType不能为Between
        /// </summary>
        /// <param name="screenType">屏幕种类</param>
        /// <param name="createScreen">生成屏幕的方法</param>
        public void ChangeScreenTo(ScreenType screenType, CreateScreen createScreen)
        {
            if (currentScreen != null)
            {
                previousScreenType = currentScreenType;
                previousScreen = currentScreen;
                //注意，一定要先释放Input资源，并且注意不要重复释放！
                previousScreen.UnloadContent();
            }
            if (!screenDictionary.ContainsKey(screenType))
            {
                /*
                #region 设置新页面
                screenDictionary.Add(screenType, createScreen());
                new Thread(new ThreadStart(screenDictionary[screenType].RegistCotent)).Start();
                nextScreenType = screenType;
                nextScreen = screenDictionary[screenType];
                #endregion

                #region 设置Between页面
                //设置BetweenScreen。
                //默认存在BetweenScreen
                currentScreenType = ScreenType.Between;
                currentScreen = screenDictionary[ScreenType.Between];
                currentScreen.ReInit();
                #endregion
                */
                //为了省事，这是老方法
                screenDictionary.Add(screenType, createScreen());
                currentScreenType = screenType;
                currentScreen = screenDictionary[screenType];
                currentScreen.BaseLoadContent();
            }
            else
            {
                #region 直接加载新页面
                currentScreenType = screenType;
                currentScreen = screenDictionary[screenType];
                //重新启动
                currentScreen.ReInit();
                #endregion

                #region 清理资源
                ReleaseScreen();
                //重置nextScreen
                nextScreen = null;
                #endregion
            }
        }
        /// <summary>
        /// 只有在加载页面动画完成后才释放资源
        /// </summary>
        public static void ReleaseScreen()
        {
            if (previousScreen != null)
            {
                //重置资源
                //特别注意的是，释放Input资源应该在加载新页面之前！
                SAGlobal.CleanTemporalContent();
                if (previousScreen.Releasable)
                {
                    screenDictionary.Remove(previousScreenType);
                }
                //重置指针
                previousScreen = null;
            }
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 页面是否加载完毕
        /// </summary>
        /*
        public bool IfScreenReady(ScreenType screenType)
        {
            if (screenDictionary.ContainsKey(screenType))
            {
                return screenDictionary[screenType].IfReady;
            }
            return false;
        }
         */
        /// <summary>
        /// 页面是否加载完毕并且可绘制
        /// </summary>
        public bool CanDrawNextScreen()
        {
            return ((SABetweenScreen)screenDictionary[ScreenType.Between]).CanDrawNextScreen;
        }
        /// <summary>
        /// 注册页面
        /// </summary>
        public static SAScreen RegistScreen(ScreenType screenType, CreateScreen createScreen)
        {
            screenDictionary.Add(screenType, createScreen());
            screenDictionary[screenType].RegistCotent();
            return screenDictionary[screenType];
        }
        #endregion

        #region 创建界面(工厂模式)
        #endregion
    }
}