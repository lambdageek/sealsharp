#include <seal/evaluator.h>
#include <seal-c/evaluator.h>

#include "evaluator.hpp"
#include "ciphertext.hpp"
#include "wrap.hpp"
#include "shared_context.hpp"
#include "relin_keys.hpp"

namespace seal_c {
	bool add (seal::Evaluator& evaluator, const seal::Ciphertext& ciphertext1, 
		     const seal::Ciphertext& ciphertext2, seal::Ciphertext& dest) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			evaluator.add (ciphertext1, ciphertext2, dest);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

	bool multiply (seal::Evaluator& evaluator, const seal::Ciphertext& ciphertext1, 
		           const seal::Ciphertext& ciphertext2, seal::Ciphertext& dest) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			evaluator.multiply (ciphertext1, ciphertext2, dest);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

	bool relinearize (seal::Evaluator& evaluator, const seal::Ciphertext& ciphertext, 
		             const seal::RelinKeys& relin_keys, seal::Ciphertext& dest) noexcept
	{
		// TODO: don't just use a boolean, also write string error message to some buffer
		try {
			evaluator.relinearize (ciphertext, relin_keys, dest);
			return true;
		} catch (std::invalid_argument) {
			return false;
		}
	}

} // namespace seal_c

void SEAL_Evaluator_destroy (SEALEvaluatorRef evaluator)
{
	delete seal_c::wrap::unwrap (evaluator);
}

SEALEvaluatorRef SEAL_Evaluator_construct (SEALSharedContextRef context)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	auto ctx = unwrap (context)->get_context ();
	auto p = std::make_unique <seal::Evaluator> (ctx);
	return wrap (p.release ());
}

SEALCiphertextRef SEAL_Evaluator_add (SEALEvaluatorRef evaluator, SEALCiphertextRef ciphertext1, 
	                                  SEALCiphertextRef ciphertext2, SEALBoolean *success)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	if (!success)
		throw new std::logic_error ("success must not be a nullptr");
	auto result = std::make_unique <seal::Ciphertext> ();
	if (seal_c::add (*unwrap (evaluator), *unwrap (ciphertext1), *unwrap (ciphertext2), *result)) {
		*success = 1;
		return wrap (result.release ());
	} else {
		*success = 0;
		return wrap<seal::Ciphertext*> (nullptr);
	}
}

SEALCiphertextRef SEAL_Evaluator_multiply (SEALEvaluatorRef evaluator, SEALCiphertextRef ciphertext1, 
	                					   SEALCiphertextRef ciphertext2, SEALBoolean *success)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	if (!success)
		throw new std::logic_error ("success must not be a nullptr");
	auto result = std::make_unique <seal::Ciphertext> ();
	if (seal_c::multiply (*unwrap (evaluator), *unwrap (ciphertext1), *unwrap (ciphertext2), *result)) {
		*success = 1;
		return wrap (result.release ());
	} else {
		*success = 0;
		return wrap<seal::Ciphertext*> (nullptr);
	}
}

SEALCiphertextRef SEAL_Evaluator_relinearize (SEALEvaluatorRef evaluator, SEALCiphertextRef ciphertext, 
	                					      SEALRelinKeysRef relin_keys, SEALBoolean *success)
{
	using seal_c::wrap::unwrap;
	using seal_c::wrap::wrap;
	if (!success)
		throw new std::logic_error ("success must not be a nullptr");
	auto result = std::make_unique <seal::Ciphertext> ();
	if (seal_c::relinearize (*unwrap (evaluator), *unwrap (ciphertext), *unwrap (relin_keys), *result)) {
		*success = 1;
		return wrap (result.release ());
	} else {
		*success = 0;
		return wrap<seal::Ciphertext*> (nullptr);
	}
}


