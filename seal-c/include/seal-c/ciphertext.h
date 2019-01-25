
#ifndef _SEAL_C_CIPHERTEXT_H
#define _SEAL_C_CIPHERTEXT_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>
#include <seal/ciphertext.h>

BEGIN_SEAL_C_DECL

SEALCiphertextRef
SEAL_Ciphertext_construct (SEALSharedContextRef context);

void
SEAL_Ciphertext_destroy (SEALCiphertextRef ciphertext);

int64_t
SEAL_Ciphertext_size (SEALCiphertextRef ciphertext);

END_SEAL_C_DECL

#endif
