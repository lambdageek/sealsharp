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

			var encoder = new IntegerEncoder (parms.PlainModulus);

			var keygen = new KeyGenerator (context);

			var public_key = keygen.PublicKey;
			var secret_key = keygen.SecretKey;

			var encryptor = new Encryptor (context, public_key);
			var decryptor = new Decryptor (context, secret_key);

			var evaluator = new Evaluator (context);

			long l = 3;
			Plaintext encoded_l = encoder.Encode (l);

			long m = 8;
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
			evaluator.Multiply(encrypted_l, encrypted_m, out Ciphertext encrypted_result_mult);
			evaluator.Relinearize(encrypted_result_mult, relin_keys, out Ciphertext result_relin);
			decryptor.Decrypt(result_relin, out Plaintext decrypted_result_mult);
			long decrypted_decoded_result_mult = encoder.DecodeLong(decrypted_result_mult);
			Console.WriteLine ($"multiplication -> {l} * {m} = {decrypted_decoded_result_mult}");


			Console.WriteLine ("All Done");
		}
	}
}
