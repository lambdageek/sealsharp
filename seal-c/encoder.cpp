
#include <stdint.h>
#include <memory>

#include <seal/encoder.h>

#include <seal-c/encoder.h>

#include "small_modulus.hpp"
#include "plaintext.hpp"

#include "encoder.hpp"

void
SEAL_AbstractIntegerEncoder_destroy (SEALAbstractIntegerEncoderRef encoder)
{
	delete seal_c::wrap::unwrap (encoder);
}

SEALPlaintextRef
SEAL_AbstractIntegerEncoder_encode_int64 (SEALAbstractIntegerEncoderRef encoder, int64_t i)
{
	auto p = std::make_unique <seal::Plaintext> (seal_c::wrap::unwrap (encoder)->encode (i));
	return seal_c::wrap::wrap (p.release ());
}

int64_t
SEAL_AbstractIntegerEncoder_decode_int64 (SEALAbstractIntegerEncoderRef encoder, SEALPlaintextRef plaintext)
{
	using seal_c::wrap::unwrap;
	return unwrap (encoder)->decode_int64 (*unwrap (plaintext));
}

SEALIntegerEncoderRef
SEAL_IntegerEncoder_construct (SEALSmallModulusRef plain_modulus, uint64_t base)
{
	auto p = std::make_unique <seal::IntegerEncoder> (*seal_c::wrap::unwrap (plain_modulus), base);
	return seal_c::wrap::wrap (p.release ());
}
