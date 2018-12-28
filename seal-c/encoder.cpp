
#include <stdint.h>
#include <memory>

#include <seal/encoder.h>

#include <seal-c/encoder.h>

#include "small_modulus.hpp"

#include "encoder.hpp"

void
SEAL_AbstractIntegerEncoder_destroy (SEALAbstractIntegerEncoderRef encoder)
{
	delete seal_c::wrap::unwrap (encoder);
}

SEALIntegerEncoderRef
SEAL_IntegerEncoder_construct (SEALSmallModulusRef plain_modulus, uint64_t base)
{
	auto p = std::make_unique <seal::IntegerEncoder> (*seal_c::wrap::unwrap (plain_modulus), base);
	return seal_c::wrap::wrap (p.release ());
}
