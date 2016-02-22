using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai.Sprites;

namespace Samurai
{
    public static class SAInput
    {
        static SAInput()
        {
            TouchPanel.EnabledGestures = GestureType.Tap;       //默认初始可以处理Tap，用于Button
            enable = true;
        }

        public delegate void OnInputDelegate(GestureSample gestureSample);      //处理手势的委托定义
        public delegate void OnBackButtonDelegate();        //处理back键的委托定义
        public delegate void OnTouchCollectionDelegate(TouchCollection touchCollection);

        private static OnBackButtonDelegate OnBackButton;        //点击Back键时Invoke的委托
        private static OnTouchCollectionDelegate OnTouchCollection;
        private static Dictionary<GestureType, OnInputDelegate> inputDictionary
            = new Dictionary<GestureType, OnInputDelegate>();       //手势与委托的字典
        private static List<SAControl> buttonList = new List<SAControl>();
        private static TouchCollection touchCollection { get; set; }
        private static bool enable { get; set; }

        #region 重置Input
        public static void ResetInput()
        {
            CleanGesture();
            CleanBackButton();
            CleanButton();
            CleanTouchCollection();
            TouchPanel.EnabledGestures = GestureType.Tap;       //默认初始可以处理Tap，用于Button
        }
        #endregion

        #region 处理底层触控接口
        public static void EnableTouchCollection(OnTouchCollectionDelegate temp)
        {
            OnTouchCollection = temp;
        }

        public static void CleanTouchCollection()
        {
            OnTouchCollection = null;
        }
        #endregion

        #region 处理手势
        public static void EnableGesture(GestureType gestureType, OnInputDelegate onInput)
        {
            if (!inputDictionary.ContainsKey(gestureType))
            {
                TouchPanel.EnabledGestures = TouchPanel.EnabledGestures | gestureType;
                inputDictionary.Add(gestureType, onInput);
            }
        }

        //ATTENTION 在去除手势的过程后，原本的foreach循环必须跳出
        public static void DisableGesture(GestureType gestureType)
        {
            if (inputDictionary.ContainsKey(gestureType))
            {
                inputDictionary.Remove(gestureType);
            }
            TouchPanel.EnabledGestures = GestureType.None;
            foreach (GestureType g in inputDictionary.Keys)
            {
                TouchPanel.EnabledGestures = TouchPanel.EnabledGestures | gestureType;
            }
        }

        public static void CleanGesture()
        {
            TouchPanel.EnabledGestures = GestureType.None;
            inputDictionary.Clear();
        }
        #endregion

        #region 处理Back按键
        public static void EnableBackButton(OnBackButtonDelegate onBackButton)
        {
            OnBackButton = onBackButton;
        }

        public static void DisableBackButton(OnBackButtonDelegate onBackButton)
        {
            OnBackButton -= onBackButton;
        }

        public static void CleanBackButton()
        {
            OnBackButton = null;
        }
        #endregion

        #region 处理Button
        public static void AddButton(SAControl button)
        {
            buttonList.Add(button);
        }

        public static void CleanButton()
        {
            buttonList.Clear();
        }

        public static void RemoveButton(SAControl button)
        {
            if (buttonList.Contains(button))
            {
                buttonList.RemoveAt(buttonList.IndexOf(button));
            }
        }
        #endregion

        #region 消息的分发
        //这个最简单，但也是效率最低的
        public static void UpdateInput()
        {
            UpdateBackButton();
            UpdateButton();
            UpdateTouchCollection();
            UpdateGesture();
        }

        public static void UpdateGesture()
        {
            if (enable)
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gestureSample = TouchPanel.ReadGesture();
                    //先处理预定义Button
                    if (gestureSample.GestureType == GestureType.Tap)
                    {
                        foreach (SAControl b in buttonList)
                        {
                            if (b.IfOnTouch(gestureSample.Position))
                            {
                                b.OnClick();
                                break;//ATTENTION 可以在此中删除Button
                            }
                        }
                    }

                    //处理手势事件
                    foreach (GestureType g in inputDictionary.Keys)
                    {
                        if (gestureSample.GestureType == g)
                        {
                            inputDictionary[g].Invoke(gestureSample);
                            break;  //ATTENTION
                        }
                    }
                }
            }
        }

        public static void UpdateTouchCollection()
        {
            if (enable)
            {
                touchCollection = TouchPanel.GetState();
                //先处理Button
                if (touchCollection.Count == 1)
                {
                    foreach (TouchLocation t in touchCollection)
                    {
                        if (t.State == TouchLocationState.Pressed || t.State == TouchLocationState.Moved)
                        {
                            foreach (SAControl b in buttonList)
                            {
                                b.OnTouch(t.Position);
                            }
                        }
                    }
                }

                if (OnTouchCollection != null)
                {
                    OnTouchCollection.Invoke(touchCollection);
                }
            }
        }

        public static void UpdateBackButton()
        {
            //处理Back的事件
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (OnBackButton != null)
                {
                    OnBackButton.Invoke();
                }
            }
        }

        public static void UpdateButton()
        {
            foreach (SAControl b in buttonList)
            {
                b.Update();
            }
        }
        #endregion

        #region 暂停与恢复(不针对BackButton)
        public static void Enable()
        {
            enable = true;
        }
        public static void Disable()
        {
            enable = false;
        }
        #endregion
    }
}
