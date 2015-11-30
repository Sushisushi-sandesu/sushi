#!/bin/sh

package="LeapMotionCoreAsset_2_3_1.unitypackage"

mkdir resources

echo "Attempting to download and extract ${package}..."
[ -f resources/${package}.zip ] || curl -o resources/${package}.zip -L https://github.com/leapmotion-examples/LeapMotionCoreAssets/releases/download/v2.3.1/${package}.zip
unzip resources/${package}.zip -d resources

echo "Attempting to import ${package}..."
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -projectPath $(pwd) \
  -logFile $(pwd)/log/import-package.log \
  -importPackage $(pwd)/resources/$package \
  -quit

status=$?
cat $(pwd)/log/import-package.log
exit $status
