using System.Diagnostics;
using System.Reflection;

namespace MediaFilesLoader {
	internal class Logger : ILogger {
		private readonly string _appSource;

		public Logger() {
			_appSource = Assembly.GetExecutingAssembly().GetName().Name;
		}

		public void LogError(string message, LogEvent logEvent) {
			try {
				EventLog.WriteEntry(_appSource, message, EventLogEntryType.Error, (int) logEvent);
			}
			catch {
			}
		}

		public void LogInfo(string message, LogEvent logEvent = LogEvent.Info) {
			try {
				EventLog.WriteEntry(_appSource, message, EventLogEntryType.Information, (int) logEvent);
			}
			catch {
			}
		}
	}
}