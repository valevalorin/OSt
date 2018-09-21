rm -rf dist

./build_submodules.sh
./pre_package_copy.sh
cp -r OSt/bin/Release dist
zip -r OSt-Win-$(cat version.txt).zip dist