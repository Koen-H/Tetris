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
            loadLevel = load;
            // Set the position of the button to the bottom middle of the screen:

            SetXY(coloms, rows);
            Console.WriteLine("A button has been created!");
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("clickedButton, now loading level:" + loadLevel);
                    MyGame myGame = (MyGame)game;
                    if (loadLevel != "main_menu.tmx")
                    {

                        myGame.LoadLevel(loadLevel);
                    }
                    else
                    {
                        myGame.LoadMainMenu();
                    }
                }
            }
        }

    }
}
