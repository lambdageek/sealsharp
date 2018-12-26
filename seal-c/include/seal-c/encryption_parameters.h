
#ifndef _SEAL_C_ENCRYPTION_PARAMETERS_H
#define _SEAL_C_ENCRYPTION_PARAMETERS_H

#include <seal-c/types.h>
#include <seal-c/c-decl.h>

BEGIN_SEAL_C_DECL

SEALEncryptionParametersRef
SEAL_EncryptionParameters_construct_BFV (void);

SEALEncryptionParametersRef
SEAL_EncryptionParameters_construct_CKKS (void);

void
SEAL_EncryptionParameters_destroy (SEALEncryptionParametersRef parms);

END_SEAL_C_DECL

#endif
