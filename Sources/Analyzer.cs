using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace TopEditor
{
    class Analyzer
    {
      //private FileStream fs = null;
      //private long start_pos = 0;
      //private char[] id = null;
      static string current_dir = System.IO.Directory.GetCurrentDirectory();
      
      
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
        //fs = null;  
        //start_pos = 0;
        //id = null;
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

      public int search_ID(ref string id, ref long start_pos, FileStream fs)
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

      public int search_SQBR (ref long start_pos, FileStream fs)
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
    
    
      public int search_COLON (ref long start_pos, FileStream fs)
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
      
      
      public int search_NUM (ref string id, ref long start_pos, FileStream fs)
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
            //id = id + '\0'.ToString();
            //id = id;
            return 1;
          }
          else
          {
            return 0;
          }
        }

      }      


      public int search_BR (ref string id, ref long start_pos, FileStream fs)
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0; 
        id = "";

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
          id = buf[0].ToString();
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }
      
      public int search_SEMICOLON (ref long start_pos, FileStream fs)
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
      
      public int search_COMMA (ref long start_pos, FileStream fs)
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
            
      public int search_ARIFM (ref string id, ref long start_pos, FileStream fs)
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        id = "";
        
        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_SQBR: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]=='+' || buf[0]=='-' || buf[0]=='*' || buf[0]=='/')
        {
          id = buf[0].ToString();
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }

      }          
      
      public int search_EQUAL (ref string id, ref long start_pos, FileStream fs)
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        id = "";
        
        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_SQBR: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]=='=')
        {
          id = buf[0].ToString();
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }
      }         
	  
      public int search_QUOTE (ref string id, ref long start_pos, FileStream fs)
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        id = "";
        
        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_SQBR: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]=='"')
        {
          id = buf[0].ToString();
          start_pos++;
          return 1;
        }
        else
        {
          return 0;
        }
      }           
            
      public int fail (int state, ref long start_pos)
      {

          if (state != 10) return (state+1);
          else
          {
            // Console.WriteLine ("Unknown\n\r");
            start_pos++;
            return 0;
          }
      }
      
      public int next_token (ref string id, ref long start_pos, FileStream fs)
      {

          char[] buf = new char[1];
          int state = 0;
          int func_res = 0;
          int read_res = 0;

          
          while (true)
          {
            id = "";
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
                if ((buf[0] == ' ') || (buf[0] == ' ') || (buf[0] == '\n') || (buf[0] == '\r'))
                {
                  start_pos++;
                  state = 0;
                }
                else
                {
                  state = this.fail(state, ref start_pos);
                }
                break;
              }
              case 1:
              {
                func_res = this.search_ID (ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("ID not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  state = 0;                
                  if (id == "logic")
				  {
                    //Console.WriteLine ("fin KW: %s\n\r", id);
                    return 1; //1 Token is keyword
				  }
                  else if (id == "module")
                  {
 				     //Console.WriteLine ("fin KW: %s\n\r", id);
                     return 1;
				  }
                  else if (id == "input")
				  {
                     //Console.WriteLine ("fin KW: %s\n\r", id);
                     return 1;
                  }
				  else if (id == "output")
                  {
				   //Console.WriteLine ("fin KW: %s\n\r", id);
                     return 1;
                  }
				  else if (id == "inout")
                  {
					 //Console.WriteLine ("fin KW: %s\n\r", id);
                     return 1;
                  }
				  else
				  {
                     //Console.WriteLine ("fin ID: %s\n\r", id);
                     return 2; //2 Token is ID
				  }
				}
                break;
              }

              case 2:
              {
                func_res = this.search_SQBR(ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin square brace\n\r");
                  state = 0;
                  return 3; //3 Token is SQBR
                }
                break;
              }

              case 3:
              {
                func_res = this.search_COLON (ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin COLON\n\r");
                  state = 0;
                  return 4;//4 Token is COLON
                }
                break;
              }

              case 4:
              {
                func_res = this.search_NUM (ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin NUM: %s\n\r", id);
                  state = 0;
                  return 5; //5 Token is NUM
                }
                break;
              }

              case 5:
              {
                func_res = this.search_BR(ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin brace\n\r");
                  state = 0;
                  return 6; //6 Token is BRACE
                }
                break;
              }

              case 6:
              {
                func_res = this.search_SEMICOLON(ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin semicolon\n\r");
                  state = 0;
                  return 7; //7 Token is SEMICOLON
                }
                break;
              }

              case 7:
              {
                func_res = this.search_COMMA(ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  // Console.WriteLine ("SQBR not found\n\r");
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                 //Console.WriteLine ("fin COMMA\n\r");
                  state = 0;
                  return 8; //8 Token is COMMA
                }
                break;
              }

              case 8:
              {
                func_res = this.search_ARIFM(ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin ARIFM\n\r");
                  state = 0;
                  return 9; //9 Token is ARIFM
                }
                break;
              }        

              case 9:
              {
                func_res = this.search_EQUAL(ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  state = this.fail(state, ref start_pos);
                  //return 11;
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin EQUAL\n\r");
                  state = 0;
                  return 10; //10 Token is EQUAL
                }
                break;
              }

              case 10:
              {
                func_res = this.search_QUOTE(ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin EQUAL\n\r");
                  state = 0;
                  return 11; //11 Token is QUOTE
                }
                break;
              }   

              default: Console.WriteLine ("Default\n\r"); return 0;
            }
          }

      }      

    public int calculate_expr(string[] in_expr, int in_expr_length)
    {
      Stack inStack = new Stack();
      int i = 0;
      int j = 0;
      int out_expr_length = 0;
      object obj;
      int num_1 = 0;
      int num_2 = 0;
      string[] out_expr = new string[256];

      // Console.WriteLine ("in_expr_length=" + in_expr_length);
      // for (i = 0; i < in_expr_length; i++)
      // {
        // Console.WriteLine ("in_expr = " + in_expr[i] + "*");
      // }
      //From Infix to Reverse Polish Notation
      for (i = 0; i < in_expr_length; i++)
      {
        switch (in_expr[i])
        {
          case "(":
            {
              inStack.Push(in_expr[i]);
              break;
            }

          case ")":
            {
              while (true)
              {
                obj = inStack.Pop();

                if (obj.ToString() == "(")
                {
                  break;
                }
                else if (inStack.Count == 0)
                {
                  //MessageBox.Show("Ошибка в арифметическом выражении!");
                  Console.WriteLine("calculate_expr: Error in arifm expression");
                  return (-1);
                }
                else
                {
                  out_expr[j] = obj.ToString();
                  j++;
                }
              }
              break;
            }

          case "+":
          case "-":
            {
              while (inStack.Count != 0 && (inStack.Peek().ToString() == "*" || inStack.Peek().ToString() == "/" || inStack.Peek().ToString() == "+" || inStack.Peek().ToString() == "-"))
              {
                out_expr[j] = inStack.Pop().ToString();
                j++;
              }
              inStack.Push(in_expr[i]);
              break;
            }

          case "*":
          case "/":
            {
              inStack.Push(in_expr[i]);
              break;
            }

          default://Число
            {
              out_expr[j] = in_expr[i];
              //Console.WriteLine(j + " " + out_expr[j] + " " + i + " " + in_expr[i] + "*");
              j++;
              break;
            }

        }//switch end

      }//for end
      while (inStack.Count != 0)
      {
        out_expr[j] = inStack.Pop().ToString();
        j++;
      }

      // for (i = 0; i < j; i++)
      // {
        // Console.Write(out_expr[i]);
      // }


      //Calculate from Reverse Polish Notation 
      out_expr_length = j;

      for (i = 0; i < out_expr_length; i++)
      {
        switch (out_expr[i])
        {
          case "+":
            {
              num_1 = Convert.ToInt32(inStack.Pop().ToString());
              num_2 = Convert.ToInt32(inStack.Pop().ToString());
              num_1 = num_1 + num_2;
              inStack.Push(num_1.ToString());
              break;
            }

          case "-":
            {
              num_1 = Convert.ToInt32(inStack.Pop().ToString());
              num_2 = Convert.ToInt32(inStack.Pop().ToString());
              num_1 = num_2 - num_1;
              inStack.Push(num_1.ToString());
              break;
            }
          case "*":
            {
              num_1 = Convert.ToInt32(inStack.Pop().ToString());
              num_2 = Convert.ToInt32(inStack.Pop().ToString());
              num_1 = num_1 * num_2;
              inStack.Push(num_1.ToString());
              break;
            }
          case "/":
            {
              num_1 = Convert.ToInt32(inStack.Pop().ToString());
              num_2 = Convert.ToInt32(inStack.Pop().ToString());
              num_1 = num_2 / num_1;
              inStack.Push(num_1.ToString());
              break;
            }
          default:
            {
              inStack.Push(out_expr[i]);
              break;
            }
        }//switch end
      }//for end
        
        return Convert.ToInt32(inStack.Peek().ToString());
    }      
      
      public int search_unpacked_dimension (ref string id, ref int dim, ref long start_pos, FileStream fs)
      {


          char[] buf = new char[1];
          int read_res = 0;
          int state = 0;
          int func_res = 0;
          int token = 0;
          long old_start_pos = 0;
          long old_start_pos_2 = 0;
          int d1 = 0;
          int d2 = 0;
          string [] expr_1 = new string [256];
          string [] expr_2 = new string [256];
          int expr_1_ind = 0;
          int expr_2_ind = 0;
          int r1 = 0;

          old_start_pos = start_pos;

          while (true)
          {
            token = this.next_token (ref id, ref start_pos, fs);
            if (token == -1) return (-2);
            switch (state)
            {
              case 0:
              {
                // token = this.next_token (ref id);
                if (token == 3) // square brace
                {
                  state = 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  return 0; // dimension is not defined
                }
                break;
              }
              case 1:
              {
                old_start_pos_2 = start_pos;
                if (token != 4) 
                {
                  if (token == 2)
                  {
                    r1 = search_param_value(ref id, fs);
                    if (r1 == (-2))
                      {
                        Console.WriteLine ("search_unpacked_dimension: Error in parameter declaration.");
                        start_pos = old_start_pos_2;
                        return (-2);
                      }
                    else if (r1 == -1)
                    {
                        Console.WriteLine ("search_unpacked_dimension: Parameter not found");
                        start_pos = old_start_pos_2;
                        return (-1);
                    }
                    else id = r1.ToString();
                  }
                  start_pos = old_start_pos_2;
                  expr_1[expr_1_ind] = id;
                  expr_1_ind++;
                  state = 1;
                  break;
                }
                else 
                {
                  if (expr_1_ind == 0) return (-2);
                  d1 = calculate_expr(expr_1, expr_1_ind);  
                  //Console.WriteLine ("d1=" + d1); 
                  state = 4;                  
                }

                // token = this.next_token (ref id);
                //if (token == 5) // number
                //{
                //  d1 = Convert.ToInt32(id);
                //  state = 2;
                //}
                // else
                // {
                  // start_pos=old_start_pos;
                  // return (-2); //error
                // }
                break;
              }

              // case 2:
              // {
                // // token = this.next_token (ref id);
                // if (token == 4) //colon ':'
                // {
                  // state = 3;
                // }
                // else
                // {
                  // start_pos=old_start_pos;
                  // return (-2); //error
                // }
                // break;
              // }

              // case 3:
              // {
                // // token = this.next_token (ref id);
                // if (token == 5) // number
                // {
                  // d2 = Convert.ToInt32(id);
                  // state = 4;
                // }
                // else
                // {
                  // start_pos=old_start_pos;
                  // return (-2);
                // }
                // break;
              // }

              case 4:
              {
                // token = this.next_token (ref id);
                if (token != 3) // square brace
                {
                  expr_2[expr_2_ind] = id;
                  expr_2_ind++;
                  state = 4;
                  break;
                }                
                else 
                {
                  if (expr_2_ind == 0) return (-2);                
                  d2 = calculate_expr(expr_2, expr_2_ind);  
                  //calculate dimension
                  if (d1>d2) dim = d1-d2+1; 
                  else dim = d2-d1+1;
                  return 1;
                }
                // else
                // {
                  // start_pos=old_start_pos;
                  // return (-2); // error
                // }
                break;
              }
            }
          }
      }

      public int search_param_value (ref string parameter, FileStream fs)
      {


          char[] buf = new char[1];
          int read_res = 0;
          int state = 0;
          int func_res = 0;
          int token = 0;
          long start_pos = 0;
          int res = 0;
          string id = "";
          string param_name = "";
          
          fs.Seek (0, SeekOrigin.Begin);
          
          while (true)
          {
            switch (state)
            {
              case 0:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                // token = this.next_token (ref id);
                if (token == 2 && id == "parameter") // square brace
                {
                  //Console.WriteLine ("parameter found");
                  state = 4;
                }
                else
                {
                  state = 0;
                }
                break;
              }
              
              case 4:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);
                if (token == 2) 
                {
                  param_name = id;
                  state = 1;
                }
                else 
                {
                  Console.WriteLine ("search_param_value: Error 1");
                  return (-2);
                }
                break;
              }
              
              case 1:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);              
                if (token !=10 ) 
                {
                  Console.WriteLine ("search_param_value: Error 2");
                  return (-2);
                }
                else 
                {
                  state = 2;                  
                }
                break;
              }

              case 2:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);              
                if (token != 5) 
                {
                  Console.WriteLine ("search_param_value: Error 3");
                  return (-2);
                }
                else 
                {
                  res = Convert.ToInt32(id.ToString());
                  state = 3;
                }
                break;
              }
              
              case 3:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);              
                if (token != 7) 
                {
                  Console.WriteLine ("search_param_value: Error 4");
                  return (-2);
                }                
                else 
                {
                  if (param_name == parameter)
                    return res;
                  else 
                  {
                    state = 0;
                    param_name = "";
                  }
                }
                break;
              }
              
            }
          }
      }
      
      public int search_port (ref int dim, ref string name, ref string data_type, ref string dir, int port_declared, ref long start_pos, FileStream fs)
      {


          char[] buf = new char[256];
          int state = 0;
          int func_res = 0;
          int token = 0;
          long old_start_pos = 0;
          long new_start_pos = 0;
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
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                //Console.WriteLine(id);
                if (token == 1 && ((id == "input") || (id == "output") || (id == " inout")))
                {
                  dir = id;
                  data_type = " "; //Порт по умолчанию не logic (но и не null)
                  state = 1;
                }
                else if (port_declared == 1)
                {
                      if ((token == 1) && (id == "logic"))
                      {
                        data_type = id;
                        state = 2;
                      }
                      else if (token == 3)
                      {
                        start_pos = old_start_pos;
                        state = 2;
                      }                      
                      else if (token == 2)
                      {
                          name = id;
                          return 1;
                      }
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
                token = this.next_token (ref id, ref start_pos, fs);
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
                  //Console.WriteLine ("Error 703");
                }
                break;
              }

              case 2:
              {
                dim = 1;
                func_res = this.search_unpacked_dimension (ref id, ref dim, ref start_pos, fs);
                if (func_res == -1) return (-1);
                if (func_res == -2)
                {
                  start_pos=old_start_pos;
                  Console.WriteLine ("search_port: Error 1");
                  return (-1);
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
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                if (token == 2)
                {
                  name = id;
                  return 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  Console.WriteLine ("search_port: Error 2");                 
                  return (-1);
                }
                break;
              }
            }
          }
      }      
      
      

      public int search_module(ref Module newModule, ref long start_pos, FileStream fs)
      {


          int state = 0;
          int func_res = 0;
          int token = 0;
          string buf = "";
          string data_type = " ";
          string mod_name = "";
          string dir = "";
          string name = "";
          string id = "";
          int port_declared = 0;
          int end_search = 0;
          int dim = 0;
          port[] ports = new port[512];
          int port_number = 0;
          int i = 0;
          Port newPort;

          while (true)
          {
            //Console.WriteLine("State = " + state.ToString());
            switch (state)
            {
              case 0:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                if ((token == 1) && (id == "module")) state = 7;
                else if (token == 0) return 0;
                break;
              case 7:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == 2)
                {
                  mod_name = id;
                  Console.WriteLine ("module name = " + mod_name + "\n\r");
                  state = 1;
                }
                else return (-2);
                break;
              case 1:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);                
                if (token == 6) state = 2;
                else if (token == 7)
                {
                  newModule.setModName(mod_name);
                  Console.WriteLine ("End\n\r");
                  return 1;
                }
                else
                {
                  Console.WriteLine ("search_module: Error 1. Incorrect module declaration.");
                  return (-2);
                }
                break;
              case 2:
                  func_res = this.search_port (ref dim, ref name, ref data_type, ref dir, port_declared, ref start_pos, fs);
                  //Console.WriteLine ("func_res = %d\n\r",func_res);
                  if (func_res == -1) return (-2);                  
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
                  else  if (func_res == 0 && port_declared == 1)
                  {
                    Console.WriteLine ("search_module: Error 2. Incorrect module declaration. search_port return: " + func_res);
                    return (-2);
                  }
                  break;
              case 3:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);                  
                if (token == 8) state = 2; // If token is COMMA
                else if (token == 6) state = 4; //If token is BRACE
                else
                {
                  Console.WriteLine ("search_module: Error 3. Token = " + token.ToString() + ". Incorrect module declaration.");
                  return (-2);
                }
                break;
              case 4:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);
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
                  Console.WriteLine ("search_module: Error 4. Incorrect module declaration.");
                  return (-2);
                }
                break;
            }
          }
      }

	  
		private int findIncl (ref string includeFilePath, FileStream fs, ref long start_pos)
        {
          char[] buf = new char[256];
          int state = 0;
          int func_res = 0;
          int token = 0;
          long old_start_pos = 0;
          long new_start_pos = 0;
          int d1 = 0;
          int d2 = 0;
          string id = "";
          int readRes = 0;


          old_start_pos = start_pos;

          while (true)
          {
            //Console.WriteLine ("state = %d\n\r", state);
            switch (state)
            {
              case 0:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                //Console.WriteLine(id);
                if (token == 2 && id == "include")
                {
                  state = 1;
                }
                else
                {
                  return (-3);
                }
                break;
              }

              case 1:
              {
                new_start_pos = start_pos;
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);
                if (token == 11)
                {
                  state = 2;
                }
                else
                {
                  return (-2);
                }
                break;
              }

              case 2:
              {
                while (true)
                {
                  if ((readRes = fs.ReadByte()) == -1)
                  {
                    return (-2);
                  }
                  else 
                  {
                    buf[0] = (char)readRes;
                    if (buf[0] == '"')
                    {
                      return 1;
                    }
                    else
                    {
                      includeFilePath = includeFilePath + buf[0].ToString();
                    }
                  }
                }
                break;
              }
            }
          }
		}
		
        
        private int fileCopy (string includeFilePath, FileStream fsFileToCopy)
        {
          char[] buf = new char[1];
          int readRes = 0;
          
          if (File.Exists(includeFilePath) == false) 
          {
            Console.WriteLine ("Error: Include file " + includeFilePath + " did not found");
            return -1;
          }
          using (FileStream fsIncludeFile = File.Open(includeFilePath, FileMode.Open, FileAccess.Read))
          {
            while (true)
            {
              if ((readRes = fsIncludeFile.ReadByte()) == -1)
              {
                fsIncludeFile.Close();
                return 1;
              }
              else
              {
                fsFileToCopy.WriteByte ((byte)readRes);
              }
            }
          }
        }
        
        private int findInclude (string inFilePath, ref string outFilePath)
        {
          string orgnFilePath = ""; //Исходный файл
          string auxFilePath = ""; //Вспомогательный файл
          int includeFound = 0; // 1 - ранее был найден и распознан include
          int searchBegan = 0; //1 - Поиск includoв был начат
          long filePosition = 0; //Позиция в файле
          int auxFileNamePrefix = 0;
          char[] buf = new char[1];
          int readRes = 0;
          int func_res = 0;
          string includeFilePath = "";//Файл в директиве include
          string auxFileDirectory = System.IO.Path.Combine(current_dir, @".\auxFiles\");

          if (System.IO.Directory.Exists(auxFileDirectory)) System.IO.Directory.Delete(auxFileDirectory, true);
          System.IO.Directory.CreateDirectory(auxFileDirectory);

          while (true)
          {
            if (searchBegan == 0)
            {
              searchBegan = 1;
              orgnFilePath = inFilePath;
              auxFilePath = System.IO.Path.Combine(auxFileDirectory, @".\" + auxFileNamePrefix.ToString()) + ".txt";
            }
            else
            {
              if (includeFound == 1)
              {
                includeFound = 0;
                orgnFilePath = auxFilePath;
                if (auxFileNamePrefix > 0) File.Delete(System.IO.Path.Combine(auxFileDirectory, @".\" + (auxFileNamePrefix - 1).ToString() + ".txt"));
                auxFileNamePrefix++;
                auxFilePath = System.IO.Path.Combine(auxFileDirectory, @".\" + auxFileNamePrefix.ToString() + ".txt");
              }
              else
              {
                outFilePath = auxFilePath;
                return (1);
              }

            }

            using (FileStream fsOrgnFile = File.Open(orgnFilePath, FileMode.Open, FileAccess.Read))
            {
              using (FileStream fsAuxFile = File.Create(auxFilePath))
              {
                while (true)
                {
                  if ((readRes = fsOrgnFile.ReadByte()) == -1)
                  {
                    fsOrgnFile.Close();
                    fsAuxFile.Close();
                    //Console.Write("findInclude: End of fsOrgnFile has been reached\n\r");
                    break;
                    //return (-1);
                  }
                  else 
                  {
                    buf[0] = (char)readRes;
                    if (buf[0] != '`')
                    {
                      fsAuxFile.WriteByte((byte)readRes);
                    }
                    else 
                    {
                      filePosition = fsOrgnFile.Position;
                      includeFilePath = "";
                      func_res = findIncl(ref includeFilePath, fsOrgnFile, ref filePosition);
                      if (func_res == -1 || func_res == -3) //-3 - not include, may be `ifdef
                      {
                        fsAuxFile.WriteByte((byte)readRes);
                        fsOrgnFile.Seek(filePosition, SeekOrigin.Begin);
                      }
                      else if (func_res == -2)
                      {
                        Console.WriteLine("findInclude: Error in `inculde declaration.");
                        return (-1);
                      }                      
                      else
                      {
                        //Console.WriteLine(includeFilePath);
                        if (fileCopy(includeFilePath, fsAuxFile) == -1)
                        {
                          //MessageBox.Show("Can't copy include file to auxFile !");
                          fsOrgnFile.Close();
                          fsAuxFile.Close();
                          File.Delete(auxFilePath);
                          return (-1);
                        }
                        else
                        {
                          includeFound = 1;
                        }


                      }
                    }
                  }
                }
              }
            }
          }


        }
      public int analizeFile(string path, ref Module[] listOfModules)
      {
        int func_res = 0;
        int i = 0;
        long start_pos = 0;
        Module newModule;
        string outFilePath = "";
        for (i = 0; i < listOfModules.Length; i++) listOfModules[i] = null;
        i = 0;

        if (findInclude (path, ref outFilePath) == -1)
        {
          Console.WriteLine("analizeFile: Include processing failed");
          return -1;
        }
        else try
        {
          //Console.WriteLine(outFilePath);
          FileStream fs = new FileStream(outFilePath, FileMode.Open);
          fs.Seek(0, SeekOrigin.Begin);
          while (true)
          {
            newModule = new Module();
            func_res = this.search_module(ref newModule, ref start_pos, fs);
            if (func_res == -2)
            {
              Console.WriteLine("analizeFile: Error in module declaration");
              fs.Close();
              return -2;
            }
            else if (func_res == -1)
            {
              Console.WriteLine("analizeFile: Looking for modules has been finished. End of file has been reached.");
              fs.Close();
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
          return 1;
        }
        catch (Exception ex)
        {
          Console.WriteLine("The open file process failed: {0}", ex.ToString());
          return -3;
        }  

      }
      
      // public void show_1 ()
      // {
        // Console.WriteLine ("Show 1 \n");      
        // Console.WriteLine ("start_pos =  {0}", start_pos);
        // start_pos++;
        // Console.WriteLine ("start_pos =  {0}\n", start_pos);
      // }
      
      // public void show_2 ()
      // {
        // Console.WriteLine ("Show 2 \n");
        // Console.WriteLine ("start_pos =  {0}", start_pos);
        // start_pos++;
        // Console.WriteLine ("start_pos =  {0}\n", start_pos);
      // }

    }
}

