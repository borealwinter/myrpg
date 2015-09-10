using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using TiledSharp;

namespace monorpg
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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

            if (Input.Up)
            {
                Map.OffsetY -= 5f;
                if (Map.OffsetY < 0)
                {
                    Map.OffsetY = 0f;
                }
            }

            if (Input.Down)
            {
                Map.OffsetY += 5f;
                if (Map.OffsetY > Map.Height - Settings.ScreenSize.Y)
                {
                    Map.OffsetY = Map.Height - Settings.ScreenSize.Y;
                }
            }

            if (Input.Left)
            {
                Map.OffsetX -= 5f;
                if (Map.OffsetX < 0)
                {
                    Map.OffsetX = 0f;
                }
            }

            if (Input.Right)
            {
                Map.OffsetX += 5f;
                if (Map.OffsetX > Map.Width - Settings.ScreenSize.X)
                {
                    Map.OffsetX = Map.Width - Settings.ScreenSize.X;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
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

            // ToDo: Print any textboxes

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
