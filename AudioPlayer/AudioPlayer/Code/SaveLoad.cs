using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace AudioPlayer
{
    public partial class MainWindow
    {
        private void SavePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListBox.Items.Count == 0)
            {
                MessageWindow.Show("Empty Playlist", "You cannot save an empty playlist.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
            }
            else
            {
                Save();
                if (saveSuccess == 0)
                {
                    MessageWindow.Show("Save failed", "The save has failed. This could mean the save was aborted or the destination file is currently in use. To solve the later problem, you can try creating a new playlist instead of overwriting an existing one.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
                }
            }
        }

        private void LoadPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt"
                };
                open.ShowDialog();
                sr = new StreamReader(open.FileName);
                string ok = sr.ReadLine();
                if (ok != "Audio Player version 1.0. Below is your playlist.")
                {
                    throw new Error();
                }
                if (SongsListBox.Items.Count != 0)
                {
                    MessageBoxResult mbr = MessageWindow.Show("Are you sure?", "Opening a saved playlist will clear your current playlist. Would you like to save your playlist first?", MessageBoxButton.YesNoCancel, MessageWindow.MessageBoxImage.Warning);
                    if (mbr == MessageBoxResult.Yes)
                    {
                        Save();
                        if (saveSuccess == 1)
                        {
                            SongsListBox.Items.Clear();
                            Stop();
                            Load();
                        }
                        else
                        {
                            MessageWindow.Show("Save failed", "The save has failed. This could mean the save was aborted or the destination file is currently in use. To solve the later problem, you can try creating a new playlist instead of overwriting an existing one. (Your playlist was not loaded.)", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
                        }
                    }
                    else if (mbr == MessageBoxResult.No)
                    {
                        SongsListBox.Items.Clear();
                        Stop();
                        Load();
                    }
                }
                else
                {
                    Load();
                }
            }
            catch (Error)
            {
                MessageWindow.Show("Fatal error", "An unexpected error occured. Please check that the loaded file was made with this application, then try again.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Error);
            }
            catch
            {

            }
            finally
            {
                sr.Dispose();
            }
        }

        public void Save()
        {
            try
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt"
                };
                sf.ShowDialog();
                StreamWriter sw = new StreamWriter(sf.FileName);
                sw.WriteLine("Audio Player version 1.0. Below is your playlist.");
                for (int i = 0; i < SongsListBox.Items.Count; i++)
                {
                    sw.WriteLine(SongsListBox.Items[i].ToString());
                }
                sw.Close();
                sw.Dispose();
                MessageWindow.Show("Success", "Your playlist has been successfully saved.", MessageBoxButton.OK, MessageWindow.MessageBoxImage.Information);
                saveSuccess = 1;
            }
            catch
            {
                saveSuccess = 0;
            }
        }

        private void Load()
        {
            do
            {
                SongsListBox.Items.Add(sr.ReadLine());
            } while (!sr.EndOfStream);
            sr.Dispose();
        }
    }
}
