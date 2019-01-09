cmake_minimum_required (VERSION 3.10)

project (seal-c LANGUAGES C CXX)

add_library (seal-c SHARED)

# Import SEAL
find_package(SEAL 3.1.0 EXACT REQUIRED)

# Link SEAL
target_link_libraries(seal-c PRIVATE SEAL::seal)

# C++ sources
target_sources (seal-c
  PRIVATE
    coeff_modulus.cpp
    encryption_parameters.cpp
    context.cpp
    key_generator.cpp
    small_modulus.cpp
    encoder.cpp
    public_key.cpp
    secret_key.cpp
    encryptor.cpp
    decryptor.cpp
    plaintext.cpp
    ciphertext.cpp
    evaluator.cpp
)

# include directories.
# public headers are under the include dir in the source tree when compiling,
# or in the include dir in the install prefix for clients of this library.
#
# Also there are private C++ headers in the current source dir.
target_include_directories (seal-c
  PUBLIC
    $<INSTALL_INTERFACE:include>
    $<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}/include>
  PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}
)
  
# use GNU style install paths relative to the prefix
include(GNUInstallDirs)

# install the shared library into the lib directory in the install prefix
install(TARGETS seal-c
  LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR})

# install the public header files in to the includedir in the install prefix
install(DIRECTORY include/ DESTINATION ${CMAKE_INSTALL_INCLUDEDIR})
