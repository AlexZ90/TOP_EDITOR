using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEditor
{
  class Instance
  {

    public string Name;
    public Module BaseModule;

    public Instance(string name, Module module)
    {
      Name = name;
      BaseModule = module;
    }

    public void setName(string newName)
    {
      Name = newName;
    }

    public string getName()
    {
      return Name;
    }

    public void setBaseModule (Module newBaseModule)
    {
      BaseModule = newBaseModule;
    }

    public Module getBaseModule()
    {
      return BaseModule;
    }

  }
}
