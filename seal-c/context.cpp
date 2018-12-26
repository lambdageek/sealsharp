
#include <memory>

#include <seal-c/context.h>
#include <seal-c/c-decl.h>

#include <SEAL/context.h>

#include "wrap.hpp"
#include "shared_context.hpp"
#include "encryption_parameters.hpp"


namespace seal_c {
	namespace shared_context {
		std::unique_ptr<SEALSharedContext>
		create (const seal::EncryptionParameters& parms, bool expand_mod_chain)
		{
			auto ctx = seal::SEALContext::Create (parms, expand_mod_chain);
			return std::make_unique<SEALSharedContext> (ctx);
		}
	} // namespace shared_context
	
} // namespace seal_c

SEALSharedContextRef
SEAL_Context_Create (SEALEncryptionParametersRef parms, SEALBoolean expand_mod_chain)
{
	namespace wrap = seal_c::wrap;
	auto p = seal_c::shared_context::create (*wrap::unwrap (parms), expand_mod_chain);
	return wrap::wrap (p.release ());
}

void
SEAL_SEALSharedContext_destroy (SEALSharedContextRef shared_context)
{
	delete seal_c::wrap::unwrap (shared_context);
}
