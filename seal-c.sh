#! /bin/bash

set -e

# get the absolute directory name where this file is located
topdir="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

# directory where we'll put the seal-c build
mkdir -p "${topdir}/build/seal-c"

# directory where we'll put the build results
mkdir -p "${topdir}/build/install"

# build seal-c
pushd "${topdir}/build/seal-c"
cmake "-DCMAKE_INSTALL_PREFIX=${topdir}/build/install" "-DCMAKE_PREFIX_PATH=${topdir}/build/install" "${topdir}/seal-c"
make
make install
popd
