using System;

namespace SEAL {
	public class Evaluator {
		internal Internal.Evaluator handle;

		internal Evaluator (Internal.Evaluator h)
		{
			handle = h;
		}

		public Evaluator (SEALContext context)
		{
			handle = Internal.Evaluator.Create (context.handle);
		}

		public bool Add (Ciphertext ciphertext1, Ciphertext ciphertext2, out Ciphertext result)
		{
			bool success = handle.Add (ciphertext1.handle, ciphertext2.handle, 
				                       out Internal.Ciphertext result_h);
			result = success ? new Ciphertext (result_h) : null;
			return success;
		}

		public bool Multiply (Ciphertext ciphertext1, Ciphertext ciphertext2, out Ciphertext result)
		{
			bool success = handle.Multiply (ciphertext1.handle, ciphertext2.handle, 
				                            out Internal.Ciphertext result_h);
			result = success ? new Ciphertext (result_h) : null;
			return success;
		}

		public bool Relinearize (Ciphertext ciphertext, RelinKeys relin_keys, out Ciphertext result)
		{
			bool success = handle.Relinearize (ciphertext.handle, relinkeys.handle, 
				                               out Internal.Ciphertext result_h);
			result = success ? new Ciphertext (result_h) : null;
			return success;
		}

	}
}