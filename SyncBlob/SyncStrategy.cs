using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	interface SyncStrategy {
		void Sync(SyncBlobOptions options);
	}
}
