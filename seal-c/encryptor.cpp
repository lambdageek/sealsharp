
#include <stdexcept>
#include <memory>

#include <seal/encryptor.h>

#include <seal-c/encryptor.h>

#include "shared_context.hpp"
#include "public_key.hpp"
#include "encryptor.hpp"
#include "plaintext.hpp"
#include "ciphertext.hpp"
#include "wrap.hpp"

namespace seal_c {
	bool
	encrypt (seal::Encryptor& encryptor, const seal::Plaintext& plaintext, seal::Ciphertext& dest) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			encryptor.encrypt (plaintext, dest);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

} // namespace seal_c

SEALEncryptorRef
SEAL_Encryptor_construct (SEALSharedContextRef context, SEALPublicKeyRef public_key)
{
	using seal_c::wrap::unwrap;
	auto ctx = unwrap (context)->get_context ();
	auto p = std::make_unique <seal::Encryptor> (ctx, *unwrap (public_key));
	return seal_c::wrap::wrap (p.release ());
}

SEALCiphertextRef
SEAL_Encryptor_encrypt_new (SEALEncryptorRef encryptor, SEALPlaintextRef plaintext, SEALBoolean *success)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	if (!success)
		throw new std::logic_error ("success must not be a nullptr");
	auto result = std::make_unique <seal::Ciphertext> ();
	if (seal_c::encrypt (*unwrap (encryptor), *unwrap (plaintext), *result)) {
		*success = 1;
		return wrap (result.release ());
	} else {
		*success = 0;
		return wrap<seal::Ciphertext*> (nullptr);
	}
}

void
SEAL_Encryptor_destroy (SEALEncryptorRef encryptor)
{
	delete seal_c::wrap::unwrap (encryptor);
}
