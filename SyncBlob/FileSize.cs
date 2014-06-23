using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class FileSize {
		private long p;

		FileSize(long bytes) {
			// TODO: Complete member initialization
			this.p = bytes;
		}
		internal static FileSize Sum(IEnumerable<FileSize> enumerable) {
			return new FileSize(enumerable.Sum(f=>f.p));
		}

		public override string ToString() {
			return string.Format("{0:F2}MB", p/(1024.0*1024));
		}

		public static FileSize Zero {
			get {
				return new FileSize(0);
			}
		}

		internal static FileSize FromBytes(long p) {
			return new FileSize(p);
		}

		internal FileSize Add(FileSize fileSize) {
			return new FileSize(p+fileSize.p);
		}
	}
}
