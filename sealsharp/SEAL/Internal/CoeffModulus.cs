using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {

	class CoeffModulus : SafeHandle {
		/* called by P/Invoke when returning a CoeffModulus */
		private CoeffModulus () : base (IntPtr.Zero, true) {}

		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_SEALCoeffModulus_destroy (handle);
			return true;
		}

		public static CoeffModulus CoeffModulus128 (uint degree)
		{
			switch (degree) {
				case 1024:
				case 2048:
				case 4096:
				case 8192:
				case 16384:
				case 32768:
					break;
				default:
					throw new ArgumentException ("degree", "must be 1024, 2048, 4096, 8192, 16384 or 32768");
			}
			return SEAL_coeff_modulus_128 (degree);
				
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_SEALCoeffModulus_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern CoeffModulus SEAL_coeff_modulus_128 (uint degree);
			
	}
}
