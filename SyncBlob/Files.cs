using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class Files : SourceFiles{
		private HashSet<FilePath> _filePaths;
		private string _basePath;

		Files(string basePath, FilePath[] filePath) {
			// TODO: Complete member initialization
			this._filePaths = new HashSet<FilePath>(filePath);;
			_basePath = basePath;
		}
		
		public override string ToString() {
			var sb = new StringBuilder();
			foreach(var file in _filePaths) {
				sb.AppendLine(file.ToString());
			}
			return sb.ToString();
		}

		public static Files EnumerateLocalFiles(string path) {
			IEnumerable<FilePath> result  = Directory.GetFiles(path).Select(FilePath.FromStringPath);
			
			foreach(var dir in Directory.GetDirectories(path).Except(new [] { ".", ".."})) {
				result = result.Concat(EnumerateLocalFiles(dir)._filePaths);
			}

			return new Files(path, result.ToArray());
		}

		internal void SyncNotAvailable(SourceFiles remoteFiles) {
			var toDownload = ToDownload.Empty;
			foreach(var downloadable in remoteFiles) {
				var destination = Path.Combine(_basePath, downloadable.LocalPath.ToString());
				if(File.Exists(destination)) {
					continue;
				}
				

				toDownload.Add(new DownloadCommand(downloadable, new FileDestination(destination)));
			}

			

			toDownload.Download();
		}

		public IEnumerator<Downloadable> GetEnumerator() {
			return _filePaths.Select(f=>f.GetDownloadable(_basePath)).GetEnumerator();
		}

		

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
