using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XenToolsGui
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        public OptionWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBoxInstallFolder.Text))
            {
                Globals.Globals.Installdirectory = TextBoxInstallFolder.Text;
            }

            if(!string.IsNullOrEmpty(TextBoxSaveFolder.Text))
            {
                Globals.Globals.SaveDirLocation = TextBoxSaveFolder.Text;
            }
        }

        private void buttonInstallFolder_Click(object sender, RoutedEventArgs e)
        {
           SelectionTextBox(TextBoxInstallFolder);
        }

        private void buttonSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            SelectionTextBox(TextBoxSaveFolder);
        }

        private void SelectionTextBox(TextBox text)
        {
            var browser = new System.Windows.Forms.FolderBrowserDialog();
            var ris = browser.ShowDialog();

            if (ris != System.Windows.Forms.DialogResult.OK)
                return;

            text.Text = browser.SelectedPath;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
           var dialog = new Microsoft.Win32.OpenFileDialog {Filter = "XmlFile *.xml|*.xml", DefaultExt = "*.xml"};
            var ris = dialog.ShowDialog();
            if(ris == true)
            {
                try
                {
                    Globals.Globals.LoadOptions(dialog.FileName);
                    TextBoxInstallFolder.Text = Globals.Globals.InstallLocation.Directory.ToString(CultureInfo.InstalledUICulture);
                    TextBoxSaveFolder.Text = Globals.Globals.SaveDirLocation;
                }
                catch (Exception exception)
                {

                    MessageBox.Show("Failed Load Config File", "Error Loading File", MessageBoxButton.OK,
                                    MessageBoxImage.Error,MessageBoxResult.None,MessageBoxOptions.None);
                }
                
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(TextBoxSaveFolder.Text))
            {
                button1_Click(sender, e);
                try
                {
                    Globals.Globals.SaveOptions();
                }
                catch (Exception)
                {
                    MessageBox.Show("Some problem in saving", "Save Config Error", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Insert Save Folder, please!", "Save Config error", MessageBoxButton.OK,
                                MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.None);
            }
        }
    }
}
