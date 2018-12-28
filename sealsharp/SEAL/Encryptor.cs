using System;

namespace SEAL {
	public class Encryptor {
		internal Internal.Encryptor handle;

		public Encryptor (SEALContext context, PublicKey public_key)
		{
			handle = Internal.Encryptor.Make (context.handle, public_key.handle);
		}

		public bool Encrypt (Plaintext plaintext, out Ciphertext result)
		{
			bool success = handle.Encrypt (plaintext.handle, out Internal.Ciphertext result_h);
			result = success ? new Ciphertext (result_h) : null;
			return success;
		}
			
	}
}
