using System;

namespace SEAL {
	public class RelinKeys {
		internal Internal.RelinKeys handle;

		internal RelinKeys (Internal.RelinKeys h)
		{
			handle = h;
		}
	}
}
