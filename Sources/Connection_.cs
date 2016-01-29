using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEditor
{
  class Connection_
  {

    public string Name;
    public List<InstPort> listOfInstPort;
    public int external;





    public Connection_(string cName, int ext)
    {
      listOfInstPort = new List<InstPort>();
      Name = cName;
      external = ext;
    }

    public void addInstPort (Instance inst, Port port)
    {
      listOfInstPort.Add(new InstPort(inst, port));
    }

    public void removeInstPort(Instance inst, Port port)
    {

      foreach (InstPort instPortVal in listOfInstPort)
      {
        if (instPortVal.inst == inst && instPortVal.port == port)
          listOfInstPort.Remove(instPortVal);
      }
    }

    public bool InstPortExists (Instance inst, Port port)
    {

      foreach (InstPort instPortVal in listOfInstPort)
      {
        if (instPortVal.inst == inst && instPortVal.port == port)
        {
          return true;
        }
      }
      return false;
    }

    public void printInstPorts()
    {
      Console.WriteLine("************* Start print InstPorts ***************");
      foreach (InstPort instPortVal in listOfInstPort)
      {
        Console.WriteLine(instPortVal.inst.Name + " " + instPortVal.port.name);
      }
      Console.WriteLine("************* End print InstPorts ***************");
    }

  }
}
