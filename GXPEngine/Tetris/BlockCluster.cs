using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{
    public enum Shape
    {
        T,
        I,
        Square,
        J,
        L,
        Z,
        S
    }

    public class BlockCluster : GameObject//This is the shape, the parent containing the small blocks
    {
        private MyGame myGame;
        Shape shape;// the type, aka the shape
        private List<CollisionBlock> blocks = new List<CollisionBlock>();//list with blocks, aka the shape
        public List<CollisionBlock> colliderBlocks, colliderBlocksBottom, colliderBlocksLeft, colliderBlocksRight, colliderBlocksTop;//list with colliderBlocks, aka the "raycast"
        private List<CollisionBlock> colliderBlocksRotation = new List<CollisionBlock>();//list with colliderBlocks used for Rotating the block;
        private Boolean rotatedUpwards = false;// Based on the real game, if the rotate can't place it correctly, it will Move it up. but this only happens once! After that it doesn't rotate
        private float blockSize;
        Boolean isGhostBlock;

        public BlockCluster(Shape _shape, Boolean _isGhostBlock = false)
        {
            isGhostBlock = _isGhostBlock;

            myGame = (MyGame)game;
            blockSize = myGame.gameManager.blockSize;
            colliderBlocks = new List<CollisionBlock>();
            colliderBlocksBottom = new List<CollisionBlock>();
            colliderBlocksLeft = new List<CollisionBlock>();
            colliderBlocksRight = new List<CollisionBlock>();
            colliderBlocksTop = new List<CollisionBlock>();
            CreateShape(_shape);

        }
        public Shape Shape{
            get{
                return shape;
            }
            set{
                shape = value;
            }
        }

        public void CreateShape(Shape _shape)//Trigger the correct method
        {

            Shape = _shape;
            switch (_shape)
            {
                case Shape.T:
                    Console.WriteLine("Constructing T");
                    ConstructTShape();
                    break;
                case Shape.I:
                    Console.WriteLine("Constructing I");
                    ConstructRotatedIShape();
                    break;
                case Shape.Square:
                    Console.WriteLine("Constructing Square");
                    ConstructSquareShape();
                    break;
                case Shape.J:
                    Console.WriteLine("Constructing J");
                    ConstructJShape();
                    break;
                case Shape.L:
                    Console.WriteLine("Constructing L");
                    ConstructLShape();
                    break;
                case Shape.Z:
                    Console.WriteLine("Constructing Z");
                    ConstructZShape();
                    break;
                case Shape.S:
                    Console.WriteLine("Constructing S");
                    ConstructSShape();
                    break;     
            }
            if (isGhostBlock)
            {
                foreach (CollisionBlock block in blocks)
                {
                    block.alpha = 0.5f;
                }
            }
        }

        private void ConstructTShape()
        {
            //create the visual block
            String blockColor = "purple_block.png";
            CollisionBlock block1 = new CollisionBlock(0, -1, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(-1, 0, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(1, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(0, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(1, -1, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            colliderBlocksTop.Add(colBlock3);
            CollisionBlock colBlock4 = new CollisionBlock(-2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock1);
            colliderBlocksLeft.Add(colBlock4);
            CollisionBlock colBlock5 = new CollisionBlock(2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock3);
            colliderBlocksRight.Add(colBlock5);
            CollisionBlock colBlock6 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock7 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock6);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);

        }
        private void ConstructIShape()
        {
            //create the visual block
            String blockColor = "cyan_block.png";
            CollisionBlock block1 = new CollisionBlock(0, -2, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(0, -1, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(0, 1, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(0, -3, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            CollisionBlock colBlock2 = new CollisionBlock(-1, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(-1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock4 = new CollisionBlock(-1, 0, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock5 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock2);
            colliderBlocksLeft.Add(colBlock3);
            colliderBlocksLeft.Add(colBlock4);
            colliderBlocksLeft.Add(colBlock5);
            CollisionBlock colBlock6 = new CollisionBlock(1, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock7 = new CollisionBlock(1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(1, 0, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock9 = new CollisionBlock(1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock6);
            colliderBlocksRight.Add(colBlock7);
            colliderBlocksRight.Add(colBlock8);
            colliderBlocksRight.Add(colBlock9);
            CollisionBlock colBlock10 = new CollisionBlock(0, 2, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock10);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);
            this.AddChild(colBlock9);
            this.AddChild(colBlock10);
        }
        private void ConstructRotatedIShape()
        {
            //create the visual block
            String blockColor = "cyan_block.png";
            CollisionBlock block1 = new CollisionBlock(-2, 0, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(-1, 0, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(1, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-2, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(-1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(0, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock4 = new CollisionBlock(1, -1, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            colliderBlocksTop.Add(colBlock3);
            colliderBlocksTop.Add(colBlock4);
            CollisionBlock colBlock5 = new CollisionBlock(-3, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock5);
            CollisionBlock colBlock6 = new CollisionBlock(2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock6);
            CollisionBlock colBlock7 = new CollisionBlock(-2, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock9 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock10 = new CollisionBlock(1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            colliderBlocksBottom.Add(colBlock9);
            colliderBlocksBottom.Add(colBlock10);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);
            this.AddChild(colBlock9);
            this.AddChild(colBlock10);
        }
        private void ConstructSquareShape()
        {
            //create the visual block
            String blockColor = "yellow_block.png";
            CollisionBlock block1 = new CollisionBlock(-1, -1, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(-1, 0, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, -1, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-1, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(0, -2, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            CollisionBlock colBlock3 = new CollisionBlock(-2, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock4 = new CollisionBlock(-2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock3);
            colliderBlocksLeft.Add(colBlock4);
            CollisionBlock colBlock5 = new CollisionBlock(1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock6 = new CollisionBlock(1, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock5);
            colliderBlocksRight.Add(colBlock6);
            CollisionBlock colBlock7 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);

        }
        private void ConstructJShape()
        {
            //create the visual block
            String blockColor = "blue_block.png";
            CollisionBlock block1 = new CollisionBlock(-1, -1, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(-1, 0, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(1, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-1, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(0, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(1, -1, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            colliderBlocksTop.Add(colBlock3);
            CollisionBlock colBlock4 = new CollisionBlock(-2, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock5 = new CollisionBlock(-2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock4);
            colliderBlocksLeft.Add(colBlock5);
            CollisionBlock colBlock6 = new CollisionBlock(2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock2);
            colliderBlocksRight.Add(colBlock6);
            CollisionBlock colBlock7 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock9 = new CollisionBlock(1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            colliderBlocksBottom.Add(colBlock9);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);
            this.AddChild(colBlock9);
        }
        private void ConstructLShape()
        {
            //create the visual block
            String blockColor = "orange_block.png";
            CollisionBlock block1 = new CollisionBlock(1, -1, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(-1, 0, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(1, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(0, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(1, -2, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            colliderBlocksTop.Add(colBlock3);
            CollisionBlock colBlock4 = new CollisionBlock(-2, 0, blockColor, BlockType.CollisionBlock);

            colliderBlocksLeft.Add(colBlock2);
            colliderBlocksLeft.Add(colBlock4);
            CollisionBlock colBlock5 = new CollisionBlock(2, 0, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock6 = new CollisionBlock(2, -1, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock5);
            colliderBlocksRight.Add(colBlock6);
            CollisionBlock colBlock7 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock9 = new CollisionBlock(1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            colliderBlocksBottom.Add(colBlock9);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);
            this.AddChild(colBlock9);
        }
        private void ConstructZShape()
        {
            //create the visual block
            String blockColor = "red_block.png";
            CollisionBlock block1 = new CollisionBlock(-1, -1, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(0, -1, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(1, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-1, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(0, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(1, -1, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            colliderBlocksTop.Add(colBlock3);
            CollisionBlock colBlock4 = new CollisionBlock(-2, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock5 = new CollisionBlock(-1, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock4);
            colliderBlocksLeft.Add(colBlock5);
            CollisionBlock colBlock6 = new CollisionBlock(2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock3);
            colliderBlocksRight.Add(colBlock6);
            CollisionBlock colBlock7 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(1, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock5);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);
        }
        private void ConstructSShape()
        {
            //create the visual block
            String blockColor = "green_block.png";
            CollisionBlock block1 = new CollisionBlock(0, -1, blockColor, BlockType.Block);
            CollisionBlock block2 = new CollisionBlock(1, -1, blockColor, BlockType.Block);
            CollisionBlock block3 = new CollisionBlock(-1, 0, blockColor, BlockType.Block);
            CollisionBlock block4 = new CollisionBlock(0, 0, blockColor, BlockType.Block);
            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
            this.AddChild(block1);
            this.AddChild(block2);
            this.AddChild(block3);
            this.AddChild(block4);

            //create the colliders around it
            blockColor = "debug_block.png";
            CollisionBlock colBlock1 = new CollisionBlock(-1, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock2 = new CollisionBlock(0, -2, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock3 = new CollisionBlock(1, -2, blockColor, BlockType.CollisionBlock);
            colliderBlocksTop.Add(colBlock1);
            colliderBlocksTop.Add(colBlock2);
            colliderBlocksTop.Add(colBlock3);
            CollisionBlock colBlock4 = new CollisionBlock(-2, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksLeft.Add(colBlock1);
            colliderBlocksLeft.Add(colBlock4);
            CollisionBlock colBlock5 = new CollisionBlock(2, -1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock6 = new CollisionBlock(1, 0, blockColor, BlockType.CollisionBlock);
            colliderBlocksRight.Add(colBlock5);
            colliderBlocksRight.Add(colBlock6);
            CollisionBlock colBlock7 = new CollisionBlock(-1, 1, blockColor, BlockType.CollisionBlock);
            CollisionBlock colBlock8 = new CollisionBlock(0, 1, blockColor, BlockType.CollisionBlock);
            colliderBlocksBottom.Add(colBlock6);
            colliderBlocksBottom.Add(colBlock7);
            colliderBlocksBottom.Add(colBlock8);
            this.AddChild(colBlock1);
            this.AddChild(colBlock2);
            this.AddChild(colBlock3);
            this.AddChild(colBlock4);
            this.AddChild(colBlock5);
            this.AddChild(colBlock6);
            this.AddChild(colBlock7);
            this.AddChild(colBlock8);
        }


        public void MoveDown(string sound = null)//Move the block down by one grid
        {
            if (sound != null)
            {
                new Sound(sound).Play();
            }
            if (IsColliding(colliderBlocksBottom))
            {
                if (!isGhostBlock)
                {
                    SetBlock();
                    new Sound("snap.wav").Play();
                }
            }
            else
            {
                this.y += blockSize;
            }


        }
        public void MoveUp()//Move the block back up, this is used for a upward rotate.
        {
            this.y -= blockSize;

        }

        public void MoveLeft(string sound = null)//Move the block left by one grid
        {
            
            if (!IsColliding(colliderBlocksLeft))
            {
                if (sound != null)
                {
                    new Sound(sound).Play();
                }
                this.x -= blockSize;

            }

        }
        public void MoveRight(string sound = null)//Move the block right by one grid
        {
            if (!IsColliding(colliderBlocksRight))
            {
                if (sound != null)
                {
                    new Sound(sound).Play();
                }
                this.x += blockSize;
            }
            
        }

        public void RotateLeft()
        {
            
            float resetRotation = this.rotation;
            float resetX = this.x;
            float resetY = this.y;

            this.rotation -= 90;
            if(rotation == -360)
            {
                rotation = 0;
            }
            //to keep track which collision block should be on the bottom
            colliderBlocksRotation = colliderBlocksTop;//list with colliderBlocks used for Rotating the block;
            colliderBlocksTop = colliderBlocksRight;//list with colliderBlocks at the top side;
            colliderBlocksRight = colliderBlocksBottom;//list with colliderBlocks at the Right side;
            colliderBlocksBottom = colliderBlocksLeft;//list with colliderBlocks at the Bottom side;
            colliderBlocksLeft = colliderBlocksRotation;//list with colliderBlocks at the Left side;
        
            if (RotationCheck())//check if rotation is broken, if it returns true. Set back to original position.
            {
                this.rotation =resetRotation;
                this.x = resetX;
                this.y = resetY;
                //to keep track which collision block should be on the bottom
                colliderBlocksRotation = colliderBlocksBottom;//list with colliderBlocks used for Rotating the block;
                colliderBlocksBottom = colliderBlocksRight;//list with colliderBlocks at the Bottom side;
                colliderBlocksRight = colliderBlocksTop;//list with colliderBlocks at the Right side;
                colliderBlocksTop = colliderBlocksLeft;//list with colliderBlocks at the top side;
                colliderBlocksLeft = colliderBlocksRotation;//list with colliderBlocks at the Left side;
            }
            else
            {
                new Sound("rotate.wav").Play();
            }
        }
        public void RotateRight()
        {
            float resetRotation = this.rotation;
            float resetX = this.x;
            float resetY = this.y;

            this.rotation += 90;
            if (rotation == 360)
            {
                rotation = 0;
            }

            //to keep track which collision block should be on the bottom
            colliderBlocksRotation = colliderBlocksBottom;//list with colliderBlocks used for Rotating the block;
            colliderBlocksBottom = colliderBlocksRight;//list with colliderBlocks at the Bottom side;
            colliderBlocksRight = colliderBlocksTop;//list with colliderBlocks at the Right side;
            colliderBlocksTop = colliderBlocksLeft;//list with colliderBlocks at the top side;
            colliderBlocksLeft = colliderBlocksRotation;//list with colliderBlocks at the Left side;

            if (RotationCheck())//check if rotation is broken, if it returns true. Set back to original position.
            {
                this.rotation = resetRotation;
                this.x = resetX;
                this.y = resetY;
                //to keep track which collision block should be on the bottom
                colliderBlocksRotation = colliderBlocksTop;//list with colliderBlocks used for Rotating the block;
                colliderBlocksTop = colliderBlocksRight;//list with colliderBlocks at the top side;
                colliderBlocksRight = colliderBlocksBottom;//list with colliderBlocks at the Right side;
                colliderBlocksBottom = colliderBlocksLeft;//list with colliderBlocks at the Bottom side;
                colliderBlocksLeft = colliderBlocksRotation;//list with colliderBlocks at the Left side;
            }
            else
            {
                new Sound("rotate.wav").Play();
            }
        }

        public void SetBlock()
        {
            foreach (CollisionBlock block in blocks)
            {
                block.SetOccupied();
            }
            myGame.gameManager.NextBlockCluster();
        }

        private Boolean RotationCheck()//TODO: Ask teacher how to optimize this, he said it was fine.
        {
            Boolean resetToOriginal = false;
            if (IsCollidingWithWall(true) ){// if it's inside the wall on the left
                if (!IsColliding(colliderBlocksRight))// and can go right
                {
                    MoveRight();// do so.
                    if (IsCollidingWithWall(true))// if it's STILL inside the wall on the left
                    {
                        if (!IsColliding(colliderBlocksRight))// and can go right
                        {
                            MoveRight();// do so.

                        }
                        else// if not, Move back to original position.
                        {
                            resetToOriginal = true;
                        }
                    }
                }
                else// if not, Move back.
                {
                    resetToOriginal = true;
                }
            }
            else if (IsCollidingWithWall(false))
            {// if it's inside the wall on the right
                if (!IsColliding(colliderBlocksLeft))// and can go left
                {
                    MoveLeft();// do so.
                    if (IsCollidingWithWall(false))// if it's STILL inside the wall on the right
                    {
                        if (!IsColliding(colliderBlocksLeft))// and can go left
                        {
                            MoveLeft();// do so.

                        }
                        else// if not, Move back to original position. and it can't rotate
                        {
                            resetToOriginal = true;
                        }
                    }
                }
                else// if not, Move back.
                {
                    resetToOriginal = true;
                }
            }
            else if (IsCollidingWithBottomOrTop(true))
            {
                if (!IsColliding(colliderBlocksTop) && !rotatedUpwards)// can go up
                {
                    MoveUp();// do so.
                    rotatedUpwards = true;
                    if (IsCollidingWithBottomOrTop(true))// if it's still inside the ground, Move up again. This could happen with the I shape!
                    {
                        if (!IsColliding(colliderBlocksTop))// and can go up
                        {
                            MoveUp();// do so.

                        }
                        else// if not, Move back to original position.
                        {
                            resetToOriginal = true;
                        }
                    }
                }
                else// if not, Move back.
                {
                    resetToOriginal = true;
                }
            }
            if (IsColliding(blocks)) //OPTIMISE
            {
                if (!rotatedUpwards)// can go up
                {
                    MoveUp();// do so.
                    rotatedUpwards = true;
                    if (IsColliding(blocks))// if it's still inside a block, Move up again. 
                    {
                        MoveUp();
                        if (IsColliding(blocks))// if it's STILL collding, check left and right.
                        {
                            if (IsColliding(colliderBlocksRight))//if it can Move to the right, do so
                            {
                                MoveRight();
                                if (IsColliding(blocks))// if it's still inside a block, try the left option.
                                {
                                    this.x -= blockSize;//Go back to previous to try the left option. NOTE: MoveLeft wouldn't always work here! using x-= instead
                                    if (IsColliding(colliderBlocksLeft))
                                    {
                                        MoveLeft();
                                        if (IsColliding(blocks))// if it's still inside a block, no rotation can be done and it should go back to roiginal
                                        {
                                            resetToOriginal = true;
                                            rotatedUpwards = false;
                                        }
                                    }
                                    else// if not, no rotation is happening!
                                    {
                                        resetToOriginal = true;
                                        rotatedUpwards = false;
                                    }
                                }
                            }
                            else if (IsColliding(colliderBlocksLeft))
                            {
                                MoveLeft();
                                if (IsColliding(blocks))// if it's still inside a block, no rotation can be done and it should go back to roiginal
                                {
                                    resetToOriginal = true;
                                }
                            }
                            else// if not, no rotation is happening!
                            {
                                resetToOriginal = true;
                            }

                        }

                    }
                }
                else// if not, try one to the left, or one to the right. Else go back to original
                {
                    if (IsColliding(colliderBlocksRight))//if it can Move to the right, do so
                    {
                        this.x += blockSize;
                        if (IsColliding(blocks))// if it's still inside a block, try the left option.
                        {
                            this.x -= blockSize;//Go back to previous to try the left option. NOTE: MoveLeft wouldn't always work here! using x-= instead
                            if (IsColliding(colliderBlocksLeft))
                            {
                                MoveLeft();
                                if (IsColliding(blocks) || IsCollidingWithWall(false) || IsCollidingWithWall(true))// if it's still inside a block or wall, no rotation can be done and it should go back to original
                                {
                                    resetToOriginal = true;
                                }
                            }
                            else
                            {
                                RotateRight();
                                rotatedUpwards = false;
                            }
                        }
                    }
                    else if (IsColliding(colliderBlocksLeft))
                    {
                        this.x -= blockSize;
                        if (IsColliding(blocks) || IsCollidingWithWall(false) || IsCollidingWithWall(true))// if it's still inside a block or wall, no rotation can be done and it should go back to roiginal
                        {
                            resetToOriginal = true;
                        }
                    }
                    else// if not, no rotation is happening!
                    {
                        RotateRight();
                    }
                }
            }
            return resetToOriginal;
        }

        public Boolean IsColliding(List<CollisionBlock> colliderBlocksList)
        {
            foreach (CollisionBlock block in colliderBlocksList)
            {
                if (block.IsColliding())
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean IsCollidingWithWall(Boolean isLeft)
        {
            foreach (CollisionBlock block in blocks)
            {
                
                if (block.IsCollidingWithWall(isLeft))
                {
                   Console.WriteLine("Inside other block!");
                    return true;
                }
            }
            return false;
        }
        private Boolean IsCollidingWithBottomOrTop(Boolean isBottom = true)
        {
            foreach (CollisionBlock block in blocks)
            {

                if (block.IsCollidingWithBottomOrTop(isBottom))
                {
                    Console.WriteLine("Inside other bottom block!");
                    return true;
                }
            }
            return false;
        }
        public Boolean CheckForGameOver()
        {
            if (IsColliding(blocks))
            {
                if (IsColliding(colliderBlocksTop))// in the original tetris, it will Move you one up! But if this isn't possible, then it's game OVER
                {
                    return true;
                }
                else
                {
                    MoveUp();
                }
            }

            return false;
        }
    }
}
