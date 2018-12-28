
#include <memory>

#include <seal/encryptor.h>

#include <seal-c/encryptor.h>

#include "shared_context.hpp"
#include "public_key.hpp"
#include "encryptor.hpp"

#include "wrap.hpp"

SEALEncryptorRef
SEAL_Encryptor_construct (SEALSharedContextRef context, SEALPublicKeyRef public_key)
{
	using seal_c::wrap::unwrap;
	auto ctx = unwrap (context)->get_context ();
	auto p = std::make_unique <seal::Encryptor> (ctx, *unwrap (public_key));
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_Encryptor_destroy (SEALEncryptorRef encryptor)
{
	delete seal_c::wrap::unwrap (encryptor);
}
