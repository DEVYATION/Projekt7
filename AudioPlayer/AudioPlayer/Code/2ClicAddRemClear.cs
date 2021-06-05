using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void SongsListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DependencyObject obj = (DependencyObject)e.OriginalSource;

                while (obj != null && obj != SongsListBox)
                {
                    if (obj.GetType() == typeof(ListBoxItem))
                    {
                        Selected_Index();
                        Start();
                        break;
                    }
                    obj = VisualTreeHelper.GetParent(obj);
                }
            }
            catch
            {

            }
        }

        private void AddSongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3",
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    if (!SongsListBox.Items.Contains(file))
                    {
                        SongsListBox.Items.Add(file);
                    }
                }
            }
        }

        private void RemoveSongButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count == 0)
            {
                MessageWindow.Show("Empty playlist", "There are no items to be removed.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
            }
            else
            {
                if (SongsListBox.SelectedIndex == -1)
                {
                    MessageWindow.Show("Select a song first", "Please select a song from the playlist first.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
                }
                else
                {
                    if (SongsListBox.SelectedIndex == playing)
                    {
                        SongsListBox.Items.Remove(SongsListBox.SelectedItem);
                        Stop();
                    }
                    else
                    {
                        if (SongsListBox.SelectedIndex > playing)
                        {
                            SongsListBox.Items.Remove(SongsListBox.SelectedItem);
                            SongsListBox.SelectedIndex = playing;
                        }
                        else
                        {
                            playing--;
                            SongsListBox.Items.Remove(SongsListBox.SelectedItem);
                            SongsListBox.SelectedIndex = playing;
                        }
                    }
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count != 0)
            {
                MessageBoxResult mbr = MessageWindow.Show("Are you sure?", "This will clear your current playlist. Would you like to save your playlist first?", MessageBoxButton.YesNoCancel, MessageWindow.MessageBoxImage.Warning);
                if (mbr == MessageBoxResult.Yes)
                {
                    Save();
                    if (saveSuccess == 1)
                    {
                        SongsListBox.Items.Clear();
                        Stop();
                    }
                    else
                    {
                        MessageWindow.Show("Save failed", "The save has failed. This could mean the save was aborted or the destination file is currently in use. To solve the later problem, you can try creating a new playlist instead of overwriting an existing one. (Your playlist was not cleared.)", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
                    }
                }
                else if (mbr == MessageBoxResult.No)
                {
                    SongsListBox.Items.Clear();
                    Stop();
                }
            }
            else
            {
                MessageWindow.Show("Empty playlist", "Your playlist is already empty.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
            }
        }
    }
}
