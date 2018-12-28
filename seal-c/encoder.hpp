
#ifndef _SEAL_C_ENCODER_HPP
#define _SEAL_C_ENCODER_HPP

#include <seal/encoder.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::AbstractIntegerEncoder*> : public WrapPair<seal::AbstractIntegerEncoder*, SEALAbstractIntegerEncoderRef> {};

		template<>
		struct Unwrap<SEALAbstractIntegerEncoderRef> : public WrapPair<seal::AbstractIntegerEncoder*, SEALAbstractIntegerEncoderRef> {};
		
		template<>
		struct Wrap<seal::IntegerEncoder*> : public WrapPair<seal::IntegerEncoder*, SEALIntegerEncoderRef> {};

		template<>
		struct Unwrap<SEALIntegerEncoderRef> : public WrapPair<seal::IntegerEncoder*, SEALIntegerEncoderRef> {};

	} // namespace wrap
} // namespace seal_c

#endif
