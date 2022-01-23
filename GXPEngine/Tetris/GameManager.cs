using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{

    public class GameManager  // OPTIMIZE, Make this non-static
    {
        //  public static int score;//player's score
        private MyGame game;
        public static float dropInterval = 1000f;//the time (in miliseconds) it will take to automatically drop one 
        private static float minDropInterval = 100f;//the dropSpeed shouldn't be lower than this
        public static float difficulty = 3;//will be used to make dropSpeed faster as the player goes on, ( By how much should dropinterval be decreased?)
        public static float lastDrop;
        private static Boolean gameOver;

        private static float moveInterval = 75f;
        private static float lastMove;

        public float blockSize = 25;//The size of the blocks (For now sprite)
        public static float playFieldCoordinateX = 60;
        public static float playFieldCoordinateY = 60;
        public static int playFieldHeight = 20;//The height of the canvas, default: 20
        public static int playFieldWidth = 10;//The width of the canvas, default: 10
        public static float playFieldCenterX; //The center, where the blocks will spawn
        public static float playFieldCenterY;//Using tiled, this is no longer the center. More like a starting position. Could be renamed!
        public static Block[,] grid; //first x, then y
        public Boolean playingTetris = false;

        public static ScoreDisplay scoreDisplay;

        private BlockCluster savedBlockCluster;
        public float saveCoordinateX;
        public float saveCoordinateY;
        private bool savedBlockClusterCooldown;

        static List<Shape> upcomingShapes;
        public BlockCluster upcomingBlockCluster;
        public float upcomingBlockClusterX;
        public float upcomingBlockClusterY;
        public BlockCluster upcomingBlockCluster2;
        public float upcomingBlockClusterX2;
        public float upcomingBlockClusterY2;
        public BlockCluster upcomingBlockCluster3;
        public float upcomingBlockClusterX3;
        public float upcomingBlockClusterY3;

        public static BlockCluster currentBlockCluster;

        //To kill the Grid if gameOver is true
        private int yKillGrid;
        private float gridKillerInterval = 350f;
        private float lastGridKilled;

        public GameManager(MyGame _game)
        {
            game = _game;
        }

        public void SetupPlayField(int _playFieldHeight, int _playFieldWidth, float _playFieldCenterX, float _playFieldCenterY, Block[,] _grid)
        {
            yKillGrid = 0;
            playFieldHeight = _playFieldHeight;
            playFieldWidth = _playFieldWidth;
            playFieldCenterX = _playFieldCenterX;
            playFieldCenterY = _playFieldCenterY;
            grid = _grid;
        }

        public void Update()
        {
            if (playingTetris)
            {
                if (currentBlockCluster != null && !gameOver)
                {
                    if (Time.time > lastDrop + dropInterval || Input.GetKeyDown(Key.S) || (Input.GetKey(Key.S) && Time.time > lastDrop + moveInterval))
                    {

                        string sound = null;
                        lastDrop = Time.time;

                        if (Input.GetKeyDown(Key.S))
                        {
                            lastDrop += 100f;// add a small cooldown for the first press
                            sound = "softdrop.wav";
                        }
                        if (Input.GetKey(Key.S) || Input.GetKeyDown(Key.S))//if the player manually drops it, add one point to the score!
                        {   
                            scoreDisplay.ApplyPoints(1);
                            sound = "softdrop.wav";
                        }
                        //note, no sound effect if it gets dropped by the game itself!
                        currentBlockCluster.MoveDown(sound);
                    }
                    if (Input.GetKeyDown(Key.F))//Save the blockCluster
                    {
                        SaveBlockCluster();
                    }
                    if (Input.GetKeyDown(Key.W))//Goes up for now, 
                    {
                        currentBlockCluster.MoveUp();
                    }
                    if (Input.GetKey(Key.A) && Time.time > lastMove + moveInterval)
                    {
                        string sound = "move.wav";
                        lastMove = Time.time;
                        if (Input.GetKeyDown(Key.A))//first time click, play a different sound effect, defined above.
                        {
                            lastMove += 100f;// add a small cooldown for the first press
                        }
                        else if (Time.time > lastMove + moveInterval)
                        {
                            sound = "move_hold.wav";
                        }
                        currentBlockCluster.MoveLeft(sound);
                    }
                    if (Input.GetKey(Key.D) && Time.time > lastMove + moveInterval)
                    {

                        string sound = "move.wav";
                        lastMove = Time.time;
                        if (Input.GetKeyDown(Key.D))//first time click, play a different sound effect, defined above.
                        {
                            lastMove += 100f;// add a small cooldown for the first press
                        }
                        else if (Time.time > lastMove + moveInterval)
                        {
                            sound = "move_hold.wav";
                        }
                        currentBlockCluster.MoveRight(sound);

                    }
                    if (Input.GetKeyDown(Key.Q))
                    {
                        currentBlockCluster.RotateLeft();
                    }
                    if (Input.GetKeyDown(Key.E))
                    {
                        currentBlockCluster.RotateRight();
                    }

                }
                if (gameOver && Time.time > lastGridKilled + gridKillerInterval && yKillGrid != playFieldHeight)
                {
                    DestroyGrid();
                    lastGridKilled = Time.time;
                }
            }
            
        }
        private void DestroyGrid()
        {
            int x = 0;
            while (x < playFieldWidth)
            {
                if (grid[x, yKillGrid] != null)
                {
                    grid[x, yKillGrid].Destroy();
                }
                ++x;
            }
                ++yKillGrid;
        }
        private void IncreaseDifficulty()
        {
            dropInterval -= difficulty;
            if (dropInterval < minDropInterval)
            {
                dropInterval = minDropInterval;
            }
            Console.WriteLine("Difficulty is now: " + dropInterval);
        }
       /* public static void CreatePlayField(Pivot playField)//no longer in use, replaced with tiled! Keeping code for backup
        {
            Console.WriteLine("CreatingPlayField");
            playField.SetXY(playFieldCoordinateX, playFieldCoordinateY);
            if (playFieldWidth % 2 == 0) //Set the center of the playfield
            {
                playFieldCenterX = ((GameManager.playFieldWidth) * GameManager.blockSize) / 2;
                playFieldCenterX += blockSize;// 1 extra block to the right.
            }
            else
            {
                playFieldCenterX = ((GameManager.playFieldWidth+1) * GameManager.blockSize) / 2;
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
        */
        public void CheckForTetris(Boolean firstCheck)//check if there is a row filled.
        {
            //if firstCheck is true, it will award points

            Pivot playField = game.playField;
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
                    switch (tetrisLines.Count())
                    {
                        case 2:
                            new Sound("tetris_double.wav").Play();
                            break;
                        case 3:
                            new Sound("tetris_triple.wav").Play();
                            break;
                        case 4:
                            new Sound("tetris_quad.wav").Play();
                            break;
                        default:
                            new Sound("tetris.wav").Play();
                            break;
                    }
                    
                    scoreDisplay.TetrisPoints(tetrisLines.Count());
                    IncreaseDifficulty();//increase the difficulty once if there was a tetris!
                }
                tetrisLines.Sort();// sort it from 1, 2, 3 etc
                ClearTetrisLine(tetrisLines.First());
                CheckForTetris(false);
                
            }
        }
        private void ClearTetrisLine(int tetrisLine)//remove tetris lines from the field, then drop the blocks accordingly
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
                        grid[x, y] = GetBlock(x,y-1);
                        grid[x, y].SetXY(x* blockSize,y*blockSize);
                        game.playField.AddChild(grid[x, y]);
                        }
                        else if(grid[x, y].blockType == BlockType.GrayBlock){
                            ignoreX.Add(x);
                        }
                        ++x;
                    }
                    --y;
                }
            
        }
        private Block GetBlock(int x, int y)
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
        public void StartTetris()//Set the playfield ready to go!
        {
            
            upcomingShapes = new List<Shape>();
            upcomingBlockCluster = new BlockCluster(GetRandomShape());
            upcomingBlockCluster2 = new BlockCluster(GetRandomShape());
            upcomingBlockCluster3 = new BlockCluster(GetRandomShape());
            game.AddChild(upcomingBlockCluster2);
            game.AddChild(upcomingBlockCluster3);
            NextBlockCluster();
            playingTetris = true;


        }
        public void QuitTetris()
        {
            playingTetris = false;
        }
        public void NextBlockCluster(Boolean isSave = false)
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
            upcomingBlockCluster = upcomingBlockCluster2;
            upcomingBlockCluster2 = upcomingBlockCluster3;
            upcomingBlockCluster3 = new BlockCluster(GetRandomShape());
            game.playField.AddChild(currentBlockCluster);
            currentBlockCluster.SetXY(playFieldCenterX, playFieldCenterY);
            currentBlockCluster.SetScaleXY(1f,1f);//Change the blockcluster back to the size of the playfield
            upcomingBlockCluster.SetXY(upcomingBlockClusterX,upcomingBlockClusterY);
            upcomingBlockCluster.SetScaleXY(1.6f,1.6f);// make the first upcoming block bigger to indicate it's the next one.
            upcomingBlockCluster2.SetXY(upcomingBlockClusterX, upcomingBlockClusterY + (blockSize * 8));
            upcomingBlockCluster3.SetXY(upcomingBlockClusterX, upcomingBlockClusterY + (blockSize * 14));
            upcomingBlockCluster.SetScaleXY(1.2f, 1.2f);
            upcomingBlockCluster.SetScaleXY(1.2f, 1.2f);
            game.AddChild(upcomingBlockCluster3);
            savedBlockClusterCooldown = false;
            CheckForGameOver();
        }

        private Shape GetRandomShape()
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

        public void SaveBlockCluster()
        {
            if(!savedBlockClusterCooldown)
                if (savedBlockCluster == null)
                {
                    savedBlockCluster = currentBlockCluster;
                        NextBlockCluster(true);
                        Console.WriteLine("No saved blockcluster found, created a new one!");
                }
                else
                {
                    
                    BlockCluster swapBlockCluster;
                    swapBlockCluster = currentBlockCluster;
                    currentBlockCluster = savedBlockCluster;
                    game.playField.AddChild(currentBlockCluster);
                    currentBlockCluster.SetScaleXY(1f,1f);
                    currentBlockCluster.SetXY(playFieldCenterX, playFieldCenterY);
                    savedBlockCluster = swapBlockCluster;
                    Console.WriteLine("Saved blockcluster found, swapped them around!");
                }
            game.AddChild(savedBlockCluster);
            savedBlockCluster.SetScaleXY(1.6f,1.6f);
            savedBlockCluster.SetXY(saveCoordinateX, saveCoordinateY);
            savedBlockClusterCooldown = true;
        }

        private void CheckForGameOver()
        {
            gameOver = currentBlockCluster.CheckForGameOver();
            
            if (gameOver)
            {
                Console.WriteLine("Game over!");
                currentBlockCluster.Destroy();
                new Sound("me_game_gameover.wav").Play(); 
            }
        }

        public void SetUpcomingCoordinates(float _upcomingBlockClusterX, float _upcomingBlockClusterY)
        {
            upcomingBlockClusterX = _upcomingBlockClusterX;
            upcomingBlockClusterY = _upcomingBlockClusterY;
                
        }
        public void SetSaveCoordinates(float _saveCoordinateX, float _saveCoordinateY)
        {
            saveCoordinateX = _saveCoordinateX;
            saveCoordinateY = _saveCoordinateY;
        }
        public void SetScoreDisplayCoordinates(float _scoreDisplayX, float _scoreDisplayY)
        {
            scoreDisplay = new ScoreDisplay();
            scoreDisplay.SetXY(_scoreDisplayX, _scoreDisplayY);
        }
        public void SetPlayfieldCoordinates(float _playFieldCoordinateX, float _playFieldCoordinateY)
        {
            playFieldCoordinateX = _playFieldCoordinateX;
            playFieldCoordinateY = _playFieldCoordinateY;
            game.playField.SetXY(playFieldCoordinateX,playFieldCoordinateY);
        }
    }
}
