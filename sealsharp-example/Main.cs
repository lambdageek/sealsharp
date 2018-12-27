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
			p.CoeffModulus = CoeffModulus.CoeffModulus128 (2048);
			var c = SEALContext.Create (p);
			Console.WriteLine ("All Done");
		}
	}
}
