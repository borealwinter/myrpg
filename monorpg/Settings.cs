using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using TiledSharp;

namespace monorpg
{
    /// <summary>
    /// Global settings and values for the game
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Path to Content folder
        /// </summary>
        public static string ContentFolder { get; set; }

        /// <summary>
        /// Tile size in pixels
        /// </summary>
        public static int TileSize
        {
            get
            {
                return 32;
            }
        }

        /// <summary>
        /// SpriteBatch object
        /// </summary>
        public static SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// ContentManager object
        /// </summary>
        public static ContentManager Content { get; set; }

        /// <summary>
        /// Returns current screen size
        /// </summary>
        public static Vector2 ScreenSize
        {
            get
            {
                return new Vector2(640, 480);
            }
        }

    }
}
