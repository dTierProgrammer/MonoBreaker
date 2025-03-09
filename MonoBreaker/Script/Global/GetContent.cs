using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Global
{
    public static class GetContent
    {
        private static Game1 _game;

        public static void Initialize(Game1 game) 
        {
            _game = game;
        }

        public static Texture2D GetTexture(string path) 
        {
            Texture2D texture = _game.Content.Load<Texture2D>($"Image/{path}");
            return texture;
        }

        public static SoundEffect GetSound(string path)
        {
            SoundEffect sound = _game.Content.Load<SoundEffect>($"Sound/{path}");
            return sound;
        }

        public static Song GetSong(string path)
        {
            Song song = _game.Content.Load<Song>(path);
            return song;
        }
    }
}
