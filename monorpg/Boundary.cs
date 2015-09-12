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
    /// An obstacle that entities normally cannot pass through, such as a wall.
    /// </summary>
    public class Boundary : MapObject
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Boundary()
        {

        }

        /// <summary>
        /// Constructs boundary
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public Boundary(float x, float y, int width, int height)
        {
            Position = new Vector2(x, y);
            _boundingBox = new Rectangle((int)x, (int)y, width, height);
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Nothing to do here
        /// </summary>
        public override void Draw()
        {
            //Do nothing
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
