using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {

	class EncryptionParameters : SafeHandle {
		public EncryptionParameters () : base (IntPtr.Zero, false) {}

		private EncryptionParameters (IntPtr h) : base (h, true) {}

		public static EncryptionParameters MakeBFV () {
			return new EncryptionParameters (SEAL_EncryptionParameters_construct_BFV ());
		}

		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle () {
			SEAL_EncryptionParameters_destroy (handle);
			return true;
		}

		[DllImport (SEALC.Lib)]
		private static extern IntPtr SEAL_EncryptionParameters_construct_BFV ();

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_EncryptionParameters_destroy (IntPtr parms);

	}
}
