
#ifndef _SEAL_C_ENCRYPTOR_H
#define _SEAL_C_ENCRYPTOR_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALEncryptorRef
SEAL_Encryptor_construct (SEALSharedContextRef context, SEALPublicKeyRef public_key);

void
SEAL_Encryptor_destroy (SEALEncryptorRef encryptor);

END_SEAL_C_DECL

#endif
