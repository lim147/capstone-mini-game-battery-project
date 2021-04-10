#!/usr/bin/env bash

set -x

echo "Combining play mode and edit mode code coverage reports"

CODE_COVERAGE_PACKAGE="com.unity.testtools.codecoverage"
PACKAGE_MANIFEST_PATH="Packages/manifest.json"

echo "Removing CodeCoverage folder"
rm -rf CodeCoverage
echo "Making CodeCoverage folder"
mkdir CodeCoverage
echo "Moving playmode and editmode coverage artifacts into CodeCoverage"
mv playmode-coverage CodeCoverage
mv playmode-results.xml CodeCoverage
mv editmode-coverage CodeCoverage
mv editmode-results.xml CodeCoverage

${UNITY_EXECUTABLE:-unity-editor} -batchmode -debugCodeOptimization -enableCodeCoverage -coverageOptions "generateHtmlReport;generateBadgeReport" -quit -nographics

UNITY_EXIT_CODE=$?

if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

if grep $CODE_COVERAGE_PACKAGE $PACKAGE_MANIFEST_PATH; then
  cat $(pwd)/CodeCoverage/Report/Summary.xml | grep Linecoverage
else
  {
    echo -e "\033[33mCode Coverage package not found in $PACKAGE_MANIFEST_PATH. Please install the package \"Code Coverage\" through Unity's Package Manager to enable coverage reports.\033[0m"
  } 2> /dev/null
fi
exit $UNITY_EXIT_CODE
