using System;

namespace SEAL {
	public class IntegerEncoder : AbstractIntegerEncoder {
		internal Internal.IntegerEncoder handle;

		override internal Internal.AbstractIntegerEncoder Handle {
			get { return handle; }
		}

		public IntegerEncoder (SmallModulus plain_modulus, ulong base_n = 2)
		{
			handle = Internal.IntegerEncoder.Make (plain_modulus.handle, base_n);
		}
	}
}
