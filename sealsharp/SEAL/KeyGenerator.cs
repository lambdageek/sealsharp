using System;

namespace SEAL {
	public class KeyGenerator {
		internal Internal.KeyGenerator handle;

		public KeyGenerator (SEALContext context)
		{
			handle = Internal.KeyGenerator.Create (context.handle);
		}
		       
	}
}
