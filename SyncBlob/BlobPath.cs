using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class BlobPath {
		private string p;

		BlobPath(string p) {
			// TODO: Complete member initialization
			this.p = p;
		}
		internal static BlobPath FromBlob(Microsoft.WindowsAzure.Storage.Blob.IListBlobItem arg) {
			return new BlobPath(arg.Uri.LocalPath);
		}

		public override string ToString() {
			return p;
		}
	}
}
