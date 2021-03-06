using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class Decryptor : SafeHandle {
		/* called by P/Invoke when returning an Decryptor */
		private Decryptor () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_Decryptor_destroy (handle);
			return true;
		}

		public static Decryptor Make (SEALSharedContext context, SecretKey secret_key)
		{
			return SEAL_Decryptor_construct (context, secret_key);
		}

		public bool Decrypt (Ciphertext ciphertext, out Plaintext result)
		{
			result = SEAL_Decryptor_decrypt_new (this, ciphertext, out int success);
			return success != 0;
		}

		public int InvariantNoiseBudget (Ciphertext ciphertext)
		{
			return SEAL_Decryptor_invariant_noise_budget (this, ciphertext);
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Decryptor_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern Decryptor SEAL_Decryptor_construct (SEALSharedContext context, SecretKey secret_key);

		[DllImport (SEALC.Lib)]
		private static extern Plaintext SEAL_Decryptor_decrypt_new (Decryptor decryptor, Ciphertext ciphertext, out int success);

		[DllImport (SEALC.Lib)]
		private static extern int SEAL_Decryptor_invariant_noise_budget (Decryptor decryptor, Ciphertext ciphertext);
	}
}
