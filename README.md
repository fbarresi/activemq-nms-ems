# <img src="https://activemq.apache.org/assets/img/activemq_logo_icon_border.png" alt="logo" style="width:128px;"/> Apache.NMS.EMS 

[![Release](https://github.com/fbarresi/activemq-nms-ems/actions/workflows/release.yml/badge.svg)](https://github.com/fbarresi/activemq-nms-ems/actions/workflows/release.yml)
[![NuGet Version](https://img.shields.io/nuget/vpre/Unofficial.Apache.NMS.EMS)](https://www.nuget.org/packages/Unofficial.Apache.NMS.EMS/)

Apache NMS for Tibco EMS Client Library

More information can be found at [http://activemq.apache.org/nms](http://activemq.apache.org/nms)

This is an unofficial fork of the original library, updated to the latest deps and framework.

Disclaimer: this library does not yet implement all functionality of Apache.NMS for the Tibco EMS.
Check out the code for details. 
Most methods are supported, the default example from the apache page and included example demonstrate the correct function. 

### Requirements

This project requires your own version of the TIBCO.EMS Library.
This library cannot be distributed together with this implementation. 
The project itself relies on a private nuget package for resolving and restoring the library. 
To build in your own you will need to provide such a nuget package named `TIBCO.EMS` in the version `1.0.1010`.

You can create your nuget package by placing this nuspec file `TIBCO.EMS.nuspec` near to the library:

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
and execute `nuget pack TIBCO.EMS.nuspec` and 
`nuget push TIBCO.EMS.1.0.1010.nupkg -Source <your-private-nuget-feed>`.


## Don't forget

- Star this repo ⭐
- Open an issue if something is missing or have a bug
- Contribute with pull-requests
