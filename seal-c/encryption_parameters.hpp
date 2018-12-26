
#ifndef _SEAL_C_ENCRYPTION_PARAMETERS_HPP
#define _SEAL_C_ENCRYPTION_PARAMETERS_HPP

#include <seal-c/types.h>
#include <seal/encryptionparams.h>
#include "wrap.hpp"

namespace seal_c {

	namespace wrap {

		template<>
		struct Wrap<seal::EncryptionParameters*> : public WrapPair<seal::EncryptionParameters*, SEALEncryptionParametersRef> {};

		template<>
		struct Unwrap<SEALEncryptionParametersRef> : public WrapPair<seal::EncryptionParameters*, SEALEncryptionParametersRef> {};

	} // namespace wrap

} // namespace seal_c

#endif
