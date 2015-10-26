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
    public static class DialogBox
    {
        #region Constructor

        static DialogBox()
        {
            _texture = Settings.Content.Load<Texture2D>("dialog");
            _bounds = _texture.Bounds;
            _bounds.X = 2;
            _bounds.Y = 318;
            _font = Settings.Content.Load<SpriteFont>("dialogfont");
            _visible = false;
        }

        #endregion

        #region Fields

        private static Rectangle _bounds;
        private static bool _visible;
        private static SpriteFont _font;
        private static string _displayedTextLine1;  
        private static string _fullTextLine1;
        private static string _displayedTextLine2;
        private static string _fullTextLine2;
        private static string _displayedTextLine3;
        private static string _fullTextLine3; 
        private static int _speed; 
        private static bool _more;
        private static int _counter0;
        private static int _counter1;
        private static int _counter2;
        private static int _counter3;
        private static bool _full;
        private static Texture2D _texture;
        

        #endregion

        #region Properties

        /// <summary>
        /// Gets the size of the textbox.  
        /// </summary>
        public static Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }

        /// <summary>
        /// Gets and sets whether the box is showing or other in use.  
        /// </summary>
        public static bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        /// <summary>
        /// Update speed of dialog box
        /// </summary>
        public static int Speed
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

        #endregion

        #region Methods

        /// <summary>
        /// Shows new dialog box with fresh text. 
        /// </summary>
        /// <param name="line1">line 1 text</param>
        /// <param name="line2">line 2 text</param>
        /// <param name="line3">line 3 text</param>
        /// <param name="end">end of conversation cursor. display</param>
        public static void SetText(string line1, string line2, string line3, bool end)
        {
            _more = end;
            _displayedTextLine1 = String.Empty;
            _displayedTextLine2 = String.Empty;
            _displayedTextLine3 = String.Empty;
            _fullTextLine1 = line1;
            _fullTextLine2 = line2;
            _fullTextLine3 = line3;
            _full = false;
            _counter1 = 0;
            _counter2 = 0;
            _counter3 = 0;
            _visible = true;  
        }

        /// <summary>
        /// Updates the text.  
        /// </summary>
        public static void Update()
        {
            if (_counter0 < 1)
            {
                _counter0 = _speed;
                if (_displayedTextLine1 != _fullTextLine1)
                {
                    _counter1++;
                    _displayedTextLine1 = _fullTextLine1.Substring(0, _counter1);
                }
                else
                {
                    if (_displayedTextLine2 != _fullTextLine2)
                    {
                        _counter2++;
                        _displayedTextLine2 = _fullTextLine2.Substring(0, _counter2);
                    }
                    else
                    {
                        if (_displayedTextLine3 != _fullTextLine3)
                        {
                            _counter3++;
                            _displayedTextLine3 = _fullTextLine3.Substring(0, _counter3);
                        }
                        else
                        {
                            
                        }
                    }
                }
            }
            else
            {
                _counter0--;
            }
        }

        /// <summary>
        /// Draws textbox.  
        /// </summary>
        public static void Draw()
        {
            Settings.SpriteBatch.Draw(_texture, _bounds, Color.White);
            Settings.SpriteBatch.DrawString(_font, _fullTextLine1, new Vector2(12f, 338f), Color.Black);
            Settings.SpriteBatch.DrawString(_font, _fullTextLine1, new Vector2(10f, 336f), Color.White);
            Settings.SpriteBatch.DrawString(_font, _fullTextLine2, new Vector2(12f, 378f), Color.Black);
            Settings.SpriteBatch.DrawString(_font, _fullTextLine2, new Vector2(10f, 376f), Color.White);
            Settings.SpriteBatch.DrawString(_font, _fullTextLine3, new Vector2(12f, 418f), Color.Black);
            Settings.SpriteBatch.DrawString(_font, _fullTextLine3, new Vector2(10f, 416f), Color.White);
        }

        #endregion
    }
}
