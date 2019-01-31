using System;
using System.Reflection;
using System.Linq.Expressions;

namespace SEAL
{
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

        public Ciphertext CompileAndRun<T1, T2>(Expression<Func<T1, T2>> e, RelinKeys relin_keys, Ciphertext c1)
        {
            var e2 = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext>>(e);
            var f = e2.Compile();
            return f(this, relin_keys, c1);
        }

        public Ciphertext CompileAndRun<T1, T2, T3> (Expression<Func<T1, T2, T3>> e, RelinKeys relin_keys, Ciphertext c1, Ciphertext c2)
        {
            var e2 = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext>>(e);
            var f = e2.Compile();
            return f(this, relin_keys, c1, c2);
        }

        public Ciphertext CompileAndRun<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4>> e, RelinKeys relin_keys, Ciphertext c1, Ciphertext c2, Ciphertext c3)
        {
            var e2 = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext, Ciphertext>>(e);
            var f = e2.Compile();
            return f(this, relin_keys, c1, c2, c3);
        }

        public Ciphertext CompileAndRun<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5>> e, RelinKeys relin_keys, Ciphertext c1, Ciphertext c2, Ciphertext c3, Ciphertext c4)
        {
            var e2 = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext, Ciphertext, Ciphertext>>(e);
            var f = e2.Compile();
            return f(this, relin_keys, c1, c2, c3, c4);
        }

        public Ciphertext CompileAndRun<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6>> e, RelinKeys relin_keys, Ciphertext c1, Ciphertext c2, Ciphertext c3, Ciphertext c4, Ciphertext c5)
        {
            var e2 = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext, Ciphertext, Ciphertext, Ciphertext>>(e);
            var f = e2.Compile();
            return f(this, relin_keys, c1, c2, c3, c4, c5);
        }

    }
}