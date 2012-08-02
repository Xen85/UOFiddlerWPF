using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenUO.MapMaker;

namespace OpenUO_WPF_Fiddler.MapMaker
{
    /// <summary>
    /// Interaction logic for MapCreator2.xaml
    /// </summary>
    public partial class MapCreator2 : UserControl
    {
        public static MakeMapSDK SDK { get; set; }

        public MapCreator2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string s = null;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    s = dialog.SelectedPath;
                }
            }
            if (!string.IsNullOrEmpty(s))
            {
                textBoxFolder.Text = s;
            }
        }

        private void ButtonRead_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFolder.Text)) return;
            try
            {
                SDK = new MakeMapSDK(textBoxFolder.Text);
                SDK.Populate();
                DataContext = SDK;
            }
            catch(Exception)
            {
                
            }
        }

        private void buttonMakeACO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(TextBoxOutputFolder.Text))
                {
                    SDK.MakeACO(TextBoxOutputFolder.Text);
                    MessageBox.Show("palette.aco file is ready in your selected folder", "", MessageBoxButton.OK,
                                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
                }
            }
            catch (Exception aException)
            {
                MessageBox.Show(aException.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonBrowseFolderOutput_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TextBoxOutputFolder.Text = dialog.SelectedPath;
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void buttonSaveArea_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBoxOutputFolder.Text))
            {
                try
                {
                    SDK.SaveBinary(TextBoxOutputFolder.Text);
                    MessageBox.Show("All saved where you specified","Save Finished", MessageBoxButton.OK,MessageBoxImage.Information);
                }
                catch (Exception aException)
                {
                    MessageBox.Show(aException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please insert a folder and press enter", "Folder empty", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (SDK == null)
            {
                SDK = new MakeMapSDK();
            }

            if(!string.IsNullOrEmpty(textBoxFolder.Text))
            {
                try
                {
                    SDK.LoadBinary(textBoxFolder.Text);
                    MessageBox.Show("All Contents were loaded", "Load Finished", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataContext = SDK;
                }
                catch (Exception aException)
                {
                    if(aException.InnerException!= null)
                    MessageBox.Show(aException.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        MessageBox.Show(aException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please insert a folder", "Folder empty", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
