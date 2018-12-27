
#include <stdint.h>

#include <memory>
#include <vector>
#include <map>

#include <seal/defaultparams.h>

#include <seal-c/coeff_modulus.h>

#include "coeff_modulus.hpp"

#include "wrap.hpp"

namespace seal_c {
	
} // namespace seal_c

SEALCoeffModulusRef
SEAL_coeff_modulus_128 (uint32_t degree)
{
	// TODO: seal/defaultparams.h says only a few possible degrees are
	// valid.  Sanity check here (or in C#) that we only see those values.
	auto p = std::make_unique<seal_c::SEALCoeffModulus> (seal::coeff_modulus_128 (static_cast <size_t> (degree)));
	return seal_c::wrap::wrap (p.release ());
}

void
SEAL_SEALCoeffModulus_destroy (SEALCoeffModulusRef coeff_modulus)
{
	delete seal_c::wrap::unwrap (coeff_modulus);
}
