#ifndef _SEAL_C_MEMORY_POOL_HANDLE_H
#define _SEAL_C_MEMORY_POOL_HANDLE_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

void
SEAL_MemoryPoolHandle_destroy (SEALMemoryPoolHandleRef pool);

END_SEAL_C_DECL

#endif