using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class ToBlobStorageSyncStrategy : SyncStrategy{
		public void Sync(SyncBlobOptions options) {
			var isAlternativeDestinationPathProvided = !String.IsNullOrEmpty(options.BlobDestinationPath);

			var destinationPath = options.OnDiskPath;
			if (isAlternativeDestinationPathProvided)
			{
				destinationPath = options.BlobDestinationPath;
			}

			DirectoryHelper.AssertDirectoryAvailable(options.OnDiskPath);

			var localFiles = Files.EnumerateLocalFiles(options.OnDiskPath);
			var remoteFiles = Blobs.EnumerateBlobs(options.BlobStorageAccountConnectionsString,options.ContainerName);

			remoteFiles.SyncNotAvailable(localFiles, destinationPath);
		}
	}
}
