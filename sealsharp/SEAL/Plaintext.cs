using System;

namespace SEAL {
	public class Plaintext {
		internal Internal.Plaintext handle;

		internal Plaintext (Internal.Plaintext h)
		{
			handle = h;
		}
	}
}
