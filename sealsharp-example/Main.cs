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

            /* SEALSharp
             *            
             * Example of a basic implementation of the SEAL BFV encryption scheme
             */

            /*
             * Reference https://github.com/Microsoft/SEAL/blob/aa7bf57aa11a91d9ca8712816550ae68793add99/examples/examples.cpp#L257
             * for information about parameter setup and class initialization
             */
            var parms = new EncryptionParameters(EncryptionParameters.Scheme.BFV);
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


            // Encode long values into Plaintext
			long l = 8;
            long m = 12;
            Plaintext encoded_l = encoder.Encode(l);
			Plaintext encoded_m = encoder.Encode(m);
            //Console.WriteLine($"x = {l}");
            //Console.WriteLine($"y = {m}");

            // Encrypt Plaintext into Ciphertext
            encryptor.Encrypt(encoded_l, out Ciphertext encrypted_l);
            encryptor.Encrypt(encoded_m, out Ciphertext encrypted_m);

            // Decrypt Ciphertext to Plaintext
            decryptor.Decrypt(encrypted_l, out Plaintext decrypted_l);
            decryptor.Decrypt(encrypted_m, out Plaintext decrypted_m);

            // Decode Plaintext to long
            long decoded_l = encoder.DecodeLong(encoded_l);
            long decoded_m = encoder.DecodeLong(encoded_m);

			// Add 2 encrypted values
			evaluator.Add(encrypted_l, encrypted_m, out Ciphertext encrypted_result);
			decryptor.Decrypt(encrypted_result, out Plaintext decrypted_result);
			long decrypted_decoded_result = encoder.DecodeLong(decrypted_result);
			//Console.WriteLine ($"addition -> {l} + {m} = {decrypted_decoded_result}");

            // Multiply 2 encrypted long
            // Expected result size is (m+n-1) where m and n are the input sizes
            evaluator.Multiply(encrypted_l, encrypted_m, out Ciphertext encrypted_result_mult);
            //Console.WriteLine($"size of m: {encrypted_l.Size()}");
            //Console.WriteLine($"size of n: {encrypted_m.Size()}");
            //Console.WriteLine($"Size after multiplication: {encrypted_result_mult.Size()}");
            int inv_before = decryptor.InvariantNoiseBudget(encrypted_result_mult);
            //Console.WriteLine($"Invariant noise budget: {inv_before}");

            // Relinearize product
            // Reduces result size to 2
            RelinKeys relin_keys = keygen.RelinKeys(16);
            evaluator.Relinearize(encrypted_result_mult, relin_keys, out Ciphertext result_relin);
            //Console.WriteLine($"Size after relinearization: {result_relin.Size()}");
            int inv_after = decryptor.InvariantNoiseBudget(result_relin);
            //Console.WriteLine($"invariant noise budget: {inv_after}");

			decryptor.Decrypt(result_relin, out Plaintext decrypted_result_mult);
			long decrypted_decoded_result_mult = encoder.DecodeLong(decrypted_result_mult);
			//Console.WriteLine ($"multiplication -> {l} * {m} = {decrypted_decoded_result_mult}");

            Expression<Func<long, long, long>> e = (x, y) => y + (x * x);
            Expression<Func<long, long, long>> e_r = (x, y) => Evaluator.R(y + Evaluator.R(x * x));

			//int num_mults = SealExpression.NumMults (e_r);
            //Console.WriteLine($"Expression: {e_r}");
			//Console.WriteLine($"Number of multiplications: {num_mults}");

            //ParameterExpression oldParam = Expression.Parameter(typeof(long), "x");
            //ParameterExpression newParam = Expression.Parameter(typeof(long), "z");
            //Expression e_replaced = SealExpression.RenameSingleParam(e, oldParam, newParam);
            //Console.WriteLine($"Expression after replacing {oldParam.Name} with {newParam.Name}: {e_replaced} \n");

            Console.WriteLine($"x = {l}");
            Console.WriteLine($"y = {m}");
            Console.WriteLine($"x_size = {encrypted_l.Size()}");
            Console.WriteLine($"y_size = {encrypted_m.Size()} \n");

            Ciphertext c = evaluator.CompileAndRun(e, relin_keys, encrypted_l, encrypted_m);
            decryptor.Decrypt(c, out Plaintext p);
            long result = encoder.DecodeLong(p);
            Console.WriteLine("Expression without relinearization");
            Console.WriteLine(e);
            Console.WriteLine($"Size of result: {c.Size()}");
            Console.WriteLine(result);

            Ciphertext c1 = evaluator.CompileAndRun(e_r, relin_keys, encrypted_l, encrypted_m);
            decryptor.Decrypt(c1, out Plaintext p1);
            long res = encoder.DecodeLong(p1);
            Console.WriteLine("\nExpression with relinearization");
            Console.WriteLine(e_r);
            Console.WriteLine($"Size of result: {c1.Size()}");
            Console.WriteLine(res);


            //Ciphertext c1 = evaluator.CompileAndRun(e, relin_keys, encrypted_l, c);
            //decryptor.Decrypt(c1, out Plaintext p1);
            //long result1 = encoder.DecodeLong(p1);
            //Console.WriteLine(c1.Size());

            //var e_call = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext>>(e);
            //Console.WriteLine($"Expression after replacing arithmetic with call: {e_call}");
            //var f = e_call.Compile();
            //Ciphertext c1 = f(evaluator, relin_keys, encrypted_l, encrypted_m);
            //Console.WriteLine($"Expected size without relinearization: (m+n-1)");
            //Console.WriteLine($"Size without relin: {c1.Size()}");

            //decryptor.Decrypt(c1, out Plaintext p1);
            //long d1 = encoder.DecodeLong(p1);
            //Console.WriteLine($"{e} => {d1} \n");


            //var e_r_call = SealExpression.ReplaceWithCall<Func<Evaluator, RelinKeys, Ciphertext, Ciphertext, Ciphertext>>(e_r);
            //Console.WriteLine($"Expression after replacing arithmetic with call: {e_r_call}");
            //var f_r = e_r_call.Compile();
            //Ciphertext c1_r = f_r(evaluator, relin_keys, encrypted_l, encrypted_m);
            //Console.WriteLine($"Expected size after relinearization: 2");
            //Console.WriteLine($"Size with relin: {c1_r.Size()}");

            //decryptor.Decrypt(c1_r, out Plaintext p1_r);
            //long d1_r = encoder.DecodeLong(p1_r);
            //Console.WriteLine($"{e_r} => {d1_r} \n");

            Console.WriteLine("\nAll Done");
        }
	}
}
