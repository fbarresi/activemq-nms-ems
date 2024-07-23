# Apache.NMS.EMS 

[![Release](https://github.com/fbarresi/activemq-nms-ems/actions/workflows/release.yml/badge.svg)](https://github.com/fbarresi/activemq-nms-ems/actions/workflows/release.yml)
[![NuGet Version](https://img.shields.io/nuget/vpre/Unofficial.Apache.NMS.EMS)](https://www.nuget.org/packages/Unofficial.Apache.NMS.EMS/)

Apache NMS for Tibco EMS Client Library

For more information see http://activemq.apache.org/nms

This is an unofficial fork for the original library updated to latest deps and framework.

Disclaimer: this library does not implement all functionality of Apache.NMS for the Tibco EMS yet.
Check out the code for details. 

### Requirements

This project required your own version of the TIBCO.EMS Library.
This library cannot be distributed together with this implementation. 
The project itself leverage on a private nuget package for resolving and restoring the library. 
In order to build in your you will need to add such a nuget package named `TIBCO.EMS` in the version `1.0.1010`.

You can create your nuget package putting this nuspec file `TIBCO.EMS.nuspec` near to the library:

````xml
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd">
    <metadata>
        <id>TIBCO.EMS</id>
        <version>1.0.1010</version>
        <description>Connector for TIBCO Enterprise Message Service</description>
        <authors>TIBCO</authors>    
    </metadata>
    <files>
        <file src="TIBCO.EMS.dll" target="lib" />
    </files>
    
</package>
````
and execute `nuget pack TIBCO.EMS.nuspec` and eventually nuget 
`nuget push TIBCO.EMS.1.0.1010.nupkg -Source <your-private-nuget-feed>`.

## Don't forget

- Star this repo
- Open an issue if something is missing
- Contribute with pull-requests