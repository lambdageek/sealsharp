using System;
using System.Runtime.InteropServices;

namespace SEAL.Internal {
	abstract class AbstractIntegerEncoder : SafeHandle {
		protected AbstractIntegerEncoder () : base (IntPtr.Zero, true) {}
		public override bool IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		protected override bool ReleaseHandle ()
		{
			SEAL_AbstractIntegerEncoder_destroy (handle);
			return true;
		}

		public Plaintext EncodeLong (long l)
		{
			return SEAL_AbstractIntegerEncoder_encode_int64 (this, l);
		}

        public Plaintext EncodeInt (int l)
        {
            return SEAL_AbstractIntegerEncoder_encode_int32(this, l);
        }

        public long DecodeLong (Plaintext plaintext)
		{
			return SEAL_AbstractIntegerEncoder_decode_int64 (this, plaintext);
		}

		[DllImport (SEALC.Lib)]
		private static extern void SEAL_AbstractIntegerEncoder_destroy (IntPtr handle);

		[DllImport (SEALC.Lib)]
		private static extern Plaintext SEAL_AbstractIntegerEncoder_encode_int64 (AbstractIntegerEncoder encoder, long l);

        [DllImport (SEALC.Lib)]
        private static extern Plaintext SEAL_AbstractIntegerEncoder_encode_int32(AbstractIntegerEncoder encoder, int l);

        [DllImport (SEALC.Lib)]
		private static extern long SEAL_AbstractIntegerEncoder_decode_int64 (AbstractIntegerEncoder encoder, Plaintext plaintext);

	}
}
