
# sealsharp - C# bindings for SEAL

## Building

## Dependencies and prerequisites

Install `cmake` (OSX: `brew install cmake`) and
[Mono](https://www.mono-project.com/downloads).  You will also need a C++
compiler.

## Building

1. Make sure to initialize submodules when you first clone or when you pull changes.

    git submodule update --init --recursive

2. Run `prepare.sh` to build SEAL and install it into `./build/install`

    ./prepare.sh

  The above will also compile the SEAL examples into `./external/SEAL/bin/sealexamples`

