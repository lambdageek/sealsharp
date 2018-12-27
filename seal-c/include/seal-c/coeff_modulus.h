
#ifndef _SEAL_C_COEFF_MODULUS_H
#define _SEAL_C_COEFF_MODULUS_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALCoeffModulusRef
SEAL_coeff_modulus_128 (uint32_t degree);

void
SEAL_SEALCoeffModulus_destroy (SEALCoeffModulusRef coeff_modulus);

END_SEAL_C_DECL

#endif
