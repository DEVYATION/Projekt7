using System;
using System.Windows.Media;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void Selected_Index()
        {
            playing = SongsListBox.SelectedIndex;
            mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
        }

        private void First_Index()
        {
            playing = 0;
            SongsListBox.SelectedIndex = 0;
            mediaPlayer.Open(new Uri(SongsListBox.Items[0].ToString()));
        }

        private void Start_Again()
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(0);
            if (isPlaying != 1)
            {
                PlayPauseButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF82EE76");
                StopButton.Background = (Brush)new BrushConverter().ConvertFrom("#FFB91414");
            }
        }

        private void Next_Song()
        {
            playing++;
            SongsListBox.SelectedIndex = playing;
            mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
        }

        private void Random_Song()
        {
            if (SongsListBox.Items.Count == 1)
            {
                if (SongsListBox.SelectedIndex == -1)
                {
                    First_Index();
                    Start();
                }
                else
                {
                    Start_Again();
                    Start();
                }
            }
            else
            {
                do
                {
                    shuffleSelection = r.Next(0, SongsListBox.Items.Count);
                    if (isPlaying == 0)
                    {
                        if (shuffleSelection != SongsListBox.SelectedIndex)
                        {
                            shuffleFound = 1;
                            playing = shuffleSelection;
                            SongsListBox.SelectedIndex = playing;
                            mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
                            Start();
                        }
                        else
                        {
                            shuffleFound = 0;
                        }
                    }
                    else
                    {
                        if (shuffleSelection != playing)
                        {
                            shuffleFound = 1;
                            playing = shuffleSelection;
                            SongsListBox.SelectedIndex = playing;
                            mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
                            Start();
                        }
                        else
                        {
                            shuffleFound = 0;
                        }
                    }
                } while (shuffleFound == 0);
            }
        }
    }
}
