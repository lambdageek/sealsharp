
#ifndef _SEAL_C_PLAINTEXT_HPP
#define _SEAL_C_PLAINTEXT_HPP

#include <seal/plaintext.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::Plaintext*> : public WrapPair<seal::Plaintext*, SEALPlaintextRef> {};

		template<>
		struct Unwrap<SEALPlaintextRef> : public WrapPair<seal::Plaintext*, SEALPlaintextRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
