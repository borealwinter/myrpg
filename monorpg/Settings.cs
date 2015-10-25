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

        /// <summary>
        /// Returns default character screen position
        /// </summary>
        public static Vector2 DefaultPersonPosition
        {
            get
            {
                return new Vector2(304, 216);
            }
        }

        /// <summary>
        /// Size of current map in pixels
        /// </summary>
        public static Vector2 MapSize { get; set; }
        /// <summary>
        /// Graphics Device Manager
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        #region Methods

        /// <summary>
        /// Creates Texture
        /// </summary>
        /// <param name="size">size of texture</param>
        /// <param name="color">color of rectangle</param>
        /// <returns></returns>
        public static Texture2D CreateTexture(Rectangle size, Color color)
        {
            Texture2D texture;
            try
            {
                texture = new Texture2D(Settings.GraphicsDeviceManager.GraphicsDevice, size.Width, size.Height);
                Color[] data = new Color[texture.Width * texture.Height];
                texture.GetData(data);
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = color;
                }
                texture.SetData(data);
            }
            catch(Exception ex)
            {
                texture = new Texture2D(GraphicsDeviceManager.GraphicsDevice, 0, 0);
            }

            return texture;
        }

        #endregion
    }
}
