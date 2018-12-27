
#include <memory>

#include <seal/keygenerator.h>

#include <seal-c/types.h>
#include <seal-c/key_generator.h>

#include "shared_context.hpp"
#include "key_generator.hpp"
#include "wrap.hpp"

SEALKeyGeneratorRef
SEAL_KeyGenerator_construct (SEALSharedContextRef context)
{
	// FIXME: KeyGenerator ctor can throw if encryption parameters aren't
	// set correctly.  Need to either catch here and signal to C#, or else
	// implement checks in C# that interrogate the EncryptionParameters.
	auto p = std::make_unique<seal::KeyGenerator> (seal_c::wrap::unwrap (context)->get_context ());
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_KeyGenerator_destroy (SEALKeyGeneratorRef keygen)
{
	delete seal_c::wrap::unwrap (keygen);
}
