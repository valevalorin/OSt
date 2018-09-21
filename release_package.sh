rm -rf dist

./build_submodules.sh
./pre_package_build.sh
cp -r OSt/bin/Release dist
cd dist
zip -r ../OSt-Win-$(cat ../version.txt).zip *
rm -rf ../dist