using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class MemoryPoolHandle : SafeHandle {
		/* called by P/Invoke when returning a MemoryPoolHandle */
		private MemoryPoolHandle () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_MemoryPoolHandle_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_MemoryPoolHandle_destroy (IntPtr handle);
	}
}
