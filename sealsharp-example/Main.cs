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

			var keygen = new KeyGenerator (context);

			Console.WriteLine ("All Done");
		}
	}
}
