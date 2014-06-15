using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TopEditor
{
  class Port
  {
        public string id;
        public int dim;
        public string name;
        public string data_type;
        public string dir;

        public Port (int dim_init, string name_init, string data_type_init, string dir_init)
        {
          dim = dim_init;
          name = name_init;
          data_type = data_type_init;
          dir = dir_init;
        }

        public string getPortId()
        {
          return id;
        }

        public int getPortDim()
        {
          return dim;
        }

        public string getPortDataType()
        {
          return data_type;
        }

        public string getPortDir()
        {
          return dir;
        }

        public string getPortDeclaration()
        {
          return (dir + " " + data_type + " " + dim.ToString() + " " + name);
        }

        public void setPortId(string idIint)
        {
          id = idIint;
        }

        public void setPortDim(int dimInit)
        {
          dim = dimInit;
        }

        public void setPortDataType(string dataTypeInit)
        {
          data_type = dataTypeInit;
        }

        public void setPortDir(string dirInit)
        {
          dir = dirInit;
        }

        public void showPort()
        {
          MessageBox.Show(dir + " " + data_type + " " + dim.ToString() + " " + name);
        }

  }
}
