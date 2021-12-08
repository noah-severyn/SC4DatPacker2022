using System;
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
		public MainWindow() {
			InitializeComponent();

			// Set default folder to SC4 plugins on startup
			string user = Environment.UserName;
			txt_CurrentDirectory.Text = "C:\\Users\\" + user + "\\Documents\\SimCity 4\\Plugins";
		}

		private void btn_Browse_Click(object sender, RoutedEventArgs e) {
			string user = Environment.UserName;

			// Folder picker dialog box
			// https://stackoverflow.com/a/41511598
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.InitialDirectory = "C:\\Users\\" + user + "\\Documents\\SimCity 4\\Plugins";
			dialog.IsFolderPicker = true;
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
				MessageBox.Show("You selected: " + dialog.FileName);
				txt_CurrentDirectory.Text = dialog.FileName;
			}
		}
	}
}
