using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	abstract class AbstractIntegerEncoder : SafeHandle {
		protected AbstractIntegerEncoder () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_AbstractIntegerEncoder_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_AbstractIntegerEncoder_destroy (IntPtr handle);

	}
}
