using Microsoft.Phone.Tasks;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flappy2048
{
    static class GameData
    {
        const string SCORE_KEY = "SCORE";
        public static Random random = new Random();
        public const int LEFT_TOP_X = 18;
        public const int LEFT_TOP_Y = 125;
        public const int BORDER_WIDTH = 444;
        public const int MOVESPEED = 5;
        public static int gBestScore;

        public static void SetBestScore(string score)
        {
            if (int.Parse(score) > gBestScore)
            {
                gBestScore = int.Parse(score);
                SAStorage.SaveData(SCORE_KEY, gBestScore.ToString());
            }
        }
        public static void ReadScore()
        {
            if (SAStorage.Contains(SCORE_KEY))
            {
                gBestScore = int.Parse(SAStorage.ReadData(SCORE_KEY));
            }
            else
            {
                SAStorage.SaveData(SCORE_KEY, "0");
                gBestScore = int.Parse(SAStorage.ReadData(SCORE_KEY));
            }
        }

        /// <summary>
        /// 评价游戏
        /// 自动存储，以后不重复了
        /// </summary>
        public static void RateGame()
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
    }
}
