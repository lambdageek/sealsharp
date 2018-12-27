
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

		public ulong PlainModulus {
			set {
				handle.SetPlainModulus (value);
			}
		}
	}
}
