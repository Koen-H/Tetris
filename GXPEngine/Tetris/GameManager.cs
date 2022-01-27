using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{

    public class GameManager 
    {

        private MyGame myGame;
        public float dropInterval = 750f;//the time (in miliseconds) it will take to automatically drop one 
        readonly private float minDropInterval = 100f;//the dropSpeed shouldn't be lower than this
        readonly private float difficulty = 5;//will be used to make dropSpeed faster as the player goes on, ( By how much should dropinterval be decreased?)
        private float lastDrop;
        private Boolean gameOver;

        readonly private float moveInterval = 75f;
        private float lastMove;

        public float blockSize = 25;//The size of the blocks (For now sprite)
        private float playFieldCoordinateX = 60;
        private float playFieldCoordinateY = 60;
        private int playFieldHeight = 20;//The height of the canvas, default: 20
        private int playFieldWidth = 10;//The width of the canvas, default: 10
        private float playFieldCenterX; //The center, where the blocks will spawn
        private float playFieldCenterY;//Using tiled, this is no longer the center. More like a starting position. Could be renamed!
        private Block[,] grid; //first x, then y
        private Boolean playingTetris = false;

        public ScoreDisplay scoreDisplay;

        private BlockCluster savedBlockCluster;
        public float saveCoordinateX;
        public float saveCoordinateY;
        private bool savedBlockClusterCooldown;
        private BlockCluster ghostBlockCluster; // This cluster is used to determine and identify the place of the "snap" function. Also know as hard drop.
        private int hardDropScore;

        private List<Shape> upcomingShapes;
        private BlockCluster upcomingBlockCluster;
        private float upcomingBlockClusterX;
        private float upcomingBlockClusterY;
        private BlockCluster upcomingBlockCluster2;
        private BlockCluster upcomingBlockCluster3;

        private BlockCluster currentBlockCluster;

        //To kill the Grid if gameOver is true
        private int yKillGrid;
        readonly private float gridKillerInterval = 350f;
        private float lastGridKilled;

        public GameManager(MyGame _myGame)
        {
            myGame = _myGame;
        }

        public void SetupPlayField(int _playFieldHeight, int _playFieldWidth, float _playFieldCenterX, float _playFieldCenterY, Block[,] _grid)//This function sets the playfield for everything ready to go
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
                        SetGhostBlockClusterPosition();
                    }
                    if (Input.GetKeyDown(Key.F) || Input.GetKeyDown(Key.SPACE))//Save the blockCluster
                    {
                        SaveBlockCluster();

                    }
                    if (Input.GetKeyDown(Key.W))
                    {
                       // currentBlockCluster.MoveUp();
                        //Note, move the current block cluster to the ghost cluster and then set the curren cluster, as the block cluster has incorrect alpha.
                        currentBlockCluster.SetXY(ghostBlockCluster.x, ghostBlockCluster.y); //Disabled, as it breaks the game right now
                        currentBlockCluster.SetBlock();
                        NextBlockCluster();
                        scoreDisplay.ApplyPoints(hardDropScore);
                        new Sound("hard_drop.wav").Play();
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
                        SetGhostBlockClusterPosition();
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
                        SetGhostBlockClusterPosition();

                    }
                    if (Input.GetKeyDown(Key.Q))//Rotate the blockcluster, and the ghostblock cluster
                    {
                        currentBlockCluster.RotateLeft();
                        SetGhostBlockClusterRotation(true);
                    }
                    if (Input.GetKeyDown(Key.E))
                    {
                        currentBlockCluster.RotateRight();
                        SetGhostBlockClusterRotation(false);
                    }

                }

                if (gameOver && Time.time > lastGridKilled + gridKillerInterval && yKillGrid != playFieldHeight)
                {
                    DestroyGrid();
                    lastGridKilled = Time.time;
                }
            }
            
        }
        private void DestroyGrid()//Destroys the grid, It's a "animation" when the game is over.
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
        private void IncreaseDifficulty()// Increases the difficulty
        {
            dropInterval -= difficulty;
            if (dropInterval < minDropInterval)
            {
                dropInterval = minDropInterval;
            }
            if (dropInterval == 400)
            {
                new Sound("speed_up_one.wav").Play();
                myGame.PlayBackgroundMusic("playing_game_fast.wav");
            }
            else if (dropInterval == minDropInterval + difficulty)//play other song
            {
                new Sound("speed_up_two.wav").Play();
                myGame.PlayBackgroundMusic("super_fast_music.wav");// play the other other song, which is even faster!
            }
            Console.WriteLine("Difficulty is now: " + dropInterval);
        }

        public void CheckForTetris(Boolean firstCheck)//check if there is a row filled.
        {
            //if firstCheck is true, it will award points

            Pivot playField = myGame.playField;
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
                            if (myGame.enableScreenShake)
                            {
                                ScreenShake screenShake = new ScreenShake(myGame.playField, 500f, tetrisLines.Count());
                                myGame.AddChild(screenShake);
                            }
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
        private void ClearTetrisLine(int tetrisLine)//remove the tetris lines from the field, then drop the blocks accordingly
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
                        myGame.playField.AddChild(grid[x, y]);
                        }
                        else if(grid[x, y].blockType == BlockType.GrayBlock){
                            ignoreX.Add(x);
                        }
                        ++x;
                    }
                    --y;
                }
            
        }
        private Block GetBlock(int x, int y)//Get a block via the grid using x and Y
        {
            try
            {
                Block blockAbove = grid[x, y];
                if(blockAbove.blockType == BlockType.GridFilled)//if the blockabove is filled, copy it. Else create a new one.
                {
                   // Console.WriteLine("Copied a new block");
                    return new Block(x, y, blockAbove.blockColor, blockAbove.blockType);
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
            
            dropInterval = 750f;
            upcomingShapes = new List<Shape>();
            savedBlockCluster = null;
            upcomingBlockCluster = new BlockCluster(GetRandomShape());
            upcomingBlockCluster2 = new BlockCluster(GetRandomShape());
            upcomingBlockCluster3 = new BlockCluster(GetRandomShape());
            myGame.AddChild(upcomingBlockCluster2);
            myGame.AddChild(upcomingBlockCluster3);
            NextBlockCluster();
            playingTetris = true;
            myGame.PlayBackgroundMusic("playing_game.wav");
        }
        public void QuitTetris()//Stop the game from working.
        {
            playingTetris = false;
        }
        public void NextBlockCluster(Boolean isSave = false)//The sequence to load the next blockCluster
        {
            IncreaseDifficulty();// A new block means a new, higher drop speed!
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
            myGame.playField.AddChild(currentBlockCluster);
            currentBlockCluster.SetXY(playFieldCenterX, playFieldCenterY);
            currentBlockCluster.SetScaleXY(1f,1f);//Change the blockcluster back to the size of the playfield
            upcomingBlockCluster.SetXY(upcomingBlockClusterX,upcomingBlockClusterY);
            upcomingBlockCluster.SetScaleXY(1.7f,1.7f);// make the first upcoming block bigger to indicate it's the next one.
            upcomingBlockCluster2.SetXY(upcomingBlockClusterX, upcomingBlockClusterY + (blockSize * 6));
            upcomingBlockCluster3.SetXY(upcomingBlockClusterX, upcomingBlockClusterY + (blockSize * 12));
            upcomingBlockCluster2.SetScaleXY(1.5f, 1.5f);
            upcomingBlockCluster3.SetScaleXY(1.5f, 1.5f);
            myGame.AddChild(upcomingBlockCluster3);
            CreateGhostBlockCluster();
            savedBlockClusterCooldown = false;
            CheckForGameOver();
        }

        private void SetGhostBlockClusterPosition()//set the coordinates of the ghostblockcluster
        {
            ghostBlockCluster.SetXY(currentBlockCluster.x, currentBlockCluster.y);
            //Console.WriteLine("Setting Ghost Block Cluster" + ghostBlockCluster.IsColliding(ghostBlockCluster.colliderBlocksBottom));
            hardDropScore = 0;
            while (!ghostBlockCluster.IsColliding(ghostBlockCluster.colliderBlocksBottom))
            {
                hardDropScore++;
                ghostBlockCluster.y += blockSize; ;
            }
        }
        private void SetGhostBlockClusterRotation(Boolean rotateLeft)//Rotates the ghostBlockCluster
        {
            ghostBlockCluster.SetXY(currentBlockCluster.x, currentBlockCluster.y);
            if (rotateLeft) ghostBlockCluster.RotateLeft();
            else ghostBlockCluster.RotateRight();
            SetGhostBlockClusterPosition();
        }
        private void CreateGhostBlockCluster()//set the ghostblockcluster, used in nextBlockCluster().
        {
            if (ghostBlockCluster != null)
            {
                ghostBlockCluster.Destroy();
            }
            ghostBlockCluster = new BlockCluster(currentBlockCluster.Shape,true);
            myGame.playField.AddChild(ghostBlockCluster);
            float doRotations = (currentBlockCluster.rotation / 90);
            while (doRotations < 0)
            {
                ghostBlockCluster.RotateLeft();
                doRotations++;
            }
            while (doRotations > 0)
            {
                ghostBlockCluster.RotateRight();
                doRotations--;
            }
            SetGhostBlockClusterPosition();
        }
        private Shape GetRandomShape()//Get a random shape that isn't in the list of upcoming shapes.
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

        public void SaveBlockCluster()// Save the current block cluster to the save slot
        {
            if (!savedBlockClusterCooldown)
            {
                new Sound("save.wav").Play();
                if (savedBlockCluster == null)
                {
                    savedBlockCluster = currentBlockCluster;
                    NextBlockCluster(true);
                }
                else
                {

                    BlockCluster swapBlockCluster;
                    swapBlockCluster = currentBlockCluster;
                    currentBlockCluster = savedBlockCluster;
                    myGame.playField.AddChild(currentBlockCluster);
                    currentBlockCluster.SetScaleXY(1f, 1f);
                    currentBlockCluster.SetXY(playFieldCenterX, playFieldCenterY);
                    savedBlockCluster = swapBlockCluster;
                }
                myGame.AddChild(savedBlockCluster);
                savedBlockCluster.SetScaleXY(1.7f, 1.7f);
                savedBlockCluster.SetXY(saveCoordinateX, saveCoordinateY);
                savedBlockClusterCooldown = true;
                CreateGhostBlockCluster();
            }
        }

        private void CheckForGameOver()//Check if there is a game over.
        {
            gameOver = currentBlockCluster.CheckForGameOver();
            
            if (gameOver)// Do the game over sequence.
            {
                Console.WriteLine("Game over!");
                currentBlockCluster.SetBlock();
                currentBlockCluster.Destroy();
                ghostBlockCluster.Hide();//Hide it, because if I use destroy it crashes the game.
                new Sound("me_game_gameover.wav").Play();
                myGame.PlayBackgroundMusic("game_over_background_music.wav");
                int finalScore = scoreDisplay.score;
                Boolean isNewHighScore = myGame.highscoreDisplay.CheckAndSaveHighScore(finalScore);
                GameOverScreen gameOverScreen = new GameOverScreen(finalScore, isNewHighScore);//open the game over pop-up
                myGame.AddChild(gameOverScreen);
            }
        }

        //To set coordinates correctly, via level.cs
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
            scoreDisplay = new ScoreDisplay("Current Score");
            scoreDisplay.SetXY(_scoreDisplayX, _scoreDisplayY);
        }
        public void SetPlayfieldCoordinates(float _playFieldCoordinateX, float _playFieldCoordinateY)
        {
            playFieldCoordinateX = _playFieldCoordinateX;
            playFieldCoordinateY = _playFieldCoordinateY;
            myGame.playField.SetXY(playFieldCoordinateX,playFieldCoordinateY);
        }
    }
}
