using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEditor
{
  class Connection
  {

    public string Name;
    public Instance inst_1;
    public Port inst_1_port;
    public Instance inst_2;
    public Port inst_2_port;


    public Connection(string cName, Instance inst1, Port inst1_port, Instance inst2, Port inst2_port)
    {
      Name = cName;
      inst_1 = inst1;
      inst_1_port = inst1_port;
      inst_2 = inst2;
      inst_2_port = inst2_port;
    }

  }
}
