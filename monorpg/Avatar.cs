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
    public class Avatar : Person
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

        /// <summary>
        /// Updates player
        /// </summary>
        public override void Update()
        {
            base.Update();
        }


    }
}
