using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	class IntegerEncoder : AbstractIntegerEncoder {
		/* called by P/Invoke when returning an IntegerEncoder */
		private IntegerEncoder () : base () {}

		public static IntegerEncoder Make (SmallModulus plain_modulus, ulong base_n)
		{
			return SEAL_IntegerEncoder_construct (plain_modulus, base_n);
		}

		[DllImport (SEALC.Lib)]
		private static extern IntegerEncoder SEAL_IntegerEncoder_construct (SmallModulus plain_modulus, ulong base_n);
	}
}
