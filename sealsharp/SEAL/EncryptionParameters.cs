
using System;

namespace SEAL {
	public class EncryptionParameters {
		public enum Scheme {
			BFV
		}

		internal Internal.EncryptionParameters handle;

		public EncryptionParameters (Scheme scheme) {
			switch (scheme) {
				case Scheme.BFV:
					handle = Internal.EncryptionParameters.MakeBFV ();
					break;
			}
		}

		public uint PolyModulusDegree {
			set {
				handle.SetPolyModulusDegree (value);
			}
		}

		public CoeffModulus CoeffModulus {
			set {
				handle.SetCoeffModulus (value.handle);
			}
		}

		public SmallModulus PlainModulus {
			set {
				handle.SetPlainModulus (value.handle);
			}
		}
	}
}
