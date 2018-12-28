using System;

namespace SEAL {
	public abstract class AbstractIntegerEncoder {
		// TODO: maybe AbstractIntegerEncoder could be an interface
		
		internal abstract Internal.AbstractIntegerEncoder Handle { get; }

		protected AbstractIntegerEncoder () {}
	}
}
