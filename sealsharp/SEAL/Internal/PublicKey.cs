using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class PublicKey : SafeHandle {
		/* called by P/Invoke when returning a PublicKey */
		private PublicKey () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_PublicKey_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_PublicKey_destroy (IntPtr handle);

	}

}
