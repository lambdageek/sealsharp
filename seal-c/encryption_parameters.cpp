
#include <stdint.h>
#include <memory>

#include <seal/encryptionparams.h>

#include <seal-c/encryption_parameters.h>

#include "coeff_modulus.hpp"
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


void
SEAL_EncryptionParameters_set_poly_modulus_degree (SEALEncryptionParametersRef parms, uint32_t degree)
{
	// TODO: SEAL uses size_t for the poly modulus degree parameter, which can be
	// a 64-bit number.  If a 32-bit uint is not big enough, we should bump
	// things up here.
	seal_c::wrap::unwrap (parms)->set_poly_modulus_degree (static_cast<size_t> (degree));
}

void
SEAL_EncryptionParameters_set_coeff_modulus (SEALEncryptionParametersRef parms, SEALCoeffModulusRef coeff_modulus)
{
	seal_c::wrap::unwrap (parms)->set_coeff_modulus (seal_c::wrap::unwrap (coeff_modulus)->get_coeff_modulus ());
}
