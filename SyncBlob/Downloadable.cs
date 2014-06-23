using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class Downloadable {
		readonly Action<Stream> _downloadToStream;
		private RelativePath _localPath;
		private FileSize _size;
		public Downloadable(Action<Stream> downloadToStream, RelativePath localPath, FileSize size) {
			_downloadToStream = downloadToStream;
			_localPath = localPath;
			_size = size;
		}
		public void DownloadTo(Stream dest) {
			_downloadToStream(dest);
		}
		
		public RelativePath LocalPath {
			get {
				return _localPath;
			}
		}

		public FileSize Size {
			get {
				return _size;
			}
		}

		internal void DownloadTo(Destination destination) {
			destination.Execute(_downloadToStream);
			
		}

		
	}
}
