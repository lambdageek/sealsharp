
#include <memory>

#include <seal/encryptionparams.h>

#include <seal-c/encryption_parameters.h>

#include "encryption_parameters.hpp"
#include "wrap.hpp"

SEALEncryptionParametersRef
SEAL_EncryptionParameters_construct_BFV (void)
{
	auto p = std::make_unique<seal::EncryptionParameters> (seal::scheme_type::BFV);
	return seal_c::wrap::wrap (p.release ());
}

SEALEncryptionParametersRef
SEAL_EncryptionParameters_construct_CKKS (void)
{
	auto p = std::make_unique<seal::EncryptionParameters> (seal::scheme_type::CKKS);
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_EncryptionParameters_destroy (SEALEncryptionParametersRef parms)
{
	delete seal_c::wrap::unwrap (parms);
}


