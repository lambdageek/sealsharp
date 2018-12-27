#ifndef _SEAL_C_KEY_GENERATOR_H
#define _SEAL_C_KEY_GENERATOR_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALKeyGeneratorRef
SEAL_KeyGenerator_construct (SEALSharedContextRef context);

void
SEAL_KeyGenerator_destroy (SEALKeyGeneratorRef keygen);

END_SEAL_C_DECL

#endif
