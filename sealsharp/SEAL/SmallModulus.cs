using System;

namespace SEAL {
	public class SmallModulus {
		internal Internal.SmallModulus handle;

		public SmallModulus (ulong value = 0) : this (Internal.SmallModulus.Make (value)) {}
		
		internal SmallModulus (Internal.SmallModulus h)
		{
			handle = h;
		}
	}
}
