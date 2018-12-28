#ifndef _SEAL_C_KEY_GENERATOR_H
#define _SEAL_C_KEY_GENERATOR_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALKeyGeneratorRef
SEAL_KeyGenerator_construct (SEALSharedContextRef context);

SEALPublicKeyRef
SEAL_KeyGenerator_get_public_key (SEALKeyGeneratorRef keygen);

SEALSecretKeyRef
SEAL_KeyGenerator_get_secret_key (SEALKeyGeneratorRef keygen);

void
SEAL_KeyGenerator_destroy (SEALKeyGeneratorRef keygen);

END_SEAL_C_DECL

#endif
