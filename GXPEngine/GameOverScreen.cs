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
        int finalScore;
        EasyDraw restartButton, saveButton, quitButton, mainMenuButton;
        Boolean saved;
        public GameOverScreen()
        {
            myGame = (MyGame)game;
            finalScore = myGame.gameManager.scoreDisplay.score;
            
            EasyDraw backgroundCanvas = new EasyDraw(700,400);
            this.SetXY(game.width/2, game.height/2);
            backgroundCanvas.Clear(100,100,100, 200);//background color

            backgroundCanvas.SetXY(-(backgroundCanvas.width / 2), -(backgroundCanvas.height / 2));
            backgroundCanvas.SetScaleXY(1.3f,1.3f);

            EasyDraw topCanvas = new EasyDraw(700,100);
            topCanvas.Clear(0,255,255, 200);
            topCanvas.Fill(255,0,0);
            topCanvas.TextAlign(CenterMode.Center, CenterMode.Center);
            topCanvas.ShapeAlign(CenterMode.Center,CenterMode.Center);
            topCanvas.TextSize(80);
            topCanvas.Text("Game Over");
            backgroundCanvas.AddChild(topCanvas);

            restartButton = new EasyDraw(200,90);
            restartButton.Clear(0, 255, 25, 220);
            restartButton.Fill(255, 255, 0);
            restartButton.TextAlign(CenterMode.Center, CenterMode.Center);
            restartButton.ShapeAlign(CenterMode.Center, CenterMode.Center);
            restartButton.TextSize(25);
            restartButton.Text("Try again");
            restartButton.SetXY(20,275);
            backgroundCanvas.AddChild(restartButton);


            saveButton = new EasyDraw(200, 90);
            saveButton.Clear(0, 255, 25);
            saveButton.Fill(255, 255, 0, 220);
            saveButton.TextAlign(CenterMode.Center, CenterMode.Center);
            saveButton.ShapeAlign(CenterMode.Center, CenterMode.Center);
            saveButton.TextSize(25);
            saveButton.Text("Save score?");
            saveButton.SetXY(250, 275);
            backgroundCanvas.AddChild(saveButton);

            quitButton = new EasyDraw(200, 90);
            quitButton.Clear(0, 255, 25);
            quitButton.Fill(255, 0, 0, 220);
            quitButton.TextAlign(CenterMode.Center, CenterMode.Center);
            quitButton.ShapeAlign(CenterMode.Center, CenterMode.Center);
            quitButton.TextSize(25);
            quitButton.Text("Quit game");
            quitButton.SetXY(480, 275);
            backgroundCanvas.AddChild(quitButton);

            mainMenuButton = new EasyDraw(200, 90);
            mainMenuButton.Clear(0, 255, 25);
            mainMenuButton.Fill(255, 0, 0, 220);
            mainMenuButton.TextAlign(CenterMode.Center, CenterMode.Center);
            mainMenuButton.ShapeAlign(CenterMode.Center, CenterMode.Center);
            mainMenuButton.TextSize(25);
            mainMenuButton.Text("Main Menu");
            mainMenuButton.SetXY(480, 375);
            backgroundCanvas.AddChild(mainMenuButton);

            AddChild(backgroundCanvas);
            /*
            CenterMode mode = CenterMode.Center;
            string text = "0";
            canvas.TextAlign(mode, mode);
            canvas.ShapeAlign(mode, mode);
            canvas.Text(text);
            canvas.SetScaleXY(2, 2);
            this.AddChild(canvas);*/

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
                else if (saveButton.HitTestPoint(Input.mouseX, Input.mouseY) && !saved)
                {
                    Console.WriteLine("clickedButton, now saving score..");
                    SaveFinalScore(loadLevel + ".txt");
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
        bool SaveFinalScore(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine("score=" + finalScore);
                    //name;
                    writer.Close();
                    saveButton.Clear(0, 255, 25);
                    saveButton.Text("Saved");
                    saved = true;
                    Console.WriteLine("Score saved to file! " + filename);
                    return true;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while trying to save: " + error.Message);
                return false;
            }
        }
    }
}
