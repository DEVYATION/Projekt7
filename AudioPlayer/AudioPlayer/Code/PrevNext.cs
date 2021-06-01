using System;
using System.Windows;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count != 0)
            {
                if (mediaPlayer.Position.TotalSeconds > 3)
                {
                    Start_Again();
                }
                else
                {
                    if (shuffle == 0)
                    {
                        if (isPlaying != 0)
                        {
                            if (playing == 0)
                            {
                                playing = SongsListBox.Items.Count - 1;
                                SongsListBox.SelectedIndex = SongsListBox.Items.Count - 1;
                                mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.Items.Count - 1].ToString()));
                                Start();
                            }
                            else
                            {
                                playing--;
                                SongsListBox.SelectedIndex = playing;
                                mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
                                Start();
                            }
                        }
                        else
                        {
                            if (SongsListBox.SelectedIndex == -1)
                            {
                                First_Index();
                                Start();
                            }
                            else if (SongsListBox.SelectedIndex == 0)
                            {
                                SongsListBox.SelectedIndex = SongsListBox.Items.Count - 1;
                                playing = SongsListBox.Items.Count - 1;
                                mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.Items.Count - 1].ToString()));
                                Start();
                            }
                            else
                            {
                                SongsListBox.SelectedIndex--;
                                playing = SongsListBox.SelectedIndex;
                                mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
                                Start();
                            }
                        }
                    }
                    else
                    {
                        if (SongsListBox.Items.Count == 1)
                        {
                            First_Index();
                            Start();
                        }
                        else
                        {
                            Random_Song();
                        }
                    }
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count != 0)
            {
                if (shuffle == 0)
                {
                    if (isPlaying != 0)
                    {
                        if (playing != SongsListBox.Items.Count - 1)
                        {
                            Next_Song();
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
                        if (SongsListBox.SelectedIndex != SongsListBox.Items.Count - 1)
                        {
                            SongsListBox.SelectedIndex++;
                            playing = SongsListBox.SelectedIndex;
                            mediaPlayer.Open(new Uri(SongsListBox.Items[SongsListBox.SelectedIndex].ToString()));
                            Start();
                        }
                        else
                        {
                            First_Index();
                            Start();
                        }
                    }
                }
                else
                {
                    if (SongsListBox.Items.Count == 1)
                    {
                        First_Index();
                        Start();
                    }
                    else
                    {
                        Random_Song();
                    }
                }
            }
        }
    }
}
