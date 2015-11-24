using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEditor
{
  class Connection
  {

    public string Name;
    public List<Instance> listOfInsts;
    public List<Port> listOfPorts;
    public int external;


    public Connection(string cName, int ext)
    {
      Name = cName;
      listOfInsts = new List<Instance>();
      listOfPorts = new List<Port>();
      external = ext;
    }

    public void addInstPort (Instance inst, Port port)
    {
      listOfInsts.Add(inst);
      listOfPorts.Add(port);
    }

    public void removeInstPort(Instance inst, Port port)
    {

      Instance instance;

    }


  }
}
