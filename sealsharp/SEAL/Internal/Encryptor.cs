using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class Encryptor : SafeHandle {
		/* called by P/Invoke when returning an Encryptor */
		private Encryptor () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_Encryptor_destroy (handle);
			return true;
		}

		public static Encryptor Make (SEALSharedContext context, PublicKey public_key)
		{
			return SEAL_Encryptor_construct (context, public_key);
		}

		public bool Encrypt (Plaintext plaintext, out Ciphertext result)
		{
			result = SEAL_Encryptor_encrypt_new (this, plaintext, out int success);
			return success != 0;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Encryptor_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern Encryptor SEAL_Encryptor_construct (SEALSharedContext context, PublicKey public_key);

		[DllImport (SEALC.Lib)]
		private static extern Ciphertext SEAL_Encryptor_encrypt_new (Encryptor encryptor, Plaintext plaintext, out int success);
	}
}
