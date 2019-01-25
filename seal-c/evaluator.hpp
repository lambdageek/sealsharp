#ifndef _SEAL_C_EVALUATOR_HPP
#define _SEAL_C_EVALUATOR_HPP

#include <seal/evaluator.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::Evaluator*> : public WrapPair<seal::Evaluator*, SEALEvaluatorRef> {};

		template<>
		struct Unwrap<SEALEvaluatorRef> : public WrapPair<seal::Evaluator*, SEALEvaluatorRef> {};
	} // namespace wrap
} // namespace seal_c

#endif