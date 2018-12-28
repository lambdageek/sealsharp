
#include <memory>

#include <seal/decryptor.h>

#include <seal-c/decryptor.h>

#include "shared_context.hpp"
#include "secret_key.hpp"
#include "decryptor.hpp"

#include "wrap.hpp"

SEALDecryptorRef
SEAL_Decryptor_construct (SEALSharedContextRef context, SEALSecretKeyRef secret_key)
{
	using seal_c::wrap::unwrap;
	auto ctx = unwrap (context)->get_context ();
	auto p = std::make_unique <seal::Decryptor> (ctx, *unwrap (secret_key));
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_Decryptor_destroy (SEALDecryptorRef decryptor)
{
	delete seal_c::wrap::unwrap (decryptor);
}
