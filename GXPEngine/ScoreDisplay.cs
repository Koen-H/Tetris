﻿using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class ScoreDisplay : GameObject
    {
        int score;
        float tetrisStreak;
        EasyDraw canvas;

        public ScoreDisplay()
        {
            canvas = new EasyDraw(100, 75);
            canvas.Clear(0);
            canvas.Fill(0, 212, 240);
            CenterMode mode = CenterMode.Center;
            string text = "0";
            canvas.TextAlign(mode, mode);
            canvas.ShapeAlign(mode, mode);
            canvas.Text(text);
            canvas.SetScaleXY(2, 2);
            canvas.SetXY(-(canvas.width / 2), -(canvas.height / 2));
            this.AddChild(canvas);
        }

        public void ApplyPoints(int _score)// and update score
        {
            score += _score;
            canvas.Clear(0);
            canvas.Text(score.ToString());
            Console.WriteLine("New score: " + score);
        }

        public void TetrisPoints(int _linesOfTetris)
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
