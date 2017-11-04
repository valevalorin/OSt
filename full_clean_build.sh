rm -rf dist

./build_submodules.sh
./pre_build_copy.sh
cp -r OSt/bin/Debug dist