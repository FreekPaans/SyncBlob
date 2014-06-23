using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class FromBlobStorageSyncStrategy : SyncStrategy{
		public void Sync(SyncBlobOptions options) {
			DirectoryHelper. AssertDirectoryAvailable(options.OnDiskPath);

			var localFiles = Files.EnumerateLocalFiles(options.OnDiskPath);
			var remoteFiles = Blobs.EnumerateBlobs(options.BlobStorageAccountConnectionsString,options.ContainerName);

			localFiles.SyncNotAvailable(remoteFiles);

			//Console.WriteLine("{0}", localFiles);
			//Console.WriteLine("{0}", remoteFiles);
		}
	}
}
