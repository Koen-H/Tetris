using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    
    public class GameOverScreen : GameObject
    {
        private MyGame myGame;
        readonly private int finalScore;
        private EasyDraw restartButton, quitButton, mainMenuButton;
        public GameOverScreen(int _finalScore, Boolean isNewHighscore)
        {
            finalScore = _finalScore;
            myGame = (MyGame)game;
            CenterMode centerMode = CenterMode.Center;
            EasyDraw backgroundCanvas = new EasyDraw(700,400);
            this.SetXY(game.width/2, game.height/2);
            backgroundCanvas.Clear(100,100,100, 200);//background color
            backgroundCanvas.TextAlign(centerMode, centerMode);
            backgroundCanvas.ShapeAlign(centerMode, centerMode);
            backgroundCanvas.TextSize(30);
            if (isNewHighscore)
            {
                backgroundCanvas.Text("Your score is: " + finalScore + "\nYou've set a new Highscore!\nCan you improve?");
            }
            else
            {
                backgroundCanvas.Text("Your score is: " + finalScore + "\nYou didn't set a new highscore.\nTry again?");
            }
            
            backgroundCanvas.SetXY(-(backgroundCanvas.width / 2), -(backgroundCanvas.height / 2));
            backgroundCanvas.SetScaleXY(1.3f,1.3f);

            EasyDraw topCanvas = new EasyDraw(700,100);
            topCanvas.Clear(0,255,255, 200);
            topCanvas.Fill(255,0,0);
            topCanvas.TextAlign(centerMode, centerMode);
            topCanvas.ShapeAlign(centerMode, centerMode);
            topCanvas.TextSize(80);
            topCanvas.Text("Game Over");
            backgroundCanvas.AddChild(topCanvas);

            restartButton = new EasyDraw(200,90);
            restartButton.Clear(0, 255, 25, 220);
            restartButton.Fill(255, 255, 0);
            restartButton.TextAlign(centerMode, centerMode);
            restartButton.ShapeAlign(centerMode, centerMode);
            restartButton.TextSize(25);
            restartButton.Text("Try again");
            restartButton.SetXY(20,275);
            backgroundCanvas.AddChild(restartButton);

            mainMenuButton = new EasyDraw(200, 90);
            mainMenuButton.Clear(0, 255, 25);
            mainMenuButton.Fill(255, 0, 0, 220);
            mainMenuButton.TextAlign(centerMode, centerMode);
            mainMenuButton.ShapeAlign(centerMode, centerMode);
            mainMenuButton.TextSize(25);
            mainMenuButton.Text("Main Menu");
            mainMenuButton.SetXY(250, 275);
            backgroundCanvas.AddChild(mainMenuButton);

            quitButton = new EasyDraw(200, 90);
            quitButton.Clear(0, 255, 25);
            quitButton.Fill(255, 0, 0, 220);
            quitButton.TextAlign(centerMode, centerMode);
            quitButton.ShapeAlign(centerMode, centerMode);
            quitButton.TextSize(25);
            quitButton.Text("Quit game");
            quitButton.SetXY(480, 275);
            backgroundCanvas.AddChild(quitButton);

            AddChild(backgroundCanvas);


        }
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                string loadLevel = myGame.currentLevel;
                if (restartButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("clickedButton, now loading level:" + loadLevel);
                    myGame.LoadLevel(loadLevel);
                }
                else if (quitButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("Quitting the game...");
                    game.Destroy();
                }
                else if (mainMenuButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("Going to main menu...");
                    myGame.LoadMainMenu();
                }
            }
        }
        
    }
}
