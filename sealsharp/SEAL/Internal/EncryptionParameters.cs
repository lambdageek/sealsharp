using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {

	class EncryptionParameters : SafeHandle {
		/* called by P/Invoke when returning an EncryptionParameters */
		private EncryptionParameters () : base (IntPtr.Zero, true) {}

		public static EncryptionParameters MakeBFV ()
		{
			return SEAL_EncryptionParameters_construct_BFV ();
		}

		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_EncryptionParameters_destroy (handle);
			return true;
		}

		public void SetPolyModulusDegree (uint degree)
		{
			SEAL_EncryptionParameters_set_poly_modulus_degree (this, degree);
		}

		public void SetCoeffModulus (CoeffModulus coeff_modulus)
		{
			SEAL_EncryptionParameters_set_coeff_modulus (this, coeff_modulus);
		}


		public void SetPlainModulus (ulong small_modulus)
		{
			SEAL_EncryptionParameters_set_plain_modulus (this, small_modulus);
		}

		[DllImport (SEALC.Lib)]
		private static extern EncryptionParameters SEAL_EncryptionParameters_construct_BFV ();

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_EncryptionParameters_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_EncryptionParameters_set_poly_modulus_degree (EncryptionParameters parms, uint degree);

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_EncryptionParameters_set_coeff_modulus (EncryptionParameters parms, CoeffModulus coeff_modulus);

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_EncryptionParameters_set_plain_modulus (EncryptionParameters parms, ulong small_modulus);

	}
}
