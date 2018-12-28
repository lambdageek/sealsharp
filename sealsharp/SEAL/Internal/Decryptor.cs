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

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Decryptor_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern Decryptor SEAL_Decryptor_construct (SEALSharedContext context, SecretKey secret_key);
	}
}
