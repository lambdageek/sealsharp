#include <seal/ciphertext.h>
#include <seal-c/ciphertext.h>
#include <type_traits>

#include "ciphertext.hpp"
#include "wrap.hpp"
#include "shared_context.hpp"

SEALCiphertextRef
SEAL_Evaluator_construct (SEALSharedContextRef context)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	auto ctx = unwrap (context)->get_context ();
	auto p = std::make_unique <seal::Ciphertext> (ctx);
	return wrap (p.release ());
}

void
SEAL_Ciphertext_destroy (SEALCiphertextRef ciphertext)
{
	delete seal_c::wrap::unwrap (ciphertext);
}

int64_t
SEAL_Ciphertext_size (SEALCiphertextRef ciphertext)
{
	//static_assert (std::is_same<seal::Ciphertext::size_type, uint64_t>::value);
	return seal_c::wrap::unwrap (ciphertext)->size ();
}