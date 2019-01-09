#include <seal/evaluator.h>
#include <seal-c/evaluator.h>
#include "evaluator.hpp"
#include "wrap.hpp"

void
SEAL_Evaluator_destroy (SEALEvaluatorRef evaluator)
{
	delete seal_c::wrap::unwrap (evaluator);
}

SEALEvaluatorRef
SEAL_Evaluator_construct (SEALSharedContextRef context)
{
	auto p = std::make_unique<seal::Evaluator> (seal_c::wrap::unwrap (context)->get_context ());
	return seal_c::wrap::wrap (p.release ());
}