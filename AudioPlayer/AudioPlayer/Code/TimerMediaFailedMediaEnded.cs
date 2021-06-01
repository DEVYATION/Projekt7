using System;
using System.Windows;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        public void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if ((mediaPlayer.Source != null) && mediaPlayer.NaturalDuration.HasTimeSpan && (positionSliderIsMoving == 0))
                {
                    PositionLabel.Content = string.Format("{0}/{1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                    PositionSlider.Minimum = 0;
                    PositionSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                    PositionSlider.Value = mediaPlayer.Position.TotalSeconds;
                }
            }
            catch
            {

            }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            MediaEnd();
        }

        private void Media_Failed(object sender, EventArgs e)
        {
            string file = "The requested audio file (" + SongsListBox.SelectedItem + ") could not be opened. Please check if the file's location or name has been changed or remove this audio file from the list, then readd it to the list.";
            MessageWindow.Show("An error occured", file, MessageBoxButton.OK, MessageWindow.MessageBoxImage.Error);
            Stop();
        }

        private void MediaEnd()
        {
            try
            {
                if (positionSliderIsMoving == 0)
                {
                    if (repeatType != 2)
                    {
                        if (shuffle == 0)
                        {
                            if (repeatType == 0)
                            {
                                if (playing < (SongsListBox.Items.Count - 1))
                                {
                                    Next_Song();
                                    Start();
                                }
                                else
                                {
                                    Stop();
                                }
                            }
                            else if (repeatType == 1)
                            {
                                if (playing < (SongsListBox.Items.Count - 1))
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
                        }
                        else
                        {
                            Random_Song();
                        }
                    }
                    else
                    {
                        Start_Again();
                    }
                }
            }
            catch
            {

            }
        }
    }
}
