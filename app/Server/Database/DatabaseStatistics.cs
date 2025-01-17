using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DHT.Server.Database {
	public class DatabaseStatistics : INotifyPropertyChanged {
		private long totalServers;
		private long totalChannels;
		private long totalUsers;
		private long totalMessages;

		public long TotalServers {
			get => totalServers;
			internal set => Change(out totalServers, value);
		}

		public long TotalChannels {
			get => totalChannels;
			internal set => Change(out totalChannels, value);
		}

		public long TotalUsers {
			get => totalUsers;
			internal set => Change(out totalUsers, value);
		}

		public long TotalMessages {
			get => totalMessages;
			internal set => Change(out totalMessages, value);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void Change<T>(out T field, T value, [CallerMemberName] string? propertyName = null) {
			field = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public DatabaseStatistics Clone() {
			return new DatabaseStatistics {
				totalServers = totalServers,
				totalChannels = totalChannels,
				totalUsers = TotalUsers,
				totalMessages = totalMessages
			};
		}
	}
}
