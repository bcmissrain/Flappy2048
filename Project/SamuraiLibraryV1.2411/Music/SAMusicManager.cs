using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai//.Music
{
    public static class SAMusicManager
    {
        //音乐/音效集合
        static Dictionary<string, Song> songList = new Dictionary<string, Song>();
        static Dictionary<string, SoundEffect> soundList = new Dictionary<string, SoundEffect>();
        //依赖项 用于加载
        private static ContentManager Content { get; set; }
        //标志
        public static bool IfPlayMusic { get; set; }
        public static bool IfPlaySound { get; set; }
        private static float SongVolume { get; set; }
        private static float SoundEffectVolume { get; set; }

        //使用前需要先注册给SADirector
        public static void Setup(ContentManager content)
        {
            SAMusicManager.Content = content;
            //IfPlaySound = IfPlayMusic = CanPlayMusic();
            IfPlaySound = IfPlayMusic = true;
            SetVolume(1);
        }

        public static void Setup()
        {
            Setup(SAGlobal.Content);
        }

        #region Music总开关
        public static void DisableAll()
        {
            EnableSoundEffect(false);
            if (CanPlayMusic())
            {
                StopSong();
            }
        }

        public static void EnableAll(string name)
        {
            PlaySong(name);
            EnableSoundEffect(true);
        }
        #endregion

        #region 加载音乐资源相关
        public static void LoadSong(string resource)
        {
            if (!songList.ContainsKey(resource))
            {
                songList.Add(resource, Content.Load<Song>(resource));
            }
        }
        public static void LoadSoundEffect(string resource)
        {
            if (!soundList.ContainsKey(resource))
            {
                soundList.Add(resource, Content.Load<SoundEffect>(resource));
            }
        }
        #endregion

        #region 播放音乐相关
        public static void PlaySong(string name)
        {
            PlaySong(name, true);
        }
        public static void PlaySong(string name, bool ifLoop)
        {
            if (!songList.ContainsKey(name))
            {
                songList.Add(name, Content.Load<Song>(name));
            }
            MediaPlayer.IsRepeating = ifLoop;
            MediaPlayer.Volume = SongVolume;
            MediaPlayer.Play(songList[name]);
            IfPlayMusic = true;
        }
        public static void RemoveSong(string name)
        {
            if (songList.ContainsKey(name))
            {
                songList.Remove(name);
            }
        }
        public static bool CanPlayMusic()
        {
            return MediaPlayer.GameHasControl;
        }
        public static void PauseSong()
        {
            MediaPlayer.Pause();
            IfPlayMusic = false;
        }
        public static void StopSong()
        {
            MediaPlayer.Stop();
            IfPlayMusic = false;
        }
        public static void ResumeSong()
        {
            MediaPlayer.Resume();
            IfPlayMusic = true;
        }
        public static bool IfPlayingSong()
        {
            return MediaPlayer.State == MediaState.Playing ? true : false;
        }
        #endregion

        #region 播放音效相关
        public static void EnableSoundEffect(bool soundOn)
        {
            SAMusicManager.IfPlaySound = soundOn;
        }
        public static void PlaySoundEffect(string name)
        {
            if (IfPlaySound)
            {
                if (!soundList.ContainsKey(name))
                {
                    soundList.Add(name, Content.Load<SoundEffect>(name));
                }
                SoundEffectInstance temp = soundList[name].CreateInstance();
                temp.Volume = SoundEffectVolume;
                temp.Play();
            }
        }
        public static void RemoveSoundEffect(string name)
        {
            if (soundList.ContainsKey(name))
            {
                soundList.Remove(name);
            }
        }
        #endregion

        #region 设置音量大小
        public static void SetVolume(float v)
        {
            SetSongVolume(v);
            SetSoundEffectVolume(v);
        }
        public static void SetSongVolume(float v)
        {
            if (v <= 1.0 && v >= 0.0)
            {
                SongVolume = v;
            }
        }
        public static void SetSoundEffectVolume(float v)
        {
            if (v <= 1.0 && v >= 0.0)
            {
                SoundEffectVolume = v;
            }
        }
        #endregion
    }
}
