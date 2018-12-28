
#ifndef _SEAL_C_DECRYPTOR_H
#define _SEAL_C_DECRYPTOR_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALDecryptorRef
SEAL_Decryptor_construct (SEALSharedContextRef context, SEALSecretKeyRef secret_key);

SEALPlaintextRef
SEAL_Decryptor_decrypt_new (SEALDecryptorRef decryptor, SEALCiphertextRef ciphertext, SEALBoolean *success);

void
SEAL_Decryptor_destroy (SEALDecryptorRef decryptor);

END_SEAL_C_DECL

#endif
