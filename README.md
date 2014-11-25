SyncBlob
========

Sync files to and from azure blob storage. Usage:

`SyncBlob.exe --direction (ToBlobStorage|FromBlobStorage) --diskLocation <localpath> --containerName <blobstorage container name> --storageConnectionString <storage connection string> [--blobDestinationPath <relative blob path>]`

 * `blobDestinationPath` can optionally be used to indicate a target directory where the files will be synced to when `direction` is set to `ToBlobStorage`
