#ifndef _SEAL_C_EVALUATOR_H
#define _SEAL_C_EVALUATOR_H

#include <seal-c/c-decl.h>
#include <seal-c/types.h>

BEGIN_SEAL_C_DECL

void
SEAL_Evaluator_destroy (SEALEvaluatorRef evaluator);

SEALEvaluatorRef
SEAL_Evaluator_construct (SEALSharedContextRef context);

END_SEAL_C_DECL

#endif