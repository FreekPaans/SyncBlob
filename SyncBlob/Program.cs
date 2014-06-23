using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class Program {
		readonly static Dictionary<DirectionEnum,SyncStrategy> _syncStrategies =new  Dictionary<DirectionEnum,SyncStrategy>();

		static Program() {

			_syncStrategies[DirectionEnum.FromBlobStorage] = new FromBlobStorageSyncStrategy();
			_syncStrategies[DirectionEnum.ToBlobStorage] = new ToBlobStorageSyncStrategy();
		}
		static int Main(string[] args) {
			var options = new SyncBlobOptions();
			var res = Parser.Default.ParseArguments(args,options);

			if(res==false) {
				//Console.WriteLine("Usage: {0}", options.GetUsage());
				return 1;
			}

			_syncStrategies[options.Direction].Sync(options);

			return 0;
		}
	}
}
