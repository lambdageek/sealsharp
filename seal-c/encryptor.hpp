
#ifndef _SEAL_C_ENCRYPTOR_HPP
#define _SEAL_C_ENCRYPTOR_HPP

#include <seal/encryptor.h>

#include <seal-c/encryptor.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::Encryptor*> : public WrapPair<seal::Encryptor*, SEALEncryptorRef> {};

		template<>
		struct Unwrap<SEALEncryptorRef> : public WrapPair<seal::Encryptor*, SEALEncryptorRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
