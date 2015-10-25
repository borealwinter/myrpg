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
    /// Direction person is facing
    /// </summary>
    public enum Facing
    {
        /// <summary>
        /// Person is facing east.
        /// </summary>
        East,

        /// <summary>
        /// Person is facing west.
        /// </summary>
        West,

        /// <summary>
        /// Person is facing north.
        /// </summary>
        North,

        /// <summary>
        /// Person is facing south
        /// </summary>
        South
    }

    /// <summary>
    /// Will the walking animation play or not?
    /// </summary>
    public enum PersonState
    {
        /// <summary>
        /// Person is standing.
        /// </summary>
        Standing,

        /// <summary>
        /// Person is walking
        /// </summary>
        Walking
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class Person : MapObject
    {

        #region Constructor

        #endregion

        #region Fields

        /// <summary>
        /// SpriteSheet for person
        /// </summary>
        protected Texture2D _spriteSheet;

        /// <summary>
        /// Source rectangle for frame
        /// </summary>
        protected Rectangle _sourceRect;

        /// <summary>
        /// Current Frame
        /// </summary>
        protected int _frame;

        /// <summary>
        /// Current direction person is facing
        /// </summary>
        protected Facing _direction;

        /// <summary>
        /// Whether the person is walking or standing.
        /// </summary>
        protected PersonState _state;

        /// <summary>
        /// Animation speed in frames per update
        /// </summary>
        protected int _speed;

        /// <summary>
        /// Iterator for frames
        /// </summary>
        protected int _iterator;

        /// <summary>
        /// Tint of the sprite
        /// </summary>
        protected Color _color;

        #endregion

        #region Properties

        /// <summary>
        /// Tint of the sprite
        /// </summary>
        public Color Tint
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// Iterator for animation frames
        /// </summary>
        public int Iterator
        {
            get
            {
                return _iterator;
            }
            set
            {
                _iterator = value; 
            }
        }

        /// <summary>
        /// Animation speed in frame
        /// </summary>
        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        /// <summary>
        /// Frame number of animation
        /// </summary>
        public int Frame
        {
            get
            {
                return _frame;
            }
            set
            {
                _frame = value;
            }
        }

        /// <summary>
        /// Source rectangle of the spritesheet
        /// </summary>
        public Rectangle SourceRect
        {
            get
            {
                return _sourceRect;
            }
            set
            {
                _sourceRect = value;
            }
        }

        /// <summary>
        /// Direction the person is facing
        /// </summary>
        public Facing Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        /// <summary>
        /// Holds whether character is walking or standing in animation.
        /// </summary>
        public PersonState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        /// <summary>
        /// Spritesheet
        /// </summary>
        public Texture2D SpriteSheet
        {
            get
            {
                return _spriteSheet;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Switch character state to standing.
        /// </summary>
        public virtual void Stand()
        {
            State = PersonState.Standing;
        }

        /// <summary>
        /// Switch character state to walking.
        /// </summary>
        public virtual void Walk()
        {
            State = PersonState.Walking;
        }

        /// <summary>
        /// Updates person
        /// </summary>
        public override void Update(List<MapObject> objects = null)
        {
            if (true)
            {
                BoundingBoxHeight = 22;
                BoundingBoxWidth = 31;

                //10x17 for top left 37x63 for bottom left
                if (State == PersonState.Standing)
                {
                    switch (_direction)
                    {
                        case Facing.East:
                            _sourceRect = new Rectangle(56, 82, 27, 45);
                            break;
                        case Facing.North:
                            _sourceRect = new Rectangle(56, 16, 31, 47);
                            break;
                        case Facing.South:
                            _sourceRect = new Rectangle(56, 144, 31, 47);
                            break;
                        case Facing.West:
                            _sourceRect = new Rectangle(56, 210, 27, 45);
                            break;
                    }
                }
                else
                {
                    if (Iterator == 0)
                    {
                        Iterator = Speed;
                        if (Frame < 0)
                        {
                            Frame = 3;
                        }
                        switch (_direction)
                        {
                            case Facing.East:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(56, 82, 27, 45);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(104, 82, 27, 45);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(56, 82, 27, 45);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(8, 82, 27, 45);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                            case Facing.North:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(56, 16, 27, 45);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(104, 16, 27, 45);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(56, 16, 27, 45);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(8, 16, 27, 45);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                            case Facing.South:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(56, 144, 27, 45);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(104, 144, 27, 45);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(56, 144, 27, 45);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(8, 144, 27, 45);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                            case Facing.West:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(56, 210, 27, 45);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(104, 210, 27, 45);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(56, 210, 27, 45);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(8, 210, 27, 45);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                        }
                        Frame--;
                    }
                    Iterator--;
                }
            }
            else
            {
                //Not Avatar
                BoundingBoxHeight = 32;
                BoundingBoxWidth = 32;

                if (State == PersonState.Standing)
                {
                    switch (_direction)
                    {
                        case Facing.East:
                            _sourceRect = new Rectangle(48, 64, 48, 64);
                            break;
                        case Facing.North:
                            _sourceRect = new Rectangle(48, 0, 48, 64);
                            break;
                        case Facing.South:
                            _sourceRect = new Rectangle(48, 128, 48, 64);
                            break;
                        case Facing.West:
                            _sourceRect = new Rectangle(48, 192, 48, 64);
                            break;
                    }
                }
                else
                {
                    if (Iterator == 0)
                    {
                        Iterator = Speed;
                        if (Frame < 0)
                        {
                            Frame = 3;
                        }
                        switch (_direction)
                        {
                            case Facing.East:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(48, 82, 48, 64);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(96, 82, 48, 64);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(48, 82, 48, 64);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(0, 82, 48, 64);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                            case Facing.North:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(48, 16, 48, 64);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(96, 16, 48, 64);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(48, 16, 48, 64);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(0, 16, 48, 64);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                            case Facing.South:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(48, 144, 48, 64);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(96, 144, 48, 64);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(48, 144, 48, 64);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(0, 144, 48, 64);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                            case Facing.West:
                                switch (_frame)
                                {
                                    case 0:
                                        _sourceRect = new Rectangle(48, 210, 48, 64);
                                        break;
                                    case 1:
                                        _sourceRect = new Rectangle(96, 210, 48, 64);
                                        break;
                                    case 2:
                                        _sourceRect = new Rectangle(48, 210, 48, 64);
                                        break;
                                    case 3:
                                        _sourceRect = new Rectangle(0, 210, 48, 64);
                                        break;
                                    default:

                                        break;
                                }
                                break;
                        }
                        Frame--;
                    }
                    Iterator--;
                }
            }
        }

        /// <summary>
        /// Draws person
        /// </summary>
        public override void Draw()
        {
            
        }
        #endregion
    }
}
