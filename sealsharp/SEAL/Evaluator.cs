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

	}
}