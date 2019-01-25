#include <memory>
#include <seal/keygenerator.h>
#include <seal-c/types.h>
#include <seal-c/key_generator.h>

#include "shared_context.hpp"
#include "key_generator.hpp"
#include "public_key.hpp"
#include "secret_key.hpp"
#include "relin_keys.hpp"
#include "wrap.hpp"

namespace seal_c {
	bool get_relin_keys (seal::KeyGenerator& keygen, const int& decomp_bit_count) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			keygen.relin_keys (decomp_bit_count);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

} // namespace seal_c

SEALKeyGeneratorRef
SEAL_KeyGenerator_construct (SEALSharedContextRef context)
{
	// FIXME: KeyGenerator ctor can throw if encryption parameters aren't
	// set correctly.  Need to either catch here and signal to C#, or else
	// implement checks in C# that interrogate the EncryptionParameters.
	auto p = std::make_unique<seal::KeyGenerator> (seal_c::wrap::unwrap (context)->get_context ());
	return seal_c::wrap::wrap (p.release ());
}

SEALPublicKeyRef
SEAL_KeyGenerator_get_public_key (SEALKeyGeneratorRef keygen)
{
	auto p = std::make_unique <seal::PublicKey> (seal_c::wrap::unwrap (keygen)->public_key ());
	return seal_c::wrap::wrap (p.release ());
}

SEALSecretKeyRef
SEAL_KeyGenerator_get_secret_key (SEALKeyGeneratorRef keygen)
{
	auto p = std::make_unique <seal::SecretKey> (seal_c::wrap::unwrap (keygen)->secret_key ());
	return seal_c::wrap::wrap (p.release ());
}

SEALRelinKeysRef
SEAL_KeyGenerator_get_relin_keys (SEALKeyGeneratorRef keygen, int decomp_bit_count)
{
	auto p = std::make_unique <seal::RelinKeys> (seal_c::wrap::unwrap (keygen)->relin_keys (decomp_bit_count));
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_KeyGenerator_destroy (SEALKeyGeneratorRef keygen)
{
	delete seal_c::wrap::unwrap (keygen);
}
