using System;

namespace SEAL {
	public class MemoryPoolHandle {
		internal Internal.MemoryPoolHandle handle;

		internal MemoryPoolHandle (Internal.MemoryPoolHandle h)
		{
			handle = h;
		}
	}
}
