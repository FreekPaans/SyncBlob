using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class DirectoryHelper {
		internal static void AssertDirectoryAvailable(string path) {
			if(Directory.Exists(path)) {
				return;
			}
			Directory.CreateDirectory(path);
		}
	}
}
