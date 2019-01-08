
# How to add new bindings #

## How to add a method for an existing C++ class ##

If you want to export a new C++ method for a class that has already been exported.

Suppose we wanted to add the method `seal::Plaintext seal::AbstractIntegerEncoder::encode (std::int32_t)`
that is declared in `external/SEAL/src/seal/encoder.h`

In this case, the class (`seal::AbstractIntegerEncoder`) to which the method
belongs, and the argument type (`std::int32_t`) and the result type
(`seal::Plaintext`) are all already have C counterparts and are also exposed in C#.

In that case we do the following:

1. Add a new method declaration to `seal-c/include/seal-c/include/seal-c/encoder.h`

   The method name will be `SEAL_AbstractIntegerEncoder_encode_int32` (normally
   the name can just be `SEAL_<className>_<methodName>` but since there are
   multiple `encode` methods taking different arguments in
   `AbstractIntegerEncoder`, we append `_int32` to disambiguate it from
   `SEAL_AbstrctIntegerEncoder_encode_int64`)
   
   The method will use the opaque C struct refs from `seal-c/include/seal-c/types.h`:
   
   * The result type `seal::Plaintext` becomes `SEALPlaintextRef` (Which is
     just defined as `struct SEALOpaquePlaintext*` that is, a pointer to a C
     struct called `SEALOpaquePlaintext`.  This opaque struct is not defined
     anywhere - it only exists to introduce a way to pass the C++ class
     `seal::Plaintext` back and forth through the C binding library.)
	 
   * The `this` argument, `seal::AbstractIntegerEncoder` is passed as a
     `SEALAbstractIntegerEncoderRef`.
	 
   * The `int32_t` integer is just passed as is.
   
   * Make sure the method declaration is enclosed by the pair of
     `BEGIN_SEAL_C_DECL`/`END_SEAL_C_DECL` macros in the header - that
     establishes that this function will be exported with C-style naming, even
     though (as we will see next) it's implemented in C++.
   
2. Add the implementation of the new method to `seal-c/encoder.cpp`

	The implementation has to do four things:
	
	1. Unwrap all the wrapped arguments to get pointers to C++ classes instead of pointers to opaque C structs.
	2. Call the seal library C++ methods.
	3. Allocate new dynamic memory for the result C++ classes (most of the SEAL
       library methods return results on the stack, which doesn't interoperate
       well with the C# marshalling).
    4. Wrap the pointer to the result C++ class to make a pointer to an opaque C struct.
	
	```
	SEALPlaintextRef
	SEAL_AbstractIntegerEncoder_encode_int32 (SEALAbstrctIntegerEncoderRef encoder, int32_t i)
	{
		seal::AbstractIntegerEncoder* encoder_ptr = seal_c::wrap::unwrap (encoder); // 1
		seal::Plaintext result_value = encoder_ptr->encode (i); // 2
		seal::Plaintext *result_ptr = new seal::Plaintext (result_value); // 3
		SEALPlaintextRef result = seal_c::wrap::wrap (result_ptr); // 4
		return result;
	}
	```
	
	Each of the lines in the function above performs the corresponding step.
	
	We can make the implementation above a bit less repetitive by not making
    intermediate variables for the unwrapped values.  We can also use `auto` to avoid having to specify some variable's types.
	
	Finally, we use
    [`std::make_unique`](https://en.cppreference.com/w/cpp/memory/unique_ptr/make_unique)
    to dynamically allocate the memory for us into a
    [`std::unique_ptr<>`](https://en.cppreference.com/w/cpp/memory/unique_ptr)
    (This isn't strictly necesssary for this short function, but it is useful
    in more involved examples so that we don't leak memory in case there are
    errors).  In this case it is important also to call the `release` method on
    the `unique_ptr` when we return the object from this function.  (If we did
    not, the `unique_ptr` would delete the object when the function returns)
	
	```
	SEALPlaintextRef
	SEAL_AbstractIntegerEncoder_encode_int32 (SEALAbstrctIntegerEncoderRef encoder, int32_t i)
	{
		seal::Plaintext result_value = seal_c::wrap::unwrap (encoder)->encode (i); // 1 and 2
		auto result_ptr = std::make_unique <seal::Plaintext> (result_value); // 3, using a unique_ptr<Plaintext>
		return seal_c::wrap::wrap (result_ptr.release ()); // 4 and release() and return
	}
	```
	
3. At this point we've exported the new function from the C library.  We can try building

   ```
   ./seal-c.sh
   ```
   
   This will update `build/install/lib/libseal-c.so` (`libseal-c.dylib` on macOS) to export our new function.
   
4. Now we can add the method to the `SEAL.Internal.AbstractIntegerEncoder` C#
   class in `sealsharp/SEAL/Internal/AbstractIntegerEncoder.cs`
   
   First we import the method from the shared library:
   
   ```
   [DllImport (SEALC.Lib)]
   private static extern Plaintext SEAL_AbstractIntegerEncoder_encode_int32 (AbstractIntegerEncoder encoder, int i);
   ```
   
   (by default the name of the C# method will be used as the name to look up in
   the C shared library.  The [`DllImport`
   attribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.dllimportattribute?view=netframework-4.7.2)
   has properties to override the name if necessary.  But it shouldn't be
   necessary.)
   
   The way the C# marshalling works is that any class that derives from
   `SafeHandle` (in this case `SEAL.Internal.Plaintext` and
   `SEAL.Internal.AbstractIntegerEncoder`) will be marshalled between C# and C
   as a pointer to an opaque C struct.  Exactly what we have.  The `int` is
   marshalled as a `int32_t` - C# integers are always 32 bits.

   Next we add a non-`static` `public` method with a nicer name that calls the C method
   
   ```
   public Plaintext EncodeLong (long l)
   {
       return SEAL_AbstractIntegerEncoder_encode_int32 (this, l);
   }
   ```
   
   Note that we pass `this` for the first argument, since
   `SEAL.Internal.AbstractIntegerEncoder` is a `SafeHandle` this will pass a
   pointer to the an opaque C struct to the C method.  (The actual pointer
   value is in
   [`SafeHandle.handle`](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle.handle?view=netframework-4.7.2),
   and the C# marshalling knows that that's the value to pass to the C code.
   You should only ever refer to it explicitly when writing a `destroy`
   method - more on that in the next section about wrapping a new class.)
   
5. Finally, we need to expose the new internal `SEAL.Internal.AbstractIntegerEncoder` method via a new public method in `SEAL.AbstractIntegerEncoder` method
   in `sealsharp/SEAL/AbstractIntegerEncoder.cs`
   
   
   ```
   public Plaintext Encode (int i)
   {
       return new Plaintext (Handle.EncodeInt (i));
   }
   ```
   
   Things to note here: `SEAL.Plaintext` is created from
   `SEAL.Internal.Plaintext` using its `Plaintext (Internal.Plaintext
   internalPlaintext)` constructor.  Every non-abstract public `SEAL` class `Foo` has an
   internal constructor that takes a `Internal.Foo` handle as an argument.
   
   We use the `Handle` property to get the
   `SEAL.Internal.AbstractIntegerEncoder` object of the current
   `SEAL.AbstractIntegerEncoder`.  Every public `SEAL` class has a `handle`
   field or `Handle` property.  (The reason some classes have a property is
   when we need to represent a C++ class hierarchy in a C# class hierarchy.  We
   store a `handle` field in the non-abstract leaf classes and a virtual
   `Handle` property in the abstract parent classes)
   
   The public method just delegates to the method on the internal handle object.
   
   The public method is named `Encode (int i)` rather than `EncodeInt` for
   stylistic reasons - people in C# prefer to have methods the same name taking
   various arguments, rather than different methods with different names for
   each type of argument.  (The internal class could have also done this, but
   it seemed better to stay a bit in sync with the C code).
)   
6. At this point we can build the C# library.  (Either in visual studio, or using `msbuild` from the command line).

7. Next we can try calling the method in `sealsharp-example/Main.cs`

   ```
   int i = 17;
   Plaintext encoded_int = encoder.Encode (i);
   ```

   This should run and encode the integer. If we also had the corresponding
   decoding function implemented, we could check that encoding and decoding
   both work.
   
   
## How to add a new C++ classes ##

Suppose we want to add the C++ class `seal::Evaluator` from
`external/SEAL/src/seal/evaluator.h` as a new class `SEAL.Evaluator` in C#.

1. First we need to make an opaque C struct for the class.  We'll call it `SEALEvaluatorRef`.

   In `seal-c/include/seal-c/types.h` add:

   ```
   typedef struct SEALOpaqueEvaluator *SEALEvaluatorRef;
   ```

   The C struct `SEALOpaqueEvaluator` will be a struct with no definition - it
   only exists so that we can wrap `seal::Evalutor *` (a pointer to a C++
   class) as a `SEALEvaluatorRef` when we need to pass arguments or return
   values of that type.
   
2. Next we need to teach the wrapper machinery about the new type.

   Create a new file `seal-c/evaluator.hpp` with this boilerplate:
   
   ```
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
   ```
   
   This will make it so that `seal_c::wrap::wrap` can take a `seal::Evaluator*`
   and turn it into a `SEALEvaluatorRef`, and `seal_c::wrap::unwrap` can turn a
   `SEALEvaluatorRef` back into a `seal::Evaluator*`.
   
   (A common source of compiler errors in laters steps is forgetting to
   `#include "evaluator.hpp"` in which case you will get a horrible mess of
   `clang` errors when you use `wrap` or `unwrap`)
   
3. Next we create a new C header that will be used to declare the C wrapper
   functions for all the methods of `SEALEvaluatorRef`.  At the beginning we
   will add the single most important method `SEAL_Evaluator_destroy` which
   will be used to call the destructor.
   
   In `seal-c/include/seal-c/evaluator.h` define:
   
   ```
   #ifndef _SEAL_C_EVALUATOR_H
   #define _SEAL_C_EVALUATOR_H
   
   #include <seal-c/c-decl.h>
   #include <seal-c/types.h>
   
   BEGIN_SEAL_C_DECL
   
   void
   SEAL_Evaluator_destroy (SEALEvaluatorRef evaluator);
   
   END_SEAL_C_DECL
   
   #endif
   ```
   
   (Note that the `include/` header file ends in `.h`, while the
   `seal-c/evaluator.hpp` internal header ends in `.hpp` - this is just a
   convention, but it's being used to distinguish the headers that will form
   the C API, from the headers that have C++ details that are internal to the
   seal-c binding.)
   
4. Next in `seal-c/evaluator.cpp` we will implement `SEAL_Evaluator_destroy`

   ```
   #include <seal/evaluator.h>      // 1
   
   #include <seal-c/evaluator.h>    // 2
   #include "evaluator.hpp"         // 3
   #include "wrap.hpp"               // 4
   
   void
   SEAL_Evaluator_destroy (SEALEvaluatorRef evaluator)
   {
   	delete seal_c::wrap::unwrap (evaluator);   // 5
   }
   ```
   
   The implementation is:
   
   1. We include the header that defines the C++ `seal::Evaluator class`
   2. We also include the header that declares the C `SEAL_Evaluator_destroy` function
   3. And also the header that defines how to `wrap` and `unwrap` an evaluator
   4. And also the header that defines the `wrap`/`unwrap` machinery in general
   5. Finally in the implementation we call `delete` on the `SEAL::Evaluator*`
      that we get by unwrapping the `SEALEvaluatorRef` argument that we will
      get from the C# world.
	  
5. At this point we should be able to build the C library

   ```
   ./seal-c.sh
   ```

6. Next we add a new internal C# class derived from `SafeHandle` that will
   represent a `SEALEvaluatorRef` in C#.
   
   In `sealsharp/SEAL/Internal/Evaluator.cs`:
   
   ```
   using System;
   using System.Runtime.InteropServices;
   
   namespace SEAL.Internal {
   	class Evaluator : SafeHandle {
   		/* called by P/Invoke when returning a Evaluator */
   		private Evaluator () : base (IntPtr.Zero, true) {}
   		public override bool IsInvalid {
   			get { return handle == IntPtr.Zero; }
   		}
   
   		protected override bool ReleaseHandle ()
   		{
   			SEAL_Evaluator_destroy (handle);
   			return true;
   		}
   
   		[DllImport (SEALC.Lib)]
   		private static extern void SEAL_Evaluator_destroy (IntPtr handle);
   	}
   }
   ```
   
   Everything here is boilerplate.  But the important details are:
   
   1. The class `Evaluator` derives from `SafeHandle`.  If the C++ class
      `seal::Evaluator` had a base class we would instead derive the
      `SEAL.Internal.Evaluator` class from the base class internal C# class.
      (for example `seal::IntegerEncoder` has the base class
      `seal::AbstractIntegerEncoder` and so `SEAL.Internal.IntegerEncoder` is
      derived from `SEAL.Internal.AbstractIntegerEncoder` instead of from
      `SafeHandle`).
	  
   2. We override `IsInvalid` and `ReleaseHandle`.  The `IsInvalid` override is
      boilerplate - a null pointer is invalid.  The `ReleaseHandle` override is
      calling our new destroy function which is imported using `DllImport`, at
      the end of the file.
	  
   3. We declared a `private` constructor `Evaluator()` that takes no
      arguments.  In general, the `SEAL.Internal` classes don't have any other
      constructors.  The private constructor is used by the marshalling
      machinery in .NET to create `SEAL.Internal.Evaluator` instances whenever
      we will `DllImport` functions with an `Evaluator` as the return type.
	  
7. Add a public C# class `SEAL.Evaluator` to represent evaluator instances.

   In `sealsharp/SEAL/Evaluator.cs`:
   
   ```
   using System;
   
   namespace SEAL {
   	public class Evaluator {
   		internal Internal.Evaluator handle;
   
   		internal Evaluator (Internal.Evaluator h)
   		{
   			handle = h;
   		}
   	}
   }
   ```
   
   This, again, is all boilerplate.  The important points are:
   
   1. If `seal::Evaluator` had a baseclass, then we would again make
      `SEAL.Evaluator` derive from the base class.  (For example:
      `SEAL.IntegerEncoder` derives from `SEAL.AbstractIntegerEncoder`).
   2. We add a new internal constructor `Evaluator (Internal.Evaluator h)` that
      makes a `SEAL.Evaluator` from a `SEAL.Internal.Evaluator`.  This will be
      used whenever we add new public methods that need to return an
      `Evaluator` instance.  (See how we created a `Plaintext` from an
      `Internal.Plaintext` in the "adding a method" example).
	  
   3. The internal handle is stored in a field with `internal` visibility.
      This is used to access the `SEAL.Internal.Evaluator` when we implement
      methods that take a `SEAL.Evaluator` as an argument.
      
	  For classes that are base classes (e.g. `SEAL.AbstractIntegerEncoder`) we
      would instead have a `internal abstract` `Handle` property getter that we
      would override in the derived class (ie `SEAL.IntegerEncoder`).  There
      should only be a `handle` field in the classes that don't themselves have
      any subclasses - if a class is a base class, it should just have an
      `abstract` `Handle` property.
	  
8. At this point we should be able to build the SEAL library either using
   Visual Studio or `msbuild` from the command line.

   However since we didn't add any constructors there aren't examples we can
   run since we don't actually have any instances created.

## Adding constructors ##

Continuing the previous example we want to add the constructor for
`seal::Evaluator` which is declared in `external/SEAL/src/seal/evaluator.h` as
`Evaluator(std::shared_ptr<seal::SEALContext> context)`.

This is actually similar to adding a new method to an existing class (ie we
will add a new declaration to `seal-c/include/seal-c/evaluator.h` and an
implementation to `seal-c/evaluator.cpp` followed by new methods in
`SEAL.Internal.Evaluator` and `SEAL.Evaluator`).

The points of note are:

1. `std::shared_ptr<seal::SEALContext>` is represented in C with a
   `SEALSharedContextRef` which actually wraps a `seal_c::SEALSharedContext`
   object defined in `seal-c/shared_context.hpp` - not a
   `std::shared_ptr<seal::SEALContext>` directly.  (This is because there's no
   easy way to work with a `std::shared_ptr<>` from C, so we wrap it in a class
   and then use our usual opaque C struct trick to work with the class).  There
   is otherwise no difference.
   
2. By convention we will call the C function for this constructor `SEAL_Evaluator_construct`

   in `seal-c/include/seal-c/evaluator.h` add
   
   ```
   SEALEvaluatorRef
   SEAL_Evaluator_construct (SEALSharedContextRef context);
   ```
   
   and in `seal-c/evaluator.cpp` add
   
   ```
   SEALEvaluatorRef
   SEAL_Evaluator_construct (SEALSharedContextRef context)
   {
   	auto p = std::make_unique<seal::Evaluator> (seal_c::wrap::unwrap (context)->get_context ());
   	return seal_c::wrap::wrap (p.release ());
   }
   ```

3. In `SEAL.Internal.Evaluator` we will add an `internal` `static` method called `Create`:

   ```
   		public static Evaluator Create (SEALSharedContext context)
   		{
   			return SEAL_Evaluator_construct (context);
   		}
   ```
   
   and the usual `DllImport`
   
   ```
   		[DllImport (SEALC.Lib)]
   		private static extern Evaluator SEAL_Evaluator_construct (SEALSharedContext context);
   ```
   
4. In `SEAL.Evaluator` we will add a new constructor `Evaluator (SEALContext)`

   ```
   		public Evaluator (SEALContext context)
   		{
   			handle = Internal.Evaluator.Create (context.handle);
   		}
   ```
   

As usual we compile the C library with `./seal-c.sh` and then `msbuild` to build the C# library.


We can try to create a new `Evaluator` in the example with:

```
   var evaluator = new Evaluator (context);
```

And that will call our `SEAL_Evaluator_construct` method, followed, soon after,
by `SEAL_Evaluator_destroy` when the evaluator object is garbage collected.the destructor for the
class as a new method.


	
