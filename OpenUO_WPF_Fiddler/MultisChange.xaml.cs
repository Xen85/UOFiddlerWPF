using System;
using System.Collections.Generic;
using System.IO;
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
using TilesInfo.Components;

namespace OpenUO_WPF_Fiddler
{
    /// <summary>
    /// Interaction logic for MultisChange.xaml
    /// </summary>
    public partial class MultisChange : Window
    {
        public MultisChange()
        {
            InitializeComponent();
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.SDK != null)
            {
                MainWindow.SDK.Populate();

                ListBoxWalls.ItemsSource = MainWindow.SDK.Walls.Categories;

                listBoxFloors.ItemsSource = MainWindow.SDK.Floors.Categories;

                listBoxMisc.ItemsSource = MainWindow.SDK.Misc.Categories;

                listBoxRoof.ItemsSource = MainWindow.SDK.Roofs.Categories;
            }

            if (!string.IsNullOrEmpty(TextBoxFileName.Text))
            {
                MainWindow.SDK.TakeFromTXTFile(TextBoxFileName.Text);
                FileCategories.ItemsSource = MainWindow.SDK.Txt.Walls;
            }
        }

        private void ButtonSearchFile_Click(object sender, RoutedEventArgs e)
        {
            var selectFile = new Microsoft.Win32.OpenFileDialog { Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*", FilterIndex = 1 };
            selectFile.ShowDialog();
            TextBoxFileName.Text = selectFile.FileName;
        }

        #region buttons

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var catIn = ListBoxWalls.SelectedItem as TileCategory;
            var catOut = FileCategories.SelectedItem as TileCategory;
            MainWindow.SDK.Txt.SubstitueWallCat(catIn, catOut);
            if (!string.IsNullOrEmpty(textBlock_SaveFileName.Text))
                File.WriteAllText(textBlock_SaveFileName.Text, MainWindow.SDK.Txt.ToString());
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var selectfile = new Microsoft.Win32.SaveFileDialog() { Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*", FilterIndex = 1 };
            selectfile.ShowDialog();
            textBlock_SaveFileName.Text = selectfile.FileName;
        }
        #endregion
    }
}
