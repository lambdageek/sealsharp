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

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Ciphertext_destroy (IntPtr handle);
	}
}
