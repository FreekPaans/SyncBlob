using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class BlobDestination : Destination{
		private Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob cloudBlockBlob;
		public BlobDestination(Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob cloudBlockBlob) {
			this.cloudBlockBlob = cloudBlockBlob;
		}
		public void Execute(Action<System.IO.Stream> downloadToStream) {
			using(var memStream = new MemoryStream()) {
				downloadToStream(memStream);
				memStream.Position = 0;
				cloudBlockBlob.UploadFromStream(memStream);
			}
			
		}
	}
}
