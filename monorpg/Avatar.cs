using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;


namespace monorpg
{
    /// <summary>
    /// Player
    /// </summary>
    public sealed class Avatar : Person
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Avatar()
        {
            _spriteSheet = Settings.Content.Load<Texture2D>("Albert");
        }
        #endregion

        #region Fields

       
        #endregion

        #region Properties

        /// <summary>
        /// Screen position of player
        /// </summary>
        public override Vector2 ScreenPosition
        {
            get
            {
                return base.ScreenPosition;
            }
            set
            {
                base.ScreenPosition = value;
            }
        }

        #endregion

        /// <summary>
        /// Updates player
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// Draws Player in Center position
        /// </summary>
        public override void Draw()
        {
            Settings.SpriteBatch.Draw(_spriteSheet, Settings.DefaultPersonPosition, _sourceRect, _color);
        }

        /// <summary>
        /// Draws Player at Coordinates
        /// </summary>
        /// <param name="position"></param>
        public void Draw(Vector2 position)
        {
            Settings.SpriteBatch.Draw(_spriteSheet, position, _sourceRect, _color);
        }
    }
}
