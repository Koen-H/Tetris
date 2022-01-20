using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{

    public static class GameManager  // OPTIMIZE, Make this non-static
    {
      //  public static int score;//player's score

        public static float dropInterval = 1000f;//the time (in miliseconds) it will take to automatically drop one 
        private static float minDropInterval = 100f;//the dropSpeed shouldn't be lower than this
        public static float difficulty = 3;//will be used to make dropSpeed faster as the player goes on, ( By how much should dropinterval be decreased?)
        public static float lastDrop;
        private static Boolean gameOver;

        private static float moveInterval = 75f;
        private static float lastMove;

        public static float blockSize = 25;//The size of the blocks (For now sprite)
        public static float playFieldCoordinateX = 60;
        public static float playFieldCoordinateY = 60;
        public static int playFieldHeight = 20;//The height of the canvas, default: 20
        public static int playFieldWidth = 10;//The width of the canvas, default: 10
        public static float playFieldCenter; //The center, where the blocks will spawn
        public static Block[,] grid; //first x, then y

        public static ScoreDisplay scoreDisplay;

        private static BlockCluster savedBlockCluster;
        public static float saveCoordinateX;
        public static float saveCoordinateY;
        private static bool savedBlockClusterCooldown;

        static List<Shape> upcomingShapes;
        public static BlockCluster upcomingBlockCluster;
        public static float upcomingBlockClusterX;
        public static float upcomingBlockClusterY;
        public static BlockCluster upcomingBlockCluster2;
        public static float upcomingBlockClusterX2;
        public static float upcomingBlockClusterY2;
        public static BlockCluster upcomingBlockCluster3;
        public static float upcomingBlockClusterX3;
        public static float upcomingBlockClusterY3;

        public static BlockCluster currentBlockCluster;
        



        public static void SetupPlayField(int _playFieldHeight, int _playFieldWidth, float _playFieldCenter, Block[,] _grid)
        {
            playFieldHeight = _playFieldHeight;
            playFieldWidth = _playFieldWidth;
            playFieldCenter = _playFieldCenter;
            grid = _grid;
        }

        public static void Update()
        {
            
            if (currentBlockCluster != null && !gameOver)
            {
                if(Time.time > lastDrop + dropInterval || Input.GetKeyDown(Key.S) || (Input.GetKey(Key.S) && Time.time > lastDrop + moveInterval))
                {
                    
                    currentBlockCluster.moveDown(); 
                    lastDrop = Time.time;

                    if (Input.GetKeyDown(Key.S))
                    {
                        lastDrop += 100f;// add a small cooldown for the first press
                    }
                    if (Input.GetKey(Key.S) || Input.GetKeyDown(Key.S))//if the player manually drops it, add one point to the score!
                    {
                        scoreDisplay.ApplyPoints(1);
                    }
                }
                if (Input.GetKeyDown(Key.F))//Save the blockCluster
                {
                    SaveBlockCluster();
                }
                if (Input.GetKeyDown(Key.W))//Goes up for now, 
                {
                    currentBlockCluster.moveUp();
                }
                if (Input.GetKeyDown(Key.A) || (Input.GetKey(Key.A) && Time.time > lastMove + moveInterval))
                {
                    currentBlockCluster.moveLeft();
                    lastMove = Time.time;
                    if (Input.GetKeyDown(Key.A))
                    {
                        lastMove += 100f;// add a small cooldown for the first press
                    }

                }
                if (Input.GetKeyDown(Key.D)|| (Input.GetKey(Key.D) && Time.time > lastMove + moveInterval))
                {
                    currentBlockCluster.moveRight();
                    lastMove = Time.time;
                    if (Input.GetKeyDown(Key.D))
                    {
                        lastMove += 100f;// add a small cooldown for the first press
                    }
                }
                if (Input.GetKeyDown(Key.Q))
                {
                    currentBlockCluster.rotateLeft();
                }
                if (Input.GetKeyDown(Key.E))
                {
                    currentBlockCluster.rotateRight();
                }

            }
        }
        private static void IncreaseDifficulty()
        {
            dropInterval -= difficulty;
            if (dropInterval < minDropInterval)
            {
                dropInterval = minDropInterval;
            }
            Console.WriteLine("Difficulty is now: " + dropInterval);
        }
        public static void CreatePlayField(Pivot playField)//no longer in use, replaced with tiled! Keeping code for backup
        {
            Console.WriteLine("CreatingPlayField");
            playField.SetXY(playFieldCoordinateX, playFieldCoordinateY);
            if (playFieldWidth % 2 == 0) //Set the center of the playfield
            {
                playFieldCenter = ((GameManager.playFieldWidth) * GameManager.blockSize) / 2;
                playFieldCenter += blockSize;// 1 extra block to the right.
            }
            else
            {
                playFieldCenter = ((GameManager.playFieldWidth+1) * GameManager.blockSize) / 2;
            }
            //create the border
            String blockColor = "gray_block_left.png";
            for (int i = 0; i < (playFieldHeight + 1); i++) {//Border on the left;
                Block borderLeft = new Block(0, i, blockColor, BlockType.WallLeft);
                playField.AddChild(borderLeft);
            }
            blockColor = "gray_block_right.png";
            for (int i = 0; i < (playFieldHeight + 1); i++)//Border on the right;
            {
                Block borderRight = new Block(playFieldWidth + 1, i, blockColor, BlockType.WallRight); //+1 to  make it a border
                playField.AddChild(borderRight);
            }
            blockColor = "gray_block_bottom.png";
            for (int i = 1; i < (playFieldWidth+1); i++)//Border on the bottom;
            {
                Block borderBottom = new Block(i, playFieldHeight, blockColor, BlockType.WallBottom);
                playField.AddChild(borderBottom);
            }
            blockColor = "empty_block.png";
            //For the background/grid system
            grid = new Block[playFieldWidth, playFieldHeight];
            int yGrid = 0;
            while (yGrid < playFieldHeight)
            {
                int xGrid = 0;
                while (xGrid < playFieldWidth)
                {
                    Block gridBlock = new Block((xGrid + 1), yGrid, blockColor, BlockType.Grid);
                    playField.AddChild(gridBlock);
                    grid[xGrid,yGrid] = gridBlock;
                    ++xGrid;
                }
                ++yGrid;
            }
        }
        public static void CheckForTetris(Boolean firstCheck)//check if there is a row filled.
        {
            //if firstCheck is true, it will award points

            Pivot playField = MyGame.playField;
            int y = 0;
            List<int> tetrisLines = new List<int>();
            while (y < (playFieldHeight -1)) { //loop through each Y line and check if there is a full line! -1 to not include the floor!
                Boolean tetris = true;// start on true, go to false if there is a blocktype Grid (a grid without a block)
                Boolean isPlayField = false; //Check if the row is within the playfield by checking if there is a filled grid
                int x = 0;
                while (x < playFieldWidth && tetris)
                {
                    if (grid[x, y].blockType == BlockType.GridFilled)//check if the x row contains gridfilled, to make sure it's within the playfield.
                    {
                        isPlayField = true;
                    }

                    if (grid[x,y].blockType == BlockType.Grid)//check if there is any empty block
                    {
                        tetris = false;
                    }
                    
                        ++x;
                }

                if (tetris && isPlayField)//If there is a tetris within a playfield line
                {
                    Console.WriteLine("Tetris on line: " + tetris );
                    tetrisLines.Add(y);
                }
                
                ++y;
            }
            if (tetrisLines.Any())
            {
                if (firstCheck)// If it's the first check, it will apply points to the score.
                {
                    scoreDisplay.TetrisPoints(tetrisLines.Count());
                    IncreaseDifficulty();//increase the difficulty once if there was a tetris!
                }
                tetrisLines.Sort();// sort it from 1, 2, 3 etc
                ClearTetrisLine(tetrisLines.First());
                CheckForTetris(false);
                
            }
        }
        private static void ClearTetrisLine(int tetrisLine)//remove tetris lines from the field, then drop the blocks accordingly
        {
            //int linesOfTetris = tetrisLines.Count();

            //Console.WriteLine("Removing tetris on line:" + tetrisLine);
                int y = tetrisLine;
                int x = 0;
                List<int> ignoreX = new List<int>();//these x's wont be updated during the current clear because there was a gray block detected below.
                while (y > 0) {
                    x = 0;
                    while (x < playFieldWidth ) 
                    {
                    if(!ignoreX.Contains(x))
                        if(grid[x,y].blockType == BlockType.GridFilled || grid[x, y].blockType == BlockType.Grid)
                        {
                        grid[x, y].Destroy();
                        grid[x, y] = GetBlockAbove(x,y-1);
                        grid[x, y].SetXY(x* blockSize,y*blockSize);
                        MyGame.playField.AddChild(grid[x, y]);
                        }
                        else if(grid[x, y].blockType == BlockType.GrayBlock){
                            ignoreX.Add(x);
                        }
                        ++x;
                    }
                    --y;
                }
            
        }
        private static Block GetBlockAbove(int x, int y)
        {
            try
            {
                Block blockAbove = grid[x, y];
                if(blockAbove.blockType == BlockType.GridFilled)//if the blockabove is filled, copy it. Else create a new one.
                {
                   // Console.WriteLine("Copied a new block");
                    return new Block(x, y, blockAbove.blockSprite, blockAbove.blockType);
                }
                //Console.WriteLine("Created a new block");
                return new Block(x, y, "empty_block.png", BlockType.Grid);


            }
            catch
            {
                Console.WriteLine("Error, created a new block");
                return new Block(x, y, "empty_block.png", BlockType.Grid);
            }

            
        }
        public static void StartTetris()//Set the playfield ready to go!
        {
            upcomingShapes = new List<Shape>();
            upcomingBlockCluster = new BlockCluster(GetRandomShape());
            NextBlockCluster();

        }
        public static void NextBlockCluster(Boolean isSave = false)
        {
            IncreaseDifficulty();
            if (currentBlockCluster != null)
            {
                upcomingShapes.Remove(currentBlockCluster.Shape);
                if (!isSave) { 
                    CheckForTetris(true);
                    currentBlockCluster.Destroy();
                }
                
                
            }
            currentBlockCluster = upcomingBlockCluster;
            MyGame.playField.AddChild(currentBlockCluster);
            currentBlockCluster.SetXY(playFieldCenter, blockSize * 2);
            upcomingBlockCluster = new BlockCluster(GetRandomShape());
            upcomingBlockCluster.SetXY(upcomingBlockClusterX,upcomingBlockClusterY);
            MyGame.playField.AddChild(upcomingBlockCluster);
            savedBlockClusterCooldown = false;
            CheckForGameOver();
        }

        private static Shape GetRandomShape()
        {
            Array values = Enum.GetValues(typeof(Shape));
            Shape randomShape;
            do
            {
                Random random = new Random();
                randomShape = (Shape)values.GetValue(random.Next(values.Length));
            }
            while (upcomingShapes.Contains(randomShape));

            upcomingShapes.Add(randomShape);
            return randomShape;
        }

        public static void SaveBlockCluster()
        {
            if(!savedBlockClusterCooldown)
                if (savedBlockCluster == null)
                {
                    savedBlockCluster = currentBlockCluster;
                        savedBlockCluster.SetXY(saveCoordinateX, saveCoordinateY);
                        NextBlockCluster(true);
                        Console.WriteLine("No saved blockcluster found, created a new one!");
                }
                else
                {
                    
                    BlockCluster swapBlockCluster;
                    swapBlockCluster = currentBlockCluster;
                    currentBlockCluster = savedBlockCluster;
                    MyGame.playField.AddChild(currentBlockCluster);
                    currentBlockCluster.SetXY(playFieldCenter, blockSize * 2);
                    savedBlockCluster = swapBlockCluster;
                    MyGame.uI.RemoveChild(savedBlockCluster);
                    Console.WriteLine(saveCoordinateX);
                    savedBlockCluster.SetXY(saveCoordinateX, saveCoordinateY);
                    Console.WriteLine("Saved blockcluster found, swapped them around!");

                }
                savedBlockClusterCooldown = true;
        }

        private static void CheckForGameOver()
        {
            gameOver = currentBlockCluster.CheckForGameOver();
            
            if (gameOver)
            {
                Console.WriteLine("Game over!");
                currentBlockCluster.Destroy();
            }
        }
    }
}
