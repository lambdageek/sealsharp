using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class RelinKeys : SafeHandle {
		/* called by P/Invoke when returning a RelinKeys */
		private RelinKeys () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_RelinKeys_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_RelinKeys_destroy (IntPtr handle);
	}
}