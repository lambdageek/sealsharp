#ifndef _SEAL_C_MEMORY_POOL_HANDLE_HPP
#define _SEAL_C_MEMORY_POOL_HANDLE_HPP

#include <seal/memorymanager.h>
#include <seal-c/types.h>
#include "wrap.hpp"

namespace seal_c {
	namespace wrap {
		template<>
		struct Wrap<seal::MemoryPoolHandle*> : public WrapPair<seal::MemoryPoolHandle*, SEALMemoryPoolHandleRef> {};

		template<>
		struct Unwrap<SEALMemoryPoolHandleRef> : public WrapPair<seal::MemoryPoolHandle*, SEALMemoryPoolHandleRef> {};
	} // namespace wrap
} // namespace seal_c

#endif
