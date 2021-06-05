using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            if (repeatType == 0)
            {
                repeatType = 1;
                RepeatButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF6BA1FF");
            }
            else if (repeatType == 1)
            {
                repeatType = 2;
                RepeatButton.Content = "🔂";
            }
            else
            {
                repeatType = 0;
                RepeatButton.Content = "🔁";
                RepeatButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF9C9C9C");
            }
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            if (shuffle == 0)
            {
                shuffle = 1;
                ShuffleButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF6BA1FF");
            }
            else
            {
                shuffle = 0;
                ShuffleButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF9C9C9C");
            }
        }

        private void PositionSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            positionSliderIsMoving = 1;
        }

        private void PositionSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            positionSliderIsMoving = 0;
            mediaPlayer.Position = TimeSpan.FromSeconds(PositionSlider.Value);
            if (PositionSlider.Value == PositionSlider.Maximum)
            {
                MediaEnd();
            }
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                PositionLabel.Content = string.Format("{0}/{1}", TimeSpan.FromSeconds(PositionSlider.Value).ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            catch
            {

            }
        }

        private void VolumeSlider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            mediaPlayer.Volume = VolumeSlider.Value;
            VolumeLabel.Content = Math.Round(VolumeSlider.Value * 100) + "%";
        }
    }
}
