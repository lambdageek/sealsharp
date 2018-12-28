using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class SecretKey : SafeHandle {
		/* called by P/Invoke when returning a SecretKey */
		private SecretKey () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_SecretKey_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_SecretKey_destroy (IntPtr handle);

	}

}
