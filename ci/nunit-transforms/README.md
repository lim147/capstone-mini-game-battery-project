This script is used to convert the NUnit XML test report generated by Unity into
a JUnit XML report so that the GitLab pipeline can pick it up and display tests which have broken between commits in merge requests.

The XSLT file used for the conversion is from [here](https://github.com/jenkinsci/nunit-plugin/blob/master/src/main/resources/hudson/plugins/nunit/nunit-to-junit.xsl) and is used under the MIT License. 
