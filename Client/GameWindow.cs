﻿using Pacman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client
{
    class GameWindow : Tiwi.Window
    {
        private Field field;
        private int score;
        private UpCommand upCommand = new UpCommand();
        private DownCommand downCommand = new DownCommand();
        private LeftCommand leftCommand = new LeftCommand();
        private RightCommand rightCommand = new RightCommand();

        public GameWindow()
        {
            Init();
            Title = "Pacman";
            DrawGame();
            TickInterval = new TimeSpan(0, 0, 0, 0, 10);
            StartTimer();
        }

        public void Init()
        {
            this.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ADirector director = new DirectorFromFile(@"../../../Assets/config.txt");
            field = director.Construct(new Builder());
            Width = field.GameObjects.GetLength(1) * 20;
            Height = field.GameObjects.GetLength(0) * 20;
        }

        public void DrawGame()
        {
            for (int i = 0; i < field.GameObjects.GetLength(0); i++)
            {
                for (int j = 0; j < field.GameObjects.GetLength(1); j++)
                {
                    AGameObject gameObject;
                    if ((gameObject = field.GameObjects[i, j]) != null)
                    {
                        field.GameObjects[i, j].Draw(this, new Vector2D(j * 20, i * 20));
                    }
                }
            }
        }

        protected override void TimerTick()
        {
            base.TimerTick();
            ClearWindow();
            field.Pacman.Loop();
            foreach (Enemy enemy in field.Enemies)
            {
                enemy.Loop();
            }
            DrawGame();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case (Key.Left):
                    leftCommand.Execute(field.Pacman);
                    break;
                case (Key.Right):
                    rightCommand.Execute(field.Pacman);
                    break;
                case (Key.Up):
                    upCommand.Execute(field.Pacman);
                    break;
                case (Key.Down):
                    downCommand.Execute(field.Pacman);
                    break;
            }
        }
    }
}
