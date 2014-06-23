using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	interface Destination {
		 void Execute(Action<System.IO.Stream> _downloadToStream);
	}
}
