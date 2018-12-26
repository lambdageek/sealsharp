
# sealsharp - C# bindings for SEAL

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

4. Build the C# bindings

       cd sealsharp
       msbuild


