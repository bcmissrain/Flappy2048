using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public enum Theme
    {
        Mario_5C,
        Mario_5S,
    };
    public static class SAGameData
    {
        //0
        public static int MAX_START_LEVEL = 10000;
        //1
        public static int MAX_SPRING_LEVEL = 20000 - 1600;
        public static int MAX_BEFORESUMMER_LEVEL = 20000;
        //2
        public static int MAX_SUMMER_LEVEL = 30000 - 1600;
        public static int MAX_BEFOREAUTUMN_LEVEL = 30000;
        //3
        public static int MAX_AUTUMN_LEVEL = 40000 - 1600;
        public static int MAX_BEFOREWINTER_LEVEL = 40000;
        //4
        public static int MAX_WINTER_LEVEL = 50000 - 1600;
        //5
        public static int MAX_BEFOREDOODLE_LEVEL = 50000;
        public static int MAX_DOODLE_LEVEL = 65536;

        private const string RECORD = "Record";
        //主题设置
        public static Theme gTheme;
        //最高分
        public static int gBestScore;
        //当前高度（注意被重置的时机）,用于设置
        public static int gCurrentHeight;
        //当前的显示高度
        public static string gCurrentHeightString { get { return (gCurrentHeight / 10).ToString(); } }
        //当前状态剩余时间
        public static int gRestTime;
        //当前的金币数目：
        public static int gCoin;

        static SAGameData()
        {
            if (SAStorage.Contains(RECORD) == false)
            {
                SAStorage.SaveData(RECORD, "0");
            }
            ReadRecord();
            gTheme = Theme.Mario_5C;
        }

        public static void SaveRecord()
        {
            if (IfNewRecord())
            {
                gBestScore = gCurrentHeight / 10;
                SAStorage.SaveData(RECORD, gBestScore.ToString());
            }
        }

        public static void ReadRecord()
        {
            string temp = SAStorage.ReadData(RECORD);
            if (temp != null)
            {
                gBestScore = int.Parse(temp);
            }
            else
            {
                gBestScore = 0;
                SAStorage.SaveData(RECORD, gBestScore.ToString());
            }
        }

        public static bool IfNewRecord()
        {
            return gCurrentHeight / 10 >= gBestScore ? true : false;
        }

        public static void CleanData()
        {
            gCurrentHeight = 0;
            gRestTime = -1;
            gCoin = 0;
        }
        public static bool IfTheme5C()
        {
            return gTheme == Theme.Mario_5C ? true : false;
        }
    }
}
