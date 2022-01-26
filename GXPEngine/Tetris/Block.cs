using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Tetris
{
    public enum BlockType
    {
       Block, CollisionBlock, WallLeft, WallRight, WallBottom, WallTop, GrayBlock, Grid, GridFilled, Nothing
    }

    public class Block : Sprite //this is one block, a BlockCluster is the shape.
    {
        private MyGame myGame;
       // bool isCollisionCheckBlock;
        public String blockColor;
        public BlockType blockType;
        readonly float xParentPos, yParentPos, blockSize;
        
        

        public Block(float xParentPos, float yParentPos, String blockColor, BlockType blockType):base(blockColor)
        {
            myGame = (MyGame)game;
            blockSize = myGame.gameManager.blockSize;
            this.xParentPos = xParentPos;
            this.yParentPos = yParentPos;
            this.blockColor = blockColor;
            this.blockType = blockType;
            SetXY(blockSize * xParentPos, blockSize * yParentPos);
            if (blockType == BlockType.CollisionBlock)
            {
                alpha = 0.0f;
            }
            
        }


        public void SetSprite(String _blockColor)
        {
            blockColor = _blockColor;
            this.initializeFromTexture(Texture2D.GetInstance(blockColor, false));
        }

        void Update()
        {

        }


        public void SetOccupied(String blockColor)
        {
            blockType = BlockType.GridFilled;
            SetSprite(blockColor);
        }

    }
}
