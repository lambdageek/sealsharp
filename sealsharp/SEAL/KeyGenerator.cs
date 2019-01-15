using System;

namespace SEAL {
	public class KeyGenerator {
		internal Internal.KeyGenerator handle;

		public KeyGenerator (SEALContext context)
		{
			handle = Internal.KeyGenerator.Create (context.handle);
		}
		       
		public PublicKey PublicKey 
		{
			get {
				return new PublicKey (handle.GetPublicKey ());
			}
		}

		public SecretKey SecretKey 
		{
			get {
				return new SecretKey (handle.GetSecretKey ());
			}
		}

		public RelinKeys RelinKeys (int decomp_bit_count) 
		{	
			return new RelinKeys (handle.GetRelinKeys (decomp_bit_count));
		}
	}
}
