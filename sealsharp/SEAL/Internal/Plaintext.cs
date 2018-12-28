using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class Plaintext : SafeHandle {
		/* called by P/Invoke when returning a Plaintext */
		private Plaintext () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_Plaintext_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Plaintext_destroy (IntPtr handle);
	}
}
