namespace MediaFilesLoader {
	public enum LogEvent {
		Info = 1,
		DbConectionError = 2,
		DbReadError = 3,
		DbWriteError = 4,
		ServerConnectionError = 5,
		UploadToServerError = 6,
		UnhandledUploadError = 7,
		DownloadFileError = 8,
		AmazonUploadServerError = 9,
		OtherAmazonUploadError = 10,
		AWSClientInitializeError = 11,
		AppConfiguratuonError = 12
	}
}