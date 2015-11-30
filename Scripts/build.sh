#!/bin/sh

project=sushi

echo "Attempting to build $project for OSX..."
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -projectPath $(pwd) \
  -logFile $(pwd)/log/build.log \
  -buildOSXUniversalPlayer $(pwd)/Build/osx/${project}.app \
  -quit

status=$?
cat $(pwd)/log/build.log
exit $status
