using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{
    //TODO, Make this extend with Block.cs
    public class CollisionBlock : Sprite //this is one block, a BlockCluster is the shape.
    {
        // bool isCollisionCheckBlock;
        protected String blockColor;
        BlockType blockType;
        float xParentPos, yParentPos, blockSize;
       // GameObject colliding;



        public CollisionBlock(float xParentPos, float yParentPos, String blockColor, BlockType blockType) : base(blockColor)
        {
            this.blockSize = GameManager.blockSize;
            this.xParentPos = xParentPos;
            this.yParentPos = yParentPos;
            this.blockColor = blockColor;
            this.blockType = blockType;
            this.SetXY(blockSize * xParentPos, blockSize * yParentPos);
            //AddChild(blockSprite);
            //setSprite("debug_block.png");
        }

        private void setSprite(String blockColor)
        {
            this.initializeFromTexture(Texture2D.GetInstance(blockColor, false));
        }

        /*void OnCollision(GameObject other)
        {
            if (other is Block)
            {
                Block block = (Block)other;
                if (block.blockType == BlockType.GridFilled || block.blockType == BlockType.WallBottom)
                {
                    GameManager.currentBlockCluster.isColliding = true;
                }
            }
            /*
          //  this.colliding = other;
            //Console.WriteLine(other.name);
            if (blockType == BlockType.Block)
            {
                Console.WriteLine("It's a block");

                if (other.name == "gray_block_left.png")
                {
                    //parent.moveRight() doesn't work
                    GameManager.currentBlockCluster.moveRight();
                    Console.WriteLine(this.parent.name);
                }
                else if (other.name == "gray_block_right.png")
                {
                    //parent.moveLeft() doesn't work
                    GameManager.currentBlockCluster.moveLeft();
                    Console.WriteLine(this.parent.name);
                }
                else if (other.name == "gray_block_bottom.png")
                {
                    //parent.moveLeft() doesn't work
                    GameManager.currentBlockCluster.isColliding = true;
                    Console.WriteLine(this.parent.name);
                }
            }

        }*/
        public Boolean IsColliding()//Used to check if the clusterlbock/shape can move a direction.
        {
            GameObject[] collisions = GetCollisions();
            foreach (GameObject collision in collisions)
            {
                if (collision is Block)
                {
                    Block block = (Block)collision;
                    if (block.blockType == BlockType.GridFilled || block.blockType == BlockType.WallBottom || block.blockType == BlockType.WallLeft || block.blockType == BlockType.WallRight || block.blockType == BlockType.WallTop || block.blockType == BlockType.GrayBlock) //OPTIMIZE
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public Boolean IsCollidingWithWall(Boolean isLeft)
        {
           
            GameObject[] collisions = GetCollisions();
            foreach (GameObject collision in collisions)
            {
                
                if (collision is Block)
                {
                    Block block = (Block)collision;
                    
                    if (block.blockType == BlockType.WallRight && !isLeft)
                    {
                        return true;
                    }
                    if (block.blockType == BlockType.WallLeft && isLeft)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public Boolean IsCollidingWithBottomOrTop(Boolean isBottom)
        {
            GameObject[] collisions = GetCollisions();
            foreach (GameObject collision in collisions)
            {

                if (collision is Block)
                {
                    Block block = (Block)collision;

                    if (block.blockType == BlockType.WallBottom && isBottom)
                    {
                        return true;
                    }
                    if (block.blockType == BlockType.WallTop && !isBottom)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        void Update()
        {
           
        }

       
        public void setOccupied()
        {
            GameObject[] collisions = GetCollisions();
            foreach(GameObject collision in collisions)
            {
                //
                if (collision is Block) {
                    Block block = (Block)collision;
                    block.setOccupied(blockColor); 
                }

            }
            
        }
    }
}
