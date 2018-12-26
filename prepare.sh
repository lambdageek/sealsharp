#! /bin/bash

set -e

# get the absolute directory name where this file is located
topdir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

# directory where we'll put SEAL
mkdir -p "${topdir}/build/install"

# build SEAL and put it in the install directory
pushd "${topdir}/build/seal"
echo "topdir is ${topdir}"
cmake "-DCMAKE_INSTALL_PREFIX=${topdir}/build/install" "${topdir}/external/SEAL/src"
make
make install
popd


# build the seal examples
mkdir -p "${topdir}/build/seal-examples"
pushd "${topdir}/build/seal-examples"
cmake "-DCMAKE_PREFIX_PATH=${topdir}/build/install" "${topdir}/external/SEAL/examples"
make
popd
