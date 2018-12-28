/*
 * Opaque C types
 */
#ifndef _SEAL_C_TYPES_H
#define _SEAL_C_TYPES_H

/* SEAL uses the C++ bool type, which isn't in C.  So use an int */
typedef int SEALBoolean;

/* Various classes defined by SEAL. */
typedef struct SEALOpaqueContext *SEALContextRef;
typedef struct SEALOpaqueEncryptionParameters *SEALEncryptionParametersRef;
typedef struct SEALOpaqueKeyGenerator *SEALKeyGeneratorRef;
typedef struct SEALOpaqueSmallModulus *SEALSmallModulusRef;
typedef struct SEALOpaquePublicKey *SEALPublicKeyRef;
typedef struct SEALOpaqueSecretKey *SEALSecretKeyRef;
typedef struct SEALOpaqueEncryptor *SEALEncryptorRef;
typedef struct SEALOpaqueDecryptor *SEALDecryptorRef;
typedef struct SEALOpaquePlaintext *SEALPlaintextRef;
typedef struct SEALOpaqueCiphertext *SEALCiphertextRef;

/* Encoder hierarchy */
typedef struct SEALOpaqueAbstractIntegerEncoder *SEALAbstractIntegerEncoderRef;
typedef struct SEALOpaqueIntegerEncoder *SEALIntegerEncoderRef;

/* This next group are a set of types that are not just classes defined in SEAL. */

/* Wraps a std::shared_ptr<SEALContext> */
typedef struct SEALOpaqueSharedContext *SEALSharedContextRef;

/* Wraps a std::vector<SmallModulus> */
typedef struct SEALOpaqueCoeffModulus *SEALCoeffModulusRef;

#endif
