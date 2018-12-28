
#ifndef _SEAL_C_PUBLIC_KEY_HPP
#define _SEAL_C_PUBLIC_KEY_HPP

#include <seal/publickey.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::PublicKey*> : public WrapPair<seal::PublicKey*, SEALPublicKeyRef> {};

		template<>
		struct Unwrap<SEALPublicKeyRef> : public WrapPair<seal::PublicKey*, SEALPublicKeyRef> {};
	}
} //namesapce seal_c

#endif
