using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class SceneObject : AnimationSprite  
    {
        public SceneObject(String blockColor, int coloms, int rows) : base(blockColor, coloms, rows,-1,false)
        {

        }
    }
}
