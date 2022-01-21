using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class SceneObject : AnimationSprite  //the scene object is just a object in the scene without any purpose besides looking pretty
    {
        public SceneObject(String blockColor, int coloms, int rows) : base(blockColor, coloms, rows,-1,false)
        {

        }
    }
}
