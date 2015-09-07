using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Timers;

namespace monorpg
{
    /// <summary>
    /// These are the main gameplay buttons
    /// </summary>
    public struct GameButtons
    {
        public bool UpButton;
        public bool DownButton;
        public bool LeftButton;
        public bool RightButton;
        public bool ActionButton;
        public bool CancelButton;
        public bool MenuButton;
    }

    public static class Input
    {
        #region Constructor
        static Input()
        {
            _current.ActionButton = false;
            _current.CancelButton = false;
            _current.DownButton = false;
            _current.LeftButton = false;
            _current.MenuButton = false;
            _current.RightButton = false;
            _current.UpButton = false;
            _last.ActionButton = false;
            _last.CancelButton = false;
            _last.DownButton = false;
            _last.LeftButton = false;
            _last.MenuButton = false;
            _last.RightButton = false;
            _last.UpButton = false;
            _movementFrozen = false;
        }
        #endregion

        #region Fields

        private static GameButtons _current;
        private static GameButtons _last;
        private static bool _movementFrozen;

        #endregion

        #region Properties

        /// <summary>
        /// true if left button is down
        /// </summary>
        public static bool Left
        {
            get
            {
                if (Input.LeftKey() == 1 || Input.LeftKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// true if right button is down
        /// </summary>
        public static bool Right
        {
            get
            {
                if (Input.RightKey() == 1 || Input.RightKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// true if up button is down
        /// </summary>
        public static bool Up
        {
            get
            {
                if (Input.UpKey() == 1 || Input.UpKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// true if down button is down
        /// </summary>
        public static bool Down
        {
            get
            {
                if (Input.DownKey() == 1 || Input.DownKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// true if action button is down
        /// </summary>
        public static bool Action
        {
            get
            {
                if (Input.ActionKey() == 1 || Input.ActionKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// true if cancel button is down
        /// </summary>
        public static bool Cancel
        {
            get
            {
                if (Input.CancelKey() == 1 || Input.CancelKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// true if menu button is down
        /// </summary>
        public static bool Menu
        {
            get
            {
                if (Input.MenuKey() == 1 || Input.MenuKey() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prevent usage of directional buttons.  
        /// </summary>
        public static void FreezeMovement()
        {
            _movementFrozen = true;
        }

        /// <summary>
        /// Resumes processing of directional buttons
        /// </summary>
        public static void UnfreezeMovement()
        {
            _movementFrozen = false;
        }

        /// <summary>
        /// Checks to see if movement buttons are processed.
        /// </summary>
        /// <returns>true if we have halted processing of movement buttons</returns>
        public static bool IsMovementFrozen()
        {
            return _movementFrozen; 
        }

        /// <summary>
        /// Resets the entire game input state to false
        /// </summary>
        public static void Reset()
        {
            _current.ActionButton = false;
            _current.CancelButton = false;
            _current.DownButton = false;
            _current.LeftButton = false;
            _current.MenuButton = false;
            _current.RightButton = false;
            _current.UpButton = false;
            _last.ActionButton = false;
            _last.CancelButton = false;
            _last.DownButton = false;
            _last.LeftButton = false;
            _last.MenuButton = false;
            _last.RightButton = false;
            _last.UpButton = false;
        }

        /// <summary>
        /// Resets current buffer only
        /// </summary>
        public static void Clear()
        {
            _current.ActionButton = false;
            _current.CancelButton = false;
            _current.DownButton = false;
            _current.LeftButton = false;
            _current.MenuButton = false;
            _current.RightButton = false;
            _current.UpButton = false;
        }

        /// <summary>
        /// Returns true if any of the GameButtons are down.
        /// </summary>
        /// <returns>true if any buttons are currently down this frame</returns>
        public static bool AreAnyButtonsDown()
        {
            if ((_current.ActionButton == false) && (_current.CancelButton == false) && (_current.DownButton == false) &&
                (_current.LeftButton == false) && (_current.MenuButton == false) && (_current.RightButton == false) &&
                (_current.UpButton == false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Updates input state
        /// </summary>
        public static void UpdateInput()
        {
            _last = _current;
            Clear();
            KeyboardState keys = Keyboard.GetState();
            GamePadState pad = GamePad.GetState(PlayerIndex.One);
            if ((keys.IsKeyDown(Keys.Left)) || (pad.DPad.Left == ButtonState.Pressed))
            {
                _current.LeftButton = true;
            }

            if ((keys.IsKeyDown(Keys.Right)) || (pad.DPad.Right == ButtonState.Pressed))
            {
                _current.RightButton = true;
            }

            if ((keys.IsKeyDown(Keys.Up)) || (pad.DPad.Up == ButtonState.Pressed))
            {
                _current.UpButton = true;
            }

            if ((keys.IsKeyDown(Keys.Down)) || (pad.DPad.Down == ButtonState.Pressed))
            {
                _current.DownButton = true;
            }

            if ((keys.IsKeyDown(Keys.Space)) || (pad.Buttons.A == ButtonState.Pressed))
            {
                _current.ActionButton = true;
            }

            if ((keys.IsKeyDown(Keys.Z)) || (pad.Buttons.B == ButtonState.Pressed))
            {
                _current.CancelButton = true;
            }

            if ((keys.IsKeyDown(Keys.Escape)) || (pad.Buttons.Y == ButtonState.Pressed))
            {
                _current.MenuButton = true;
            }
        }

        /// <summary>
        /// Returns state of Left Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte LeftKey()
        {
            if ((_last.LeftButton == false) && (_current.LeftButton == false))
            {
                return 0;
            }
            else if ((_last.LeftButton == false) && (_current.LeftButton == true))
            {
                return 1;
            }
            else if ((_last.LeftButton == true) && (_current.LeftButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// Returns state of Right Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte RightKey()
        {
            if ((_last.RightButton == false) && (_current.RightButton == false))
            {
                return 0;
            }
            else if ((_last.RightButton == false) && (_current.RightButton == true))
            {
                return 1;
            }
            else if ((_last.RightButton == true) && (_current.RightButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// Returns state of Up Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte UpKey()
        {
            if ((_last.UpButton == false) && (_current.UpButton == false))
            {
                return 0;
            }
            else if ((_last.UpButton == false) && (_current.UpButton == true))
            {
                return 1;
            }
            else if ((_last.UpButton == true) && (_current.UpButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// Returns state of Down Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte DownKey()
        {
            if ((_last.DownButton == false) && (_current.DownButton == false))
            {
                return 0;
            }
            else if ((_last.DownButton == false) && (_current.DownButton == true))
            {
                return 1;
            }
            else if ((_last.DownButton == true) && (_current.DownButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// Returns state of Action Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte ActionKey()
        {
            if ((_last.ActionButton == false) && (_current.ActionButton == false))
            {
                return 0;
            }
            else if ((_last.ActionButton == false) && (_current.ActionButton == true))
            {
                return 1;
            }
            else if ((_last.ActionButton == true) && (_current.ActionButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// Returns state of Cancel Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte CancelKey()
        {
            if ((_last.CancelButton == false) && (_current.CancelButton == false))
            {
                return 0;
            }
            else if ((_last.CancelButton == false) && (_current.CancelButton == true))
            {
                return 1;
            }
            else if ((_last.CancelButton == true) && (_current.CancelButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// Returns state of Menu Key
        /// </summary>
        /// <returns>0 if not pressed, 1 if newly pressed, 2 if button is held down, 3 when button is just released</returns>
        public static byte MenuKey()
        {
            if ((_last.MenuButton == false) && (_current.MenuButton == false))
            {
                return 0;
            }
            else if ((_last.MenuButton == false) && (_current.MenuButton == true))
            {
                return 1;
            }
            else if ((_last.MenuButton == true) && (_current.MenuButton == true))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        #endregion
    }
}
