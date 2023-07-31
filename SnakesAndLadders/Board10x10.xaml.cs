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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakesAndLadders
{
    /// <summary>
    /// Interaction logic for Board10x10.xaml
    /// </summary>
    public partial class Board10x10 : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        ///  Active Player
        /// </summary>
        public int ActivePlayer
        {
            get => activeplayer;
            set
            {
                if (value >= PlayerImgs.Count)
                {
                    value -= PlayerImgs.Count;
                }
                activeplayer = value;
                PropertyChanged?.Invoke(this, new(nameof(ActivePlayer)));
            }
        }
        private int activeplayer = 0;


        /// <summary>
        ///  Boards
        /// </summary>
        public ObservableCollection<BitmapImage> Boards { get; } = new()
        {
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Boards\10x10.png", UriKind.Relative)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Boards\12x12.png", UriKind.Relative)),
            new(new Uri(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\Boards\15x15.png", UriKind.Relative)),
        };


        /// <summary>
        ///  Players
        /// </summary>
        public ObservableCollection<Player> Players { get; } = new();


        /// <summary>
        ///  Player profile images
        /// </summary>
        public ObservableCollection<Image> PlayerImgs { get; } = new();


        /// <summary>
        ///  Moves per player
        /// </summary>
        public List<int> PlayerMoves = new();


        public Board10x10(ObservableCollection<Player> Players)
        {
            InitializeComponent();
            this.Players = Players;
            grid.Background = new ImageBrush(Boards[0]);

            foreach (var item in Players)
            {
                Image image = new()
                {
                    Source = item.Profile.Source,
                    Height = 50,
                    Width = 50,
                };
                PlayerImgs.Add(image);

                PlayerMoves.Add(new());
            }

            foreach (var item in PlayerImgs)
            {
                grid.Children.Add(item);
                Grid.SetColumn(item, 0);
                Grid.SetRow(item, 9);
            }
        }


        public void Move(int inc)
        {
            var currentpos = 0;
            var row = Grid.GetRow(PlayerImgs[ActivePlayer]);
            var column = Grid.GetColumn(PlayerImgs[ActivePlayer]);

            currentpos += 90 - (row * 10);

            if (row % 2 == 0)
            {
                currentpos += 10 - column;
            }
            else
            {
                currentpos += column + 1;
            }

            currentpos += inc + 1;

            currentpos = currentpos switch
            {
                8 => 33,
                22 => 41,
                30 => 90,
                35 => 6,
                47 => 54, 
                59 => 77,
                65 => 2,
                73 => 30,
                85 => 80,
                94 => 66,
                _ => currentpos,
            };
            
            if (currentpos >= 100)
            {
                Win(ActivePlayer);
                Grid.SetRow(PlayerImgs[ActivePlayer], 0);
                Grid.SetColumn(PlayerImgs[ActivePlayer], 0);
                return;
            }

            var newRow = 9;
            var newColumn = 0;

            while (currentpos > 10)
            {
                currentpos -= 10;
                newRow -= 1;
            }

            if (newRow % 2 == 0)
            {
                newColumn = 10 - currentpos;
            }
            else
            {
                newColumn = currentpos - 1;
            }

            Grid.SetRow(PlayerImgs[ActivePlayer], newRow);
            Grid.SetColumn(PlayerImgs[ActivePlayer], newColumn);

            PlayerMoves[ActivePlayer]++;

            ActivePlayer++;
        }

        public Result result;
        public bool Won;

        private void Win(int winner)
        {
            Won = true;
            MediaPlayer mp = new();
            mp.Open(Players[winner].VictorySong);
            mp.Play();

            result = new(Players[winner].Name, 10000 / PlayerMoves[winner]);
        }
    }
}
