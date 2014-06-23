using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	struct FilePath {
		private string arg;

		public FilePath(string arg) {
			// TODO: Complete member initialization
			this.arg = arg;
		}
		internal static FilePath FromStringPath(string arg) {
			return new FilePath(arg);
		}

		public override string ToString() {
			return arg.ToString();
		}

		public override bool Equals(object obj) {
			if(!(obj is FilePath)) {
				return false;
			}

			return arg == ((FilePath)obj).arg;
		}

		public override int GetHashCode() {
			return arg.GetHashCode();
		}

		internal Downloadable GetDownloadable(string basePath) {
			return new Downloadable(DownloadToStream, RelativePath.FromFile(arg,basePath), FileSize.FromBytes(new FileInfo(arg).Length));
		}

		private void DownloadToStream(Stream obj) {
			using(var fs=  new FileStream(arg, FileMode.Open,FileAccess.Read)) {
				fs.CopyTo(obj);
			}
		}
	}
}
