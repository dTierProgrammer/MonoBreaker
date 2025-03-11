using Microsoft.Xna.Framework.Graphics;
using MonoBreaker.Script.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoBreaker.Script.Font
{
    public static class Fonts
    {
        public static SpriteFont debugFont = GetContent.GetFont("debug");
        public static SpriteFont menuFont = GetContent.GetFont("menu");
        public static SpriteFont titleFont = GetContent.GetFont("title");
        public static SpriteFont subTitleFont = GetContent.GetFont("titlesmall");
    }
}
