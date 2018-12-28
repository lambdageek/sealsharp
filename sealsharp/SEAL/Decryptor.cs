using System;

namespace SEAL {
	public class Decryptor {
		internal Internal.Decryptor handle;

		public Decryptor (SEALContext context, SecretKey secret_key)
		{
			handle = Internal.Decryptor.Make (context.handle, secret_key.handle);
		}

		public bool Decrypt (Ciphertext ciphertext, out Plaintext result)
		{
			bool success = handle.Decrypt (ciphertext.handle, out Internal.Plaintext result_h);
			result = success ? new Plaintext (result_h) : null;
			return success;
		}

	}
}
