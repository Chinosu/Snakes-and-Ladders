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
    /// Interaction logic for Board15x15.xaml
    /// </summary>
    public partial class Board15x15 : UserControl, INotifyPropertyChanged
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


        public Board15x15(ObservableCollection<Player> Players)
        {
            InitializeComponent();
            this.Players = Players;
            grid.Background = new ImageBrush(Boards[2]);

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
                Grid.SetRow(item, 14);
            }
        }


        public void Move(int inc)
        {
            var currentpos = 0;
            var row = Grid.GetRow(PlayerImgs[ActivePlayer]);
            var column = Grid.GetColumn(PlayerImgs[ActivePlayer]);

            currentpos += 210 - (row * 15);

            if (row % 2 != 0)
            {
                currentpos += 15 - column;
            }
            else
            {
                currentpos += column + 1;
            }

            currentpos += inc + 1;

            currentpos = currentpos switch
            {
                3 => 93,
                16 => 46,
                39 => 26,
                59 => 2,
                72 => 17,
                82 => 18, 
                61 => 209,
                95 => 200, 
                104 => 170,
                112 => 142,
                134 => 165,
                147 => 215,
                194 => 70,
                203 => 158,
                213 => 225,
                _ => currentpos,
            };

            if (currentpos >= 225)
            {
                Win(ActivePlayer);
                Grid.SetRow(PlayerImgs[ActivePlayer], 0);
                Grid.SetColumn(PlayerImgs[ActivePlayer], 14);
                return;
            }

            var newRow = 14;
            var newColumn = 0;

            while (currentpos > 15)
            {
                currentpos -= 15;
                newRow -= 1;
            }

            if (newRow % 2 != 0)
            {
                newColumn = 15 - currentpos;
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