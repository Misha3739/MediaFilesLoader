using System.Configuration;

namespace MediaFilesLoader {
	internal sealed class Configuration : IConfiguration {
		public string this[string index] => ConfigurationManager.AppSettings[index];
	}
}