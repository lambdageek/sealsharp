
using System;

namespace SEAL {
	public class EncryptionParameters {
		public enum Scheme {
			BFV
		}

		private Internal.EncryptionParameters handle;

		public EncryptionParameters (Scheme scheme) {
			switch (scheme) {
				case Scheme.BFV:
					handle = Internal.EncryptionParameters.MakeBFV ();
					break;
			}
		}
	}
}
