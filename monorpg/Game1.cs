using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using TiledSharp;
using System.Diagnostics;
using System;

namespace monorpg
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Avatar player;
        float frameRate;
        private SpriteFont font;

        /// <summary>
        /// We are at the start of the program
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Settings.ContentFolder = Content.RootDirectory;
            Settings.Content = Content;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Settings.SpriteBatch = spriteBatch;
            player = new Avatar();
            player.Direction = Facing.East;
            player.State = PersonState.Walking;
            player.Speed = 10;
            player.Tint = Color.White;
            player.Position = new Vector2(75, 75);
            player.ScreenPosition = new Vector2(75, 75);
            font = Content.Load<SpriteFont>("ExFont");
            Map.Load("test2");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.UpdateInput();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!Input.AreAnyDirectionButtonsDown())
            {
                player.State = PersonState.Standing;
            }
            else
            {
                player.State = PersonState.Walking;
            }

            Vector2 lastPlayer = new Vector2(player.PositionX, player.PositionY);
            Vector2 lastOffset = new Vector2(Map.OffsetX, Map.OffsetY);
            bool verticalScrolling;
            bool horizontalScrolling;

            if (Input.Up)
            {
                if (player.Direction != Facing.North)
                {
                    player.Direction = Facing.North;
                }
                else
                {
                    player.PositionY -= 5;
                    if (player.PositionY < 0f)
                    {
                        player.PositionY = 0f;
                        player.State = PersonState.Standing;
                    }

                    if (((player.PositionY) < Settings.DefaultPersonPosition.Y) || (player.PositionY > (Map.Height - Settings.DefaultPersonPosition.Y - 45f)))
                    {
                        verticalScrolling = false;
                    }
                    else
                    {
                        verticalScrolling = true;
                    }

                    if (verticalScrolling)
                    {
                        Map.OffsetY += player.PositionY - lastPlayer.Y;
                    }
                    else
                    {
                        player.ScreenPositionY += player.PositionY - lastPlayer.Y;

                        if (player.PositionY <= Settings.DefaultPersonPosition.Y)
                        {
                            Map.OffsetY = 0f;
                        }
                        else
                        {
                            Map.OffsetY = Map.Height - Settings.ScreenSize.Y;
                        }
                    }
                }
            }
            else if (Input.Down)
            {
                if (player.Direction != Facing.South)
                {
                    player.Direction = Facing.South;
                }
                else
                {
                    player.PositionY += 5;

                    if (player.PositionY > Map.Height)
                    {
                        player.PositionY =  Map.Height;
                        player.State = PersonState.Standing;
                    }

                    if (((player.PositionY) < Settings.DefaultPersonPosition.Y) || (player.PositionY > (Map.Height - Settings.DefaultPersonPosition.Y - 45f)))
                    {
                        verticalScrolling = false;
                    }
                    else
                    {
                        verticalScrolling = true;
                    }

                    if (verticalScrolling)
                    {
                        Map.OffsetY += player.PositionY - lastPlayer.Y;
                    }
                    else
                    {
                        player.ScreenPositionY += player.PositionY - lastPlayer.Y;

                        if (player.PositionY <= Settings.DefaultPersonPosition.Y)
                        {
                            Map.OffsetY = 0f;
                        }
                        else
                        {
                            Map.OffsetY = Map.Height - Settings.ScreenSize.Y;
                        }
                    }
                }
            }
            else if (Input.Left)
            {
                if (player.Direction != Facing.West)
                {
                    player.Direction = Facing.West;
                }
                else
                {
                    player.PositionX -= 5;
                    if (player.PositionX < 0f)
                    {
                        player.PositionX = 0f;
                        player.State = PersonState.Standing;
                    }

                    if (((player.PositionX) < Settings.DefaultPersonPosition.X) || (player.PositionX > (Map.Width - Settings.DefaultPersonPosition.X - 32f)))
                    {
                        horizontalScrolling = false;
                    }
                    else
                    {
                        horizontalScrolling = true;
                    }

                    if (horizontalScrolling)
                    {
                        Map.OffsetX += player.PositionX - lastPlayer.X;
                    }
                    else
                    {
                        player.ScreenPositionX += player.PositionX - lastPlayer.X;

                        if (player.PositionX <= Settings.DefaultPersonPosition.X)
                        {
                            Map.OffsetX = 0f;
                        }
                        else
                        {
                            Map.OffsetX = Map.Width - Settings.ScreenSize.X;
                        }
                    }
                }
            }
            else if (Input.Right)
            {
                if (player.Direction != Facing.East)
                {
                    player.Direction = Facing.East;
                }
                else
                {
                    player.PositionX += 5;

                    if (player.PositionX > Map.Width)
                    {
                        player.PositionX = Map.Width;
                        player.State = PersonState.Standing;
                    }

                    if (((player.PositionX) < Settings.DefaultPersonPosition.X) || (player.PositionX > (Map.Width - Settings.DefaultPersonPosition.X - 32f)))
                    {
                        horizontalScrolling = false;
                    }
                    else
                    {
                        horizontalScrolling = true;
                    }

                    if (horizontalScrolling)
                    {
                        Map.OffsetX += player.PositionX - lastPlayer.X;
                    }
                    else
                    {
                        player.ScreenPositionX += player.PositionX - lastPlayer.X;

                        if (player.PositionX <= Settings.DefaultPersonPosition.X)
                        {
                            Map.OffsetX = 0f;
                        }
                        else
                        {
                            Map.OffsetX = Map.Width - Settings.ScreenSize.X;
                        }
                    }
                }
            }

            // TODO: Add your update logic here
            player.Update();
            base.Update(gameTime);
            frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Debug.WriteLine(frameRate);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            Map.Draw();
            player.Draw(player.ScreenPosition);
            spriteBatch.DrawString(font, 
String.Format("X: {0}  Y: {1}  SX: {2}  SY: {3}", player.Position.X, player.Position.Y, player.ScreenPositionX, player.ScreenPositionY),
                Vector2.Zero, Color.White);

            // ToDo: Print any textboxes

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
