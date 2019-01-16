using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class Ciphertext : SafeHandle {
		/* called by P/Invoke when returning a Ciphertext */
		private Ciphertext () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_Ciphertext_destroy (handle);
			return true;
		}

		public static Ciphertext Create (SEALSharedContext context)
		{
			return SEAL_Ciphertext_construct (context);
		}

		public long Size()
		{
			return SEAL_Ciphertext_size (this);
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Ciphertext_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern Ciphertext SEAL_Ciphertext_construct (SEALSharedContext context);

		[DllImport (SEALC.Lib)]
		private static extern long SEAL_Ciphertext_size (Ciphertext ciphertext);
	}
}
