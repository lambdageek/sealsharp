/* A wrapper around a std::shared_ptr<seal::SEALContext> */

#ifndef _SEAL_C_SHARED_CONTEXT_HPP
#define _SEAL_C_SHARED_CONTEXT_HPP

#include <memory>

#include <seal/encryptionparams.h>

#include <seal-c/types.h>
#include "wrap.hpp"

namespace seal_c {
	class SEALSharedContext {
	public:
		SEALSharedContext () = delete;
		SEALSharedContext (const SEALSharedContext&) = delete;
		SEALSharedContext& operator= (const SEALSharedContext&) = delete;
		explicit SEALSharedContext (std::shared_ptr<seal::SEALContext> ptr) : ptr_(ptr) {}
		~SEALSharedContext () = default;

		std::shared_ptr<seal::SEALContext> get_context () const { return ptr_; }
	private:
		std::shared_ptr<seal::SEALContext> ptr_;
	};

	namespace wrap {
		template <>
		struct Wrap<SEALSharedContext*> : public WrapPair<SEALSharedContext*, SEALSharedContextRef> {};
		template <>
		struct Unwrap<SEALSharedContextRef> : public WrapPair<SEALSharedContext*, SEALSharedContextRef> {};
	} // namespace wrap

	namespace shared_context {
		std::unique_ptr<SEALSharedContext>
		create (const seal::EncryptionParameters& parms, bool expand_mod_chain);
	} // namespace shared_context

} //namespace seal_c

#endif
