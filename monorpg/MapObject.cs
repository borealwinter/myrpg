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
    /// Abstract class of objects that the player interacts with on map.
    /// </summary>
    public abstract class MapObject : IComparable<MapObject>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MapObject()
        {
            _boundingBox = new Rectangle(0, 0, 0, 0);
            _position = Vector2.Zero;
        }

        #endregion

        #region Fields

        /// <summary>
        /// position of object
        /// </summary>
        protected Vector2 _position;

        /// <summary>
        /// Collision bounding box of object
        /// </summary>
        protected Rectangle _boundingBox;
        /// <summary>
        /// position of object on screen
        /// </summary>
        protected Vector2 _screenPosition;

        #endregion

        #region Properties

        /// <summary>
        /// Gets position vector for object
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                _boundingBox.X = (int)_position.X;
                _boundingBox.Y = (int)_position.Y;
            }
        }

        /// <summary>
        /// Screen position of person
        /// </summary>
        public virtual Vector2 ScreenPosition
        {
            get
            {
                return _screenPosition;
            }
            set
            {
                _screenPosition = value;
            }
        }

        /// <summary>
        /// Screen position X
        /// </summary>
        public virtual float ScreenPositionX
        {
            get
            {
                return _screenPosition.X;
            }
            set
            {
                _screenPosition.X = value;
            }
        }

        /// <summary>
        /// position X
        /// </summary>
        public virtual float PositionX
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
                _boundingBox.X = (int)_position.X;
            }
        }

        /// <summary>
        /// Screen position Y
        /// </summary>
        public virtual float ScreenPositionY
        {
            get
            {
                return _screenPosition.Y;
            }
            set
            {
                _screenPosition.Y = value;
            }
        }

        /// <summary>
        /// position Y
        /// </summary>
        public virtual float PositionY
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value; 
                _boundingBox.Y = (int)_position.Y;
            }
        }

        /// <summary>
        /// Bottom edge of the bounding box.
        /// </summary>
        public virtual float BottomEdge
        {
            get
            {
                return this._position.Y + this._boundingBox.Height;
            }
        }

        /// <summary>
        /// Top edge of bounding box
        /// </summary>
        public virtual float TopEdge
        {
            get
            {
                return this._position.Y;
            }
        }

        /// <summary>
        /// Right edge of the bounding box
        /// </summary>
        public virtual float RightEdge
        {
            get
            {
                return this._position.X + this._boundingBox.Width;
            }
        }

        /// <summary>
        /// Left edge of the bounding box
        /// </summary>
        public virtual float LeftEdge
        {
            get
            {
                return this._position.X;
            }
        }

        /// <summary>
        /// Returns bounding box rectangle
        /// </summary>
        public virtual Rectangle BoundingBox
        {
            get
            {
                return _boundingBox;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the bottoms of the objects to determine draw order.
        /// </summary>
        /// <param name="other">Another MapObject</param>
        /// <returns>-1 if  this is lower than other, 1 if higher, 0 if equal</returns>
        public int CompareTo(MapObject other)
        {
            if (this.BottomEdge < other.BottomEdge) return -1;
            else if (this.BottomEdge > other.BottomEdge) return 1;
            else return 0;
        }

        /// <summary>
        /// increments position
        /// </summary>
        /// <param name="x">x displacement</param>
        /// <param name="y">y displacement</param>
        public virtual void Move(float x, float y)
        {
            _position.X += x;
            _position.Y += y;
            _boundingBox.X = (int)_position.X;
            _boundingBox.Y = (int)_position.Y;
        }

        /// <summary>
        /// increments position
        /// </summary>
        /// <param name="displacement">The amount you want to increment your position.</param>
        public virtual void Move(Vector2 displacement)
        {
            Position = displacement;
        }

        /// <summary>
        /// Checks to see if there is intersection between bounding boxes of two objects
        /// </summary>
        /// <param name="other">The object to test collision against</param>
        /// <returns>true if intersection exists, false otherwise</returns>
        public bool Collision(MapObject other)
        {
            return this._boundingBox.Intersects(other.BoundingBox);
        }

        /// <summary>
        /// Update Cycle
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Draw Cycle
        /// </summary>
        public abstract void Draw();

        #endregion

    }
}
