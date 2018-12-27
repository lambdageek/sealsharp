/* A wrapper around std::vector<SmallModulus> */

#ifndef _SEAL_C_COEFF_MODULUS_HPP
#define _SEAL_C_COEFF_MODULUS_HPP

#include <vector>

#include <seal/smallmodulus.h>

#include <seal-c/types.h>

#include "wrap.hpp"

namespace seal_c {

	class SEALCoeffModulus {
	public:
		typedef std::vector<seal::SmallModulus> data_type;
		
		SEALCoeffModulus () = delete;
		SEALCoeffModulus (const SEALCoeffModulus&) = delete;
		SEALCoeffModulus& operator= (const SEALCoeffModulus&) = delete;
		explicit SEALCoeffModulus (data_type v) : v_ (std::move (v)) {}
		~SEALCoeffModulus () = default;

		const data_type& get_coeff_modulus () const { return v_; }
	private:
		data_type v_;
	};

	namespace wrap {
		template <>
		struct Wrap<SEALCoeffModulus*> : public WrapPair<SEALCoeffModulus*, SEALCoeffModulusRef> {};
		template <>
		struct Unwrap<SEALCoeffModulusRef> : public WrapPair<SEALCoeffModulus*, SEALCoeffModulusRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
