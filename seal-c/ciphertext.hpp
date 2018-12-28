
#ifndef _SEAL_C_CIPHERTEXT_HPP
#define _SEAL_C_CIPHERTEXT_HPP

#include <seal/ciphertext.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::Ciphertext*> : public WrapPair<seal::Ciphertext*, SEALCiphertextRef> {};

		template<>
		struct Unwrap<SEALCiphertextRef> : public WrapPair<seal::Ciphertext*, SEALCiphertextRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
