using System;

namespace SEAL {
	public class SecretKey {
		internal Internal.SecretKey handle;

		internal SecretKey (Internal.SecretKey h) { handle = h; }

	}
}
