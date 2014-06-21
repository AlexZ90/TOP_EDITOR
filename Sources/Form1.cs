using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TopEditor
{
    public partial class Form1 : Form
    {
        Analyzer testAnalyzer = new Analyzer();
        Module testModule;
        string path = @"d:\Test.txt";
        Module[] listofModules;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          int i = 0;
          
          listofModules = testAnalyzer.analizeFile(path);
          for (i = 0; i < listofModules.Length; i++)
            if (listofModules[i] != null) listofModules[i].showModDeclaration();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          string id = "";
          int func_res = 0;
          while (true)
          {
            testModule = new Module();
            func_res = testAnalyzer.search_module(ref testModule);
            if (func_res == -2 || func_res == -1) 
            {
              Console.WriteLine("End of file");
              break;
            }
            else Console.WriteLine("Found module");
            testModule.showModDeclaration();
          }
          Console.WriteLine(id);
        }
    }
}
