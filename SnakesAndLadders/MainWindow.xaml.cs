using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
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
using Box = System.Windows.MessageBox;

namespace SnakesAndLadders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        ///  Generate a randomly ordered string
        /// </summary>
        private string RandomString(int length)
        {
            Random r = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[r.Next(s.Length)]).ToArray());
        }


        /// <summary>
        ///  Main brush used for UI elements
        /// </summary>
        public SolidColorBrush Brown
        {
            get => brown;
            set
            {
                brown = value;
                PropertyChanged?.Invoke(this, new(nameof(Brown)));
            }
        }
        private SolidColorBrush brown = new(Color.FromRgb(174, 141, 82));


        /// <summary>
        ///  Brush with the same colour as the background
        /// </summary>
        public SolidColorBrush BackgroundBrush
        {
            get => backgroundbrush;
            set
            {
                backgroundbrush = value;
                PropertyChanged?.Invoke(this, new(nameof(BackgroundBrush)));
            }
        }
        private SolidColorBrush backgroundbrush = new(Color.FromRgb(22, 65, 82));


        /// <summary>
        ///  White brush with low opacity
        /// </summary>
        public SolidColorBrush Lighten
        {
            get => lighten;
            set
            {
                lighten = value;
                PropertyChanged?.Invoke(this, new(nameof(Lighten)));
            }
        }
        private SolidColorBrush lighten = new(Colors.White);

        public SolidColorBrush MoreLighten
        {
            get => morelighten;
            set
            {
                morelighten = value;
                PropertyChanged?.Invoke(this, new(nameof(MoreLighten)));
            }
        }
        private SolidColorBrush morelighten = new(Colors.White);


        /// <summary>
        ///  Bound to the visible page
        /// </summary>
        public int CurrentPage
        {
            get => currentpage;
            set
            {
                currentpage = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentPage)));

                foreach (var item in StackpanelForPages.Children)
                {
                    if (item is Button button
                        && button.Content is StackPanel stackpanel
                        && stackpanel.Children[1] is Rectangle rectangle)
                    {
                        rectangle.Visibility = Visibility.Collapsed;
                    }
                }
                if (StackpanelForPages.Children[CurrentPage + 1] is Button btn
                    && btn.Content is StackPanel stackpnl
                    && stackpnl.Children[1] is Rectangle rect)
                {
                    rect.Visibility = Visibility.Visible;
                }
                if (CurrentPage is 7)
                {
                    Start.Visibility = Visibility.Collapsed;
                }
                else if (CurrentPage is 4 or 5 or 6)
                {
                    Start.Visibility = Visibility.Visible;
                }
            }
        }
        private int currentpage;

        public int CurrentPagex10
        {
            get => currentpagex10;
            set
            {
                currentpagex10 = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentPagex10)));

                foreach (var item in ItemsControlForx10.Items)
                {
                    if (item is Player player)
                    {
                        player.Visibility = Visibility.Collapsed;
                    }
                }
                if (ItemsControlForx10.Items[CurrentPagex10] is Player player_)
                {
                    player_.Visibility = Visibility.Visible;
                }
            }
        }
        private int currentpagex10;

        public int CurrentPagex12
        {
            get => currentpagex12;
            set
            {
                currentpagex12 = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentPagex12)));

                foreach (var item in ItemsControlForx12.Items)
                {
                    if (item is Player player)
                    {
                        player.Visibility = Visibility.Collapsed;
                    }
                }
                if (ItemsControlForx12.Items[CurrentPagex12] is Player player_)
                {
                    player_.Visibility = Visibility.Visible;
                }
            }
        }
        private int currentpagex12;

        public int CurrentPagex15
        {
            get => currentpagex15;
            set
            {
                currentpagex15 = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentPagex15)));

                foreach (var item in ItemsControlForx15.Items)
                {
                    if (item is Player player)
                    {
                        player.Visibility = Visibility.Collapsed;
                    }
                }
                if (ItemsControlForx15.Items[CurrentPagex15] is Player player_)
                {
                    player_.Visibility = Visibility.Visible;
                }
            }
        }
        private int currentpagex15;


        /// <summary>
        ///  Containers for the top players of each board size
        /// </summary>
        public ObservableCollection<Result> Leaderboardx10 => LbSort(leaderboardx10);
        private ObservableCollection<Result> leaderboardx10 = new();

        public ObservableCollection<Result> Leaderboardx12 => LbSort(leaderboardx12);
        private ObservableCollection<Result> leaderboardx12 = new();

        public ObservableCollection<Result> Leaderboardx15 => LbSort(leaderboardx15);
        private ObservableCollection<Result> leaderboardx15 = new();


        /// <summary>
        ///  Containers for each board's players when starting game
        /// </summary>
        public ObservableCollection<Player> Playersx10 { get; } = new();

        public ObservableCollection<Player> Playersx12 { get; } = new();

        public ObservableCollection<Player> Playersx15 { get; } = new();


        /// <summary>
        ///  Data-bound fields for the Options page
        /// </summary>
        public bool IsHighscoreFrozen
        {
            get => ishighscorefrozen;
            set
            {
                ishighscorefrozen = value;
                PropertyChanged?.Invoke(this, new(nameof(IsHighscoreFrozen)));
            }
        }
        private bool ishighscorefrozen;

        public bool IsMaximised
        {
            get => ismaximised;
            set
            {
                ismaximised = value;
                if (IsMaximised)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
                PropertyChanged?.Invoke(this, new(nameof(IsMaximised)));
            }
        }
        private bool ismaximised;

        public double SFXVolume
        {
            get => sfxvolume;
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                sfxvolume = value;
                PropertyChanged?.Invoke(this, new(nameof(SFXVolume)));
            }
        }
        private double sfxvolume = 0.5;

        public double MusicVolume
        {
            get => musicvolume;
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                if (ContinueBGM)
                {
                    Random r = new();
                    BGM.Open(BackgroundSongs[r.Next(0, BackgroundSongs.Count)]);
                    BGM.Volume = 0.4 * MusicVolume;
                    BGM.Play();
                }
                musicvolume = value;
                PropertyChanged?.Invoke(this, new(nameof(MusicVolume)));
            }
        }
        private double musicvolume = 0.5;

        public List<string> Pages { get; set; } = new()
        {
            new("Home"),
            new("Leaderboard"),
            new("Options"),
        };

        public string SelectedPage
        {
            get => selectedpage;
            set
            {
                selectedpage = value;
                PropertyChanged?.Invoke(this, new(nameof(SelectedPage)));
            }
        }
        private string selectedpage;


        /// <summary>
        ///  Container for possible victory songs
        /// </summary>
        public ObservableCollection<Uri> VictorySongs { get; } = new()
        {
            new(@"VicMus\Celebration.wav", UriKind.Relative),
            new(@"VicMus\Fanfare Trumpets.mp3", UriKind.Relative),
            new(@"VicMus\Fanfare.mp3", UriKind.Relative),
            new(@"VicMus\GoodResult.wav", UriKind.Relative),
            new(@"VicMus\Level Up.wav", UriKind.Relative),
            new(@"VicMus\Rezyma Victory.mp3", UriKind.Relative),
            new(@"VicMus\Tuudurt.wav", UriKind.Relative),
            new(@"VicMus\Victory Cry.wav", UriKind.Relative),
            new(@"VicMus\Victory Fanfare.wav", UriKind.Relative),
        };


        /// <summary>
        ///  Proflie pictures
        /// </summary>
        public ObservableCollection<Image> ProfilePictures { get; set; } = new();


        /// <summary>
        ///  Audio tracks
        /// </summary>
        public ObservableCollection<Uri> BackgroundSongs { get; set; } = new()
        {
            new(@"BGMus\000.mp3", UriKind.Relative),
            new(@"BGMus\001.mp3", UriKind.Relative),
            new(@"BGMus\002.mp3", UriKind.Relative),
            new(@"BGMus\003.mp3", UriKind.Relative),
            new(@"BGMus\004.mp3", UriKind.Relative),
            new(@"BGMus\005.mp3", UriKind.Relative),
            new(@"BGMus\006.mp3", UriKind.Relative),
            new(@"BGMus\007.mp3", UriKind.Relative),
            new(@"BGMus\008.mp3", UriKind.Relative),
            new(@"BGMus\009.mp3", UriKind.Relative),
            new(@"BGMus\010.mp3", UriKind.Relative),
        };


        /// <summary>
        ///  Sound effects
        /// </summary>
        public ObservableCollection<Uri> SFX { get; set; } = new()
        {
            new(@"SFX\ES_Beep Tone 7 - SFX Producer.mp3", UriKind.Relative),
            new(@"SFX\ES_Horror Hit Metal - SFX Producer.mp3", UriKind.Relative)
        };


        /// <summary>
        ///  Audio players
        /// </summary>
        public MediaPlayer BGM = new();
        public MediaPlayer ButtonClick = new();
        public MediaPlayer PlayButtonClick = new();


        /// <summary>
        ///  BGM loop controller
        /// </summary>
        public bool ContinueBGM = true;


        /// <summary>
        ///  Constructor
        /// </summary>
        public MainWindow()
        {
            var OptionsFile = File.ReadAllLines(@"txt\Options.txt");
            if (OptionsFile.Length == 7)
            {
                IsHighscoreFrozen = Convert.ToBoolean(OptionsFile[0]);
                IsMaximised = Convert.ToBoolean(OptionsFile[2]);
                SFXVolume = Convert.ToDouble(OptionsFile[3]);
                MusicVolume = Convert.ToDouble(OptionsFile[4]);
                SelectedPage = OptionsFile[5];
            }
            else
            {
                IsHighscoreFrozen = false;
                IsManipulationEnabled = false;
                SFXVolume = 0.5;
                MusicVolume = 0.5;
                SelectedPage = "Home";
            }

            var Leaderboardx10File = File.ReadAllLines(@"txt\Highscorex10.txt");
            for (int i = 0; i < Leaderboardx10File.Length; i += 2)
            {
                Leaderboardx10.Add(new(Leaderboardx10File[i], Convert.ToInt32(Leaderboardx10File[i + 1])));
            }

            var Leaderboardx12File = File.ReadAllLines(@"txt\Highscorex12.txt");
            for (int i = 0; i < Leaderboardx12File.Length; i += 2)
            {
                Leaderboardx12.Add(new(Leaderboardx12File[i], Convert.ToInt32(Leaderboardx12File[i + 1])));
            }

            var Leaderboardx15File = File.ReadAllLines(@"txt\Highscorex15.txt");
            for (int i = 0; i < Leaderboardx15File.Length; i += 2)
            {
                Leaderboardx12.Add(new(Leaderboardx15File[i], Convert.ToInt32(Leaderboardx15File[i + 1])));
            }

            Random r = new();
            InitializeComponent();
            DataContext = this;
            BackgroundBrush.Opacity = 0.9;
            Lighten.Opacity = 0.07;
            MoreLighten.Opacity = 0.2;
            CurrentPage = 0;

            MainTabControl.SelectedIndex = SelectedPage switch
            {
                "Home" => 0,
                "Leaderboard" => 1,
                "Options" => 2,
                _ => 0,
            };


            /// <summary>
            ///  Initialize Audio
            /// </summary>
            void PlayBGM(object sender = null, dynamic e = null)
            {
                if (ContinueBGM)
                {
                    BGM.Open(BackgroundSongs[r.Next(0, BackgroundSongs.Count)]);
                    BGM.Volume = 0.4 * MusicVolume;
                    BGM.Play();
                }
            }
            BGM.MediaEnded += PlayBGM;
            PlayBGM();


            /// <summary>
            ///  Base profiles
            /// </summary>
            Playersx10.Add(new("Player 1", false, null, null, null, Visibility.Visible));
            Playersx12.Add(new("Player 1", false, null, null, null, Visibility.Visible));
            Playersx15.Add(new("Player 1", false, null, null, null, Visibility.Visible));


            /// <summary>
            ///  Fill the ProfilePictures container with images
            /// </summary>
            string BaseLink = "https://robohash.org/";
            for (int i = 0; i < 50; i++)
            {
                Image image = new();
                image.Source = new BitmapImage(new Uri(BaseLink + RandomString(r.Next(1, 15))));
                image.Height = 30;
                image.Width = 30;
                ProfilePictures.Add(image);
            }

            Closed += (_, _) =>
            {
                ContinueBGM = false;
                BGM.Stop();
                ButtonClick.Stop();

                using (StreamWriter sw = new(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\txt\Options.txt"))
                {
                    sw.WriteLine(IsHighscoreFrozen.ToString());
                    sw.WriteLine(IsMaximised.ToString());
                    sw.WriteLine(SFXVolume.ToString());
                    sw.WriteLine(MusicVolume.ToString());
                    sw.WriteLine(SelectedPage);
                }

                using (StreamWriter sw = new(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\txt\Highscorex10.txt"))
                {
                    foreach (var item in Leaderboardx10)
                    {
                        sw.WriteLine(item.Name);
                        sw.WriteLine(item.Score);
                    }
                }

                using (StreamWriter sw = new(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\txt\Highscorex12.txt"))
                {
                    foreach (var item in Leaderboardx12)
                    {
                        sw.WriteLine(item.Name);
                        sw.WriteLine(item.Score);
                    }
                }

                using (StreamWriter sw = new(@"C:\Users\timta\Downloads\SnakesAndLadders\SnakesAndLadders\bin\Debug\net5.0-windows\txt\Highscorex15.txt"))
                {
                    foreach (var item in Leaderboardx15)
                    {
                        sw.WriteLine(item.Name);
                        sw.WriteLine(item.Score);
                    }
                }

                Environment.Exit(0);
            };
        }


        private void BtnClickSFX()
        {
            ButtonClick.Open(SFX[0]);
            ButtonClick.Volume = SFXVolume;
            ButtonClick.Play();
        }
        private void PlayBtnClickSFX()
        {
            PlayButtonClick.Open(SFX[1]);
            PlayButtonClick.Volume = 0.56 * SFXVolume;
            PlayButtonClick.SpeedRatio = 1.9;
            PlayButtonClick.Play();
        }


        /// <summary>
        ///  Bubble sort a leaderboard in descending order of Score
        /// </summary>
        private ObservableCollection<Result> LbSort(ObservableCollection<Result> items)
        {
            for (int j = 0; j <= items.Count - 2; j++)
            {
                for (int i = 0; i <= items.Count - 2; i++)
                {
                    if (items[i].Score < items[i + 1].Score)
                    {
                        var temp = items[i + 1];
                        items[i + 1] = items[i];
                        items[i] = temp;
                    }
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                items[i] = new(items[i].Name, items[i].Score, Convert.ToString(i + 1) + ".");
            }
            return items;
        }


        /// <summary>
        ///  Show the selected page
        /// </summary>
        private void Page_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button
                && button.Content is StackPanel stackpanel)
            {
                CurrentPage = Convert.ToInt32(button.Name.Replace("Page_", ""));
                BtnClickSFX();
            }
        }

        private void Pagex10_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn
                && btn.Content is StackPanel stackpnl
                && stackpnl.Children[0] is TextBlock txtblock)
            {
                BtnClickSFX();
                for (int i = 0; i < ItemsControlForx10.Items.Count; i++)
                {
                    if (ItemsControlForx10.Items[i] is Player player
                        && player.Name == txtblock.Text)
                    {
                        CurrentPagex10 = i;
                        break;
                    }
                }
            }
        }
        private void Pagex12_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn
                && btn.Content is StackPanel stackpnl
                && stackpnl.Children[0] is TextBlock txtblock)
            {
                BtnClickSFX();
                for (int i = 0; i < ItemsControlForx12.Items.Count; i++)
                {
                    if (ItemsControlForx12.Items[i] is Player player
                        && player.Name == txtblock.Text)
                    {
                        CurrentPagex12 = i;
                        break;
                    }
                }
            }
        }
        private void Pagex15_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn
                && btn.Content is StackPanel stackpnl
                && stackpnl.Children[0] is TextBlock txtblock)
            {
                BtnClickSFX();
                for (int i = 0; i < ItemsControlForx15.Items.Count; i++)
                {
                    if (ItemsControlForx15.Items[i] is Player player
                        && player.Name == txtblock.Text)
                    {
                        CurrentPagex10 = i;
                        break;
                    }
                }
            }
        }


        /// <summary>
        ///  Switch between Play and Cancel
        /// </summary>
        private void Play_OnClick(object sender, RoutedEventArgs e)
        {
            if (Play.Content is string content
                && content == "PLAY")
            {
                CurrentPage = 4;
                Page_0.Visibility = Visibility.Collapsed;
                Page_1.Visibility = Visibility.Collapsed;
                Page_3.Visibility = Visibility.Collapsed;
                Page_4.Visibility = Visibility.Visible;
                Page_5.Visibility = Visibility.Visible;
                Page_6.Visibility = Visibility.Visible;
                Play.Content = "Cancel";
                Start.Visibility = Visibility.Visible;
                PlayBtnClickSFX();
            }
            else
            {
                CurrentPage = 0;
                Page_0.Visibility = Visibility.Visible;
                Page_1.Visibility = Visibility.Visible;
                Page_3.Visibility = Visibility.Visible;
                Page_4.Visibility = Visibility.Collapsed;
                Page_5.Visibility = Visibility.Collapsed;
                Page_6.Visibility = Visibility.Collapsed;
                Play.Content = "PLAY";
                Start.Visibility = Visibility.Collapsed;
                PlayBtnClickSFX();
            }
        }


        /// <summary>
        ///  UI refreshing 
        /// </summary>
        private void NameTB1_OnTextChangedx10(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 1";
                    }
                    Playersx10[0].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx10)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB2_OnTextChangedx10(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 2";
                    }
                    Playersx10[1].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx10)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB3_OnTextChangedx10(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 3";
                    }
                    Playersx10[2].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx10)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB4_OnTextChangedx10(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 4";
                    }
                    Playersx10[3].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx10)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB5_OnTextChangedx10(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 5";
                    }
                    Playersx10[4].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx10)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB1_OnTextChangedx12(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 1";
                    }
                    Playersx12[0].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx12)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB2_OnTextChangedx12(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 2";
                    }
                    Playersx12[1].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx12)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB3_OnTextChangedx12(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 3";
                    }
                    Playersx12[2].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx12)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB4_OnTextChangedx12(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 4";
                    }
                    Playersx12[3].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx12)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB5_OnTextChangedx12(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 5";
                    }
                    Playersx12[4].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx12)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB1_OnTextChangedx15(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 1";
                    }
                    Playersx15[0].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx15)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB2_OnTextChangedx15(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 2";
                    }
                    Playersx15[1].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx15)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB3_OnTextChangedx15(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 3";
                    }
                    Playersx15[2].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx15)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB4_OnTextChangedx15(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 4";
                    }
                    Playersx15[3].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx15)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }
        private void NameTB5_OnTextChangedx15(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (tb.Text == string.Empty)
                    {
                        tb.Text = "Player 5";
                    }
                    Playersx15[4].Name = tb.Text;

                    var occurrences = 0;
                    foreach (var item in Playersx15)
                    {
                        if (item.Name == tb.Text)
                        {
                            occurrences++;
                        }
                    }
                    if (occurrences > 1)
                    {
                        tb.Text = tb.Text + " 1";
                    }
                }
                catch { }
            }
        }


        /// <summary>
        ///  Button -> remove player
        /// </summary>
        private void RemoveBtn1_OnClickx10(object sender, RoutedEventArgs e)
        {
            if (Playersx10.Count > 1)
            {
                Playersx10.RemoveAt(0);
                BtnClickSFX();
            }
            CurrentPagex10 = 0;
        }
        private void RemoveBtn2_OnClickx10(object sender, RoutedEventArgs e)
        {
            Playersx10.RemoveAt(1);
            CurrentPagex10 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn3_OnClickx10(object sender, RoutedEventArgs e)
        {
            Playersx10.RemoveAt(2);
            CurrentPagex10 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn4_OnClickx10(object sender, RoutedEventArgs e)
        {
            Playersx10.RemoveAt(3);
            CurrentPagex10 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn5_OnClickx10(object sender, RoutedEventArgs e)
        {
            Playersx10.RemoveAt(4);
            CurrentPagex10 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn1_OnClickx12(object sender, RoutedEventArgs e)
        {
            if (Playersx12.Count > 1)
            {
                Playersx12.RemoveAt(0);
                BtnClickSFX();
            }
            CurrentPagex12 = 0;
        }
        private void RemoveBtn2_OnClickx12(object sender, RoutedEventArgs e)
        {
            Playersx12.RemoveAt(1);
            CurrentPagex12 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn3_OnClickx12(object sender, RoutedEventArgs e)
        {
            Playersx12.RemoveAt(2);
            CurrentPagex12 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn4_OnClickx12(object sender, RoutedEventArgs e)
        {
            Playersx12.RemoveAt(3);
            CurrentPagex12 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn5_OnClickx12(object sender, RoutedEventArgs e)
        {
            Playersx12.RemoveAt(4);
            CurrentPagex12 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn1_OnClickx15(object sender, RoutedEventArgs e)
        {
            if (Playersx15.Count > 1)
            {
                Playersx15.RemoveAt(0);
                BtnClickSFX();
            }
            CurrentPagex15 = 0;
        }
        private void RemoveBtn2_OnClickx15(object sender, RoutedEventArgs e)
        {
            Playersx15.RemoveAt(1);
            CurrentPagex15 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn3_OnClickx15(object sender, RoutedEventArgs e)
        {
            Playersx15.RemoveAt(2);
            CurrentPagex15 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn4_OnClickx15(object sender, RoutedEventArgs e)
        {
            Playersx15.RemoveAt(3);
            CurrentPagex15 = 0;
            BtnClickSFX();
        }
        private void RemoveBtn5_OnClickx15(object sender, RoutedEventArgs e)
        {
            Playersx15.RemoveAt(4);
            CurrentPagex15 = 0;
            BtnClickSFX();
        }


        /// <summary>
        ///  Add new player
        /// </summary>
        private void AddBtn_OnClickx10(object sender, RoutedEventArgs e)
        {
            if (Playersx10.Count < 5)
            {
                Random r = new();
                Playersx10.Add(new("Player " + r.Next(1, 100).ToString(), false, null, null, null, Visibility.Collapsed));
                BtnClickSFX();
            }
        }
        private void AddBtn_OnClickx12(object sender, RoutedEventArgs e)
        {
            if (Playersx12.Count < 5)
            {
                Random r = new();
                Playersx12.Add(new("Player " + r.Next(1, 100).ToString(), false, null, null, null, Visibility.Collapsed));
                BtnClickSFX();
            }
        }
        private void AddBtn_OnClickx15(object sender, RoutedEventArgs e)
        {
            if (Playersx15.Count < 5)
            {
                Random r = new();
                Playersx15.Add(new("Player " + r.Next(1, 100).ToString(), false, null, null, null, Visibility.Collapsed));
                BtnClickSFX();
            }
        }


        /// <summary>
        ///  Launch game
        /// </summary>
        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            /// <summary>
            ///  Player profile null check and duplicate check
            ///  If null or duplicate, replace with new random
            /// </summary>
            Random r = new();
            var Players = CurrentPage switch
            {
                4 => Playersx10,
                5 => Playersx12,
                6 => Playersx15,
                _ => throw new ArgumentException(),
            };
            foreach (var item in Players)
            {
                if (item.Profile == null)
                {
                    item.Profile = new()
                    {
                        Source = ProfilePictures[r.Next(0, ProfilePictures.Count)].Source,
                    };
                }
                var dulpicate = 0;
                foreach (var item2 in Players)
                {
                    if (item.Profile == item2.Profile)
                    {
                        dulpicate++;
                    }
                }
                if (dulpicate > 1)
                {
                    item.Profile = new()
                    {
                        Source = ProfilePictures[r.Next(0, ProfilePictures.Count)].Source,
                    };
                }

                if (item.VictorySong == null)
                {
                    item.VictorySong = VictorySongs[r.Next(0, VictorySongs.Count)];
                }
            }


            PlayBtnClickSFX();
            GameWindow gamewindow = new(this,
                CurrentPage switch
                {
                    4 => Playersx10,
                    5 => Playersx12,
                    6 => Playersx15,
                    _ => throw new ArgumentException(CurrentPage.ToString()),
                },
                CurrentPage switch
                {
                    4 => "10x10",
                    5 => "12x12",
                    6 => "15x15",
                    _ => throw new ArgumentException(CurrentPage.ToString()),
                }
            );
            gamewindow.ShowDialog();
        }
    }


    /// <summary>
    ///  Records
    /// </summary>
    public record Result(string Name, int Score, string Index = null);
    public record Game(string Name, string Mode, string Winner, params string[] Players);
    public record Player : INotifyPropertyChanged
    {
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new(nameof(Name)));
            }
        }
        private string name;

        public bool IsBot
        {
            get => isbot;
            set
            {
                isbot = value;
                PropertyChanged?.Invoke(this, new(nameof(IsBot)));
            }
        }
        private bool isbot;

        public Image Profile
        {
            get => profile;
            set
            {
                profile = value;
                PropertyChanged?.Invoke(this, new(nameof(Profile)));
            }
        }
        private Image profile = new();

        public Uri VictorySong
        {
            get => victorySong;
            set
            {
                victorySong = value;
                PropertyChanged?.Invoke(this, new(nameof(VictorySong)));
            }
        }
        private Uri victorySong;
        public Button Button
        {
            get => button;
            set
            {
                button = value;
                PropertyChanged?.Invoke(this, new(nameof(Button)));
            }
        }
        private Button button = new();

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                PropertyChanged?.Invoke(this, new(nameof(Visibility)));
            }
        }
        private Visibility visibility = Visibility.Collapsed;

        public event PropertyChangedEventHandler PropertyChanged;

        public Player(string name, bool isbot, Image profile, Uri victoryaudio, Button button, Visibility visibility)
        {
            Name = name;
            IsBot = isbot;
            Profile = profile;
            VictorySong = victoryaudio;
            Button = button;
            Visibility = visibility;
        }
    }
}
