using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TopEditor
{
    class Analyzer
    {
      private FileStream fs = null;
      private int start_pos = 0;
      private char[] id = null;

      public struct port
      {

        public int dim;
        public string name;
        public string data_type;
        public string dir;

        public port(int dim_init, string name_init, string data_type_init, string dir_init)
        {
          dim = dim_init;
          name = name_init;
          data_type = data_type_init;
          dir = dir_init;
        }
      }


      public Analyzer ()
      {
        fs = null;  
        start_pos = 0;
        id = null;
      }
      
      // public void openFile (string path)
      // {
            // try
            // {
              // fs = new FileStream(path, FileMode.Open);
            // }
            // catch (Exception ex)
            // {
                // Console.WriteLine("The open file process failed: {0}", ex.ToString());
            // }        
      // }

      public int search_ID(ref string id)
      {

        int state = 0;
        int id_ind = 0;
        char[] buf = new char[1];
        int read_res = 0;

        id = "";
        fs.Seek ((long)start_pos, SeekOrigin.Begin);
        // printf ("Search ID \n\r");
        while (true)
        {
          switch (state)
          {
            case 0:
            {
              if ((read_res = fs.ReadByte()) == -1)
              {
                Console.Write("search_ID: End of fs has been reached\n\r");
                return (-1);
              }
              buf[0] = (char)read_res;
              if ((buf[0]>=65 && buf[0]<=90) || (buf[0]>=97 && buf[0]<=122) || buf[0]=='_')
              {
                id = id + buf[0].ToString();
                id_ind++;
                start_pos++;
                state = 1;
              }
              else
              {
                return 0;
              }
              break;
            }
            case 1:
            {
              if ((read_res = fs.ReadByte()) == -1)
              {
                Console.Write("search_ID: End of fs has been reached\n\r");
                return 1;
              }
              buf[0] = (char)read_res;
              if ((buf[0]>=65 && buf[0]<=90) || (buf[0]>=97 && buf[0]<=122) || (buf[0]>=48 && buf[0]<=57) || buf[0]=='_' || buf[0]=='$')
              {
                id = id + buf[0].ToString();
                id_ind++;
                start_pos++;
                state = 1;
              }
              else
              {
               // id = id + '\0'.ToString();
                return 1;
              }
              break;
            }
            default: Console.Write("Default\n\r"); return 0;
          }
        }
      }

      public int search_SQBR ()
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        
        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_SQBR: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]=='[' || buf[0]==']')
        {
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }
      
      public int search_COLON ()
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        
        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_COLON: End of fs has been reached\n\r");
          return (-1);
        }
        
        buf[0] = (char)read_res;
        if (buf[0]==':')
        {
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }      
      
      
      public int search_NUM (ref string id)
      {

        int state = 0;
        char[] buf = new char[1];
        int id_ind = 0;
        int read_res = 0;

        id = "";
        // Console.WriteLine ("Search NUM \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);
        while (true)
        {

          if ((read_res = fs.ReadByte()) == -1)
          {
            Console.WriteLine("search_NUM: End of fs has been reached\n\r");
            if (id_ind > 0) return 1;
            else return (-1);
          }
          
          buf[0] = (char)read_res;
          if (buf[0]>=48 && buf[0]<=57)
          {
            id = id + buf[0].ToString();
            id_ind++;
            start_pos++;
          }
          else if (id_ind>0)
          {
            id = id + '\0'.ToString();
            return 1;
          }
          else
          {
            return 0;
          }
        }

      }      


      public int search_BR ()
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;          

        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_BR: End of fs has been reached\n\r");
          return (-1);
        }
        
        buf[0] = (char)read_res;        
        if (buf[0]=='(' || buf[0]==')')
        {
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }
      
      public int search_SEMICOLON ()
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;

        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_SEMICOLON: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]==';')
        {
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }   
      
      public int search_COMMA ()
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;

        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine ("Search comma: End of fs has been reached\n\r");
          return (-1);
        }
        
        buf[0] = (char)read_res;
        if (buf[0]==',')
        {
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }      
            
            
      public int fail (int state)
      {

          if (state != 7) return (state+1);
          else
          {
            // Console.WriteLine ("Unknown\n\r");
            start_pos++;
            return 0;
          }
      }
      
      public int next_token (ref string id)
      {

          char[] buf = new char[1];
          int state = 0;
          int func_res = 0;
          int read_res = 0;

          while (true)
          {
            switch (state)
            {
              case 0:
              {
                fs.Seek ((long)start_pos, SeekOrigin.Begin);
                if ((read_res = fs.ReadByte()) == -1)
                {
                  Console.WriteLine ("next token: End of fs has been reached\n\r");
                  return (-1);
                }
                buf[0] = (char)read_res;
                if ((buf[0] == ' ') || (buf[0] == '	') || (buf[0] == '\n') || (buf[0] == '\r'))
                {
                  start_pos++;
                  state = 0;
                }
                else
                {
                  state = this.fail(state);
                }
                break;
              }
              case 1:
              {
                func_res = this.search_ID (ref id);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("ID not found\n\r");
                  state = this.fail(state);
                }
                else if (func_res == 1)
                {
                  state = 0;                
                  if (id == "logic")
                    // Console.WriteLine ("find KW: %s\n\r", id);
                    return 1;
                  else if (id == "module")
                    // Console.WriteLine ("find KW: %s\n\r", id);
                    return 1;
                  else if (id == "input")
                    // Console.WriteLine ("find KW: %s\n\r", id);
                    return 1;
                  else if (id == "output")
                    // Console.WriteLine ("find KW: %s\n\r", id);
                    return 1;
                  else if (id == "inout")
                    // Console.WriteLine ("find KW: %s\n\r", id);
                    return 1;
                  else
                    // Console.WriteLine ("find ID: %s\n\r", id);
                    return 2;
                }
                break;
              }

              case 2:
              {
                func_res = this.search_SQBR();
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state);
                }
                else if (func_res == 1)
                {
                  // Console.WriteLine ("find square brace\n\r");
                  state = 0;
                  return 3;
                }
                break;
              }

              case 3:
              {
                func_res = this.search_COLON ();
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state);
                }
                else if (func_res == 1)
                {
                  // Console.WriteLine ("find COLON\n\r");
                  state = 0;
                  return 4;
                }
                break;
              }

              case 4:
              {
                func_res = this.search_NUM (ref id);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state);
                }
                else if (func_res == 1)
                {
                  // Console.WriteLine ("find NUM: %s\n\r", id);
                  state = 0;
                  return 5;
                }
                break;
              }

              case 5:
              {
                func_res = this.search_BR();
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state);
                }
                else if (func_res == 1)
                {
                  // Console.WriteLine ("find brace\n\r");
                  state = 0;
                  return 6;
                }
                break;
              }

              case 6:
              {
                func_res = this.search_SEMICOLON();
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state);
                }
                else if (func_res == 1)
                {
                  // Console.WriteLine ("find semicolon\n\r");
                  state = 0;
                  return 7;
                }
                break;
              }

              case 7:
              {
                func_res = this.search_COMMA();
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state);
                  return 9;
                }
                else if (func_res == 1)
                {
                  // Console.WriteLine ("find COMMA\n\r");
                  state = 0;
                  return 8;
                }
                break;
              }

              default: Console.WriteLine ("Default\n\r"); return 0;
            }
          }

      }      

      public int search_unpacked_dimension (ref string id, ref int dim)
      {


          char[] buf = new char[1];
          int read_res = 0;
          int state = 0;
          int func_res = 0;
          int token = 0;
          int old_start_pos = 0;
          int d1 = 0;
          int d2 = 0;

          old_start_pos = start_pos;

          while (true)
          {
            token = this.next_token (ref id);
            if (token == -1) return (-1);
            switch (state)
            {
              case 0:
              {
                // token = this.next_token (ref id);
                if (token == 3)
                {
                  state = 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  return 0;
                }
                break;
              }
              case 1:
              {
                // token = this.next_token (ref id);
                if (token == 5)
                {
                  d1 = Convert.ToInt32(id);
                  state = 2;
                }
                else
                {
                  start_pos=old_start_pos;
                  return (-2);
                }
                break;
              }

              case 2:
              {
                // token = this.next_token (ref id);
                if (token == 4)
                {
                  state = 3;
                }
                else
                {
                  start_pos=old_start_pos;
                  return (-2);
                }
                break;
              }

              case 3:
              {
                // token = this.next_token (ref id);
                if (token == 5)
                {
                  d2 = Convert.ToInt32(id);
                  state = 4;
                }
                else
                {
                  start_pos=old_start_pos;
                  return (-2);
                }
                break;
              }

              case 4:
              {
                // token = this.next_token (ref id);
                if (token == 3)
                {
                  if (d1>d2) dim = d1-d2+1;
                  else dim = d2-d1+1;
                  return 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  return (-2);
                }
                break;
              }
            }
          }
      }
      
      public int search_port (ref int dim, ref string name, ref string data_type, ref string dir, int port_declared)
      {


          char[] buf = new char[256];
          int state = 0;
          int func_res = 0;
          int token = 0;
          int old_start_pos = 0;
          int new_start_pos = 0;
          int d1 = 0;
          int d2 = 0;
          string id = "";


          old_start_pos = start_pos;

          while (true)
          {
            //Console.WriteLine ("state = %d\n\r", state);
            switch (state)
            {
              case 0:
              {
                token = this.next_token (ref id);
                if (token == -1) return (-1);
                Console.WriteLine(id);
                if (token == 1 && ((id == "input") || (id == "output") || (id == " inout")))
                {
                  dir = id;
                  state = 1;
                }
                else if ((port_declared == 1) && (token == 2))
                {
                    name = id;
                    return 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  return 0;
                }
                break;
              }

              case 1:
              {
                new_start_pos = start_pos;
                token = this.next_token (ref id);
                if (token == -1) return (-1);
                if ((token == 1) && (id == "logic"))
                {
                  data_type = id;
                  state = 2;
                }
                else
                {
                  start_pos=new_start_pos;
                  state = 2;
                }
                break;
              }

              case 2:
              {
                dim = 1;
                func_res = this.search_unpacked_dimension (ref id, ref dim);
                if (func_res == -1) return (-1);
                if (func_res == -2)
                {
                  start_pos=old_start_pos;
                  return (-2);
                }
                else
                {
                  //Console.WriteLine ("DIMENSION d=%d !!!!\n\r", *dim);
                  state = 3;
                }
                break;
              }

              case 3:
              {
                token = this.next_token (ref id);
                if (token == -1) return (-1);
                if (token == 2)
                {
                  name = id;
                  return 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  return (-2);
                }
                break;
              }
            }
          }
      }      
      
      

      public int search_module(ref Module newModule)
      {


          int state = 0;
          int func_res = 0;
          int token = 0;
          string buf = "";
          string data_type = "";
          string mod_name = "";
          string dir = "";
          string name = "";
          string id = "";
          // long start_pos = 0;
          int port_declared = 0;
          int end_search = 0;
          int dim = 0;
          port[] ports = new port[512];
          int port_number = 0;
          int i = 0;
          Port newPort;

          while (true)
          {
            Console.WriteLine("State = " + state.ToString());
            switch (state)
            {
              case 0:
                token = this.next_token (ref id);
                if (token == -1) return (-1);
                if ((token == 1) && (id == "module")) state = 7;
                else if (token == 0) return 0;
                break;
              case 7:
                token = this.next_token (ref id);
                if (token == 2)
                {
                  mod_name = id;
                  Console.WriteLine ("module name = " + mod_name + "\n\r");
                  state = 1;
                }
                else return (-2);
                break;
              case 1:
                token = this.next_token (ref id);
                if (token == -1) return (-1);                
                if (token == 6) state = 2;
                else if (token == 7)
                {
                  newModule.setModName(mod_name);
                  Console.WriteLine ("End\n\r");
                  return 1;
                }
                else
                {
                  Console.WriteLine ("Error\n\r");
                  return (-2);
                }
                break;
              case 2:
                  func_res = this.search_port (ref dim, ref name, ref data_type, ref dir, port_declared);
                  //Console.WriteLine ("func_res = %d\n\r",func_res);
                  if (func_res == -1) return (-1);                  
                  if (func_res == 1)
                  {
                    Console.WriteLine ("FOUND " + dir + " PORT " + name + " size " + dim + " data type " + data_type + "\n\r");
                    ports[port_number].dir = dir;
                    ports[port_number].name = name;
                    ports[port_number].dim = dim;
                    ports[port_number].data_type = data_type;
                    port_number++;

                    port_declared = 1;
                    state = 3;
                  }
                  else  if (func_res == 0 && port_declared == 0)
                  {
                    state = 3;
                  }
                  else  if ((func_res == 0 && port_declared == 1) || func_res == -2)
                  {
                    Console.WriteLine ("Error\n\r");
                    return (-2);
                  }
                  break;
              case 3:
                token = this.next_token (ref id);
                if (token == -1) return (-1);                  
                if (token == 8) state = 2;
                else if (token == 6) state = 4;
                else
                {
                  Console.WriteLine ("Error\n\r");
                  return (-2);
                }
                break;
              case 4:
                token = this.next_token (ref id);
                if (token == -1) return (-1);
                if (token == 7)
                {
                  Console.WriteLine ("End\n\r");

                  using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\"+ mod_name +".txt"))
                  {
                        
                    file.WriteLine("mod_name: " + mod_name);
                    newModule.setModName(mod_name);
                    for (i = 0; i < port_number; i++)
                    {
                      file.Write(ports[i].dir + " ");
                      file.Write(ports[i].data_type + " ");
                      file.Write(ports[i].name + " ");
                      file.Write(ports[i].dim.ToString() + " ");
                      newPort = new Port(ports[i].dim, ports[i].name, ports[i].data_type, ports[i].dir);
                      newModule.addPort(newPort);
                      //newModule.showModDeclaration();
                      file.WriteLine();
                    }
                    file.Close();
                  }
                  return 1;
                }
                else
                {
                  Console.WriteLine ("Error\n\r");
                  return (-2);
                }
                break;
            }
          }
      }

      public Module[] analizeFile(string path)
      {
        int func_res = 0;
        int i = 0;
        Module newModule;
        Module[] listOfModules = new Module[100];
        for (i = 0; i < listOfModules.Length; i++) listOfModules[i] = null;
        i = 0;
        start_pos = 0;
          try
          {
            fs = new FileStream(path, FileMode.Open);
            fs.Seek(0, SeekOrigin.Begin);
            while (true)
            {
              newModule = new Module();
              func_res = this.search_module(ref newModule);
              if (func_res == -2 || func_res == -1)
              {
                Console.WriteLine("analizeFile: End of file");

                break;
              }
              else
              {
                listOfModules[i] = newModule;
                //listOfModules[i].showModDeclaration();
                Console.WriteLine("Found module");
                i++;
              }
              
            }
            fs.Close();
            return listOfModules;
          }
          catch (Exception ex)
          {
            Console.WriteLine("The open file process failed: {0}", ex.ToString());
            return listOfModules;
          }  

      }
      
      public void show_1 ()
      {
        Console.WriteLine ("Show 1 \n");      
        Console.WriteLine ("start_pos =  {0}", start_pos);
        start_pos++;
        Console.WriteLine ("start_pos =  {0}\n", start_pos);
      }
      
      public void show_2 ()
      {
        Console.WriteLine ("Show 2 \n");
        Console.WriteLine ("start_pos =  {0}", start_pos);
        start_pos++;
        Console.WriteLine ("start_pos =  {0}\n", start_pos);
      }

    }
}
