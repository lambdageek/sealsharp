using System;

namespace SEAL {
	public class Ciphertext {
		internal Internal.Ciphertext handle;

		internal Ciphertext (Internal.Ciphertext h)
		{
			handle = h;
		}
	}
}
