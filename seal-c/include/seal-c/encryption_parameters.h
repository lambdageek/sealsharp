
#ifndef _SEAL_C_ENCRYPTION_PARAMETERS_H
#define _SEAL_C_ENCRYPTION_PARAMETERS_H

#include <stdint.h>

#include <seal-c/types.h>
#include <seal-c/c-decl.h>

BEGIN_SEAL_C_DECL

SEALEncryptionParametersRef
SEAL_EncryptionParameters_construct_BFV (void);

SEALEncryptionParametersRef
SEAL_EncryptionParameters_construct_CKKS (void);

void
SEAL_EncryptionParameters_set_poly_modulus_degree (SEALEncryptionParametersRef parms, uint32_t degree);

void
SEAL_EncryptionParameters_set_coeff_modulus (SEALEncryptionParametersRef parms, SEALCoeffModulusRef coeff_modulus);

void
SEAL_EncryptionParameters_set_plain_modulus (SEALEncryptionParametersRef parms, SEALSmallModulusRef small_modulus);

SEALSmallModulusRef
SEAL_EncryptionParameters_get_plain_modulus (SEALEncryptionParametersRef parms);

void
SEAL_EncryptionParameters_destroy (SEALEncryptionParametersRef parms);

END_SEAL_C_DECL

#endif
