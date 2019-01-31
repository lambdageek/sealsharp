
# sealsharp - C# bindings and API for SEAL

## Introduction

Sealsharp is a user-friendly API for SEAL. Computations are represented using C# LINQ Expressions to form an abstract syntax tree. Sealsharp analyzes and compiles this expression tree using C# bindings to the underlying SEAL library.

More info about SEAL:
Team -  https://www.microsoft.com/en-us/research/project/homomorphic-encryption/
Github - https://github.com/Microsoft/SEAL/tree/aa7bf57aa11a91d9ca8712816550ae68793add99

## Example

Define expression:

    Expression<Func<long, long, long>> e = (x, y) => x + (x * y);

Evaluate expression:

    Ciphertext c = evaluator.CompileAndRun(e, relin_keys, encrypted_l, encrypted_m);

## Building

### Dependencies and prerequisites

Install `cmake` (OSX: `brew install cmake`) and
[Mono](https://www.mono-project.com/downloads).  You will also need a C++
compiler.

### Building

1. Make sure to initialize submodules when you first clone or when you pull changes.

       git submodule update --init --recursive

2. Run `prepare.sh` to build SEAL and install it into `./build/install`

       ./prepare.sh

   The above will also compile the SEAL examples into `./external/SEAL/bin/sealexamples`

3. Compile the `seal-c` C bindings

       ./seal-c.sh

   This will leave a shared library in `./build/install/lib/` named `libseal-c.so` or `libseal-c.dylib`

4. Build the C# bindings and example

       msbuild /t:Restore
	   msbuild

5. Run the C# example

       mono build/sealsharp-example/bin/Debug/net462/sealsharp-example.exe

The last two steps could also be done from Visual Studio by opening `sealsharp.sln` and building from the menus
