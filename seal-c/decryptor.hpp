
#ifndef _SEAL_C_DECRYPTOR_HPP
#define _SEAL_C_DECRYPTOR_HPP

#include <seal/decryptor.h>

#include <seal-c/decryptor.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::Decryptor*> : public WrapPair<seal::Decryptor*, SEALDecryptorRef> {};

		template<>
		struct Unwrap<SEALDecryptorRef> : public WrapPair<seal::Decryptor*, SEALDecryptorRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
