using System;

namespace SEAL {
	public class SEALContext {
		internal Internal.SEALSharedContext handle;

		private SEALContext (Internal.SEALSharedContext h)
		{
			handle = h;
		}

		public static SEALContext Create (EncryptionParameters parms, bool expand_mod_chain = true)
		{
			return new SEALContext (Internal.SEALSharedContext.Create (parms.handle, expand_mod_chain));
		}
	}
}
