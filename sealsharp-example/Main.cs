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
			var p = new EncryptionParameters (EncryptionParameters.Scheme.BFV);
			p.PolyModulusDegree = 2048;
			p.PlainModulus = 1 << 8;
			p.CoeffModulus = CoeffModulus.CoeffModulus128 (2048);
			var context = SEALContext.Create (p);

			var keygen = KeyGenerator.Create (context);

			Console.WriteLine ("All Done");
		}
	}
}
