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
        Shape shape;// the type, aka the shape
        private List<CollisionBlock> blocks = new List<CollisionBlock>();//list with blocks, aka the shape
        private List<CollisionBlock> colliderBlocks, colliderBlocksBottom, colliderBlocksLeft, colliderBlocksRight, colliderBlocksTop;//list with colliderBlocks, aka the "raycast"
        private List<CollisionBlock> colliderBlocksRotation = new List<CollisionBlock>();//list with colliderBlocks used for Rotating the block;
        private Boolean rotatedUpwards = false;// Based on the real game, if the rotate can't place it correctly, it will move it up. but this only happens once! After that it doesn't rotate

        public BlockCluster(Shape shape)
        {
            colliderBlocks = new List<CollisionBlock>();
            colliderBlocksBottom = new List<CollisionBlock>();
            colliderBlocksLeft = new List<CollisionBlock>();
            colliderBlocksRight = new List<CollisionBlock>();
            colliderBlocksTop = new List<CollisionBlock>();
            this.shape = shape;
            createShape(shape);
        }
        public Shape Shape{
            get{
                return shape;
            }
            set{
                shape = value;
            }
        }

        public void createShape(Shape _shape)//Trigger the correct method
        {

            Shape = _shape;
            switch (_shape)
            {
                case Shape.T:
                    Console.WriteLine("Constructing T");
                    constructTShape();
                    break;
                case Shape.I:
                    Console.WriteLine("Constructing I");
                    constructRotatedIShape();
                    break;
                case Shape.Square:
                    Console.WriteLine("Constructing Square");
                    constructSquareShape();
                    break;
                case Shape.J:
                    Console.WriteLine("Constructing J");
                    constructJShape();
                    break;
                case Shape.L:
                    Console.WriteLine("Constructing L");
                    constructLShape();
                    break;
                case Shape.Z:
                    Console.WriteLine("Constructing Z");
                    constructZShape();
                    break;
                case Shape.S:
                    Console.WriteLine("Constructing S");
                    constructSShape();
                    break;     
            }
               
        }

        private void constructTShape()
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
        private void constructIShape()
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
        private void constructRotatedIShape()
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
        private void constructSquareShape()
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
        private void constructJShape()
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
        private void constructLShape()
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
        private void constructZShape()
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
        private void constructSShape()
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


        public void moveDown()//move the block down by one grid
        {
            if (isColliding(colliderBlocksBottom))
            {
                foreach (CollisionBlock block in blocks)
                {
                    block.setOccupied();
                }
                GameManager.NextBlockCluster();
            }
            else
            {
                this.y += GameManager.blockSize;
            }


        }
        public void moveUp()//move the block back up, this is used for a upward rotate.
        {
            this.y -= GameManager.blockSize;

        }
        public void moveLeft()//move the block left by one grid
        {
            if (!isColliding(colliderBlocksLeft))
            {
                this.x -= GameManager.blockSize;
               
            }
            
        }
        public void moveRight()//move the block right by one grid
        {
            if (!isColliding(colliderBlocksRight))
            {
                this.x += GameManager.blockSize;
            }
            
        }

        public void rotateLeft()
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
        
            if (rotationCheck())//check if rotation is broken, if it returns true. Set back to original position.
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
        }
        public void rotateRight()
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

            if (rotationCheck())//check if rotation is broken, if it returns true. Set back to original position.
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
        }

        private Boolean rotationCheck()//TODO: Ask teacher how to optimize this
        {
            Boolean resetToOriginal = false;
            if (isCollidingWithWall(true) ){// if it's inside the wall on the left
                if (!isColliding(colliderBlocksRight))// and can go right
                {
                    moveRight();// do so.
                    if (isCollidingWithWall(true))// if it's STILL inside the wall on the left
                    {
                        if (!isColliding(colliderBlocksRight))// and can go right
                        {
                            moveRight();// do so.

                        }
                        else// if not, move back to original position.
                        {
                            resetToOriginal = true;
                        }
                    }
                }
                else// if not, move back.
                {
                    resetToOriginal = true;
                }
            }
            else if (isCollidingWithWall(false))
            {// if it's inside the wall on the right
                if (!isColliding(colliderBlocksLeft))// and can go left
                {
                    moveLeft();// do so.
                    if (isCollidingWithWall(false))// if it's STILL inside the wall on the right
                    {
                        if (!isColliding(colliderBlocksLeft))// and can go left
                        {
                            moveLeft();// do so.

                        }
                        else// if not, move back to original position. and it can't rotate
                        {
                            resetToOriginal = true;
                        }
                    }
                }
                else// if not, move back.
                {
                    resetToOriginal = true;
                }
            }
            else if (IsCollidingWithBottomOrTop(true))
            {
                if (!isColliding(colliderBlocksTop) && !rotatedUpwards)// can go up
                {
                    moveUp();// do so.
                    rotatedUpwards = true;
                    if (IsCollidingWithBottomOrTop(true))// if it's still inside the ground, move up again. This could happen with the I shape!
                    {
                        if (!isColliding(colliderBlocksTop))// and can go up
                        {
                            moveUp();// do so.

                        }
                        else// if not, move back to original position.
                        {
                            resetToOriginal = true;
                        }
                    }
                }
                else// if not, move back.
                {
                    resetToOriginal = true;
                }
            }
            if (isColliding(blocks)) //OPTIMISE
            {
                if (!rotatedUpwards)// can go up
                {
                    moveUp();// do so.
                    rotatedUpwards = true;
                    if (isColliding(blocks))// if it's still inside a block, move up again. 
                    {
                        moveUp();
                        if (isColliding(blocks))// if it's STILL collding, check left and right.
                        {
                            if (isColliding(colliderBlocksRight))//if it can move to the right, do so
                            {
                                moveRight();
                                if (isColliding(blocks))// if it's still inside a block, try the left option.
                                {
                                    this.x -= GameManager.blockSize;//Go back to previous to try the left option. NOTE: MoveLeft wouldn't always work here! using x-= instead
                                    if (isColliding(colliderBlocksLeft))
                                    {
                                        moveLeft();
                                        if (isColliding(blocks))// if it's still inside a block, no rotation can be done and it should go back to roiginal
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
                            else if (isColliding(colliderBlocksLeft))
                            {
                                moveLeft();
                                if (isColliding(blocks))// if it's still inside a block, no rotation can be done and it should go back to roiginal
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
                    if (isColliding(colliderBlocksRight))//if it can move to the right, do so
                    {
                        this.x += GameManager.blockSize;
                        if (isColliding(blocks))// if it's still inside a block, try the left option.
                        {
                            this.x -= GameManager.blockSize;//Go back to previous to try the left option. NOTE: MoveLeft wouldn't always work here! using x-= instead
                            if (isColliding(colliderBlocksLeft))
                            {
                                moveLeft();
                                if (isColliding(blocks) || isCollidingWithWall(false) || isCollidingWithWall(true))// if it's still inside a block or wall, no rotation can be done and it should go back to original
                                {
                                    resetToOriginal = true;
                                }
                            }
                            else
                            {
                                rotateRight();
                                rotatedUpwards = false;
                            }
                        }
                    }
                    else if (isColliding(colliderBlocksLeft))
                    {
                        this.x -= GameManager.blockSize;
                        if (isColliding(blocks) || isCollidingWithWall(false) || isCollidingWithWall(true))// if it's still inside a block or wall, no rotation can be done and it should go back to roiginal
                        {
                            resetToOriginal = true;
                        }
                    }
                    else// if not, no rotation is happening!
                    {
                        rotateRight();
                    }
                }
            }
            return resetToOriginal;
        }

        private Boolean isColliding(List<CollisionBlock> colliderBlocksList)
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
        private Boolean isCollidingWithWall(Boolean isLeft)
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
            if (isColliding(blocks))
            {
                if (isColliding(colliderBlocksTop))// in the original tetris, it will move you one up! But if this isn't possible, then it's game OVER
                {
                    return true;
                }
                else
                {
                    moveUp();
                }
            }

            return false;
        }
    }
}
