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
    /// NPC class for NPCs on map
    /// </summary>
    public class NPCPerson : Person
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public NPCPerson()
        {
            random = new Random();
        }

        /// <summary>
        /// Constructor with image
        /// </summary>
        /// <param name="spriteSheet">name of spritesheet</param>
        public NPCPerson(string spriteSheet)
        {
            _spriteSheet = Settings.Content.Load<Texture2D>(spriteSheet);
            random = new Random();
        }

        #endregion

        #region Fields

        private int _moveScript;
        private int _counter0;

        private int _counter1;
        private int _counter2;
        private int _counter3;
        private int _counter4;
        private int _counter5;
        private Random random;

        #endregion

        #region Properties

        /// <summary>
        /// Counter (0) to govern NPC behavior
        /// </summary>
        public int Counter0
        {
            get 
            { 
                return _counter0; 
            }
            set 
            { 
                _counter0 = value; 
            }
        }

        /// <summary>
        /// Counter (1) to govern NPC behavior
        /// </summary>
        public int Counter1
        {
            get
            {
                return _counter1;
            }
            set
            {
                _counter1 = value;
            }
        }

        /// <summary>
        /// Counter (2) to govern NPC behavior
        /// </summary>
        public int Counter2
        {
            get
            {
                return _counter2;
            }
            set
            {
                _counter2 = value;
            }
        }

        /// <summary>
        /// Counter (3) to govern NPC behavior
        /// </summary>
        public int Counter3
        {
            get
            {
                return _counter3;
            }
            set
            {
                _counter3 = value;
            }
        }

        /// <summary>
        /// Counter (4) to govern NPC behavior
        /// </summary>
        public int Counter4
        {
            get
            {
                return _counter4;
            }
            set
            {
                _counter4 = value;
            }
        }

        /// <summary>
        /// Counter (5) to govern NPC behavior
        /// </summary>
        public int Counter5
        {
            get
            {
                return _counter5;
            }
            set
            {
                _counter5 = value;
            }
        }

        /// <summary>
        /// Determines normal behavior of npc
        /// </summary>
        public int MoveScript
        {
            get
            {
                return _moveScript;
            }
            set
            {
                _moveScript = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update
        /// </summary>
        public override void Update()
        {
            switch (MoveScript)
            {
                case 0:
                    ExecuteMoveScript1();
                    break;
            }

            base.Update();
        }

        /// <summary>
        /// Executes move script number 1
        /// </summary>
        public void ExecuteMoveScript1()
        {
            if (Counter0 <= 0)
            {
                if (State == PersonState.Standing)
                {
                    if (Counter1 > 0)
                    {
                        Counter1--;
                    }
                    else
                    {
                        int dir = random.Next(4);
                        switch (dir)
                        {
                            case 0:
                                Direction = Facing.North;
                                break;
                            case 1:
                                Direction = Facing.South;
                                break;
                            case 2:
                                Direction = Facing.East;
                                break;
                            case 3:
                                Direction = Facing.West;
                                break;
                        }
                        State = PersonState.Walking;
                        Counter0 = random.Next(32, 150);
                    }
                }
                else
                {
                    State = PersonState.Standing;
                    Counter1 = 60;
                }
            }
            else
            {
                Counter0--;
            }
        }

        /// <summary>
        /// draw
        /// </summary>
        public override void Draw()
        {
            base.Draw();
        }

        /// <summary>
        /// Draws person
        /// </summary>
        /// <param name="screenPosition">position on screen to draw</param>
        public void Draw(Vector2 screenPosition)
        {
            Settings.SpriteBatch.Draw(_spriteSheet, screenPosition, _sourceRect, _color);
        }

        #endregion

    }
}
