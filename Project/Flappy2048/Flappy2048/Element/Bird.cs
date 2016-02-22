using Microsoft.Xna.Framework;
using Samurai;
using Samurai.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flappy2048
{
    class Bird:SAActionSprite
    {
        #region ÌøÔ¾Ïà¹Ø
        const int GRAVITY = 1;
        const int JUMP_SPEED = 12;
        const int MAX_DOWN_SPEED = 20;
        int currentSpeed;
        #endregion

        public bool IfWin { get { return MAX_NUM - 1 == index ? true : false; } }
        const int MAX_NUM = 12;
        string[] scores;
        public String Score { get { return scores[index]; } }
        public Bird()
        {
            this.texture = SAGraphicManager.GetImage("Images/tile");
            this._sourceRectangles = new Rectangle[MAX_NUM];
            this._sizes = new Vector2[MAX_NUM];
            this.scores = new string[MAX_NUM];
            for (int i = 0; i < MAX_NUM; i++)
            {
                scores[i] = Math.Pow(2, i).ToString();
                this._sourceRectangles[i] = new Rectangle(10+(i%4)*105,10+(i/4)*110,101,101);
            }
            for (int i = 0; i < MAX_NUM; i++)
            {
                this._sizes[i] = new Vector2(this._sourceRectangles[i].Width, this._sourceRectangles[i].Height);
            }
            this.position = new Vector2(5+GameData.LEFT_TOP_X,5+222+GameData.LEFT_TOP_Y);
            this.color = Color.White;
            this.index = 0;
        }
        public void Update()
        {
            UpdateY();
        }
        void UpdateY()
        {
            position.Y += currentSpeed;
            if (position.Y + Size.Y > GameData.LEFT_TOP_Y + GameData.BORDER_WIDTH)
            {
                if (index > 0)
                {
                    SAMusicManager.PlaySoundEffect("Sound/Collide");
                }
                Clean();
                Fly();
            }
            else
            {
                if (position.Y < GameData.LEFT_TOP_Y)
                {
                    position.Y = GameData.LEFT_TOP_Y;
                }
                currentSpeed += GRAVITY;
                if (currentSpeed > MAX_DOWN_SPEED)
                {
                    currentSpeed = MAX_DOWN_SPEED;
                }
            }
        }
        public void Add()
        {
            this.index++;
            if (this.index > MAX_NUM - 1)
            {
                this.index = MAX_NUM - 1;
            }
        }
        public void Clean()
        {
            GameData.SetBestScore(Score);
            this.index = 0;
        }
        public void Fly()
        {
            this.currentSpeed = -JUMP_SPEED;
            SAMusicManager.PlaySoundEffect("Sound/Jump");
        }
    }
}
