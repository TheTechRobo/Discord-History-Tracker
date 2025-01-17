using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using DHT.Desktop.Common;
using DHT.Desktop.Dialogs;
using DHT.Desktop.Models;
using DHT.Server.Database;

namespace DHT.Desktop.Main {
	public class WelcomeScreenModel : BaseModel {
		public string Version => Program.Version;

		public IDatabaseFile? Db { get; private set; }
		public bool HasDatabase => Db != null;

		private readonly Window window;

		private string? dbFilePath;

		[Obsolete("Designer")]
		public WelcomeScreenModel() : this(null!) {}

		public WelcomeScreenModel(Window window) {
			this.window = window;
		}

		public async void OpenOrCreateDatabase() {
			var dialog = DatabaseGui.NewOpenOrCreateDatabaseFileDialog();
			dialog.Directory = Path.GetDirectoryName(dbFilePath);

			string? path = await dialog.ShowAsync(window);
			if (!string.IsNullOrWhiteSpace(path)) {
				await OpenOrCreateDatabaseFromPath(path);
			}
		}

		public async Task OpenOrCreateDatabaseFromPath(string path) {
			if (Db != null) {
				Db = null;
			}

			dbFilePath = path;
			Db = await DatabaseGui.TryOpenOrCreateDatabaseFromPath(path, window, CheckCanUpgradeDatabase);

			OnPropertyChanged(nameof(Db));
			OnPropertyChanged(nameof(HasDatabase));
		}

		private async Task<bool> CheckCanUpgradeDatabase() {
			return DialogResult.YesNo.Yes == await DatabaseGui.ShowCanUpgradeDatabaseDialog(window);
		}

		public void CloseDatabase() {
			Db = null;

			OnPropertyChanged(nameof(Db));
			OnPropertyChanged(nameof(HasDatabase));
		}

		public async void ShowAboutDialog() {
			await new AboutWindow { DataContext = new AboutWindowModel() }.ShowDialog(this.window);
		}

		public void Exit() {
			window.Close();
		}
	}
}
