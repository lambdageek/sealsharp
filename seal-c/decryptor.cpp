
#include <memory>

#include <seal/decryptor.h>

#include <seal-c/decryptor.h>

#include "shared_context.hpp"
#include "secret_key.hpp"
#include "decryptor.hpp"
#include "ciphertext.hpp"
#include "plaintext.hpp"
#include "wrap.hpp"


namespace seal_c {
	bool
	decrypt (seal::Decryptor& decryptor, const seal::Ciphertext& ciphertext, seal::Plaintext& dest) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			decryptor.decrypt (ciphertext, dest);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

	bool
	invariant_noise_budget (seal::Decryptor& decryptor, const seal::Ciphertext& ciphertext) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			decryptor.invariant_noise_budget (ciphertext);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

} // namespace seal_c

SEALDecryptorRef
SEAL_Decryptor_construct (SEALSharedContextRef context, SEALSecretKeyRef secret_key)
{
	using seal_c::wrap::unwrap;
	auto ctx = unwrap (context)->get_context ();
	auto p = std::make_unique <seal::Decryptor> (ctx, *unwrap (secret_key));
	return seal_c::wrap::wrap (p.release ());
}

SEALPlaintextRef
SEAL_Decryptor_decrypt_new (SEALDecryptorRef decryptor, SEALCiphertextRef ciphertext, SEALBoolean *success)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	if (!success)
		throw new std::logic_error ("success must not be a nullptr");
	auto result = std::make_unique <seal::Plaintext> ();
	if (seal_c::decrypt (*unwrap (decryptor), *unwrap (ciphertext), *result)) {
		*success = 1;
		return wrap (result.release ());
	} else {
		*success = 0;
		return wrap<seal::Plaintext*> (nullptr);
	}
}

int
SEAL_Decryptor_invariant_noise_budget (SEALDecryptorRef decryptor, SEALCiphertextRef ciphertext)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	if (seal_c::invariant_noise_budget (*unwrap (decryptor), *unwrap (ciphertext))) {
		return seal_c::invariant_noise_budget (*unwrap (decryptor), *unwrap (ciphertext));
	} else {
		return 0;
	}
}

void
SEAL_Decryptor_destroy (SEALDecryptorRef decryptor)
{
	delete seal_c::wrap::unwrap (decryptor);
}
