using System;
using System.IO; //required for GetDirectories, GetFiles
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
using Microsoft.WindowsAPICodePack.Dialogs; //required for the folder selection dialogs;

namespace SC4DP2022_wpf {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private string activeDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SimCity 4\\Plugins";
		private string destinationDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SimCity 4\\Plugins\\Plugins_Compressed";
		private bool recurseIntoSubfolders = true;

		public MainWindow() {
			InitializeComponent();

			//Read settings file on startup and set properties if set
			if (!Properties.Settings.Default.DefaultSourceDirectory.Equals("")) {
				activeDirectoryPath = Properties.Settings.Default.DefaultSourceDirectory;
			}
			if (!Properties.Settings.Default.DefaultDestinationDirectory.Equals("")) {
				destinationDirectoryPath = Properties.Settings.Default.DefaultDestinationDirectory;
			}

			int h; int w; int resolutionDividerLocn;
			resolutionDividerLocn = Properties.Settings.Default.DefaultWindowDimensions.IndexOf("x");
			if (resolutionDividerLocn == -1) {
				throw new ArgumentException($"Invalid resolution setting. Expected format WWWxHHH, received {Properties.Settings.Default.DefaultWindowDimensions}");
			}
			Int32.TryParse(Properties.Settings.Default.DefaultWindowDimensions.Substring(0, 3), out w);
			this.Height = w;
			Int32.TryParse(Properties.Settings.Default.DefaultWindowDimensions.Substring(0, 3), out h);
			this.Width = h;
			recurseIntoSubfolders = Properties.Settings.Default.DefaultPackMethod;

			// Set default folders on startup + fill listbox with items in default folder
			SourceDirectory.Text = activeDirectoryPath;
			DestinationDirectory.Text = destinationDirectoryPath;
			PopulateFolderListbox(activeDirectoryPath);
		}



		/// <summary>
		/// Opens a folder picker dialog to choose the source directory. The source directory determines the folders to be shown in FolderList.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BrowseSourceDirectory_Click(object sender, RoutedEventArgs e) {

			// Folder picker dialog box
			// https://stackoverflow.com/a/41511598
			CommonOpenFileDialog dialog = new CommonOpenFileDialog {
				InitialDirectory = activeDirectoryPath,
				IsFolderPicker = true
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
				//MessageBox.Show("You selected: " + dialog.FileName);
				SourceDirectory.Text = dialog.FileName;
				activeDirectoryPath = dialog.FileName;
			}

			PopulateFolderListbox(activeDirectoryPath);
		}



		/// <summary>
		/// Opens a folder picker dialog to choose the destination directory. The output packed dat file is saved inside of this folder.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BrowseDestinationDirectory_Click(object sender, RoutedEventArgs e) {
			CommonOpenFileDialog dialog = new CommonOpenFileDialog {
				InitialDirectory = destinationDirectoryPath,
				IsFolderPicker = true
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
				DestinationDirectory.Text = dialog.FileName;
				destinationDirectoryPath = dialog.FileName;
			}
		}



		/// <summary>
		/// Lists all folders in the specified path in FolderList listBox
		/// </summary>
		/// <param name="path"></param>
		private void PopulateFolderListbox(string path) {
			string[] folders = Directory.GetDirectories(path);
			FolderList.Items.Clear();

			foreach (string dir in folders) {
				FolderList.Items.Add(dir.Substring(dir.LastIndexOf("\\") + 1));
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Pack_Click(object sender, RoutedEventArgs e) {

			// do nothing if no files were selected
			if (FolderList.SelectedItems.Count == 0) {
				return;
			}

			//instantiate lists to be used later
			List<string> allFiles = new List<string>();
			List<string> sc4Files = new List<string>();
			List<string> skippedFiles = new List<string>();

			//loop over the files the user selected in the list box
			SearchOption so;
			foreach (string folder in FolderList.SelectedItems) {
				if (recurseIntoSubfolders) {
					so = SearchOption.AllDirectories;
				}
				else {
					so = SearchOption.TopDirectoryOnly;
				}

				//list all files in the current root folder
				string[] files = Directory.GetFiles(activeDirectoryPath + "\\" + folder, "*", so);

				//loop over the list of files and add them to the master file list
				foreach (string file in files) {
					allFiles.Add(file);
				}

				// return two lists with the sorted files
				(sc4Files, skippedFiles) = DBPFUtil.FilterFilesByExtension(allFiles);
				foreach (string file in sc4Files) {
					System.Diagnostics.Debug.WriteLine("sc4: " + file);
				}
				foreach (string file in skippedFiles) {
					System.Diagnostics.Debug.WriteLine("skipped: " + file);
				}
			}

			DBPFFile dbpfFile = new DBPFFile(sc4Files[0]);
		}



		/// <summary>
		/// Quits the application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Quit_Click(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}

		private void Refresh_Click(object sender, RoutedEventArgs e) {
			PopulateFolderListbox(activeDirectoryPath);
		}

		private void Preferences_Click(object sender, RoutedEventArgs e) {
			Preferences p = new Preferences();
			p.Show();
		}
	}
}
