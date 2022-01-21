using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Tetris;
using TiledMapParser;

namespace GXPEngine
{
    
    public class Scene : Pivot
    {

        private string levelToLoad;
        private Sprite background;

        public Scene(string map)
        {
            background = new Sprite("background.jpg",false);
            AddChild(background);
            levelToLoad = map;
            Construct();
        }

        public void Construct()
        {
            Map mapData = MapParser.ReadMap(levelToLoad);
            SpawnTiles(mapData);
            SpawnObjects(mapData);
        }

        public void SpawnTiles(Map mapData)
        {
            if (mapData.Layers == null || mapData.Layers.Length == 0)
                return;
            Layer mainLayer = mapData.Layers[0];
            short[,] tileArray = mainLayer.GetTileArray();
            for (int row = 0; row < mainLayer.Height; row++)
            {
                for (int col = 0; col < mainLayer.Width; col++)
                {
                    int tileNumber = tileArray[col, row];
                    TileSet tiles = mapData.GetTileSet(tileNumber);
                    if (tileNumber > 0)
                    {
                        if (levelToLoad == "main_menu.tmx") {//for the main menu
                            int levelNumber = tileNumber;
                            if (tileNumber > 5)
                            {
                                levelNumber = 0;
                            }
                            Button tile = new Button(tiles.Image.FileName, tiles.Columns, tiles.Rows, "level" + levelNumber + ".tmx");
                            tile.SetFrame(tileNumber - tiles.FirstGId);
                            tile.SetXY(col * tile.width, row * tile.height);
                            AddChild(tile);
                        }
                        else//for levels
                        {
                            SceneObject tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                            switch (tileNumber) { 
                                case 9://Save Coordinate
                                    GameManager.saveCoordinateX = col * tile.width;
                                    GameManager.saveCoordinateY = row * tile.height;
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    break;
                                case 10://Exit Coordinate button
                                    //GameManager.playFieldCoordinateX = col * tile.width,;
                                   // GameManager.playFieldCoordinateY = tiles.Rows;
                                     tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    break;
                                case 11://Upcoming Coordinate
                                    GameManager.upcomingBlockClusterX = col * tile.width;
                                    GameManager.upcomingBlockClusterY = row * tile.height;
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    break;
                                case 12://PlayfieldCoordinate
                                    GameManager.playFieldCoordinateX = col * tile.width;
                                    GameManager.playFieldCoordinateY = row * tile.height;
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    break;
                                case 13://Score coordinate
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    GameManager.scoreDisplay = new ScoreDisplay();
                                    GameManager.scoreDisplay.SetXY(col * tile.width, row * tile.height);
                                    AddChild(GameManager.scoreDisplay);
                                    // GameManager.scoreDisplay.SetXY(col, row);
                                    break;
                                default://Scene Objects, that are decoration
                                     tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    break;

                            }
                            tile.SetFrame(tileNumber - tiles.FirstGId);
                            tile.SetXY(col * tile.width, row * tile.height);
                            this.AddChild(tile);



                        }
                    }
                }
            }
        }

        public void SpawnObjects(Map mapData)
        {

        }
    }
}
