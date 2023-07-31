using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using Box = System.Windows.MessageBox;
using System.Windows.Threading;

namespace SnakesAndLadders
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        ///  Black brushes with percentage opacity
        /// </summary>
        public SolidColorBrush BlackTransparent
        {
            get => blacktransparent;
            set
            {
                blacktransparent = value;
                PropertyChanged?.Invoke(this, new(nameof(BlackTransparent)));
            }
        }
        private SolidColorBrush blacktransparent = new(Colors.Black)
        {
            Opacity = 0.7,
        };

        public SolidColorBrush BlackTransparent2
        {
            get => blacktransparent2;
            set
            {
                blacktransparent2 = value;
                PropertyChanged?.Invoke(this, new(nameof(BlackTransparent2)));
            }
        }
        private SolidColorBrush blacktransparent2 = new(Colors.Black)
        {
            Opacity = 0.9,
        };


        /// <summary>
        ///  Player collection
        /// </summary>
        public ObservableCollection<Player> Players { get; } = new();


        /// <summary>
        ///  Die images
        /// </summary>
        public ObservableCollection<BitmapImage> Dice { get; } = new()
        {
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Dice\1.png", UriKind.RelativeOrAbsolute)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Dice\2.png", UriKind.RelativeOrAbsolute)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Dice\3.png", UriKind.RelativeOrAbsolute)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Dice\4.png", UriKind.RelativeOrAbsolute)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Dice\5.png", UriKind.RelativeOrAbsolute)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Dice\6.png", UriKind.RelativeOrAbsolute)),
        };


        /// <summary>
        ///  Die roll audio
        /// </summary>
        public Uri DieRollSFX
        {
            get => dierollsfx;
            set
            {
                dierollsfx = value;
                PropertyChanged?.Invoke(this, new(nameof(DieRollSFX)));
            }
        }
        private Uri dierollsfx = new(@"Dice\ES_Game Dice Roll 1 - SFX Producer.mp3", UriKind.Relative);
        public MediaPlayer RollingSFX = new();


        /// <summary>
        ///  Size of the board, passed on from the MainWindow
        /// </summary>
        public readonly string BoardSize;


        /// <summary>
        ///  Board
        /// </summary>
        public dynamic Board { get; set; }


        /// <summary>
        ///  Audio settings
        /// </summary>
        public double SFXVolume;
        public double MusicVolume;


        /// <summary>
        ///  Constructor
        /// </summary>
        public GameWindow(object sender, ObservableCollection<Player> Players, string BoardSize)
        {
            InitializeComponent();
            if (sender is MainWindow mainwindow)
            {
                mainwindow.ContinueBGM = false;
                mainwindow.Hide();
                mainwindow.BGM.Stop();
                WindowState = mainwindow.WindowState;
                SFXVolume = mainwindow.SFXVolume;
                MusicVolume = mainwindow.MusicVolume;
                DataContext = this;

                this.Players = Players;
                this.BoardSize = BoardSize;

                BitmapImage bi = new();
                bi.BeginInit();
                bi.UriSource = new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Asset 1@4x.png", UriKind.Absolute);
                bi.EndInit();
                BackgroundImg.Source = bi;


                Board = BoardSize switch
                {
                    "10x10" => new Board10x10(Players),
                    "12x12" => new Board12x12(Players),
                    "15x15" => new Board15x15(Players),
                    _ => throw new ArgumentException(BoardSize),
                };

                Type = BoardSize;

                BoardHolder.Child = Board;

                if (Players[0].IsBot)
                {
                    RollBtn_OnClick();
                }

                Closed += (_, _) =>
                {
                    mainwindow = new();
                    if (Board.Won && !mainwindow.IsHighscoreFrozen)
                    {
                        switch (Type)
                        {
                            case "10x10":
                                mainwindow.Leaderboardx10.Add(result);
                                break;
                            case "12x12":
                                mainwindow.Leaderboardx12.Add(result);
                                break;
                            case "15x15":
                                mainwindow.Leaderboardx15.Add(result);
                                break;
                        }
                    }
                    if (Board.Won)
                    {
                        RollBtn.IsEnabled = false;
                    }
                    mainwindow.Show();
                };
            }
        }


        public Result result => Board.result;
        public string Type;


        /// <summary>
        ///  Disable button and generate a random roll
        /// </summary>
        private async void RollBtn_OnClick(object sender = null, RoutedEventArgs e = null)
        {
            RollBtn.IsEnabled = false;

            RollingSFX.Open(DieRollSFX);
            RollingSFX.Volume = SFXVolume;
            RollingSFX.Play();
            Random r = new();

            var time = 80;
            for (int i = 0; i < r.Next(8, 16); i++)
            {
                DieImage.Source = Dice[r.Next(0, 6)];
                await Task.Delay(time);
                if (time < 1000)
                {
                    time += 20;
                }
            }
            var result = r.Next(0, 6);
            DieImage.Source = Dice[result];

            Board.Move(result);

            if (Players[Board.ActivePlayer].IsBot && !Board.Won)
            {
                RollBtn_OnClick();
                return;
            }

            RollBtn.IsEnabled = true;
        }
    }
}
