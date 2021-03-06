using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class ScoreDisplay : GameObject
    {
        public int score;
        EasyDraw canvas;

        public ScoreDisplay(string topText)
        {
            canvas = new EasyDraw(88, 75);
            canvas.Clear(0);
            canvas.Fill(0, 212, 240);
            CenterMode mode = CenterMode.Center;
            string text = "0";
            canvas.TextAlign(mode, mode);
            canvas.ShapeAlign(mode, mode);
            canvas.Text(text);
            canvas.SetScaleXY(2, 2);
            canvas.SetXY(-(canvas.width / 2)-12, -(canvas.height / 2));

            EasyDraw topCanvas = new EasyDraw(88, 25);
            topCanvas.Clear(0, 255, 255);
            topCanvas.Fill(255, 0, 0);
            topCanvas.TextAlign(CenterMode.Center, CenterMode.Center);
            topCanvas.ShapeAlign(CenterMode.Center, CenterMode.Center);
            topCanvas.TextSize(10);
            topCanvas.Text(topText);
            topCanvas.SetScaleXY(2, 2);
            topCanvas.SetXY(-(topCanvas.width / 2) -12, -(topCanvas.height) * 2);
           
            this.AddChild(canvas);
            AddChild(topCanvas);
        }

        public void ApplyPoints(int points)// and update score
        {
            score += points;
            SetScore(score);
            //Console.WriteLine("New score: " + score);
        }
        public void SetScore(int scoreToSet)//Set a score to display
        {
            canvas.Clear(0);
            canvas.Text(scoreToSet.ToString());
        }
        public void TetrisPoints(int _linesOfTetris)//In a future build, this method will also keep track of tetris streaks for even more points.
        {
            switch (_linesOfTetris)
            {
                case 1:
                    ApplyPoints(100);
                    break;
                case 2:
                    ApplyPoints(250);
                    break;
                case 3:
                    ApplyPoints(500);
                    break;
                case 4:
                    ApplyPoints(1000);
                    break;
                default:
                    Console.WriteLine("default happend in tetris points, this shouldn't happen?");
                    break;
            }
        }
    }
}
