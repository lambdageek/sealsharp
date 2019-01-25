#include <seal/relinkeys.h>
#include <seal-c/relin_keys.h>

#include "relin_keys.hpp"
#include "wrap.hpp"

void
SEAL_RelinKeys_destroy (SEALRelinKeysRef relin_keys)
{
	delete seal_c::wrap::unwrap (relin_keys);
}