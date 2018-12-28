using System;

namespace SEAL {
	public class PublicKey {
		internal Internal.PublicKey handle;

		internal PublicKey (Internal.PublicKey h) { handle = h; }

	}
}
