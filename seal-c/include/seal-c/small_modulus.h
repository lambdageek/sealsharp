
#ifndef _SEAL_C_SMALL_MODULUS_H
#define _SEAL_C_SMALL_MODULUS_H

#include <stdint.h>

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

SEALSmallModulusRef
SEAL_SmallModulus_construct (uint64_t value);

void
SEAL_SmallModulus_destroy (SEALSmallModulusRef small_modulus);

END_SEAL_C_DECL

#endif
