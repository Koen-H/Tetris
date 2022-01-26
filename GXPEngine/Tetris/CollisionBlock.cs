using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{

    public class CollisionBlock : Block //this is one block, a BlockCluster is the shape.
    {

        public CollisionBlock(float xParentPos, float yParentPos, String blockColor, BlockType blockType) : base(xParentPos, yParentPos, blockColor, blockType)
        {
           

        }


        public Boolean IsColliding()//Used to check if the clusterblock/shape can Move a direction.
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

       
        public void SetOccupied()
        {
            GameObject[] collisions = GetCollisions();
            foreach(GameObject collision in collisions)
            {
                //
                if (collision is Block) {
                    Block block = (Block)collision;
                    block.SetOccupied(blockColor); 
                }

            }
            
        }
    }
}
