#include <seal/memorymanager.h>
#include <seal-c/memory_pool_handle.h>

#include "memory_pool_handle.hpp"
#include "wrap.hpp"

void
SEAL_MemoryPoolHandle_destroy (SEALMemoryPoolHandleRef pool)
{
	delete seal_c::wrap::unwrap (pool);
}