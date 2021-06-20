using System;
using DHT.Desktop.Models;
using DHT.Server.Database;

namespace DHT.Desktop.Main.Pages {
	public class AttachmentsPageModel : BaseModel, IDisposable {
		private readonly IDatabaseFile db;

		public AttachmentsPageModel() : this(DummyDatabaseFile.Instance) {}

		public AttachmentsPageModel(IDatabaseFile db) {
			this.db = db;
		}

		public void Dispose() {
			// TODO
			GC.SuppressFinalize(this);
		}
	}
}
