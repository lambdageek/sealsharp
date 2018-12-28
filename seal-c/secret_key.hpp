
#ifndef _SEAL_C_SECRET_KEY_HPP
#define _SEAL_C_SECRET_KEY_HPP

#include <seal/secretkey.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::SecretKey*> : public WrapPair<seal::SecretKey*, SEALSecretKeyRef> {};

		template<>
		struct Unwrap<SEALSecretKeyRef> : public WrapPair<seal::SecretKey*, SEALSecretKeyRef> {};
	}
} //namesapce seal_c

#endif
