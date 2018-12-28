
#ifndef _SEAL_C_SMALL_MODULUS_HPP
#define _SEAL_C_SMALL_MODULUS_HPP

#include <seal/smallmodulus.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::SmallModulus*> : public WrapPair<seal::SmallModulus*, SEALSmallModulusRef> {};

		template<>
		struct Unwrap<SEALSmallModulusRef> : public WrapPair<seal::SmallModulus*, SEALSmallModulusRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
