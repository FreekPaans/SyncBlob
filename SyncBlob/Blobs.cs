using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class Blobs : SourceFiles{
		private string connectionString;
		private string containerName;
		private IListBlobItem[] _res;


		Blobs(string connectionString,string containerName,IListBlobItem[] res) {
			// TODO: Complete member initialization
			this.connectionString = connectionString;
			this.containerName = containerName;
			this._res = res;
		}
		internal static Blobs EnumerateBlobs(string connectionString,string containerName) {
			
			var container = GetContainer(connectionString, containerName);
			var res = container.ListBlobs(useFlatBlobListing:true).ToArray();

			return new Blobs(connectionString,containerName, res);
		}

		private static CloudBlobContainer GetContainer(string connectionString,string containerName) {
			return CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient().GetContainerReference(containerName);
		}

		public override string ToString() {
			var sb = new StringBuilder();
			foreach(var file in _res) {
				sb.AppendLine(file.ToString());
			}

			return sb.ToString();
		}

		public IEnumerator<Downloadable> GetEnumerator() {
			return _res.Select(GetDownloadableFromBlob).GetEnumerator();
		}

		private Downloadable GetDownloadableFromBlob(IListBlobItem arg) {
			var downloadable = (CloudBlockBlob)arg;

			return new Downloadable(stream=>DownloadToStream(arg,stream),RelativePath.FromBlob(arg), FileSize.FromBytes(downloadable.Properties.Length));
		}

		

		void DownloadToStream(IListBlobItem blob, Stream inStream)  {
			Console.WriteLine("Downloading {0}", blob.Uri);
			var container = GetContainer(connectionString,containerName);
			((CloudBlockBlob)blob).DownloadToStream(inStream);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		internal void SyncNotAvailable(SourceFiles sourceFiles)
		{
			SyncNotAvailable(sourceFiles, null);
		}

		internal void SyncNotAvailable(SourceFiles sourceFiles, string targetPath) {
			var toDownload = ToDownload.Empty;

			var blobFiles = new HashSet<RelativePath>(_res.Select(r=>RelativePath.FromBlob(r)));

			var container = GetContainer(connectionString,containerName);

			foreach(var downloadable in sourceFiles) {
				var destinationPath = PrependDestinationPath(downloadable.LocalPath, targetPath);
				if (blobFiles.Contains(destinationPath))
				{
					continue;
				}

				toDownload.Add(new DownloadCommand(downloadable, new BlobDestination(container.GetBlockBlobReference(destinationPath.ToString()))));
			}

			toDownload.Download();
		}

		private RelativePath PrependDestinationPath(RelativePath filePath, string destinationPath)
		{
			var isDestinationPathProvided = !String.IsNullOrEmpty(destinationPath);
			if (!isDestinationPathProvided)
			{
				return filePath;
			}

			return new RelativePath(String.Format("{0}\\{1}", destinationPath, filePath));
		}
	}
}
