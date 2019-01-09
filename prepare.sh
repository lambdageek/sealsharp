#! /bin/bash

OPTNINJA=`which ninja`
if [ $? -eq 0 ]; then
    GENERATOR="-GNinja"
else
    GENERATOR=""
fi

set -e

# get the absolute directory name where this file is located
topdir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

# directory where we'll put SEAL
mkdir -p "${topdir}/build/install"

# build with -fPIC
EXTRA_CMAKE_OPTIONS="-DCMAKE_POSITION_INDEPENDENT_CODE=ON"

# build SEAL and put it in the install directory
mkdir -p "${topdir}/build/seal"
pushd "${topdir}/build/seal"
echo "topdir is ${topdir}"
cmake "${GENERATOR}" "-DCMAKE_INSTALL_PREFIX=${topdir}/build/install" "${EXTRA_CMAKE_OPTIONS}" "${topdir}/external/SEAL/src"
cmake --build . --target all
cmake --build . --target install
popd


# build the seal examples
mkdir -p "${topdir}/build/seal-examples"
pushd "${topdir}/build/seal-examples"
cmake "${GENERATOR}" "-DCMAKE_PREFIX_PATH=${topdir}/build/install" "${topdir}/external/SEAL/examples"
cmake --build . --target all
popd
