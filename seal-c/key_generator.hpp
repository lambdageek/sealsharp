
#ifndef _SEAL_C_KEY_GENERATOR_HPP
#define _SEAL_C_KEY_GENERATOR_HPP

#include <seal/keygenerator.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template <>
		struct Wrap<seal::KeyGenerator*> : public WrapPair<seal::KeyGenerator*, SEALKeyGeneratorRef> {};
		template <>
		struct Unwrap<SEALKeyGeneratorRef> : public WrapPair<seal::KeyGenerator*, SEALKeyGeneratorRef> {};
	} // namespace wrap

} //end namespace seal_c

#endif
