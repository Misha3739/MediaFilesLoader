using Amazon;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace MediaFilesLoader {
	public class AmazonWebClient : IWebClient {
		private readonly ILogger _logger;

		private readonly string _bucketName;

		private readonly AWSCredentials _credentials;
		private readonly AmazonS3Config _config;

		public AmazonWebClient(ILogger logger, IConfiguration configuration) {
			_logger = logger;
			try {
				_bucketName = configuration["bucket"];
				_credentials = new BasicAWSCredentials(configuration["accessKey"], configuration["secretKey"]);
			}
			catch (Exception e) {
				_logger.LogError($"Configuration exception occured: {e.Message}", LogEvent.AppConfiguratuonError);
			}

			_config = new AmazonS3Config {
				RegionEndpoint = RegionEndpoint.USEast1
			};
		}

		public async Task<byte[]> DownloadFileAsync(string url) {
			using (WebClient client = new WebClient()) {
				try {
					byte[] fileData = await client.DownloadDataTaskAsync(url);
					//_logger.LogInfo($"File \"{url}\" successfully downloaded");
					return fileData;
				}
				catch (WebException e) {
					_logger.LogError($"File downloading error: {e.Message}", LogEvent.DownloadFileError);
				}

				return null;
			}
		}

		public async Task<string> UploadFileAsync(byte[] file, string fileName) {
			try {
				using (IAmazonS3 s3Client = new AmazonS3Client(_credentials, _config)) {
					try {
						var fileTransferUtility = new TransferUtility(s3Client);

						using (var fileToUpload = new MemoryStream(file)) {
							await fileTransferUtility.UploadAsync(fileToUpload,
								_bucketName, fileName);
						}

						string filePath = $"{_bucketName}/{fileName}";
						//_logger.LogInfo($"File uploaded to {_bucketName}/{fileName}");
						return filePath;
					}
					catch (AmazonS3Exception e) {
						_logger.LogError($"Error encountered on server. Message:'{e.Message}' when writing an object",
							LogEvent.AmazonUploadServerError);
					}
					catch (Exception e) {
						_logger.LogError($"Unknown encountered on server. Message:'{e.Message}' when writing an object",
							LogEvent.OtherAmazonUploadError);
					}
				}
			}
			catch (Exception e) {
				_logger.LogError($"AWS Instance client error. Message:'{e.Message}'",
					LogEvent.AWSClientInitializeError);
			}

			return null;
		}
	}
}