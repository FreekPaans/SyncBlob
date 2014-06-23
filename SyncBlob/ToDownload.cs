using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class ToDownload {
		private List<DownloadCommand> list;

		ToDownload(List<DownloadCommand> list) {
			// TODO: Complete member initialization
			this.list = list;
		}

		public static ToDownload Empty {
			get {
				return new ToDownload(new List<DownloadCommand>());
			}
		}

		internal void Add(DownloadCommand downloadable) {
			list.Add(downloadable);
		}

		internal void Download() {
			var total = FileSize.Sum(list.Select(l=>l.Size));
			Console.WriteLine("Downloading {0} items, total size: {1}", list.Count, total);

			var downloaded = FileSize.Zero;

			foreach(var file in list) {
				Console.WriteLine("{0}/{1} {2} {3}", downloaded, total,file.Size,file.LocalPath);
				file.Execute();
				downloaded = downloaded.Add(file.Size);
			}
		}
	}
}
