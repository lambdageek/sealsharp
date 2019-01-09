using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class Evaluator : SafeHandle {
		/* called by P/Invoke when returning a Evaluator */
		private Evaluator () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_Evaluator_destroy (handle);
			return true;
		}

		public static Evaluator Create (SEALSharedContext context)
		{
			return SEAL_Evaluator_construct (context);
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_Evaluator_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern Evaluator SEAL_Evaluator_construct (SEALSharedContext context);
	}
}