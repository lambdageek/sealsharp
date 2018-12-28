
#include <seal/ciphertext.h>

#include <seal-c/ciphertext.h>

#include "ciphertext.hpp"

#include "wrap.hpp"

void
SEAL_Ciphertext_destroy (SEALCiphertextRef ciphertext)
{
	delete seal_c::wrap::unwrap (ciphertext);
}
