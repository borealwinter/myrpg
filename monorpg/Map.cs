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

        #endregion

        #region Properties

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
                return _sizex + Settings.TileSize;
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
        /// Loads the tmx file
        /// </summary>
        /// <param name="mapName">name of tmx file without the extension</param>
        private static void loadTmxMap(string mapName)
        {
            string name = String.Concat(mapName, ".tmx");
            _map = new TmxMap(Path.Combine(Settings.ContentFolder, "../../../../", "maps", name));
            _tileset = Settings.Content.Load<Texture2D>(_map.Tilesets[0].Name.ToString());
            _sizex = _tileset.Width / Settings.TileSize;
            _sizey = _tileset.Height / Settings.TileSize;
            _tiles = new List<Rectangle>();
            _tiles.Add(new Rectangle(0, 0, 0, 0));

            for (int i = 0; i < TilesPerLayer; i++)
            {
                int x = (i % (_tileset.Width / 32)) * 32;
                int y = (i / (_tileset.Width / 32)) * 32;

                _tiles.Add(new Rectangle(x, y, 32, 32));
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
        }

        /// <summary>
        /// Draw whole map to screen.  
        /// This one is optimized for screen sized or smaller maps
        /// </summary>
        public static void Draw()
        {
            int k = 0;
            if (IsMapLoaded)
            {
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
                                    Settings.SpriteBatch.Draw(_tileset, new Rectangle(i * 32, j * 32, 32, 32), _tiles[gid], Color.White);
                                }

                                k++;
                            }
                        }
                    }
                }

                //ToDo: Print Object Layer

                k = 0;
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
                                    Settings.SpriteBatch.Draw(_tileset, new Rectangle(i * 32, j * 32, 32, 32), _tiles[gid], Color.White);
                                }

                                k++;
                            }
                        }
                    }
                }
            }

            // ToDo: Print any textboxes
        }

        #endregion
    }
}
