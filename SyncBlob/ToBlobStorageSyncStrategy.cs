using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class ToBlobStorageSyncStrategy : SyncStrategy{
		public void Sync(SyncBlobOptions options) {
			DirectoryHelper.AssertDirectoryAvailable(options.OnDiskPath);

			var localFiles = Files.EnumerateLocalFiles(options.OnDiskPath);
			var remoteFiles = Blobs.EnumerateBlobs(options.BlobStorageAccountConnectionsString,options.ContainerName);

			remoteFiles.SyncNotAvailable(localFiles);
		}
	}
}
