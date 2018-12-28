using System;

namespace SEAL {
	public class Encryptor {
		internal Internal.Encryptor handle;

		public Encryptor (SEALContext context, PublicKey public_key)
		{
			handle = Internal.Encryptor.Make (context.handle, public_key.handle);
		}
	}
}
