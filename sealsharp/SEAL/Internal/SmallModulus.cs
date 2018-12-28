using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class SmallModulus : SafeHandle {
		/* called by P/Invoke when returning a SmallModulus */
		private SmallModulus () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_SmallModulus_destroy (handle);
			return true;
		}

		public static SmallModulus Make (ulong value)
		{
			return SEAL_SmallModulus_construct (value);
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_SmallModulus_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern SmallModulus SEAL_SmallModulus_construct (ulong value);
	}
}
