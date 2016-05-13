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
      public const int TOKEN_KEYWORD      = 1;
      public const int TOKEN_ID           = 2;
      public const int TOKEN_SQBR         = 3;
      public const int TOKEN_COLON        = 4;
      public const int TOKEN_NUM          = 5;
      public const int TOKEN_BR           = 6;
      public const int TOKEN_SEMICOLON    = 7;
      public const int TOKEN_COMMA        = 8;
      public const int TOKEN_ARIFM        = 9;
      public const int TOKEN_EQUAL        = 10;
      public const int TOKEN_QUOTE        = 11;
      public const int TOKEN_GRAVE_ACCENT = 12;
      public const int TOKEN_NUMBER_SIGN  = 13;

      public struct port
      {

        public int dim;
        public string dim_str; //!Добавил
        public string name;
        public string data_type;
        public string dir;

        public port(int dim_init, string dim_str_init, string name_init, string data_type_init, string dir_init) //!Добавил
        {
          dim = dim_init;
          dim_str = dim_str_init; //!Добавил
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
          Console.WriteLine("search_ARIFM: End of fs has been reached\n\r");
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

      public int search_GRAVE_ACCENT (ref string id, ref long start_pos, FileStream fs)
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        id = "";

        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_GRAVE_ACCENT: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]=='`')
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

      public int search_NUMBER_SIGN (ref string id, ref long start_pos, FileStream fs)
      {

        int state = 0;
        char[] buf = new char[1];
        int read_res = 0;
        id = "";

        // Console.WriteLine ("Search SQBR \n\r");
        fs.Seek ((long)start_pos, SeekOrigin.Begin);

        if ((read_res = fs.ReadByte()) == -1)
        {
          Console.WriteLine("search_NUMBER_SIGN: End of fs has been reached\n\r");
          return (-1);
        }
        buf[0] = (char)read_res;
        if (buf[0]=='#')
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

          if (state != 12) return (state+1); //if != last state in next_token
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
                    return TOKEN_KEYWORD; //1 Token is keyword
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
                     return TOKEN_ID; //2 Token is ID
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
                  return TOKEN_SQBR; //3 Token is SQBR
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
                  return TOKEN_COLON;//4 Token is COLON
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
                  return TOKEN_NUM; //5 Token is NUM
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
                  return TOKEN_BR; //6 Token is BRACE
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
                  return TOKEN_SEMICOLON; //7 Token is SEMICOLON
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
                  return TOKEN_COMMA; //8 Token is COMMA
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
                  return TOKEN_ARIFM; //9 Token is ARIFM
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
                  return TOKEN_EQUAL; //10 Token is EQUAL
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
                  return TOKEN_QUOTE; //11 Token is QUOTE
                }
                break;
              }

              case 11:
              {
                func_res = this.search_GRAVE_ACCENT(ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin EQUAL\n\r");
                  state = 0;
                  return TOKEN_GRAVE_ACCENT;
                }
                break;
              }

              case 12:
              {
                func_res = this.search_NUMBER_SIGN(ref id, ref start_pos, fs);
                // Console.WriteLine ("func res = %d\n\r", func_res);
                if (func_res == 0)
                {
                  state = this.fail(state, ref start_pos);
                }
                else if (func_res == 1)
                {
                  //Console.WriteLine ("fin EQUAL\n\r");
                  state = 0;
                  return TOKEN_NUMBER_SIGN;
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

    public int search_unpacked_dimension(ref string id, ref int dim, ref string dim_str, ref long start_pos, FileStream fs) //!Добавил
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

          string expr_1_str = ""; //!Добавил
          string expr_2_str = ""; //!Добавил
          string expr_3_str = ""; //!Добавил

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
                if (token == TOKEN_SQBR) // square brace
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
                if (token != TOKEN_COLON)
                {

                  expr_1_str = expr_1_str + id; //!Добавил

                  /*Временно закомментировал, пока не напишу норамльный поиск параметров и обработку инклудов */
                  /*Комментарии с этой же причиной - TEMP_INCLUDE_COMM */

                  // if (token == TOKEN_ID)
                  // {
                    // r1 = search_param_value(ref id, fs);
                    // if (r1 == (-2))
                      // {
                        // Console.WriteLine ("search_unpacked_dimension: Error in parameter declaration.");
                        // start_pos = old_start_pos_2;
                        // return (-2);
                      // }
                    // else if (r1 == -1)
                    // {
                        // Console.WriteLine ("search_unpacked_dimension: Parameter not found");
                        // start_pos = old_start_pos_2;
                        // return (-1);
                    // }
                    // else id = r1.ToString();
                  // }
                  // start_pos = old_start_pos_2;
                  // expr_1[expr_1_ind] = id;

                  /*************************************************************************************************/

                  expr_1_ind++;

                  state = 1;
                  break;
                }
                else
                {
                  if (expr_1_ind == 0) return (-2);
                  //d1 = calculate_expr(expr_1, expr_1_ind); //TEMP_INCLUDE_COMM
                  Console.WriteLine ("\n\n ***********|expr_1_str = {0}", expr_1_str); //!Добавил
                  state = 4;
                }
                break;
              }

              case 4:
              {
                if (token != TOKEN_SQBR) // square brace
                {
                  expr_2[expr_2_ind] = id;
                  expr_2_ind++;

                  expr_2_str = expr_2_str + id; //!Добавил

                  state = 4;
                  break;
                }
                else
                {
                  if (expr_2_ind == 0) return (-2);

                  Console.WriteLine ("\n\n ***********|expr_2_str = {0}", expr_2_str); //!Добавил

                  d2 = calculate_expr(expr_2, expr_2_ind);
                  //calculate dimension
                  // if (d1>d2) dim = d1-d2+1;  //TEMP_INCLUDE_COMM
                  // else dim = d2-d1+1;        //TEMP_INCLUDE_COMM
                  dim = 999;                    //TEMP_INCLUDE_COMM - убрать эту строку целиком позже
                  expr_3_str = "[" + expr_1_str + ":" + expr_2_str + "]"; //!Добавил
                  Console.WriteLine ("\n\n ***********|expr_3_str = {0}", expr_3_str); //!Добавил
                  dim_str = dim_str + expr_3_str;//!Добавил
                  return 1;
                }
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
                if (token == TOKEN_ID && id == "parameter")
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
                if (token == TOKEN_ID)
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
                if (token !=TOKEN_EQUAL )
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
                if (token != TOKEN_NUM)
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
                if (token != TOKEN_SEMICOLON)
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


      public Parameter search_param(ref long start_pos, FileStream fs, ref int errorCode)
      {


        char[] buf = new char[1];
        int state = 0;
        int token = 0;

        int res = 0;
        string id = "";
        string param_name = "";
        errorCode = 0;
        fs.Seek(start_pos, SeekOrigin.Begin);

        while (true)
        {
          switch (state)
          {
            case 0:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -1;//EOF There is no parameters in the fs
                  return (null);
                }
                if (token == TOKEN_ID && id == "parameter")
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
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token == TOKEN_ID)
                {
                  param_name = id;
                  state = 1;
                }
                else
                {
                  Console.WriteLine("search_param_value: Error 1");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                break;
              }

            case 1:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_EQUAL)
                {
                  Console.WriteLine("search_param_value: Error 2");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                else
                {
                  state = 2;
                }
                break;
              }

            case 2:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_NUM)
                {
                  Console.WriteLine("search_param_value: Error 3");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
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
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_SEMICOLON)
                {
                  Console.WriteLine("search_param_value: Error 4");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                else
                {
                  Parameter p = new Parameter();
                  p.Name = param_name;
                  p.Value = res;
                  errorCode = 0;
                  return (p);
                }
                break;
              }
          }
        }
      }

      public List<Parameter> search_all_params(FileStream fs)
      {
        long start_pos = 0;
        fs.Seek(start_pos, SeekOrigin.Begin);
        Parameter p;
        int errorCode = 0;
        List<Parameter> parameters = new List<Parameter>();
        Console.WriteLine (fs);
        while (true)
        {
          if ((p = search_param(ref start_pos, fs, ref errorCode)) != null)
          {
            parameters.Add(p);
          }
          else if (errorCode == -1)
          {
            //EOF there are no parameters in fs
            return parameters;
          }
          else
          {
            MessageBox.Show("Синтаксическая ошибка при поиске параметров! " + errorCode.ToString());
            return null;
          }
        }
      }

      public List<Macro> search_all_macro(FileStream fs)
      {
        long start_pos = 0;
        fs.Seek(start_pos, SeekOrigin.Begin);
        Macro m;
        int errorCode = 0;
        List<Macro> macros = new List<Macro>();
        Console.WriteLine (fs);
        while (true)
        {
          if ((m = search_macro(ref start_pos, fs, ref errorCode)) != null)
          {
            macros.Add(m);
          }
          else if (errorCode == -1)
          {
            //EOF there are no macros in fs
            return macros;
          }
          else
          {
            MessageBox.Show("Синтаксическая ошибка при поиске макросов! " + errorCode.ToString());
            return null;
          }
        }
      }

      public int search_port(ref int dim, ref string dim_str, ref string name, ref string data_type, ref string dir, int port_declared, ref long start_pos, FileStream fs) //!Добавил
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
          bool id_detected = false;

          dim_str = ""; //!Добавил
          old_start_pos = start_pos;

          while (true)
          {
            //Console.WriteLine ("state = %d\n\r", state);
            switch (state)
            {
              case 0:
              {
                new_start_pos = start_pos;
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                //Console.WriteLine(id);
                if (token == TOKEN_KEYWORD && ((id == "input") || (id == "output") || (id == "inout")))
                {
                  dir = id;
                  data_type = ""; //Порт по умолчанию не logic (для поддержки verilog)
                  state = 1;
                }
                else if (port_declared == 1)
                {
                      if (token == TOKEN_ID) //тип данных или имя порта
                      {
                        if (id_detected) // второй идентификатор => первый - тип, а второй имя
                        {
                          id_detected = false;
                          name = id;
                          return 1;
                        }
                        else // найден первый идентификатор после запятой, ищем следующий токен для проверки
                        {
                          id_detected = true;
                          data_type = id;
                          name = id;
                          state = 0;
                        }

                      }
                      else if (token == TOKEN_SQBR) // возможно, далее идет разрядность
                      {
                        start_pos = new_start_pos;
                        state = 2;
                      }
                      else 
                      {
                        if (id_detected) // первый токен - идентификатор, второй - нет => 1-ый токен - имя порта
                        {
                          id_detected = false;
                          data_type = "logic";
                          start_pos = new_start_pos;
                          return 1;
                        }
                        else
                        {
                          start_pos = old_start_pos; //не найден ни один из допустимых идентификаторов после запятой при уже объявленном порте
                          return 0;
                        }
                      }
                }
                else
                {
                  start_pos = old_start_pos;
                  return 0;
                }
                break;
              }

              case 1:
              {
                new_start_pos = start_pos;
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                if ((token == TOKEN_KEYWORD) && (id == "logic"))
                {
                  //Console.WriteLine("Error 704");
                  data_type = id;
                  state = 2;
                }
                else if (token == TOKEN_ID)
                {
                //Console.WriteLine("Error 704");
                data_type = "";
                name = id;
                return 1;
                }
              else
              {
                  start_pos=new_start_pos;
                  state = 2;
                  Console.WriteLine ("Error 703");
                }
                break;
              }
 
              case 2:
              {
                dim = 1;
                dim_str = "";
                while (true)
                {
                  func_res = this.search_unpacked_dimension(ref id, ref dim, ref dim_str, ref start_pos, fs);//!Добавил
                  Console.WriteLine (func_res);
                  if (func_res == -1) return (-1);
                  if (func_res == -2)
                  {
                    start_pos=old_start_pos;
                    Console.WriteLine ("search_port: Error 1");
                    return (-1);
                  }
                  else if (func_res == 0)
                  {
                    Console.WriteLine ("DIMENSION !!!!\n\r");
                    state = 3;
                    break;
                  }
                
                }
                break;
              }

              case 3:
              {
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                if (token == TOKEN_ID)
                {
                  name = id;
                  return 1;
                }
                else
                {
                  start_pos=old_start_pos;
                  Console.WriteLine ("search_port: ID expected, found: " + id);
                  return (-1);
                }
                break;
              }
            }
          }
      }

      public Macro search_macro(ref long start_pos, FileStream fs, ref int errorCode)
      {


        char[] buf = new char[1];
        int state = 0;
        int token = 0;

        int res = 0;
        int read_res = 0;
        string id = "";
        string macroName = "";
        string macroArgumentName = "";
        string macroValueName = "";
        errorCode = 0;
        fs.Seek(start_pos, SeekOrigin.Begin);

        while (true)
        {
          switch (state)
          {
            case 0:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -1;//EOF There is no Macro in the fs
                  return (null);
                }
                if (token == TOKEN_GRAVE_ACCENT)
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
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token == TOKEN_ID && id == "define")
                {
                  state = 1;
                }
                else
                {
                  state = 0;
                }
                break;
              }

            case 1:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_ID)
                {
                  Console.WriteLine("search_macro: Error 2");
                  errorCode = -3;//There is only part of macro definition
                  return (null);
                }
                else
                {
                  macroName = id;
                  state = 2;
                }
                break;
              }

            case 2:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_BR)
                {
                  // Console.WriteLine("search_macro: Error 3");
                  // errorCode = -3;//There is only part of parameter definition
                  // return (null);
                  state = 0;
                }
                else
                {
                  state = 3;
                }
                break;
              }

            case 3:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_ID)
                {
                  Console.WriteLine("search_macro: Error 4");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                else
                {
                  macroArgumentName = id;
                  state = 5;
                  errorCode = 0;
                  // Parameter p = new Parameter();
                  // p.Name = param_name;
                  // p.Value = res;
                  // return (p);
                }
                break;
              }

            case 5:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_BR)
                {
                  Console.WriteLine("search_macro: Error 5");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                else
                {
                  state = 6;
                }
                break;
              }

            case 6:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_GRAVE_ACCENT)
                {
                  Console.WriteLine("search_macro: Error 6");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                else
                {
                  state = 7;
                }
                break;
              }

            case 7:
              {
                token = this.next_token(ref id, ref start_pos, fs);
                if (token == -1)
                {
                  errorCode = -2;//There is only part of parameter definition
                  return (null);
                }
                if (token != TOKEN_QUOTE)
                {
                  Console.WriteLine("search_macro: Error 7");
                  errorCode = -3;//There is only part of parameter definition
                  return (null);
                }
                else
                {
                  state = 8;
                }
                break;
              }

              case 8:
              {
                while (true)
                {
                  if ((read_res = fs.ReadByte()) == -1)
                  {
                    Console.WriteLine ("next search_macro: End of fs has been reached\n\r");
                    errorCode = -3;
                    return (null);
                  }
                  buf[0] = (char)read_res;
                  start_pos++;
                  if (buf[0] == '`')
                  {
                    Console.WriteLine(macroArgumentName);
                    state = 9;
                    break;
                  }
                  else if ((buf[0] == ' ') || (buf[0] == ' ') || (buf[0] == '\n') || (buf[0] == '\r')) //Написать код табуляции ( и в других местах тоже)
                  {}
                  else
                  {
                    macroValueName = macroValueName + buf[0];
                  }
                }
                break;
              }

            case 9:
            {

              token = this.next_token(ref id, ref start_pos, fs);
              if (token == -1)
              {
                errorCode = -2;//There is only part of parameter definition
                return (null);
              }
              if (token != TOKEN_QUOTE)
              {
                Console.WriteLine("search_macro: Error 8");
                errorCode = -3;//There is only part of parameter definition
                return (null);
              }
              else
              {
                state = 0;
                errorCode = 0;
                Macro m = new Macro();
                m.Name = macroName;
                m.Argument = macroArgumentName;
                m.Value = macroValueName;
                return (m);
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
          char[] buf = new char[1];
          string data_type = " ";
          string mod_name = "";
          string dir = "";
          string name = "";
          string id = "";
          int port_declared = 0;
          int end_search = 0;
          int dim = 0;
          string dim_str = "";//!Добавил
          port[] ports = new port[512];
          int port_number = 0;
          int i = 0;
          int read_res = 0;
          Port newPort;
      int braceCnt = 0;

          while (true)
          {
            //Console.WriteLine("State = " + state.ToString());
            switch (state)
            {
              case 0:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-1);
                if ((token == TOKEN_KEYWORD) && (id == "module")) state = 7;
                else if (token == 0) return 0;
                break;
              case 7:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == TOKEN_ID)
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
                if (token == TOKEN_BR) state = 2;
                else if (token == TOKEN_SEMICOLON)
                {
                  newModule.setModName(mod_name);
                  Console.WriteLine ("End\n\r");
                  return 1;
                }
                else if (token == TOKEN_NUMBER_SIGN)
                {
              Console.WriteLine("search_module(state 1): found TOKEN_NUMBER_SIGN", token);
              braceCnt = 0; //Обнуляем счетчик скобок для поиска завершения обяъвления параметров 
              state = 5;
                }
                else
                {
                  Console.WriteLine ("search_module(state 1): Error 1. Incorrect module declaration. Token = {0}", token);
                  return (-2);
                }
                break;
              case 2:
                  func_res = this.search_port (ref dim, ref dim_str, ref name, ref data_type, ref dir, port_declared, ref start_pos, fs); //!Добавил
                  //Console.WriteLine ("func_res = %d\n\r",func_res);
                  if (func_res == -1) return (-2);
                  if (func_res == 1)
                  {
                    Console.WriteLine ("FOUND " + dir + " PORT " + name + " size " + dim + " data type " + data_type + "\n\r");
                    ports[port_number].dir = dir;
                    ports[port_number].name = name;
                    ports[port_number].dim = dim;
                    ports[port_number].dim_str = dim_str;//!Добавил
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
                if (token == TOKEN_COMMA) // If token is COMMA
                {
                  state = 2;
                  Console.WriteLine("Search port: found COMMA");
                }
                else if (token == TOKEN_BR) //If token is BRACE
                {
                  state = 4; 
                  Console.WriteLine("Search port: found BRACE"); 
                }
                else
                {
                  Console.WriteLine ("search_module: Error 3. Token = " + token.ToString() + ". Incorrect module declaration.");
                  return (-2);
                }
                break;
              case 4:
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);
                if (token == TOKEN_SEMICOLON)
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
                      newPort = new Port(ports[i].dim, ports[i].dim_str, ports[i].name, ports[i].data_type, ports[i].dir, ""); //!Добавил
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
                
              case 5: //Пропускаем параметры модуля
              {
                while (true)
                {
                  if ((read_res = fs.ReadByte()) == -1)
                  {
                    Console.WriteLine ("search_module: Error 5. End of file.");
                    return (-1);
                  }
                  buf[0] = (char)read_res;
                  start_pos++;
                if (buf[0] == '(')
                    braceCnt++;
                else if (buf[0] == ')')
                  {
                  braceCnt--;
                    if (braceCnt == 0)
                      {
                        state = 1;
                        break;
                      }
                  }
                }
                break;
              }
            
            
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
                if (token == TOKEN_ID && id == "include")
                {
                  state = 1;
                }
                else
                {
                  start_pos = old_start_pos;
                  return (-3);
                }
                break;
              }

              case 1:
              {
                new_start_pos = start_pos;
                token = this.next_token (ref id, ref start_pos, fs);
                if (token == -1) return (-2);
                if (token == TOKEN_QUOTE)
                {
                    state = 2;
                }
                else
                {
                    start_pos = old_start_pos;
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
                    start_pos = old_start_pos;
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

        private int findInclMacro(ref string macroName, ref string macroArgument, FileStream fs, ref long start_pos)
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
                            token = this.next_token(ref id, ref start_pos, fs);
                            if (token == -1) return (-1);
                            //Console.WriteLine(id);
                            if (token == TOKEN_ID && id == "include")
                            {
                                state = 1;
                            }
                            else
                            {
                                start_pos = old_start_pos;
                                return (-3);
                            }
                            break;
                        }

                    case 1:
                        {
                            new_start_pos = start_pos;
                            token = this.next_token(ref id, ref start_pos, fs);
                            if (token == -1) return (-2);
                            if (token == TOKEN_GRAVE_ACCENT)
                            {
                                state = 2;
                            }
                            else
                            {
                                start_pos = old_start_pos;
                                return (-3);
                            }
                            break;
                        }

                    case 2:
                        {
                            new_start_pos = start_pos;
                            token = this.next_token(ref id, ref start_pos, fs);
                            if (token == -1) return (-2);
                            if (token == TOKEN_ID)
                            {
                                macroName = id;
                                state = 3;
                            }
                            else
                            {
                                start_pos = old_start_pos;
                                return (-3);
                            }
                            break;
                        }

                    case 3:
                        {
                            new_start_pos = start_pos;
                            token = this.next_token(ref id, ref start_pos, fs);
                            if (token == -1) return (-2);
                            if (token == TOKEN_BR)
                            {
                                state = 4;
                            }
                            else
                            {
                                start_pos = old_start_pos;
                                return (-3);
                            }
                            break;
                        }

                    case 4:
                        {
                            while (true)
                            {
                                if ((readRes = fs.ReadByte()) == -1)
                                {
                                    start_pos = old_start_pos;
                                    return (-2);
                                }
                                else
                                {
                                    buf[0] = (char)readRes;
                                    if (buf[0] == ')')
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        macroArgument = macroArgument + buf[0].ToString();
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


    public string delComments(string inFile, string outFile)
    {

      string testFileName = inFile;
      string testCopyFileName =  outFile;
      string fileString = "";

      if (File.Exists(outFile) == true)
      {
          File.Delete(outFile);
      }

      //Console.Write(testFileName.Substring(testFileName.LastIndexOf('\\', testFileName.Length - 1) + 1), testFileName.Length); //Директория исходного файла
      //Console.Write("|"); //Директория исходного файла

      int blockCommStartPos = 0;
      int blockCommEndPos = 0;

      int lineCommStartPos = 0;

      int needReadNewLine = 1;

      if (File.Exists(testFileName))
      {
        using (System.IO.StreamReader sr = new System.IO.StreamReader(testFileName))
        {
          using (System.IO.StreamWriter file = new System.IO.StreamWriter(testCopyFileName))
          {
            while (true)
            {
              if (needReadNewLine == 1)
              {
                if ((fileString = sr.ReadLine()) == null) break;
              }
              else
              {
                needReadNewLine = 1;
              }

              blockCommStartPos = fileString.IndexOf("/*");
              lineCommStartPos = fileString.IndexOf("//");

              if ((lineCommStartPos == -1) && (blockCommStartPos == -1))// нет комментариев в строке
              {
                file.WriteLine(fileString);
                //Console.WriteLine(fileString);
              }
              else if ((lineCommStartPos > blockCommStartPos) && (blockCommStartPos == -1) || ((blockCommStartPos > lineCommStartPos) && (lineCommStartPos > -1))) //в строке только комментарий "//" или "//" раньше "/*"
              {
                fileString = fileString.Substring(0, lineCommStartPos);
                file.WriteLine(fileString);
                //Console.WriteLine(fileString);
              }
              else /*if ((blockCommStartPos > lineCommStartPos) && (lineCommStartPos == -1) || ((lineCommStartPos > blockCommStartPos) && (blockCommStartPos > -1)))*/ //в строке только комментарий "/*" или "/*" раньше "//"
              {
                blockCommEndPos = fileString.IndexOf("*/");
                if (blockCommEndPos > -1) //конец комментария есть в этой же строке
                {
                  fileString = string.Concat(fileString.Substring(0, blockCommStartPos), " ", fileString.Substring(blockCommEndPos + 2, fileString.Length - blockCommEndPos - 2));
                  needReadNewLine = 0;
                }
                else //Ищем конец комментария "*/" в остальных строках
                {
                  file.WriteLine(fileString.Substring(0, blockCommStartPos));
                  //Console.WriteLine(fileString.Substring(0, blockCommStartPos));

                  while ((fileString = sr.ReadLine()) != null)
                  {
                    blockCommEndPos = fileString.IndexOf("*/");
                    if (blockCommEndPos > -1)
                    {
                      fileString = fileString.Substring(blockCommEndPos + 2, fileString.Length - blockCommEndPos - 2);
                      needReadNewLine = 0;
                      break;
                    }
                  }

                }
              }

            }

            //Вставляем старый текст теста
            //while ((val = sr.Read()) > 0) file.Write((char)val);
            file.Close();
          }
          sr.Close();
        }

        //File.Delete(testFileName);
        //File.Move(testCopyFileName, testFileName);
      }
      return ("");
    }


        private int findInclude (string inFilePath, ref string outFilePath)
        {
          string orgnFilePath = ""; //Исходный файл
          string orgnFileDirPath = ""; //Директория исходного файла
          string auxFilePath = ""; //Вспомогательный файл
          int includeFound = 0; // 1 - ранее был найден и распознан include
          int searchBegan = 0; //1 - Поиск includoв был начат
          long filePosition = 0; //Позиция в файле
          int auxFileNamePrefix = 0;
          string inFileWithoutComments = "";
          char[] buf = new char[1];
          int readRes = 0;
          int func_res = 0;
          string includeFilePath = "";//Файл в директиве include
          string auxFileDirectory = System.IO.Path.Combine(current_dir, @".\auxFiles\");
          bool absoluteIncludeFilePath = false; //true - путь абсолютный, false - относительный

          inFileWithoutComments = System.IO.Path.Combine (auxFileDirectory, "inFileWithoutComments.txt");

          if (System.IO.Directory.Exists(auxFileDirectory)) System.IO.Directory.Delete(auxFileDirectory, true);
          System.IO.Directory.CreateDirectory(auxFileDirectory);

          orgnFileDirPath = inFilePath.Substring(0, inFilePath.LastIndexOf('\\', inFilePath.Length - 1) + 1); //Директория исходного файла

         // Console.WriteLine("Inclide in file path {0}", in.LastIndexOf('o', "Hello".Length-1));

          while (true)
          {
            //Если поиск инклудов не был начат, то исходный файл - из параметра, дополнительный - новый файл
            if (searchBegan == 0)
            {
              searchBegan = 1;

              delComments(inFilePath, inFileWithoutComments);
              orgnFilePath = inFileWithoutComments;
              auxFilePath = System.IO.Path.Combine(auxFileDirectory, @".\" + auxFileNamePrefix.ToString()) + ".txt";
            }
            //в противном случае был выполнен поиск инклуда
            else
            {
              //Если был найден инклуд, то в дополнительном файле содержится исходный файл и то, что инклудилось,
              //Так как в "инкудившихся" файлах также могли быть инклуды, нужно проанализировать доп. файл.
              //Для этого делаем его исходным и создаем новый доп. файл.
              if (includeFound == 1)
              {
                includeFound = 0;

                //Удаляем комментарии и делаем доп. файл исходным
                delComments(auxFilePath, inFileWithoutComments);
                File.Delete(auxFilePath);
                File.Move(inFileWithoutComments, auxFilePath);

                orgnFilePath = auxFilePath;
                //Удаляем лишние доп. файлы, которые были созданы раньше
                if (auxFileNamePrefix > 0) File.Delete(System.IO.Path.Combine(auxFileDirectory, @".\" + (auxFileNamePrefix - 1).ToString() + ".txt"));
                auxFileNamePrefix++;
                auxFilePath = System.IO.Path.Combine(auxFileDirectory, @".\" + auxFileNamePrefix.ToString() + ".txt");
              }
              //Если инклуд в проанализированном файле найден не был, то поиск закончен
              else
              {
                outFilePath = auxFilePath;
                return (1);
              }

            }

            //Осуществляем поиск в исходном файле инклудов, параллельно копируя его в дополнительный файл
            //Если находим инклуд, открываем файл, который инклудится и копируем его тоже в дополнительный файл
            //Когда весь исходный файл скопирован, то в дополнительном файле содержится исходный файл и то, что инклудилось в исходном файле
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
                        // Console.WriteLine("findInclude: Error in `inculde declaration.");
                        // return (-1);

                        //Игнорируем ошибки в инклудах, чтобы искать инклуды с макросами
                        fsAuxFile.WriteByte((byte)readRes);
                        fsOrgnFile.Seek(filePosition, SeekOrigin.Begin);

                    }
                      else
                      {
                        Console.WriteLine(includeFilePath);
                        //Инклуд содержит относительный или абсолютный путь к файлу

                        absoluteIncludeFilePath = includeFilePath.Contains(":");

                        if (absoluteIncludeFilePath == false)
                          includeFilePath = System.IO.Path.Combine(orgnFileDirPath, includeFilePath);

                        Console.WriteLine(includeFilePath);

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

        private int findIncludeMacro(string inFilePath, ref string outFilePath, List<Macro> macros)
        {
            string orgnFilePath = ""; //Исходный файл
            string orgnFileDirPath = ""; //Директория исходного файла
            string auxFilePath = ""; //Вспомогательный файл
            int includeFound = 0; // 1 - ранее был найден и распознан include
            int searchBegan = 0; //1 - Поиск includoв был начат
            long filePosition = 0; //Позиция в файле
            int auxFileNamePrefix = 123;
            string inFileWithoutComments = "";
            char[] buf = new char[1];
            int readRes = 0;
            int func_res = 0;
            string macroName = "";
            string macroArg = "";
            string auxFileDirectory = System.IO.Path.Combine(current_dir, @".\auxFiles\");
            bool absoluteIncludeFilePath = false; //true - путь абсолютный, false - относительный
            string macroPath = "";
            string includeStr = "";
            bool replaceMacroDone = false;
            int newIncludeExists = 0;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            orgnFilePath = inFilePath;
            auxFilePath = System.IO.Path.Combine(auxFileDirectory, @".\" + auxFileNamePrefix.ToString()) + ".txt";


            inFileWithoutComments = System.IO.Path.Combine(auxFileDirectory, "inFileWithoutComments.txt");

            //if (System.IO.Directory.Exists(auxFileDirectory)) System.IO.Directory.Delete(auxFileDirectory, true);
            //System.IO.Directory.CreateDirectory(auxFileDirectory);

            orgnFileDirPath = inFilePath.Substring(0, inFilePath.LastIndexOf('\\', inFilePath.Length - 1) + 1); //Директория исходного файла

            // Console.WriteLine("Inclide in file path {0}", in.LastIndexOf('o', "Hello".Length-1));

            //Осуществляем поиск в исходном файле инклудов, параллельно копируя его в дополнительный файл
            //Если находим инклуд, открываем файл, который инклудится и копируем его тоже в дополнительный файл
            //Когда весь исходный файл скопирован, то в дополнительном файле содержится исходный файл и то, что инклудилось в исходном файле
            using (FileStream fsOrgnFile = File.Open(orgnFilePath, FileMode.Open, FileAccess.Read))
            {
                using (System.IO.StreamWriter fsAuxFile = new System.IO.StreamWriter(auxFilePath))
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
                                fsAuxFile.Write((char)readRes);
                            }
                            else
                            {
                                filePosition = fsOrgnFile.Position;
                                macroName = "";
                                func_res = findInclMacro(ref macroName, ref macroArg, fsOrgnFile, ref filePosition);
                                if (func_res == -1 || func_res == -3) //-3 - not include, may be `ifdef
                                {
                                    fsAuxFile.Write((char)readRes);
                                    fsOrgnFile.Seek(filePosition, SeekOrigin.Begin);
                                }
                                else if (func_res == -2)
                                {
                                    Console.WriteLine("findInclude: Error in `inculde declaration.");
                                    return (-1);
                                }
                                else
                                {
                                    if (macros != null)
                                    {
                                        foreach (Macro m in macros)
                                        {
                                            if (m.Name == macroName)
                                            {
                                                macroPath = m.Value.Replace(m.Argument, macroArg);
                                                Console.WriteLine("macroPath = " + macroPath);
                                                replaceMacroDone = true;

                                            }
                                            //Console.WriteLine("macroName = {0}* macroArgument = {1}* macroValue = {2}* ", m.Name, m.Argument, m.Value);
                                        }
                                        if (replaceMacroDone)
                                        {
                                            replaceMacroDone = false;
                                            includeStr = "`include \"" + macroPath + "\"";
                                            fsAuxFile.WriteLine (includeStr);
                                            newIncludeExists = 1;
                                        }
                                        else
                                        {
                                            fsAuxFile.Write((char)readRes);
                                            fsOrgnFile.Seek(filePosition, SeekOrigin.Begin);
                                        }
                                    }
                                    Console.WriteLine("Macroname: " + macroName + " MacroArg: " + macroArg);

                                }
                            }
                        }
                    }
                }
            }
            outFilePath = auxFilePath;
            if (newIncludeExists == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }


        public int analizeFile(string path, ref Module[] listOfModules, bool onlyTest)
      {
        int func_res = 0;
        int i = 0;
        long start_pos = 0;
        Module newModule;
        string outFilePath = "";
        string inFileWithoutComments = "";

        List<Parameter> parameters = new List<Parameter>();
        parameters.Clear();


        List<Macro> macros = new List<Macro>();
        macros.Clear();

        for (i = 0; i < listOfModules.Length; i++) listOfModules[i] = null;
        i = 0;



        string auxFileDirectory = current_dir;
        inFileWithoutComments = System.IO.Path.Combine(auxFileDirectory, "onlyTestWithoutComments.txt");
        delComments(path, inFileWithoutComments);

        outFilePath = inFileWithoutComments;

        if (onlyTest == false)
        {
            FileStream fs = new FileStream(outFilePath, FileMode.Open);
            macros = search_all_macro(fs);
            fs.Close();

            if (macros != null)
            {
                foreach (Macro m in macros)
                {
                    Console.WriteLine("macroName = {0}* macroArgument = {1}* macroValue = {2}* ", m.Name, m.Argument, m.Value);
                }
            }

            if (findInclude(inFileWithoutComments, ref outFilePath) == -1)
            {
                Console.WriteLine("analizeFile: Include processing failed");
                return -1;
            }

                inFileWithoutComments = outFilePath;
                outFilePath = "";

        if (findIncludeMacro(inFileWithoutComments, ref outFilePath, macros) == -1)
        {
            Console.WriteLine("analizeFile: Include processing failed");
            return -1;
        }

            }
          
        try
        {
          Console.WriteLine("After find include: {0}", outFilePath);
          FileStream fs = new FileStream(outFilePath, FileMode.Open);


          if (onlyTest == false)
          {
            parameters = search_all_params(fs);
            if (parameters != null)
            {
              foreach (Parameter p in parameters)
              {
                Console.WriteLine("Parameter = {0}, value = {1}", p.Name, p.Value);
              }
            }
          }


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

