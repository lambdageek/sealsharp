
#include <memory>

#include <seal-c/secret_key.h>

#include "secret_key.hpp"
#include "wrap.hpp"

void
SEAL_SecretKey_destroy (SEALSecretKeyRef secret_key)
{
	delete seal_c::wrap::unwrap (secret_key);
}
