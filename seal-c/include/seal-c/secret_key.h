
#ifndef _SEAL_C_SECRET_KEY_H
#define _SEAL_C_SECRET_KEY_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

void
SEAL_SecretKey_destroy (SEALSecretKeyRef secret_key);

END_SEAL_C_DECL

#endif
