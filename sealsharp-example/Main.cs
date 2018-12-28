using System;

using SEAL;

namespace Example {
	public class Example {
		public static void Main()
		{
			example_bfv_basics_i ();
		}


		public static void example_bfv_basics_i ()
		{
			var parms = new EncryptionParameters (EncryptionParameters.Scheme.BFV);
			parms.PolyModulusDegree = 2048;
			parms.PlainModulus = new SmallModulus (1 << 8);
			parms.CoeffModulus = CoeffModulus.CoeffModulus128 (2048);
			var context = SEALContext.Create (parms);

			var encoder = new IntegerEncoder (parms.PlainModulus);

			var keygen = new KeyGenerator (context);

			var public_key = keygen.PublicKey;
			var secret_key = keygen.SecretKey;
			
			Console.WriteLine ("All Done");
		}
	}
}
