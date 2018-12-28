
#include <seal/plaintext.h>

#include <seal-c/plaintext.h>

#include "plaintext.hpp"

#include "wrap.hpp"

void
SEAL_Plaintext_destroy (SEALPlaintextRef plaintext)
{
	delete seal_c::wrap::unwrap (plaintext);
}
