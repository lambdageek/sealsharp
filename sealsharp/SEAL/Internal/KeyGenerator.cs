using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class KeyGenerator : SafeHandle {
		/* called by P/Invoke when returning a KeyGenerator */
		private KeyGenerator () : base (IntPtr.Zero, true) {}

		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_KeyGenerator_destroy (handle);
			return true;
		}
		
		public static KeyGenerator Create (SEALSharedContext context)
		{
			return SEAL_KeyGenerator_construct (context);
		}

		public PublicKey GetPublicKey ()
		{
			return SEAL_KeyGenerator_get_public_key (this);
		}

		public SecretKey GetSecretKey ()
		{
			return SEAL_KeyGenerator_get_secret_key (this);
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_KeyGenerator_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern KeyGenerator SEAL_KeyGenerator_construct (SEALSharedContext context);

		[DllImport (SEALC.Lib)]
		private static extern PublicKey SEAL_KeyGenerator_get_public_key (KeyGenerator keygen);

		[DllImport (SEALC.Lib)]
		private static extern SecretKey SEAL_KeyGenerator_get_secret_key (KeyGenerator keygen);
		
	}
}
