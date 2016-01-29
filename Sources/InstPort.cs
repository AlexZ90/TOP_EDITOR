using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEditor
{
  class InstPort
  {
      public Instance inst;
      public Port port;

      public InstPort(Instance inst_init, Port port_init)
      {
        inst = inst_init;
        port = port_init;
      }
    

  }
}
