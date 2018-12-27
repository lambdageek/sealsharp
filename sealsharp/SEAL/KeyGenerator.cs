using System;

namespace SEAL {
	public class KeyGenerator {
		internal Internal.KeyGenerator handle;

		private KeyGenerator (Internal.KeyGenerator h)
		{
			handle = h;
		}

		public static KeyGenerator Create (SEALContext context)
		{
			return new KeyGenerator (Internal.KeyGenerator.Create (context.handle));
		}
		       
	}
}
