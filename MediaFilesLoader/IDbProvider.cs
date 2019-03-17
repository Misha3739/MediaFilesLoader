using System.Collections.Generic;

namespace MediaFilesLoader {
	public interface IDbProvider {
		List<FileEntity> GetFilesForUpload();

		bool UpdateFileEntity(FileEntity entity);
	}
}