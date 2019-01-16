using System;

namespace SEAL {
	public class Ciphertext {
		internal Internal.Ciphertext handle;

		internal Ciphertext (Internal.Ciphertext h)
		{
			handle = h;
		}

		public Ciphertext (SEALContext context)
		{
			handle = Internal.Ciphertext.Create (context.handle);
		}

		public long Size ()
		{
			return handle.Size ();
		}
	}
}
