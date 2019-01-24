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

        public static Ciphertext SimpleAdd (Evaluator evaluator, Ciphertext ciphertext1, Ciphertext ciphertext2)
        {
            if (evaluator.Add(ciphertext1, ciphertext2, out Ciphertext result))
                return result;
            throw new ArgumentException("SimpleAdd failed - check the arguments");
            // Exrpression addCall = Expressions.Call (typeof (SEAL.Evaluator), "SimpleAdd",  
            //                                         null, evaluatorExpr, argExpr1, argExpr2);
            //   "SEAL.Evaluator.SimpleAdd (evaluator, arg1, arg2)"
        }

        public bool Multiply (Ciphertext ciphertext1, Ciphertext ciphertext2, out Ciphertext result)
		{
			bool success = handle.Multiply (ciphertext1.handle, ciphertext2.handle, 
				                            out Internal.Ciphertext result_h);
			result = success ? new Ciphertext (result_h) : null;
			return success;
		}

        public static Ciphertext SimpleMult(Evaluator evaluator, Ciphertext ciphertext1, Ciphertext ciphertext2)
        {
            if (evaluator.Multiply(ciphertext1, ciphertext2, out Ciphertext result))
                return result;
            throw new ArgumentException("SimpleMult failed - check the arguments");
        }


        public bool Relinearize (Ciphertext ciphertext, RelinKeys relin_keys, out Ciphertext result)
		{
			bool success = handle.Relinearize (ciphertext.handle, relin_keys.handle, 
				                               out Internal.Ciphertext result_h);
			result = success ? new Ciphertext (result_h) : null;
			return success;
		}

        public static Ciphertext SimpleRelin(Evaluator evaluator, RelinKeys relin_keys, Ciphertext ciphertext)
        {
            if (evaluator.Relinearize(ciphertext, relin_keys, out Ciphertext result))
                return result;
            throw new ArgumentException("SimpleRelin failed - check the arguments");
        }

        public static long R(long x)
        {
            return x;
        }

    }
}