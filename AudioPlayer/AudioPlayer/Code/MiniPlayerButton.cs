using System;
using System.Windows;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void MiniPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count != 0)
            {
                MiniPlayer mp = new MiniPlayer();
                for (int i = 0; i < SongsListBox.Items.Count; i++)
                {
                    mp.SongsListBox.Items.Add(SongsListBox.Items[i]);
                }
                mp.playing = playing;
                mp.SongsListBox.SelectedIndex = SongsListBox.SelectedIndex;
                mp.isPlaying = isPlaying;
                mp.timer.Interval = timer.Interval;
                mp.timer.Tick += mp.Timer_Tick;
                mp.timer.Start();
                mp.PositionSlider.Value = PositionSlider.Value;
                mp.PositionSlider.Maximum = PositionSlider.Maximum;
                mp.PositionSlider.IsEnabled = PositionSlider.IsEnabled;
                mp.PositionLabel.Content = PositionLabel.Content;
                mp.VolumeSlider.Value = VolumeSlider.Value;
                mp.VolumeLabel.Content = VolumeLabel.Content;
                mp.mediaPlayer.Volume = VolumeSlider.Value;
                mp.mediaPlayer.Balance = mediaPlayer.Balance;
                mp.PlayPauseButton.Background = PlayPauseButton.Background;
                mp.StopButton.Background = StopButton.Background;
                mp.RepeatButton.Background = RepeatButton.Background;
                mp.RepeatButton.Content = RepeatButton.Content;
                mp.repeatType = repeatType;
                mp.ShuffleButton.Background = ShuffleButton.Background;
                mp.shuffle = shuffle;
                try
                {
                    mp.mediaPlayer.Open(new Uri(SongsListBox.Items[playing].ToString()));
                    mp.Show();
                    if (mp.isPlaying == 2)
                    {
                        mp.mediaPlayer.Play();
                    }
                    else if (mp.isPlaying == 1)
                    {
                        mp.mediaPlayer.Pause();
                    }
                    mp.mediaPlayer.Position = mediaPlayer.Position;
                }
                catch
                {
                    mp.Show();
                }
                mediaPlayer.Close();
                Close();
            }
            else
            {
                MessageWindow.Show("Empty playlist", "Please add some songs to your playlist first.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
            }
        }
    }
}
