using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class SEALSharedContext : SafeHandle {
		/* called by P/Invoke when returning a SEALSharedContext  */
		private SEALSharedContext () : base (IntPtr.Zero, true) {}

		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_SEALSharedContext_destroy (handle);
			return true;
		}

		public static SEALSharedContext Create (EncryptionParameters parms, bool expand_mod_chain)
		{
			return SEAL_Context_Create (parms, expand_mod_chain ? 1 : 0);
		}


		[DllImport (SEALC.Lib)]
		private static extern void SEAL_SEALSharedContext_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern SEALSharedContext SEAL_Context_Create (EncryptionParameters parms, int expand_mod_chain);

	}
}
