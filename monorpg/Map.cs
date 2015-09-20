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
    /// Handles pretty much all map functionality
    /// </summary>
    public static class Map
    {
        #region Fields

        private static TmxMap _map;
        private static Texture2D _tileset;
        private static int _sizex;
        private static int _sizey;
        private static List<Rectangle> _tiles;
        private static int _numLayers;
        private static int _numBackgroundLayers;
        private static int _numForegroundLayers;
        private static List<TmxObjectGroup> _objectGroups;
        private static Vector2 _offset;

        private static Avatar player;
        private static SpriteFont font;


        #endregion

        #region Properties

        /// <summary>
        /// Is the map larger than a screen-sized map?
        /// </summary>
        public static bool IsScrollable { get; set; }

        /// <summary>
        /// Gets map offset 
        /// </summary>
        public static Vector2 Offset
        {
            get
            {
                return _offset;
            }

            set
            {
                _offset = value;
            }
        }

        /// <summary>
        /// Gets the horizontal offset
        /// </summary>
        public static float OffsetX
        {
            get
            {
                return _offset.X;
            }

            set
            {
                _offset.X = value;
            }
        }

        /// <summary>
        /// Gets the vertical offset
        /// </summary>
        public static float OffsetY
        {
            get
            {
                return _offset.Y;
            }

            set
            {
                _offset.Y = value;
            }
        }

        /// <summary>
        /// Returns list of object groups in map
        /// </summary>
        public static List<TmxObjectGroup> ObjectGroups
        {
            get
            {
                return _objectGroups;
            }
        }

        /// <summary>
        /// Gets the background layers
        /// </summary>
        public static List<TmxLayer> BackgroundLayers
        {
            get
            {
                return (from TmxLayer background in _map.Layers
                        where background.Name.ToLower().Contains("background")
                        select background).ToList();
            }
        }

        /// <summary>
        /// Gets the foreground layers
        /// </summary>
        public static List<TmxLayer> ForegroundLayers
        {
            get
            {
                return (from TmxLayer foreground in _map.Layers
                        where foreground.Name.ToLower().Contains("foreground")
                        select foreground).ToList();
            }
        }

        /// <summary>
        /// Gets layers in map
        /// </summary>
        public static List<TmxLayer> Layers
        {
            get
            {
                return (from TmxLayer layer in _map.Layers
                        select layer).ToList(); ;
            }
        }

        /// <summary>
        /// Number of Layers in map
        /// </summary>
        public static int NumberOfLayers
        {
            get
            {
                return _numLayers;
            }
        }

        /// <summary>
        /// Number of Background Layers
        /// </summary>
        public static int NumberOfBackgroundLayers
        {
            get
            {
                return _numBackgroundLayers;
            }
        }

        /// <summary>
        /// Number of Foreground Layers
        /// </summary>
        public static int NumberOfForegroundLayers
        {
            get
            {
                return _numForegroundLayers;
            }
        }
        /// <summary>
        /// Determines whether it is safe to begin normal operation
        /// </summary>
        public static bool IsMapLoaded { get; set; }

        /// <summary>
        /// Width in tiles
        /// </summary>
        public static int SizeX
        {
            get
            {
                return _sizex;
            }
        }

        /// <summary>
        /// Height in tiles
        /// </summary>
        public static int SizeY
        {
            get
            {
                return _sizey;
            }
        }

        /// <summary>
        /// Total number of tiles in a layer
        /// </summary>
        public static int TilesPerLayer
        {
            get
            {
                return SizeX * SizeY;
            }
        }

        /// <summary>
        /// Width of map in pixels
        /// </summary>
        public static int Width
        {
            get
            {
                return _sizex * Settings.TileSize;
            }
        }

        /// <summary>
        /// Height of map in pixels
        /// </summary>
        public static int Height
        {
            get
            {
                return _sizey * Settings.TileSize;
            }
        }

        /// <summary>
        /// Collection of tile gid-rectangle mappings
        /// </summary>
        public static List<Rectangle> Tiles
        {
            get
            {
                return _tiles;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the map file
        /// </summary>
        /// <param name="fileName">name of file</param>
        public static void Load(string fileName)
        {
            IsMapLoaded = false;
            loadTmxMap(fileName);

            IsMapLoaded = true;
        }

        /// <summary>
        /// Unloads resources
        /// </summary>
        public static void Unload()
        {
            _map = null;
            _tileset = null;
            _tiles.Clear();
            _tiles = null;
            Settings.Content.Unload();
        }

        /// <summary>
        /// Updates Map
        /// </summary>
        public static void Update()
        {
            UpdateNormal();
        }

        /// <summary>
        /// Runs the normal update when no events are happening.  
        /// </summary>
        public static void UpdateNormal()
        {
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
                        player.PositionY = Map.Height;
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

        }

        /// <summary>
        /// Loads the tmx file
        /// </summary>
        /// <param name="mapName">name of tmx file without the extension</param>
        private static void loadTmxMap(string mapName)
        {
            string name = String.Concat(mapName, ".tmx");
            _map = new TmxMap(Path.Combine(Settings.ContentFolder, "../../../../", "maps", name));
            _tileset = Settings.Content.Load<Texture2D>(_map.Tilesets[0].Name.ToString());
            _sizex = _map.Width;
            _sizey = _map.Height;
            _tiles = new List<Rectangle>();
            _tiles.Add(new Rectangle(0, 0, 0, 0));

            for (int i = 0; i < TilesPerLayer; i++)
            {
                int x = (i % (_tileset.Width / Settings.TileSize)) * Settings.TileSize;
                int y = (i / (_tileset.Width / Settings.TileSize)) * Settings.TileSize;

                _tiles.Add(new Rectangle(x, y, Settings.TileSize, Settings.TileSize));
            }

            _numBackgroundLayers = (from TmxLayer background in _map.Layers
                                    where background.Name.ToLower().Contains("background")
                                    select background).Count();

            _numForegroundLayers = (from TmxLayer foreground in _map.Layers
                                    where foreground.Name.ToLower().Contains("foreground")
                                    select foreground).Count();

            _numLayers = _numBackgroundLayers + _numForegroundLayers + 1;

            _objectGroups = (from TmxObjectGroup objectGroup in _map.ObjectGroups
                             select objectGroup).ToList();

            IsScrollable = (_map.Width > 20 || _map.Height > 15) ? true : false;

            _offset = new Vector2(0f, 0f);

            player = new Avatar();
            player.Direction = Facing.East;
            player.State = PersonState.Walking;
            player.Speed = 10;
            player.Tint = Color.White;
            player.Position = new Vector2(75, 75);
            player.ScreenPosition = new Vector2(75, 75);
            font = Settings.Content.Load<SpriteFont>("ExFont");


        }

        /// <summary>
        /// Draws the foreground layers of the map
        /// </summary>
        public static void DrawForegroundLayers()
        {
            int k = 0;
            foreach (var layer in _map.Layers)
            {
                if (layer.Name.ToLower().Contains("foreground"))
                {
                    for (int j = 0; j < _map.Height; j++)
                    {
                        for (int i = 0; i < _map.Width; i++)
                        {
                            int gid = layer.Tiles[k].Gid;
                            if (gid > 0)
                            {
                                int xDraw = ((i * 32)) - (int)_offset.X;
                                int yDraw = ((j * 32)) - (int)_offset.Y;

                                if ((xDraw > (-32)) || (xDraw < 640) || (yDraw > (-32)) || (xDraw < 480))
                                {
                                    Settings.SpriteBatch.Draw(_tileset, new Rectangle(xDraw, yDraw, 32, 32), _tiles[gid], Color.White);
                                }
                            }
                            k++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws the foreground layers of the map
        /// </summary>
        public static void DrawBackgroundLayers()
        {
            int k = 0;
            foreach (var layer in _map.Layers)
            {
                if (layer.Name.ToLower().Contains("background"))
                {
                    for (int j = 0; j < _map.Height; j++)
                    {
                        for (int i = 0; i < _map.Width; i++)
                        {
                            int gid = layer.Tiles[k].Gid;
                            if (gid > 0)
                            {
                                int xDraw = ((i * 32)) - (int)_offset.X;
                                int yDraw = ((j * 32)) - (int)_offset.Y;

                                if ((xDraw > (-32)) || (xDraw < 640) || (yDraw > (-32)) || (xDraw < 480))
                                {
                                    Settings.SpriteBatch.Draw(_tileset, new Rectangle(xDraw, yDraw, 32, 32), _tiles[gid], Color.White);
                                }
                            }
                            k++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draw whole map to screen.  
        /// This one is optimized for screen sized or smaller maps
        /// </summary>
        public static void Draw()
        {
            // Wait until map is loaded to prevent null reference error
            if (IsMapLoaded)
            {
                DrawBackgroundLayers();

                player.Draw(player.ScreenPosition);

                DrawForegroundLayers();

                Settings.SpriteBatch.DrawString(font,
                    String.Concat("X: ",player.PositionX,"  Y: ",player.PositionY,"  SX: ",player.ScreenPositionX,"  SY: ",player.ScreenPositionY),
                    Vector2.Zero, Color.White);
            }
            
        }

        #endregion
    }
}
