
#ifndef _SEAL_C_CONTEXT_H
#define _SEAL_C_CONTEXT_H

#include <seal-c/types.h>
#include <seal-c/c-decl.h>

BEGIN_SEAL_C_DECL

SEALSharedContextRef
SEAL_Context_Create (SEALEncryptionParametersRef parms, SEALBoolean expand_mod_chain);

void
SEAL_SEALSharedContext_destroy (SEALSharedContextRef shared_context);

END_SEAL_C_DECL

#endif
