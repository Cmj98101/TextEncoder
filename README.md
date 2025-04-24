# TextEncoder
A windows based application that modifys the .dxf file to accurately scale and correct text in .dxf files for printing.

## Overview

For context, the widely used .dxf file format is interpreted differently by various CAD software due to differing layer structures. This project was created to provide a printing solution for users of less advanced CAD systems, ensuring accurate output and readable text from their files.

Originally developed in Python, this software was subsequently rewritten in C# to achieve significant speed and performance improvements.
**This software is still running in production without issues.**

**Key Features:**

* Reads files one at a time or in bulk
* Looks for problem areas in the file and fixes them.
* Modifies the files in seconds.
  
## Technologies Used
* **Frontend:** C#
* **Backend:** none
* **Database:** none
* **Deployment:** executable file
