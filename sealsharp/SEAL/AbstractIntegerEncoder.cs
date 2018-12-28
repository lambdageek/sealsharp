using System;

namespace SEAL {
	public abstract class AbstractIntegerEncoder {
		// TODO: maybe AbstractIntegerEncoder could be an interface
		
		internal abstract Internal.AbstractIntegerEncoder Handle { get; }

		protected AbstractIntegerEncoder () {}

		public Plaintext Encode (long l)
		{
			return new Plaintext (Handle.EncodeLong (l));
		}

		public long DecodeLong (Plaintext plaintext)
		{
			return Handle.DecodeLong (plaintext.handle);
		}
	}
}
