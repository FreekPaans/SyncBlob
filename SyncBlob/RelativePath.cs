using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class RelativePath {
		private string rel;

		public RelativePath(string inp) {
			rel= string.Join("/", inp.Replace('\\', '/').Split(new []{ '/'}, StringSplitOptions.RemoveEmptyEntries));
		}
		internal static RelativePath FromBlob(Microsoft.WindowsAzure.Storage.Blob.IListBlobItem arg) {
			var rel = string.Join("/", arg.Uri.LocalPath.Split(new [] {'/'}, StringSplitOptions.RemoveEmptyEntries).Skip(1));
			return new RelativePath(rel);
		}

		public override string ToString() {
			return rel;
		}

		public override bool Equals(object obj) {
			if(!(obj is RelativePath)) {
				return false;
			}

			return rel == ((RelativePath)obj).rel;
		}

		public override int GetHashCode() {
			return rel.GetHashCode();
		}

		internal static RelativePath FromFile(string arg,string basePath) {
			return new RelativePath(arg.Substring(basePath.Length));
		}
		
	}
}
