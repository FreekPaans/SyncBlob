using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class FileDestination : Destination{
		private string destination;

		public FileDestination(string destination) {
			// TODO: Complete member initialization
			this.destination = destination;
		}

		public void Execute(Action<System.IO.Stream> _downloadToStream) {
			AssertDirectoryAvailable();
			using(var stream = new FileStream(destination,FileMode.CreateNew,FileAccess.Write)) {
				_downloadToStream(stream);
			}
		}

		private void AssertDirectoryAvailable() {
			var dir = Path.GetDirectoryName(destination);
			if(Directory.Exists(dir)) {
				return;
			}

			Directory.CreateDirectory(dir);
		}
	}
}
