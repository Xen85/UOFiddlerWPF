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
using OpenUO.MapMaker.MapMaking;

namespace OpenUO_WPF_Fiddler.MapMaker
{
    /// <summary>
    /// Interaction logic for MapMakerMake.xaml
    /// </summary>
    public partial class MapMakerMake : UserControl
    {
        public MapMakerMake()
        {
            InitializeComponent();
            comboBoxMapChoose.ItemsSource = Globals.names;
            comboBoxMapChoose.SelectedIndex = 0;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var selectfile = new Microsoft.Win32.OpenFileDialog() { Filter = "Bmp Files (.bmp)|*.bmp|All Files (*.*)|*.*", FilterIndex = 1 };
            selectfile.ShowDialog();
            string fileload;
            if (!string.IsNullOrEmpty(selectfile.FileName))
                fileload = selectfile.FileName;
            fileload = selectfile.FileName;

            if (string.IsNullOrEmpty(fileload))
                return;

            textBoxBitmapMapLocation.Text = fileload;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var selectfile = new Microsoft.Win32.OpenFileDialog() { Filter = "Bmp Files (.bmp)|*.bmp|All Files (*.*)|*.*", FilterIndex = 1 };
            selectfile.ShowDialog();
            string fileload;
            if (!string.IsNullOrEmpty(selectfile.FileName))
                fileload = selectfile.FileName;
            fileload = selectfile.FileName;

            if (string.IsNullOrEmpty(fileload))
                return;

            textBoxBitmapZLocation.Text = fileload;
        }

        private void buttonChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            string s ="";
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.Desktop;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    s = dialog.SelectedPath;
                }
            }
            textBoxFolder.Text = s;
        }

        private void buttonRun_Click(object sender, RoutedEventArgs e)
        {
            if (MapCreator2.SDK == null)
            {
                MessageBox.Show("First load informations", "Not Loaded informations", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            if(string.IsNullOrEmpty(textBoxFolder.Text) || string.IsNullOrEmpty(textBoxBitmapZLocation.Text) || string.IsNullOrEmpty(textBoxBitmapMapLocation.Text))
            {
                MessageBox.Show("Someone of informations in this tab is empty", "Not Loaded informations", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            MapCreator2.SDK.MapMake(textBoxFolder.Text, textBoxBitmapMapLocation.Text, textBoxBitmapZLocation.Text, Globals.Dimentions[comboBoxMapChoose.SelectedIndex].First(), Globals.Dimentions[comboBoxMapChoose.SelectedIndex].Last(),Globals.Indexes[comboBoxMapChoose.SelectedIndex],false);

        }
    }
}
