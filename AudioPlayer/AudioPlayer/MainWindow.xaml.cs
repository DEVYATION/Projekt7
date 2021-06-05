using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly MediaPlayer mediaPlayer = new MediaPlayer();
        public readonly DispatcherTimer timer = new DispatcherTimer();
        StreamReader sr;
        public readonly Random r = new Random();
        public int positionSliderIsMoving = 0, isPlaying = 0, playing = -1, repeatType = 0, shuffle = 0, shuffleSelection = 0, shuffleFound = 0, saveSuccess = 0;

        private void Window_StateChanged(object sender, EventArgs e)
        {
            SongsListBox.Width = SystemParameters.WorkArea.Width - 80;
            SongsListBox.Height = SystemParameters.WorkArea.Height - 325;
            ControlsGrid.Width = SystemParameters.WorkArea.Width - 80;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SongsListBox.Width = Width - 80;
            SongsListBox.Height = Height - 325;
            ControlsGrid.Width = Width - 80;
        }

        class Error : Exception
        {
            public Error()
            {

            }
        }

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                sr = new StreamReader("./autoconfig_do_not_delete_or_modify.txt");
                string ok = sr.ReadLine();
                if (ok != "Audio Player version 1.0. Below is your automatic config. DO NOT DELETE OR MODIFY THIS FILE!")
                {
                    throw new Error();
                }
                VolumeSlider.Value = Convert.ToDouble(sr.ReadLine());
                VolumeLabel.Content = Math.Round(VolumeSlider.Value * 100) + "%";
                mediaPlayer.Volume = VolumeSlider.Value;
                int fileRepeat = Convert.ToInt32(sr.ReadLine());
                if (fileRepeat == 1)
                {
                    repeatType = 1;
                    RepeatButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF6BA1FF");
                }
                else if (fileRepeat == 2)
                {
                    repeatType = 2;
                    RepeatButton.Content = "🔂";
                    RepeatButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF6BA1FF");
                }
                int fileShuffle = Convert.ToInt32(sr.ReadLine());
                if (fileShuffle == 1)
                {
                    shuffle = 1;
                    ShuffleButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF6BA1FF");
                }
                int fileSelectedIndex = Convert.ToInt32(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    SongsListBox.Items.Add(sr.ReadLine());
                }
                SongsListBox.SelectedIndex = fileSelectedIndex;
                sr.Close();
                sr.Dispose();
            }
            catch (Error)
            {

            }
            catch
            {

            }
            mediaPlayer.MediaEnded += new EventHandler(Media_Ended);
            Closing += new CancelEventHandler(MainWindow_Closing);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("./autoconfig_do_not_delete_or_modify.txt");
                sw.WriteLine("Audio Player version 1.0. Below is your automatic config. DO NOT DELETE OR MODIFY THIS FILE!");
                sw.WriteLine(mediaPlayer.Volume);
                sw.WriteLine(repeatType);
                sw.WriteLine(shuffle);
                if (SongsListBox.Items.Count > 0)
                {
                    sw.WriteLine(SongsListBox.SelectedIndex);
                    for (int i = 0; i < SongsListBox.Items.Count - 1; i++)
                    {
                        sw.WriteLine(SongsListBox.Items[i].ToString());
                    }
                    sw.Write(SongsListBox.Items[SongsListBox.Items.Count - 1].ToString());
                }
                else
                {
                    sw.Write("-1");
                }
                sw.Close();
                sw.Dispose();
            }
            catch
            {
                MessageBoxResult mbr = MessageWindow.Show("Automatic config failed", "The automatic configuration write failed. This could mean the file is under use. Please close all applications that are using the file them try again. Would you like to close the window anyway?", MessageBoxButton.YesNo, MessageWindow.MessageBoxImage.Error);
                if (mbr == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
