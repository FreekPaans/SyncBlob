using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncBlob {
	class SyncBlobOptions {
		[Option("storageConnectionString", Required=true)]
		public string BlobStorageAccountConnectionsString {get;set;}
		[Option("containerName", Required=true)]
		public string ContainerName{get;set;}
		[Option("diskLocation", Required=true)]
		public string OnDiskPath{get;set;}

		[Option("direction", HelpText="ToBlobStorage or FromBlobStorage", Required=true)]
		public DirectionEnum Direction{get;set;}

		[HelpOption]
		public string GetUsage() {
			return HelpText.AutoBuild(this,
			(HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
		}

		public override string ToString() {
			return string.Format("StorageConnectionString: {0}\nContainerName: {1}\nOnDiskPath: {2}\nDirection: {3}", BlobStorageAccountConnectionsString,ContainerName,OnDiskPath,Direction);
		}
	}
}
