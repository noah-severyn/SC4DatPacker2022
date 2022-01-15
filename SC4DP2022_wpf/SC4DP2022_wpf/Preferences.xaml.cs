using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SC4DP2022_wpf {
	/// <summary>
	/// Interaction logic for Preferences.xaml
	/// </summary>
	public partial class Preferences : Window {
		public Preferences() {
			InitializeComponent();
			this.DefaultSourceDirectory.Text = Properties.Settings.Default.DefaultSourceDirectory;
			this.DefaultDestinationDirectory.Text = Properties.Settings.Default.DefaultSourceDirectory;
			this.DefaultWindowSize.Text = Properties.Settings.Default.DefaultWindowDimensions;
			this.RecurseIntoSubfolders.IsChecked = Properties.Settings.Default.DefaultPackMethod;
		}

		private void Preferences_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			Properties.Settings.Default.DefaultSourceDirectory = DefaultSourceDirectory.Text;
			Properties.Settings.Default.DefaultSourceDirectory = DefaultDestinationDirectory.Text;
			Properties.Settings.Default.DefaultWindowDimensions = DefaultWindowSize.Text;
			Properties.Settings.Default.DefaultPackMethod = (bool) RecurseIntoSubfolders.IsChecked;
			Properties.Settings.Default.Save();
		}
	}
}
