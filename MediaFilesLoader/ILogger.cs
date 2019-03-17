namespace MediaFilesLoader {
	public interface ILogger {
		void LogError(string message, LogEvent logEvent);

		void LogInfo(string message, LogEvent logEvent = LogEvent.Info);
	}
}