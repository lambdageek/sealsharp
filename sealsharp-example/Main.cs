using System;
using System.Linq.Expressions;
using SEAL;

namespace Example
{
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

			var encryptor = new Encryptor (context, public_key);
			var decryptor = new Decryptor (context, secret_key);

			var evaluator = new Evaluator (context);

			long l = 8;
			Plaintext encoded_l = encoder.Encode (l);

			long m = 12;
			Plaintext encoded_m = encoder.Encode (m);
			encryptor.Encrypt (encoded_m, out Ciphertext encrypted_m);

			// Encode and Decode Long 
			long decoded_l = encoder.DecodeLong (encoded_l);
			Console.WriteLine ($"encoding {l} -> ... -> decoding {decoded_l}"); // TODO Plaintext.ToString ()

			// Encrypt Long
			if (!encryptor.Encrypt (encoded_l, out Ciphertext encrypted_l))
				throw new Exception ("encryption failed");

			if (!decryptor.Decrypt (encrypted_l, out Plaintext decrypted_l))
				throw new Exception ("decryption failed");

			long decrypted_decoded_l = encoder.DecodeLong (decrypted_l);
			Console.WriteLine ($"encoding {l} -> encrypting -> ... -> decrypting -> decoding {decrypted_decoded_l}");

			if (decrypted_decoded_l != l)
				throw new Exception ("encoding+encrypting roundtrip failed");

			// Add 2 encrypted long
			evaluator.Add(encrypted_l, encrypted_m, out Ciphertext encrypted_result);
			decryptor.Decrypt(encrypted_result, out Plaintext decrypted_result);
			long decrypted_decoded_result = encoder.DecodeLong(decrypted_result);
			Console.WriteLine ($"addition -> {l} + {m} = {decrypted_decoded_result}");

			RelinKeys relin_keys = keygen.RelinKeys (16);

			//Multiply 2 encrypted long
			Console.WriteLine ($"size of m: {encrypted_l.Size()}");
			Console.WriteLine ($"size of n: {encrypted_m.Size()}");

			evaluator.Multiply(encrypted_l, encrypted_m, out Ciphertext encrypted_result_mult);
			int inv_before = decryptor.InvariantNoiseBudget(encrypted_result_mult);
			Console.WriteLine ($"expected size before relinearization -> (m+n-1): {encrypted_result_mult.Size()}");
			

			evaluator.Relinearize(encrypted_result_mult, relin_keys, out Ciphertext result_relin);
			int inv_after = decryptor.InvariantNoiseBudget(result_relin);
			Console.WriteLine ($"expected size after relinearization -> 2: {result_relin.Size()}");

			Console.WriteLine ($"invariant noise budget before relinearize: {inv_before}");
			Console.WriteLine ($"invariant noise budget after relinearize: {inv_after}");

			decryptor.Decrypt(result_relin, out Plaintext decrypted_result_mult);
			long decrypted_decoded_result_mult = encoder.DecodeLong(decrypted_result_mult);
			Console.WriteLine ($"multiplication -> {l} * {m} = {decrypted_decoded_result_mult}");


            //Expression<Func<int,int,int>> e = (x1, x2) => x1 * ((x1 * -x2) + x2);
            Expression<Func<long, long, long>> e = (x, y) => x * y;
            Expression<Func<long, long, long>> e_r = (x, y) => Evaluator.R(x * y);
            SealExpression seal_e = new SealExpression (e);
			int num_mults = SealExpression.NumMults (e);
            Console.WriteLine($"Expression: {e}");
			Console.WriteLine($"Number of multiplications: {num_mults}");

            ParameterExpression oldParam = Expression.Parameter(typeof(long), "x");
            ParameterExpression newParam = Expression.Parameter(typeof(long), "z");
            Expression e_replaced = SealExpression.RenameSingleParam(e, oldParam, newParam);
            Console.WriteLine($"Expression after replacing {oldParam.Name} with {newParam.Name}: {e_replaced} \n");

            Console.WriteLine($"x = {l}, x_size = {encrypted_l.Size()}");
            Console.WriteLine($"y = {m}, y_size = {encrypted_m.Size()} \n");


            var e_call = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext>>(e);
            Console.WriteLine($"Expression after replacing arithmetic with call: {e_call}");
            var f = e_call.Compile();
            Ciphertext c1 = f(evaluator, relin_keys, encrypted_l, encrypted_m);
            Console.WriteLine($"Expected size without relinearization: (m+n-1)");
            Console.WriteLine($"Size without relin: {c1.Size()}");

            decryptor.Decrypt(c1, out Plaintext p1);
            long d1 = encoder.DecodeLong(p1);
            Console.WriteLine($"{e} => {d1} \n");


            var e_r_call = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext>>(e_r);
            Console.WriteLine($"Expression after replacing arithmetic with call: {e_r_call}");
            var f_r = e_r_call.Compile();
            Ciphertext c1_r = f_r(evaluator, relin_keys, encrypted_l, encrypted_m);
            Console.WriteLine($"Expected size after relinearization: 2");
            Console.WriteLine($"Size with relin: {c1_r.Size()}");

            decryptor.Decrypt(c1_r, out Plaintext p1_r);
            long d1_r = encoder.DecodeLong(p1_r);
            Console.WriteLine($"{e_r} => {d1_r} \n");

            Console.WriteLine("All Done");
        }
	}
}
