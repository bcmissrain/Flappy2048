using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
namespace Flappy2048
{
    class Block
    {
        SASimpleSprite block0;
        SASimpleSprite block1;
        public Block()
        {
            block0 = new SASimpleSprite("Images/block");
            block1 = new SASimpleSprite("Images/block");
        }
        public void Update(Bird bird)
        {
            if (IfCollide(bird.rectangle))
            {
                if (bird.index > 0)
                {
                    SAMusicManager.PlaySoundEffect("Sound/Collide");
                }
                bird.Clean();
            }
            MoveLeft(GameData.MOVESPEED);
            if (CanRelease())
            {
                SAMusicManager.PlaySoundEffect("Sound/Coin");
                bird.Add();
                Reset();
            }
        }
        public void SetLeft(int x) 
        {
            block0.position.X = GameData.LEFT_TOP_X + 5 + x;
            block1.position.X = GameData.LEFT_TOP_X + 5 + x;
        }
        public void MoveLeft(int speed)
        {
            block0.position.X -= speed;
            block1.position.X -= speed;
        }
        public void Draw()
        {
            block0.Draw();
            block1.Draw();
        }
        public bool IfCollide(Rectangle rect)
        {
            if (block0.IfCollide(rect))
            {
                return true;
            }
            if (block1.IfCollide(rect))
            {
                return true;
            }
            return false;
        }
        public bool CanRelease()
        {
            if (block0.position.X + block0.Size.X <= 0)
            {
                return true;
            }
            return false;
        }
        public void Reset()
        {
            int temp = GameData.random.Next() % 3;
            if (temp == 0)
            {
                block0.position.Y = GameData.LEFT_TOP_Y + 5;
                block1.position.Y = GameData.LEFT_TOP_Y + 333 + 5;
            }
            else if (temp == 1)
            {
                block0.position.Y = GameData.LEFT_TOP_Y + 5;
                block1.position.Y = GameData.LEFT_TOP_Y + 111 + 5;
            }
            else
            {
                block0.position.Y = GameData.LEFT_TOP_Y + 222 + 5;
                block1.position.Y = GameData.LEFT_TOP_Y + 333 + 5;
            }
            MoveLeft(-666);
        }
    }
}
