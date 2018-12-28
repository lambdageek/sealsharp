
#include <stdint.h>
#include <memory>

#include <seal/smallmodulus.h>

#include <seal-c/small_modulus.h>

#include "small_modulus.hpp"

#include "wrap.hpp"

SEALSmallModulusRef
SEAL_SmallModulus_construct (uint64_t value)
{
	auto p = std::make_unique<seal::SmallModulus> (value);
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_SmallModulus_destroy (SEALSmallModulusRef small_modulus)
{
	delete seal_c::wrap::unwrap (small_modulus);
}
