using System.Diagnostics;

namespace MediaFilesLoader {
	class Program {
		static void Main(string[] args) {
			ILogger logger = new Logger();
			IConfiguration configuration = new Configuration();
			IDbProvider provider = new DummyDbProvider();
			IWebClient client = new AmazonWebClient(logger, configuration);
			ScheduleWorker worker = new ScheduleWorker(logger, provider, client);
			worker.Run();
		}
	}
}