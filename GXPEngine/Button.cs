using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace GXPEngine
{
    public class Button : AnimationSprite
    {
        string loadLevel;//Which level or scene should it load on click?

        
        public Button(String blockColor, int coloms, int rows, string load) : base(blockColor, coloms, rows)
        {
            this.loadLevel = load;
            // Set the position of the button to the bottom middle of the screen:

            this.SetXY(coloms, rows);
            Console.WriteLine("A button has been created!");
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    //MyGame.LoadLevel();
                    //MyGame.playField
                    


                    //MyGame.LoadLevel(loadLevel);  
                    Console.WriteLine("clickedButton");
                    Scene levelScene = new Scene("scene_level.tmx");
                    AddChild(levelScene);//should be done with loadlevel!
                    
                    //
                    Level level = new Level(loadLevel);
                    AddChild(level);//should be done with loadlevel!
                }
            }
        }

    }
}
