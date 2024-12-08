using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class AudioManager
    {
        private static Song _currentBackgroundMusic;
        public static SoundEffect _soundEffect;
        private static float _defaultBackgroundMusicVolume = 0.02f;
        private static float _defaultSoundEffectVolume = 0.1f;

        public static void LoadContent()
        {
            _currentBackgroundMusic = ResourceManager.GetMusic("BackgroundMusic");
            MediaPlayer.Play(_currentBackgroundMusic);
            MediaPlayer.Volume = _defaultBackgroundMusicVolume;
            MediaPlayer.IsRepeating = true;
        }
        public static void Update(GameTime gameTime)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.Playing:
                    break;
                case GameManager.GameState.MainMenu:
                case GameManager.GameState.Pause:
                case GameManager.GameState.GameOver:
                case GameManager.GameState.Victory:
                case GameManager.GameState.Restart:
                case GameManager.GameState.Exit:
                    break;
            }
        }
        public static void ChangeMusic(Song music, float volume)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(music);
            if (volume > 0)
            {
                MediaPlayer.Volume = volume;
            }
        }
        public static void PlaySoundEffect(string name, float volume = 0)
        {
            float defaultPitch = 0;
            float defaultPan = 0;

            if (volume > 0)
            {
                _defaultSoundEffectVolume = volume;
            }
            _soundEffect = ResourceManager.GetSoundEffect(name);
            _soundEffect.Play(_defaultSoundEffectVolume, defaultPitch, defaultPan);
        }
    }
}
