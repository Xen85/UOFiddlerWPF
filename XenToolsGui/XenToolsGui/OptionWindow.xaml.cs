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
    }
}
