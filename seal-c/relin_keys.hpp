#ifndef _SEAL_C_RELIN_KEYS_HPP
#define _SEAL_C_RELIN_KEYS_HPP

#include <seal/relinkeys.h>
#include <seal-c/types.h>
#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::RelinKeys*> : public WrapPair<seal::RelinKeys*, SEALRelinKeysRef> {};

		template<>
		struct Unwrap<SEALRelinKeysRef> : public WrapPair<seal::RelinKeys*, SEALRelinKeysRef> {};
	} // namespace wrap
} // namespace seal_c

#endif