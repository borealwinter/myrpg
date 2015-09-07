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
        Vector2 pos;
        TmxMap map;

        private Texture2D circle;
        private Texture2D tileset;

        int sizeX;
        int sizeY;

        List<Rectangle> tileRects;

        int tileWidth;
        int tileHeight;

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
            circle = Content.Load<Texture2D>("circle");
            pos = new Vector2();

            map = new TmxMap(Path.Combine(Content.RootDirectory, "../../../../","maps/test1.tmx"));
            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());

            sizeX = tileset.Width / 32;
            sizeY = tileset.Height / 32;
            int totalSize = sizeX * sizeY;

            tileRects = new List<Rectangle>();
            tileRects.Add(new Rectangle(0,0,0,0));

            for (int i = 0; i < totalSize; i++)
            {
                int xx = (i % (tileset.Width/32)) * 32;
                int yy = (i / (tileset.Width / 32)) * 32;

                tileRects.Add(new Rectangle(xx, yy, 32, 32));
            }

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

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
                pos.Y -= 3;
                if (pos.Y < 0.0f)
                {
                    pos.Y = 0.0f;
                }
            }

            if (Input.Down)
            {
                pos.Y += 3;
                if (pos.Y > (graphics.GraphicsDevice.PresentationParameters.BackBufferHeight - circle.Height))
                {
                    pos.Y = graphics.GraphicsDevice.PresentationParameters.BackBufferHeight - circle.Height;
                }
            }

            if (Input.Left)
            {
                pos.X -= 3;
                if (pos.X < 0.0f)
                {
                    pos.X = 0.0f;
                }
            }

            if (Input.Right)
            {
                pos.X += 3;
                if (pos.X > (graphics.GraphicsDevice.PresentationParameters.BackBufferWidth - circle.Width))
                {
                    pos.X = graphics.GraphicsDevice.PresentationParameters.BackBufferWidth - circle.Width;
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

            //spriteBatch.Draw(tileset, pos, Color.White);

            int k = 0;

            foreach (var layer in map.Layers)
            {
                if (layer.Name.ToLower().Contains("background"))
                {
                    for (int j = 0; j < map.Height; j++)
                    {
                        for (int i = 0; i < map.Width; i++)
                        {
                            int gid = layer.Tiles[k].Gid;
                            if (gid > 0)
                            {
                                spriteBatch.Draw(tileset, new Rectangle(i * 32, j * 32, 32, 32), tileRects[gid], Color.White);
                            }

                            k++;
                        }
                    }
                }
            }

            //ToDo: Print Object Layer

            k = 0;
            foreach (var layer in map.Layers)
            {
                if (layer.Name.ToLower().Contains("foreground"))
                {
                    for (int j = 0; j < map.Height; j++)
                    {
                        for (int i = 0; i < map.Width; i++)
                        {
                            int gid = layer.Tiles[k].Gid;

                            if (gid > 0)
                            {
                                spriteBatch.Draw(tileset, new Rectangle(i * 32, j * 32, 32, 32), tileRects[gid], Color.White);
                            }
                            
                            k++;
                        }
                    }
                }
            }

            // ToDo: Print any dialogs

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
