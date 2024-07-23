=======================================================================
Welcome to:
 * Apache.NMS.EMS : Apache NMS for Tibco EMS Client Library
=======================================================================

For more information see http://activemq.apache.org/nms

=======================================================================
Building With NAnt 0.86 see http://nant.sourceforge.net/
=======================================================================

NAnt version 0.86 or newer is required to build Apache.NMS.EMS.  Version 0.90
or newer is highly recommended.
To build the code using NAnt, run:

  nant

The NMS documentation can be generated into three different formats using
Microsoft's Sandcastle open source product. The Sandcastle Styles project
was used to enhance the output generated from the current release of Sandcastle.

The Sandcastle project is located here:

http://sandcastle.codeplex.com/

The Sandcastle Styles project is located here:

http://sandcastlestyles.codeplex.com/

To generate the documentation, run:

  nant sandcastle-all

=======================================================================
Building With Visual Studio 2008
=======================================================================

First build the project with nant, this will download and install 
all the 3rd party dependencies for you.

Open the solution File.  Build using "Build"->"Build Solution" 
menu option.

The resulting DLLs will be in build\${framework}\debug or the 
build\${framework}\release directories depending on your settings 
under "Build"->"Configuration Manager"

