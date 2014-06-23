using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class DownloadCommand {
		private Destination _destination;
		private Downloadable _toDownload;
		public DownloadCommand(Downloadable toDownload, Destination destination) {
			_destination= destination;
			_toDownload = toDownload;
		}

		public FileSize Size {
			get {
				return _toDownload.Size;
			}
		}

		public RelativePath LocalPath {
			get {
				return _toDownload.LocalPath;
			}
		}

		//public override string ToString() {
		//	return string.Format(" 
		//}

		internal void Execute() {
			_toDownload.DownloadTo(_destination);
		}
	}
}
