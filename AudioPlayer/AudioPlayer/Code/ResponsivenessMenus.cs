using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AudioPlayer
{
    public partial class MainWindow : Window
    {
        private void PositionSlider_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Height < 500)
            {
                System.Threading.Thread.Sleep(250);
                PositionLabel.Visibility = Visibility.Hidden;
            }
        }

        private void PositionSlider_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Height < 500)
            {
                System.Threading.Thread.Sleep(250);
                PositionLabel.Visibility = Visibility.Visible;
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            MenuButtons.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((UIElement)sender);
            var result = VisualTreeHelper.HitTest(MenuButtons, pt);
            var result2 = VisualTreeHelper.HitTest(MenuButton, pt);
            if (result == null && result2 == null)
            {
                MenuButtons.Visibility = Visibility.Hidden;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Width < 500)
            {
                CPLabel.Visibility = Visibility.Collapsed;
                CPGrid.Width = 100;
                PositionSlider.Width = double.NaN;
                PositionSlider.HorizontalAlignment = HorizontalAlignment.Stretch;
                ShuffleButton.Visibility = Visibility.Collapsed;
                RepeatButton.Visibility = Visibility.Collapsed;
                MiniPlayerButton.Visibility = Visibility.Collapsed;
                VolumeControlGrid.Visibility = Visibility.Collapsed;
                MenuButton.Visibility = Visibility.Visible;
                ControlsGrid.SetValue(Grid.ColumnSpanProperty, 2);
                PrevButton.Margin = new Thickness(0, 0, 0, 0);
                PlayPauseButton.Margin = new Thickness(55, 0, 0, 0);
                NextButton.Margin = new Thickness(110, 0, 0, 0);
                MenuButton.Margin = new Thickness(165, 10, 0, 0);
                MusicControlsGrid.Width = 205;
                VolumeSlider.Orientation = Orientation.Horizontal;
            }
            else if (Width >= 500 && Width < 600)
            {
                CPLabel.Visibility = Visibility.Collapsed;
                CPGrid.Width = 100;
                PositionSlider.Width = double.NaN;
                PositionSlider.HorizontalAlignment = HorizontalAlignment.Stretch;
                ShuffleButton.Visibility = Visibility.Visible;
                RepeatButton.Visibility = Visibility.Visible;
                MiniPlayerButton.Visibility = Visibility.Visible;
                VolumeControlGrid.Visibility = Visibility.Visible;
                MenuButton.Visibility = Visibility.Hidden;
                ControlsGrid.SetValue(Grid.ColumnSpanProperty, 1);
                PrevButton.Margin = new Thickness(55, 0, 0, 0);
                PlayPauseButton.Margin = new Thickness(110, 0, 0, 0);
                NextButton.Margin = new Thickness(165, 0, 0, 0);
                MenuButton.Margin = new Thickness(275, 10, 0, 0);
                MusicControlsGrid.Width = 315;
                VolumeSlider.Orientation = Orientation.Vertical;
            }
            else
            {
                CPLabel.Visibility = Visibility.Visible;
                CPGrid.Width = 285;
                PositionSlider.Width = 400;
                PositionSlider.HorizontalAlignment = HorizontalAlignment.Center;
            }

            if (Height < 500)
            {
                SongsListBox.SetValue(Grid.RowSpanProperty, 2);
                PositionLabel.Visibility = Visibility.Collapsed;
                PositionLabel.Margin = new Thickness(0, 10, 0, 0);
                PositionLabel.Background = (Brush)new BrushConverter().ConvertFrom("#ff151515");
                ControlsGrid.Margin = new Thickness(0, 0, 0, -40);
                VolumeIcon.Visibility = Visibility.Hidden;
            }
            else
            {
                SongsListBox.SetValue(Grid.RowSpanProperty, 1);
                PositionLabel.Visibility = Visibility.Visible;
                PositionLabel.Margin = new Thickness(0, 110, 0, 0);
                PositionLabel.Background = (Brush)new BrushConverter().ConvertFrom("#00000000");
                ControlsGrid.Margin = new Thickness(0, 0, 0, 0);
                VolumeIcon.Visibility = Visibility.Visible;
            }
        }
    }
}
