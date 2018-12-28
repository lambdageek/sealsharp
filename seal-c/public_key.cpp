
#include <memory>

#include <seal-c/public_key.h>

#include "public_key.hpp"
#include "wrap.hpp"

void
SEAL_PublicKey_destroy (SEALPublicKeyRef public_key)
{
	delete seal_c::wrap::unwrap (public_key);
}
