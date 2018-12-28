using System;

namespace SEAL {
	public class Decryptor {
		internal Internal.Decryptor handle;

		public Decryptor (SEALContext context, SecretKey secret_key)
		{
			handle = Internal.Decryptor.Make (context.handle, secret_key.handle);
		}
	}
}
