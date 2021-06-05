using System;
using System.Windows;
using System.Windows.Media;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count != 0)
            {
                if (isPlaying == 0)
                {
                    if (shuffle == 0)
                    {
                        if (SongsListBox.SelectedIndex != -1)
                        {
                            Selected_Index();
                            Start();
                        }
                        else
                        {
                            First_Index();
                            Start();
                        }
                    }
                    else
                    {
                        if (SongsListBox.SelectedIndex == -1)
                        {
                            Random_Song();
                        }
                        else
                        {
                            Selected_Index();
                            Start();
                        }
                    }
                }
                else if (isPlaying == 1)
                {
                    mediaPlayer.Play();
                    isPlaying = 2;
                    PlayPauseButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF82EE76");
                }
                else
                {
                    mediaPlayer.Pause();
                    isPlaying = 1;
                    PlayPauseButton.Background = (Brush)new BrushConverter().ConvertFrom("#FFC5C511");
                }
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void Start()
        {
            mediaPlayer.Play();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            isPlaying = 2;
            PositionSlider.IsEnabled = true;
            PlayPauseButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF82EE76");
            mediaPlayer.Volume = VolumeSlider.Value;
            mediaPlayer.MediaFailed += Media_Failed;
        }

        private void Stop()
        {
            playing = -1;
            SongsListBox.SelectedIndex = -1;
            mediaPlayer.Close();
            timer.Stop();
            isPlaying = 0;
            PositionSlider.Value = 0;
            PositionSlider.Maximum = 1;
            PositionSlider.IsEnabled = false;
            PositionLabel.Content = "00:00/00:00";
            PlayPauseButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF12B900");
        }
    }
}
