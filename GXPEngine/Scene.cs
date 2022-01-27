using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Tetris;
using TiledMapParser;

namespace GXPEngine
{
    
    public class Scene : GameObject
    {
        private MyGame myGame;

        readonly private string levelToLoad;
        readonly private Sprite background;

        public Scene(string map)
        {
            myGame = (MyGame)game;
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
                        /* if (levelToLoad == "scene_main_menu.tmx") //for the old menu
                          {
                             int levelNumber = tileNumber;
                             if (tileNumber > 5)
                             {
                                 levelNumber = 0;
                             }
                             Button tile = new Button(tiles.Image.FileName, tiles.Columns, tiles.Rows, "level" + levelNumber + ".tmx");
                             tile.SetFrame(tileNumber - tiles.FirstGId);
                             tile.SetXY(col * tile.width, row * tile.height);
                             AddChild(tile);
                         }*/
                        if (levelToLoad == "scene_main_menu.tmx")
                        {//for the main menu
                            int levelNumber = -1;//This is one weird way to do it. But it i'll do the job...
                            switch (tileNumber)
                            {
                                case 3://level 2
                                    levelNumber = 2;
                                    break;
                                case 4://level 1
                                    levelNumber = 1;
                                    break;
                                case 5://level 3
                                    levelNumber = 3;
                                    break;
                                case 6:// level 5
                                    levelNumber = 5;
                                    break;
                                case 7://level 0 (classic)
                                    levelNumber = 0;
                                    break;
                                case 8://level 4
                                    levelNumber = 4;
                                   
                                    break;
                            }
                            AnimationSprite tile;
                            if (levelNumber != -1)
                            {
                                 
                                tile = new Button(tiles.Image.FileName, tiles.Columns, tiles.Rows, "level" + levelNumber + ".tmx");
                            }
                            else
                            {
                                tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                            }
                            tile.SetFrame(tileNumber - tiles.FirstGId);
                            tile.SetXY(col * tile.width, row * tile.height);
                            AddChild(tile);

                        }
                        else//for levels
                        {
                            SceneObject tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                            int tileFrame = tileNumber - tiles.FirstGId;
                            switch (tileNumber) { 
                                case 9://Save Coordinate

                                    myGame.gameManager.SetSaveCoordinates(col * tile.width, row * tile.height);
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    tileFrame = 0;
                                    break;
                                case 10://Exit Coordinate button
                                    Button exitButton = new Button("exit_button.png", 1, 1, "main_menu.tmx");
                                    exitButton.SetFrame(1);
                                    exitButton.SetXY(col * tile.width, row * tile.height);
                                    exitButton.SetScaleXY(0.8f,0.8f);
                                    AddChild(exitButton);
                                    tileFrame = 16;
                                    break;
                                case 11://Upcoming Coordinate
                                    myGame.gameManager.SetUpcomingCoordinates(col * tile.width, row * tile.height);
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    tileFrame = 16;
                                    break;
                                case 12://PlayfieldCoordinate
                                    myGame.gameManager.SetPlayfieldCoordinates(col * tile.width, row * tile.height);
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    tileFrame = 16;
                                    break;
                                case 13://Score coordinate
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    myGame.gameManager.SetScoreDisplayCoordinates(col * tile.width, row * tile.height);
                                    myGame.gameManager.scoreDisplay = new ScoreDisplay("Current Score");
                                    myGame.gameManager.scoreDisplay.SetXY(col * tile.width, row * tile.height);
                                    AddChild(myGame.gameManager.scoreDisplay);
                                    // GameManager.scoreDisplay.SetXY(col, row);
                                    tileFrame = 16;
                                    break;
                                case 14:// HighScore coordinate
                                    tileFrame = 16;
                                    HighscoresDisplay highscoreDisplay = new HighscoresDisplay(myGame.currentLevel);
                                    highscoreDisplay.SetXY(col * tile.width, row * tile.height);
                                    myGame.highscoreDisplay = highscoreDisplay;
                                    AddChild(highscoreDisplay);
                                    break;
                                case 15:// KeybindInfo coordinate
                                    tileFrame = 16;
                                    SceneObject keyBindInfo = new SceneObject("keybinds.png", 1, 1);
                                    keyBindInfo.SetFrame(1);
                                    keyBindInfo.SetXY(col * tile.width, row * tile.height);
                                    keyBindInfo.SetScaleXY(1.0f, 1.0f);
                                    AddChild(keyBindInfo);
                                    break;
                                default://Scene Objects, that are decoration
                                    tile = new SceneObject(tiles.Image.FileName, tiles.Columns, tiles.Rows);
                                    break;
                            }
                            tile.SetFrame(tileFrame);
                            tile.SetXY(col * tile.width, row * tile.height);
                            AddChild(tile);
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
