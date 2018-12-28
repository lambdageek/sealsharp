
#ifndef _SEAL_C_DECRYPTOR_H
#define _SEAL_C_DECRYPTOR_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALDecryptorRef
SEAL_Decryptor_construct (SEALSharedContextRef context, SEALSecretKeyRef secret_key);

void
SEAL_Decryptor_destroy (SEALDecryptorRef decryptor);

END_SEAL_C_DECL

#endif
