
#ifndef _SEAL_C_ENCODER_H
#define _SEAL_C_ENCODER_H

#include <stdint.h>

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

void
SEAL_AbstractIntegerEncoder_destroy (SEALAbstractIntegerEncoderRef encoder);

SEALPlaintextRef
SEAL_AbstractIntegerEncoder_encode_int64 (SEALAbstractIntegerEncoderRef encoder, int64_t i);

SEALPlaintextRef
SEAL_AbstractIntegerEncoder_encode_int32 (SEALAbstractIntegerEncoderRef encoder, int32_t i);

int64_t
SEAL_AbstractIntegerEncoder_decode_int64 (SEALAbstractIntegerEncoderRef encoder, SEALPlaintextRef plaintext);

SEALIntegerEncoderRef
SEAL_IntegerEncoder_construct (SEALSmallModulusRef plain_modulus, uint64_t base);

END_SEAL_C_DECL

#endif
