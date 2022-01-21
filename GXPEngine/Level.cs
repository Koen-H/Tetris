using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Tetris;
using TiledMapParser;

namespace GXPEngine
{
    public class Level : GameObject //Level class is used for the level, where the action happens.
    {

        private string levelToLoad;
        private Pivot playField;

        public Level(string map)
        {
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

            playField = new Pivot();
            playField.SetXY(GameManager.playFieldCoordinateX, GameManager.playFieldCoordinateY);
            AddChild(playField);
            Block[,] levelGrid = new Block[mainLayer.Width, mainLayer.Height];
            float blockSize = GameManager.blockSize;
            float playFieldCenterX = 11;
            float playFieldCenterY = 5;

            //  Console.WriteLine(tileArray);
            for (int row = 0; row < mainLayer.Height; row++)
            {
                for (int col = 0; col < mainLayer.Width; col++)
                {
                    int tileNumber = tileArray[col, row];
                    // Console.WriteLine(tileNumber);
                    TileSet tiles = mapData.GetTileSet(tileNumber);
                    //Console.WriteLine(tiles);
                    Block tile; //create a new block!
                    switch (tileNumber)
                    {
                        //Potential improvement to change Block to AnimationSprite to make the block use the spritesheet from tiled. see TempBlock.cs
                        //For now, blockColor will be a direct string to the png. This will deffinetly be improved if I've got time left to do so.
                        case 0://Nothing, this is nothing other than a transparant filler.
                            tile = new Block(col, row, "transparant.png", BlockType.Nothing);
                            break;
                        case 1://GRID
                            tile = new Block(col, row, "empty_block.png", BlockType.Grid);
                            break;
                        case 2://DEBUG - For testing purposes only, really
                            tile = new Block(col, row, "debug_block.png", BlockType.Grid);
                            Console.WriteLine("col = " + col + " row = " + row);
                            break;
                        case 3://WALL-LEFT
                            tile = new Block(col, row, "gray_block.png", BlockType.WallLeft);
                            break;
                        case 4://WALL-BOTTOM
                            tile = new Block(col, row, "gray_block.png", BlockType.WallBottom);
                            break;
                        case 5://WALL-RIGHT
                            tile = new Block(col, row, "gray_block.png", BlockType.WallRight);
                            break;
                        case 6://WALL-TOP
                            tile = new Block(col, row, "gray_block.png", BlockType.WallTop);
                            break;
                        case 7://GRAY-BLOCK
                            tile = new Block(col, row, "gray_block.png", BlockType.GrayBlock);
                            break;
                        case 8: //START POSITION
                            tile = new Block(col, row, "empty_block.png", BlockType.Grid);
                            playFieldCenterX = col * blockSize;
                            playFieldCenterY = row * blockSize;                            
                            break;
                        case 9://CYAN-BLOCK
                            tile = new Block(col, row, "cyan_block.png", BlockType.GridFilled);
                            break;
                        case 10://GREEN-BLOCK
                            tile = new Block(col, row, "green_block.png", BlockType.GridFilled);
                            break;
                        case 11://ORANGE-BLOCK
                            tile = new Block(col, row, "orange_block.png", BlockType.GridFilled);
                            break;
                        case 12://RED-BLOCK
                            tile = new Block(col, row, "red_block.png", BlockType.GridFilled);
                            break;
                        case 13://YELLOW-BLOCK
                            tile = new Block(col, row, "yellow_block.png", BlockType.GridFilled);
                            break;
                        case 14://BLUE-BLOCK
                            tile = new Block(col, row, "blue_block.png", BlockType.GridFilled);
                            break;
                        default:// there shouldn't be a default, so I'll use debug block texture for it.
                            tile = new Block(col, row, "debug_block.png", BlockType.GridFilled);
                            break;

                    }
                    //TempBlock tile = new TempBlock(tiles.Image.FileName, tiles.Columns,tiles.Rows);
                    //tile.SetFrame(tileNumber - tiles.FirstGId);
                    playField.AddChild(tile);
                    levelGrid[col, row] = tile;
                    tile.SetXY(col * blockSize, row * blockSize);
                }
            }

            GameManager.SetupPlayField(mainLayer.Height, mainLayer.Width, playFieldCenterX, playFieldCenterY, levelGrid);
            //GameManager.grid = levelGrid;

            MyGame myGame = (MyGame)game;
            myGame.DestroyAll();
                myGame.playField = playField;
            GameManager.StartTetris();
            // GameManager.CheckForTetris();
        }

        public void SpawnObjects(Map mapData)
        {

        }
    }

}
