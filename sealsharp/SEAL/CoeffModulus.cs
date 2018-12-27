using System;

namespace SEAL {
	public class CoeffModulus {
		internal Internal.CoeffModulus handle;

		private CoeffModulus (Internal.CoeffModulus h)
		{
			handle = h;
		}

		public static CoeffModulus CoeffModulus128 (uint degree)
		{
			return new CoeffModulus (Internal.CoeffModulus.CoeffModulus128 (degree));
		}
	}
}
