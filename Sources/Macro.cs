using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopEditor
{
  class Macro
  {

    public string Name { get; set; }
    public string Argument { get; set; }
    public string Value { get; set; }

    public Macro()
    {
      this.Name = "";
      this.Argument = "";
      this.Value = "";
    }



  }
}
