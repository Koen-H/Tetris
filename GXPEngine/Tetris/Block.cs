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
        public String blockSprite;
        public BlockType blockType;
        float xParentPos, yParentPos, blockSize;
        
        

        public Block(float xParentPos, float yParentPos, String blockColor, BlockType blockType):base(blockColor)
        {
            myGame = (MyGame)game;
            this.blockSize = myGame.gameManager.blockSize;
            this.xParentPos = xParentPos;
            this.yParentPos = yParentPos;
            this.blockSprite = blockColor;
            this.blockType = blockType;
            this.SetXY(blockSize * xParentPos, blockSize * yParentPos);
            
            //AddChild(blockSprite);
            //setSprite("debug_block.png");
        }


        public void SetSprite(String blockColor)
        {
            blockSprite = blockColor;
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
