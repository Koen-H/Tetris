using System;
using GXPEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class HighscoresDisplay : GameObject
    {
        int savedScore;
        ScoreDisplay scoreDisplay;
        //TODO: make this nice and fancy display of highscores, for now, just the saved score with a easydraw.

        public HighscoresDisplay(string currentLevel)
        {
            EasyDraw topCanvas = new EasyDraw(100, 25);
            topCanvas.Clear(0, 255, 255);
            topCanvas.Fill(255, 0, 0);
            topCanvas.TextAlign(CenterMode.Center, CenterMode.Center);
            topCanvas.ShapeAlign(CenterMode.Center, CenterMode.Center);
            topCanvas.TextSize(10);
            topCanvas.Text("Saved Score");//TODO:Replace with High scores once implemented.
            topCanvas.SetScaleXY(2, 2);
            topCanvas.SetXY(-(topCanvas.width / 2), -(topCanvas.height)*2);
            scoreDisplay = new ScoreDisplay();
            LoadData(currentLevel + ".txt");
            scoreDisplay.SetScore(savedScore);
            AddChild(scoreDisplay);
            AddChild(topCanvas);//Note, topcanvas AFTER scoreDisplay so it is on top of it.
            Console.WriteLine(savedScore);
        }

        void LoadData(string filename)//get the data from the document.
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("No save file found!");
                return;
            }
            try
            {
                // StreamReader: For reading a text file - requires System.IO namespace:
                // Note: the "using" block ensures that resources are released (reader.Dispose is called) when an exception occurs
                using (StreamReader reader = new StreamReader(filename))
                {

                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        // Here's a demo of different string parsing methods:

                        // Find the position of the first '=' symbol (-1 if doesn't exist)
                        int splitPos = line.IndexOf('=');
                        if (splitPos >= 0)
                        {
                            // Everything before the '=' symbol:
                            string key = line.Substring(0, splitPos);
                            // Everything after the '=' symbol:
                            string value = line.Substring(splitPos + 1);

                            // Split value up for every comma:
                            string[] numbers = value.Split(',');

                            switch (key)
                            {
                                case "position":
                                   /* if (numbers.Length == 2)
                                    {
                                        // These may trigger an exception if the string doesn't represent a float value:
                                       // car.x = float.Parse(numbers[0]);
                                        //car.y = float.Parse(numbers[1]);
                                        Console.WriteLine("Car has position {0},{1}", car.x, car.y);
                                    }*/
                                    break;
                                case "score":
                                    if (numbers.Length == 1)
                                    {
                                        // This may trigger an exception if the string doesn't represent a float value:
                                        savedScore = int.Parse(numbers[0]);
                                        //Console.WriteLine("Car has rotation {0}", car.rotation);
                                        Console.WriteLine("This happens!" + savedScore);
                                    }
                                    break;
                            }
                        }
                        line = reader.ReadLine();
                    }
                    reader.Close();

                    Console.WriteLine("Load from {0} successful ", filename);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while reading save file: {0}", error.Message);
            }
        }
    }
}
