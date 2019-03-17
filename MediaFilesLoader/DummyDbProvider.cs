using System.Collections.Generic;
using System.Linq;

namespace MediaFilesLoader {
	internal class DummyDbProvider : IDbProvider {
		public List<FileEntity> GetFilesForUpload() {
			List<FileEntity> result = new List<FileEntity>();
			for (int i = 0; i < 30; i++) {
				result.Add(new FileEntity {
					Id = 1,
					Name = $"record{i+1}.mp3",
					RecordUrl =
						"http://www-ru-23-18.voximplant.com/records/2019/01/26/3tRGkaPaSliqYAtMOrmSK1n7JnY87EB3t9j7MLTLaeM.mp3?record_id=24997860"
				});
			}
			return result;
		}

		public bool UpdateFileEntity(FileEntity entity) {
			return true;
		}
	}
}