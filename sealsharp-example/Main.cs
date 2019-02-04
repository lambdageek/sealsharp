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

            /* Sealsharp
             *            
             * Example of a basic implementation of the SEAL BFV encryption scheme
             */

            /*
             * C# Bindings 
             * 
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


            /*
             * Example of computation using only C# bindings
             */
			long l = 8;
            long m = 12;

            // Encode long values into Plaintext
            Plaintext encoded_l = encoder.Encode(l);
			Plaintext encoded_m = encoder.Encode(m);

            // Encrypt Plaintext into Ciphertext
            encryptor.Encrypt(encoded_l, out Ciphertext encrypted_l);
            encryptor.Encrypt(encoded_m, out Ciphertext encrypted_m);

            // Generate relinearization keys
            RelinKeys relin_keys = keygen.RelinKeys(16);

            // Multiply 2 encrypted long
            evaluator.Multiply(encrypted_l, encrypted_m, out Ciphertext encrypted_result_mult);

            // Relinearize product
            evaluator.Relinearize(encrypted_result_mult, relin_keys, out Ciphertext result_relin);

            // Add 2 encrypted long
            evaluator.Add(result_relin, encrypted_l, out Ciphertext encrypted_res);

            // Decypt and Decode
            decryptor.Decrypt(encrypted_res, out Plaintext p);
            long result = encoder.DecodeLong(p);


            /*
             * Simplified computation
             */
            // Define expression
            Expression<Func<long, long, long>> computation = (x, y) => x + (x * y);

            // Expression with relinearization
            Expression<Func<long, long, long>> relin_computation = (x, y) => x + Evaluator.R(x * y);

            // Evaluate expression
            Ciphertext encrypted_result = evaluator.CompileAndRun(computation, relin_keys, encrypted_l, encrypted_m);

            // Decrypt and decode result
            decryptor.Decrypt(encrypted_result, out Plaintext plaintext_result);
            long result1 = encoder.DecodeLong(plaintext_result);

            // Evaluate result and size
            Console.WriteLine(computation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Size: {encrypted_res.Size()}");
            Console.WriteLine("\n");
            Console.WriteLine(relin_computation);
            Console.WriteLine($"Result: {result1}");
            Console.WriteLine($"Size: {encrypted_result.Size()}");
        }
	}
}
