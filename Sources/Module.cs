using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TopEditor
{
  class Module
  {
    public string modName;
    public Port[] listOfPorts = new Port [100];
    int numOfPorts;


    public Module ()
    {
      modName = "defaultModName";
      numOfPorts = 0;
    }

    public void setModName(string newModName)
    {
      modName = newModName;
    }

    public string getModName()
    {
      return modName;
    }


    public void addPort(Port newPort)
    {
      int i = 0;
      for (i = 0; i < listOfPorts.Length; i++)
        if (listOfPorts[i] == null)
        {
          listOfPorts[i] = newPort;
          numOfPorts++;
          break;
        }      
    }

    public void delPort(int portNum)
    {
      listOfPorts[portNum] = null;
    }

    public Port getPort(string portName)
    {
      int i = 0;
      for (i = 0; i < listOfPorts.Length; i++)
        if (listOfPorts[i] != null && listOfPorts[i].name == portName)
        {
          return listOfPorts[i];
        }
      return null;
    }

    public int getNumOfPorts()
    {
      return numOfPorts;
    }

    public void showModDeclaration()
    {
      string ports = "";
      int i = 0;

      for (i = 0; i < listOfPorts.Length; i++)
        if (listOfPorts[i] != null)
        {
          ports = ports + " port № " + i.ToString() + " " + listOfPorts[i].getPortDeclaration() + "\n";
        }
      MessageBox.Show(modName + "\n" + ports);
    }



  }
}
