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
        /// <summary>
        /// Up button 
        /// </summary>
        public bool UpButton;

        /// <summary>
        /// Down button
        /// </summary>
        public bool DownButton;

        /// <summary>
        /// Left button
        /// </summary>
        public bool LeftButton;

        /// <summary>
        /// Right button
        /// </summary>
        public bool RightButton;

        /// <summary>
        /// Action button
        /// </summary>
        public bool ActionButton;

        /// <summary>
        /// Cancel button
        /// </summary>
        public bool CancelButton;

        /// <summary>
        /// Menu button
        /// </summary>
        public bool MenuButton;
    }

    /// <summary>
    /// Detail State of Game Buttons
    /// </summary>
    public enum GameButtonState
    {
        /// <summary>
        /// Button has been inactive the past two frames
        /// </summary>
        NoAction,

        /// <summary>
        /// Button has been down the past two frames
        /// </summary>
        HeldDown,

        /// <summary>
        /// Button is down this frame, but not the last
        /// </summary>
        NewlyPressed,

        /// <summary>
        /// Button was down in the previous frame, but not in the current one
        /// </summary>
        JustReleased
    }

    /// <summary>
    /// This handles the input for the game
    /// </summary>
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
                if (Input.LeftKey() == GameButtonState.NewlyPressed || Input.LeftKey() == GameButtonState.HeldDown)
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
                if (Input.RightKey() == GameButtonState.NewlyPressed || Input.RightKey() == GameButtonState.HeldDown)
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
                if (Input.UpKey() == GameButtonState.NewlyPressed || Input.UpKey() == GameButtonState.HeldDown)
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
                if (Input.DownKey() == GameButtonState.NewlyPressed || Input.DownKey() == GameButtonState.HeldDown)
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
                if (Input.ActionKey() == GameButtonState.NewlyPressed || Input.ActionKey() == GameButtonState.HeldDown)
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
                if (Input.CancelKey() == GameButtonState.NewlyPressed || Input.CancelKey() == GameButtonState.HeldDown)
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
                if (Input.MenuKey() == GameButtonState.NewlyPressed || Input.MenuKey() == GameButtonState.HeldDown)
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
        /// <returns>GameButtonState object</returns>
        public static GameButtonState LeftKey()
        {
            if ((_last.LeftButton == false) && (_current.LeftButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.LeftButton == false) && (_current.LeftButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.LeftButton == true) && (_current.LeftButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        /// <summary>
        /// Returns state of Right Key
        /// </summary>
        /// <returns>GameButtonState object</returns>
        public static GameButtonState RightKey()
        {
            if ((_last.RightButton == false) && (_current.RightButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.RightButton == false) && (_current.RightButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.RightButton == true) && (_current.RightButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        /// <summary>
        /// Returns state of Up Key
        /// </summary>
        /// <returns>GameButtonState object</returns>
        public static GameButtonState UpKey()
        {
            if ((_last.UpButton == false) && (_current.UpButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.UpButton == false) && (_current.UpButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.UpButton == true) && (_current.UpButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        /// <summary>
        /// Returns state of Down Key
        /// </summary>
        /// <returns>GameButtonState object</returns>
        public static GameButtonState DownKey()
        {
            if ((_last.DownButton == false) && (_current.DownButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.DownButton == false) && (_current.DownButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.DownButton == true) && (_current.DownButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        /// <summary>
        /// Returns state of Action Key
        /// </summary>
        /// <returns>GameButtonState object</returns>
        public static GameButtonState ActionKey()
        {
            if ((_last.ActionButton == false) && (_current.ActionButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.ActionButton == false) && (_current.ActionButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.ActionButton == true) && (_current.ActionButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        /// <summary>
        /// Returns state of Cancel Key
        /// </summary>
        /// <returns>GameButtonState object</returns>
        public static GameButtonState CancelKey()
        {
            if ((_last.CancelButton == false) && (_current.CancelButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.CancelButton == false) && (_current.CancelButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.CancelButton == true) && (_current.CancelButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        /// <summary>
        /// Returns state of Menu Key
        /// </summary>
        /// <returns>GameButtonState object</returns>
        public static GameButtonState MenuKey()
        {
            if ((_last.MenuButton == false) && (_current.MenuButton == false))
            {
                return GameButtonState.NoAction;
            }
            else if ((_last.MenuButton == false) && (_current.MenuButton == true))
            {
                return GameButtonState.NewlyPressed;
            }
            else if ((_last.MenuButton == true) && (_current.MenuButton == true))
            {
                return GameButtonState.HeldDown;
            }
            else
            {
                return GameButtonState.JustReleased;
            }
        }

        #endregion
    }
}
