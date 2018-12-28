
#ifndef _SEAL_C_ENCODER_H
#define _SEAL_C_ENCODER_H

#include <stdint.h>

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

void
SEAL_AbstractIntegerEncoder_destroy (SEALAbstractIntegerEncoderRef encoder);

SEALIntegerEncoderRef
SEAL_IntegerEncoder_construct (SEALSmallModulusRef plain_modulus, uint64_t base);

END_SEAL_C_DECL

#endif
