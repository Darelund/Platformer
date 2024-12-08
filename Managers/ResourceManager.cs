using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public static class ResourceManager
    {
        public static List<Texture2D> Textures = new List<Texture2D>();
        public static List<SoundEffect> SoundEffects = new List<SoundEffect>();
        public static List<Song> Music = new List<Song>();
        public static List<SpriteFont> SpriteFont = new List<SpriteFont>();
        public static List<Effect> Effects = new List<Effect>();

        public static void LoadResources(ContentManager content, string textures, string soundEffects, string musics, string spriteFonts, string effects)
        {
            var text = textures.Split(',');
            foreach (string tex in textures.Split(','))
            {
                if (tex == "") continue;
                Texture2D texture = content.Load<Texture2D>($"Textures/{tex.Trim()}");
                texture.Name = tex.Trim();
                Textures.Add(texture);
            }
            foreach (string soundEffect in soundEffects.Split(','))
            {
                if (soundEffect == "") continue;
                SoundEffect sF = content.Load<SoundEffect>($"SoundEffects/{soundEffect.Trim()}");
                sF.Name = soundEffect.Trim();
                SoundEffects.Add(sF);
            }
            foreach (string music in musics.Split(','))
            {
                if (music == "") continue;
                Song m = content.Load<Song>($"Music/{music.Trim()}");
                Music.Add(m);
            }
            foreach (string spriteFont in spriteFonts.Split(','))
            {
                if (spriteFont == "") continue;
                SpriteFont sF = content.Load<SpriteFont>($"Fonts/{spriteFont.Trim()}");
                sF.Texture.Name = spriteFont.Trim();
                SpriteFont.Add(sF);
            }
            foreach (string effect in effects.Split(','))
            {
                if (effect == "") continue;
                Effect e = content.Load<Effect>($"Effect/{effect.Trim()}");
                e.Name = effect.Trim();
                Effects.Add(e);
            }
        }
        public static Texture2D GetTexture(string name) => Textures.Find(n => n.Name == name);
        public static SoundEffect GetSoundEffect(string name) => SoundEffects.Find(n => n.Name == name);
        public static Song GetMusic(string name) => Music.Find(n => n.Name == name);
        public static SpriteFont GetSpriteFont(string name) => SpriteFont.Find(n => n.Texture.Name == name);
        public static Effect GetEffect(string name) => Effects.Find(n => n.Name == name);
    }
}
