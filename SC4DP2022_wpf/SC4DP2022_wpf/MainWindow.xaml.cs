using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SC4DP2022_wpf {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private string activeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SimCity 4\\Plugins";
		private string destinationDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SimCity 4\\Plugins\\Plugins_Compressed";

		public MainWindow() {
			InitializeComponent();

			// Set default folders on startup + fill listbox with items in default folder
			SourceDirectory.Text = activeDirectory;
			DestinationDirectory.Text = destinationDirectory;
			FillFolderList(activeDirectory);
		}

		private void BrowseSourceDirectory_Click(object sender, RoutedEventArgs e) {

			// Folder picker dialog box
			// https://stackoverflow.com/a/41511598
			CommonOpenFileDialog dialog = new CommonOpenFileDialog {
				InitialDirectory = activeDirectory,
				IsFolderPicker = true
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
				//MessageBox.Show("You selected: " + dialog.FileName);
				SourceDirectory.Text = dialog.FileName;
				activeDirectory = dialog.FileName;
			}

			FillFolderList(activeDirectory);
		}



		/// <summary>
		/// Lists all folders in the specified path in list_Folders listBox
		/// </summary>
		/// <param name="path"></param>
		private void FillFolderList(string path) {
			string[] folders = Directory.GetDirectories(path);

			// Remove all existing list items


			// Add the new list items
			foreach (string dir in folders) {
				list_Folders.Items.Add(dir.Substring(dir.LastIndexOf("\\") + 1));
			}
		}

		private void BrowseDestinationDirectory_Click(object sender, RoutedEventArgs e) {

		}
	}
}
