/*
 * Utilities for working with wrapped types
 */

#ifndef _SEAL_C_WRAP_HPP
#define _SEAL_C_WRAP_HPP

namespace seal_c {
	namespace wrap {
		template <typename unwrapped_type>
		struct Wrap;

		template <typename wrapped_type>
		struct Unwrap;

		// use to define Wrap<> and Unwrap<> specializations
		//
		// template<> struct Wrap<Foo*> : public WrapPair<Foo*, FooOpaqueRef> {};
		// template<> struct Unwrap<FooOpaqueRef> : public WrapPair<Foo*, FooOpaqueRef> {};
		template <typename Unwrapped, typename Wrapped>
		struct WrapPair
		{
			typedef Unwrapped unwrapped_type;
			typedef Wrapped wrapped_type;
		};

		template <typename Unwrapped>
		static inline
		typename Wrap<Unwrapped>::wrapped_type wrap (Unwrapped u)
		{
			return reinterpret_cast<typename Wrap<Unwrapped>::wrapped_type> (u);
		}

		template <typename Wrapped>
		static inline
		typename Unwrap<Wrapped>::unwrapped_type unwrap (Wrapped w)
		{
			return reinterpret_cast<typename Unwrap<Wrapped>::unwrapped_type> (w);
		}
	} // namespace wrap
} // namespace seal_c

#endif
