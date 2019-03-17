using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MediaFilesLoader {
	internal sealed class ScheduleWorker {
		private readonly ILogger _logger;
		private readonly IDbProvider _dbProvider;
		private readonly IWebClient _webClient;

		public ScheduleWorker(ILogger logger, IDbProvider dbProvider, IWebClient webClient) {
			_logger = logger;
			_dbProvider = dbProvider;
			_webClient = webClient;
		}

		public void Run() {
			
			
			List<FileEntity> files = _dbProvider.GetFilesForUpload();
			if (!files.Any()) {
				_logger.LogInfo("No files were received for upload!");
				return;
			}
			Stopwatch sw = new Stopwatch();
			sw.Start();
			List<Task> tasks = new List<Task>();
			foreach (FileEntity file in files) {
				tasks.Add(Task.Run(async () => {
					byte[] fileContent = await _webClient.DownloadFileAsync(file.RecordUrl);
					file.RecordUrl2 = await _webClient.UploadFileAsync(fileContent, file.Name);
					_dbProvider.UpdateFileEntity(file);
				}));
			}

			try {
				Task.WaitAll(tasks.ToArray());
			}
			catch (AggregateException e) {
				e.Handle((ex) => {
					_logger.LogError($"Task unhandled error occured: {e.Message}", LogEvent.UnhandledUploadError);
					return false;
				});
			}
			finally {
				sw.Stop();
			}
			_logger.LogInfo($"Were uploaded {files.Count} files. Total time: {sw.ElapsedMilliseconds} ms.");
		}
	}
}