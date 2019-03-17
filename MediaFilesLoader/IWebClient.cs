using System.Threading.Tasks;

namespace MediaFilesLoader {
	public interface IWebClient {
		Task<byte[]> DownloadFileAsync(string url);
		Task<string> UploadFileAsync(byte[] file, string fileName);
	}
}