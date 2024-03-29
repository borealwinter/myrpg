﻿using Microsoft.Xna.Framework;
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
        /// Constructor with seed;
        /// </summary>
        /// <param name="seed"></param>
        public NPCPerson(string spriteSheet, int seed)
        {
            _spriteSheet = Settings.Content.Load<Texture2D>(spriteSheet);
            random = new Random(seed);
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
        /// Dummy update for now
        /// </summary>
        //public override void Update() { }

        /// <summary>
        /// Update
        /// </summary>
        public override void Update(List<MapObject> objects = null)
        {
            switch (MoveScript)
            {
                case 0:
                    State = PersonState.Standing;
                    break;
                case 1:
                    ExecuteMoveScript1(objects);
                    break;
                case 2:
                    ExecuteMoveScript2(objects);
                    break;
            }

            base.Update();
        }

        /// <summary>
        /// Executes move script number 1, normal RPG NPC Wandering
        /// </summary>
        public void ExecuteMoveScript1(List<MapObject> objects = null)
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
                        Counter0 = random.Next(32, 300);
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
                if (State == PersonState.Standing)
                {
                    switch (Direction)
                    {
                        case Facing.East:
                            break;
                        case Facing.West:
                            break;
                        case Facing.North:
                            break;
                        case Facing.South:
                            break;
                    }
                }
                else
                {
                    switch (Direction)
                    {
                        case Facing.East:
                            PositionX += 2f;
                            break;
                        case Facing.West:
                            PositionX -= 2f;
                            break;
                        case Facing.North:
                            PositionY -= 2f;
                            break;
                        case Facing.South:
                            PositionY += 2f;
                            break;
                    }

                    if (PositionX < 0)
                    {
                        PositionX = 0;
                        RanIntoSomething = true;
                    }
                    if (PositionX > Settings.MapSize.X - BoundingBoxWidth)
                    {
                        PositionX = Settings.MapSize.X - BoundingBoxWidth;
                        RanIntoSomething = true;
                    }
                    if (PositionY < 0)
                    {
                        PositionY = 0;
                        RanIntoSomething = true;
                    }
                    if (PositionY > Settings.MapSize.Y - BoundingBoxHeight)
                    {
                        PositionY = Settings.MapSize.Y - BoundingBoxHeight;
                        RanIntoSomething = true;
                    }
                    if (RanIntoSomething)
                    {
                        RanIntoSomething = false;
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
                        Counter0 = random.Next(32, 300);
                    }
                    
                    if (objects != null)
                    {
                        for (int i = 0; i < objects.Count; i++)
                        {
                            if (Name != objects[i].Name)
                            {
                                if (BoundingBox.Intersects(objects[i].BoundingBox))
                                {
                                    switch (Direction)
                                    {
                                        case Facing.North:
                                            BoundingBoxHeight = (int)objects[i].PositionY + objects[i].BoundingBoxHeight;
                                            Direction = Facing.South;
                                            break;
                                        case Facing.South:
                                            BoundingBoxHeight = (int)objects[i].PositionY - BoundingBoxHeight;
                                            Direction = Facing.North;
                                            break;
                                        case Facing.East:
                                            BoundingBoxWidth = (int)objects[i].PositionX - BoundingBoxWidth;
                                            Direction = Facing.West;
                                            break;
                                        case Facing.West:
                                            BoundingBoxHeight = (int)objects[i].PositionX + objects[i].BoundingBoxWidth;
                                            Direction = Facing.East;
                                            break;
                                    }
                                    //int dir = random.Next(4);
                                    //switch (dir)
                                    //{
                                    //    case 0:
                                    //        Direction = Facing.North;
                                    //        break;
                                    //    case 1:
                                    //        Direction = Facing.South;
                                    //        break;
                                    //    case 2:
                                    //        Direction = Facing.East;
                                    //        break;
                                    //    case 3:
                                    //        Direction = Facing.West;
                                    //        break;
                                    //}

                                    //State = PersonState.Walking;
                                    //Counter0 = random.Next(32, 300);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Executes move script number 2
        /// </summary>
        public void ExecuteMoveScript2(List<MapObject> objects = null)
        {
            if (Counter0 <= 0)
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
                Counter0 = random.Next(32, 300);
            }
            else
            {
                Counter0--;
                switch (Direction)
                {
                    case Facing.East:
                        PositionX += 2f;
                        break;
                    case Facing.West:
                        PositionX -= 2f;
                        break;
                    case Facing.North:
                        PositionY -= 2f;
                        break;
                    case Facing.South:
                        PositionY += 2f;
                        break;
                }

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
        public override void Draw(Vector2 screenPosition)
        {
            Settings.SpriteBatch.Draw(_spriteSheet, new Vector2(screenPosition.X, screenPosition.Y - 25f), _sourceRect, _color);
        }

        #endregion

    }
}
