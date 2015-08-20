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
        Module[] newlistOfModules;
        Module[] listOfModules = new Module [100];
        int curModNumber = 0;
        int curInstNumber = 0;
        int curConntNumber = 0;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        Instance [] listOfInstances = new Instance [100];
        Connection[] listOfConnections = new Connection[100];

        string safeFileName = ""; //!Добавил
        string fileDirPath = ""; //!Добавил
        string fullFilePath = ""; //!Добавил
        
      
        public Form1()
        {
            InitializeComponent();
            dt1.Columns.Add("Dir");
            dt1.Columns.Add("Data type");
            dt1.Columns.Add("Size");
            dt1.Columns.Add("Name");

            dt1.Columns.Add("Size string"); //!Добавил 

            dataGridView1.DataSource = dt1;
            listOfModuleLB.Click += new EventHandler(showPortsBtn_Click);

            dt2.Columns.Add("Dir");
            dt2.Columns.Add("Data type");
            dt2.Columns.Add("Size");
            dt2.Columns.Add("Name");
            dataGridView2.DataSource = dt2;
            listOfInstLB.Click += new EventHandler(listOfInstLB_Click);

            dt3.Columns.Add("Dir");
            dt3.Columns.Add("Data type");
            dt3.Columns.Add("Size");
            dt3.Columns.Add("Name");
            dataGridView3.DataSource = dt3;
            listOfInstLB2.Click += new EventHandler(listOfInstLB2_Click);


            dt4.Columns.Add("Inst");
            dt4.Columns.Add("Port");
            dataGridView4.DataSource = dt4;
            listOfConnLB.Click += new EventHandler(listOfConnLB_Click);

        }

        private void button1_Click(object sender, EventArgs e)
        {
          int i = 0;
          int j = 0;
          int modAlrdyExist = 0;
          Module[] newlistOfModules = new Module[100];
          int funcRes = 0;

          bool onlyTest = false;

          funcRes = testAnalyzer.analizeFile(textBox1.Text, ref newlistOfModules, cbOnlyForTest.Checked);
          if (funcRes == 1)
          {
            for (i = 0; i < newlistOfModules.Length; i++)
              if (newlistOfModules[i] != null)
              {
                modAlrdyExist = 0;
                for (j = 0; j < listOfModules.Length; j++)
                {
                  if (listOfModules[j] != null && newlistOfModules[i].getModName() == listOfModules[j].getModName())
                    modAlrdyExist++;
                }
                if (modAlrdyExist > 0)
                  MessageBox.Show("Модуль " + newlistOfModules[i].getModName() + " уже существует.");
                else
                {
                  listOfModules[curModNumber] = newlistOfModules[i];
                  curModNumber++;
                }
                //newlistOfModules[i].showModDeclaration();
              }
            this.updateListOfModules(listOfModules);
          }
          else if (funcRes == -1) MessageBox.Show("Ошибка обработки включений файлов");
          else if (funcRes == -2) MessageBox.Show("Обнаружена синтаксическая ошибка в объявлении модуля или не найдено значение параметра");
          else if (funcRes == -3) MessageBox.Show("Не удалось открыть файл " + textBox1.Text);
}


        private void updateListOfModules(Module[] listOfModules)
        {
          int i = 0;
          int j = 0;
          listOfModuleLB.Items.Clear();
          for (i = 0; i < listOfModules.Length; i++)
            if (listOfModules[i] != null)
            {
              j = 1;
              listOfModuleLB.Items.Add(listOfModules[i].getModName());
              //newlistOfModules[i].showModDeclaration();
            }
          if (j != 0) listOfModuleLB.SelectedItem = listOfModuleLB.Items[0];
        }

        public void deleteModule(string modName)
        {
          int i = 0;
          for (i = 0; i < listOfModules.Length; i++)
            if (listOfModules[i] != null && listOfModules[i].getModName() == modName)
            {
              listOfModules[i] = null;
              this.updateListOfModules(listOfModules);
            }
        }

        Module getModule(string modName)
        {
          int i = 0;
          for (i = 0; i < listOfModules.Length; i++)
            if (listOfModules[i] != null && listOfModules[i].getModName() == modName)
            {
              return listOfModules[i];
            }
          return null;
        }

        private void showModPorts(string modName, DataTable dt)
        {
          int i = 0;
          int j = 0;
          dt.Clear();

          for (i = 0; i < listOfModules.Length; i++)
          {
            if (listOfModules[i] != null && listOfModules[i].getModName() == modName)
              for (j = 0; j < listOfModules[i].listOfPorts.Length; j++)
                if (listOfModules[i].listOfPorts[j] != null)
                  dt.Rows.Add(listOfModules[i].listOfPorts[j].dir, listOfModules[i].listOfPorts[j].data_type, listOfModules[i].listOfPorts[j].dim, listOfModules[i].listOfPorts[j].name, listOfModules[i].listOfPorts[j].dim_str);
          }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
          if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          {
            fullFilePath = openFileDialog1.FileName;//!Добавил
            textBox1.Text = fullFilePath;//!Добавил
            safeFileName = openFileDialog1.SafeFileName;//!Добавил
            fileDirPath = fullFilePath.Replace(safeFileName, "");//!Добавил
          }
        }

        private void delModBtn_Click(object sender, EventArgs e)
        {
          deleteModule(listOfModuleLB.SelectedItem.ToString());
        }

        private void showPortsBtn_Click(object sender, EventArgs e)
        {
          showModPorts(listOfModuleLB.SelectedItem.ToString(), dt1);
        }

        private void listOfInstLB_Click(object sender, EventArgs e)
        {
          showInstPorts(listOfInstLB.SelectedItem.ToString(), dt2);
        }

        private void listOfInstLB2_Click(object sender, EventArgs e)
        {
          showInstPorts(listOfInstLB2.SelectedItem.ToString(), dt3);
        }

        private void createInstBtn_Click(object sender, EventArgs e)
        {
          addInstance(listOfModuleLB.SelectedItem.ToString(), instNameTB.Text);
        }

        public void addInstance (string baseModName, string newInstName)
        {
          int i = 0;
          int j = 0;
          int instAlrdyExist = 0;
          int baseModNumber = 0;
          
          for (i = 0; i < listOfModules.Length; i++)
            if (listOfModules[i] != null && baseModName == listOfModules[i].getModName())
            {
              baseModNumber = i;
            }

          for (j = 0; j < listOfInstances.Length; j++)
            {
              if (listOfInstances[j] != null && newInstName == listOfInstances[j].Name)
                instAlrdyExist++;
            }
          if (instAlrdyExist > 0)
            MessageBox.Show("Экземпляр " + newInstName + " уже существует.");
            else
              {

                listOfInstances[curInstNumber] = new Instance(newInstName, listOfModules[baseModNumber]);
                curInstNumber++;
              }

          this.updateListOfInstances(listOfInstances, listOfInstLB);
          this.updateListOfInstances(listOfInstances, listOfInstLB2);
        }

        private void updateListOfInstances(Instance [] listOfInstances, ListBox lb)
        {
          int i = 0;
          lb.Items.Clear();
          for (i = 0; i < listOfInstances.Length; i++)
            if (listOfInstances[i] != null)
            {
              lb.Items.Add(listOfInstances[i].Name);
              //newlistOfModules[i].showModDeclaration();
            }
        }

        public void deleteInstance (string InstName)
        {
          int i = 0;
          for (i = 0; i < listOfInstances.Length; i++)
            if (listOfInstances[i] != null && listOfInstances[i].Name == InstName)
            {
              listOfInstances[i] = null;
              this.updateListOfInstances(listOfInstances, listOfInstLB);
              this.updateListOfInstances(listOfInstances, listOfInstLB2);
            } 
        }

        Instance getInstance(string InstName)
        {
          int i = 0;
          for (i = 0; i < listOfInstances.Length; i++)
            if (listOfInstances[i] != null && listOfInstances[i].Name == InstName)
            {
              return listOfInstances[i];
            }
          return null;
        }


        private void delInstBtn_Click(object sender, EventArgs e)
        {
          deleteInstance(listOfInstLB.SelectedItem.ToString());
        }

        private void showInstPorts(string InstName, DataTable dt)
        {
          int i = 0;
          int j = 0;
          for (i = 0; i < listOfInstances.Length; i++)
          {
            if (listOfInstances[i] != null && listOfInstances[i].Name == InstName)
              showModPorts(listOfInstances[i].BaseModule.modName, dt);
          }
        }

        public void addConnection(string connName, string inst1_name, string inst1_port_name, string inst2_name, string inst2_port_name)
        {
          int i = 0;
          int j = 0;
          int connAlrdyExist = 0;
          Instance inst1;
          Port inst1_port;
          Instance inst2;
          Port inst2_port;
          int ext = 0;

          inst1 = this.getInstance(inst1_name);
          inst1_port = inst1.BaseModule.getPort(inst1_port_name);
          inst2 = this.getInstance(inst2_name);
          inst2_port = inst2.BaseModule.getPort(inst2_port_name);

          Console.WriteLine(inst1.Name);

          if (extConnCHBX.Checked == false)
            ext = 0;
          else
            ext = 1;
          if (ext == 0)
          {
              for (j = 0; j < listOfConnections.Length; j++)
              {
                  if (listOfConnections[j] != null && connName == listOfConnections[j].Name && inst1_name == listOfConnections[j].inst_1.Name && inst2_name == listOfConnections[j].inst_2.Name && inst1_port_name == listOfConnections[j].inst_1_port.name && inst2_port_name == listOfConnections[j].inst_2_port.name)
                      connAlrdyExist++;
              }
          }
          else
          {
              for (j = 0; j < listOfConnections.Length; j++)
              {
                  if (listOfConnections[j] != null && connName == listOfConnections[j].Name && listOfConnections[j].external == 1)
                      connAlrdyExist++;
              }
          }




          if (connAlrdyExist > 0)
            MessageBox.Show("Связь " + connName + " уже существует.");
          else
          {
            if (extConnCHBX.Checked == false)
              listOfConnections[curConntNumber] = new  Connection (connName, inst1, inst1_port, inst2, inst2_port, 0);
            else
              listOfConnections[curConntNumber] = new Connection(connName, inst1, inst1_port, inst1, inst1_port, 1);
            Console.WriteLine(listOfConnections[curConntNumber].inst_1.Name);
            curConntNumber++;
          }

          this.updateListOfConnections(listOfConnections, listOfConnLB);
        }

        private void updateListOfConnections(Connection[] listOfConnections, ListBox lb)
        {
          int i = 0;
          int j = 0;
          int match_count = 0;
          lb.Items.Clear();
          for (i = 0; i < listOfConnections.Length; i++)
            if (listOfConnections[i] != null)
            {
                match_count = 0;
                for (j = 0; j < i; j++)
                {
                    if (listOfConnections[j].Name == listOfConnections[i].Name)
                    {
                        match_count++;
                        break;
                    }
                }
                if (match_count == 0) lb.Items.Add(listOfConnections[i].Name);

              //newlistOfModules[i].showModDeclaration();
            }
        }

        private void createConnBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine(dataGridView2.CurrentRow.Cells[3].Value.ToString());
            addConnection(connectionTB.Text, listOfInstLB.SelectedItem.ToString(), dataGridView2.CurrentRow.Cells[3].Value.ToString(), listOfInstLB2.SelectedItem.ToString(), dataGridView3.CurrentRow.Cells[3].Value.ToString());
          Console.WriteLine(dataGridView2.CurrentCell.Value.ToString());
        }

        public void deleteConnection(string ConnName)
        {
          int i = 0;
          for (i = 0; i < listOfConnections.Length; i++)
            if (listOfConnections[i] != null && listOfConnections[i].Name == ConnName)
            {
              listOfConnections[i] = null;
              this.updateListOfConnections(listOfConnections, listOfConnLB);
            }
        }

        private void listOfConnLB_Click(object sender, EventArgs e)
        {
            showConnections(listOfConnLB.SelectedItem.ToString(), dt4);
        }

        public struct connInstsPorts
        {
            public string instName;
            public string portName;
        }

        private void showConnections(string connName, DataTable dt)
        {
          int i = 0;
          int j = 0;
          int k = 0;
          int m = 0;
          int instPortAlreadyExist = 0;
          

          connInstsPorts[] listOfInstPorts = new connInstsPorts [256];

          dt.Clear();
          for (i = 0; i < listOfConnections.Length; i++)
          {
            if (listOfConnections[i] != null && listOfConnections[i].Name == connName)
                if (listOfConnections[i].external == 0)
                {
                    //Проверяем наличие в массиве подключенных экземпляров и портов первого экземляра и его порта
                    instPortAlreadyExist = 0;
                    for (m = 0; m < k; m++)
                    {
                        if (listOfInstPorts[m].instName == listOfConnections[i].inst_1.Name && listOfInstPorts[m].portName == listOfConnections[i].inst_1_port.name)
                        {
                            instPortAlreadyExist = 1;
                        }
                    }
                    if (instPortAlreadyExist == 0)
                    {
                        listOfInstPorts[k].instName = listOfConnections[i].inst_1.Name;
                        listOfInstPorts[k].portName = listOfConnections[i].inst_1_port.name;
                        k++;
                    }

                    //Проверяем наличие в массиве подключенных экземпляров и портов второго экземляра и его порта
                    instPortAlreadyExist = 0;
                    for (m = 0; m < k; m++)
                    {
                        if (listOfInstPorts[m].instName == listOfConnections[i].inst_2.Name && listOfInstPorts[m].portName == listOfConnections[i].inst_2_port.name)
                        {
                            instPortAlreadyExist = 1;
                        }
                    }
                    if (instPortAlreadyExist == 0)
                    {
                        listOfInstPorts[k].instName = listOfConnections[i].inst_2.Name;
                        listOfInstPorts[k].portName = listOfConnections[i].inst_2_port.name;
                        k++;
                    }

                    //dt.Rows.Add(listOfConnections[i].inst_1.Name, listOfConnections[i].inst_1_port.name);
                    //dt.Rows.Add(listOfConnections[i].inst_2.Name, listOfConnections[i].inst_2_port.name);

                    j++;
                }
          }
          for (i = 0; i < listOfConnections.Length; i++)
          {
              if (listOfConnections[i] != null && listOfConnections[i].Name == connName)
                  if (listOfConnections[i].external == 1)
                  {
                      if (j == 0)
                      {
                          listOfInstPorts[k].instName = listOfConnections[i].inst_1.Name;
                          listOfInstPorts[k].portName = listOfConnections[i].inst_1_port.name;
                          k++;
                          listOfInstPorts[k].instName = "External";
                          listOfInstPorts[k].portName = " ";
                          k++;
                          //dt.Rows.Add(listOfConnections[i].inst_1.Name, listOfConnections[i].inst_1_port.name);
                          //dt.Rows.Add("External", " ");
                      }
                      else
                      {
                          listOfInstPorts[k].instName = "External";
                          listOfInstPorts[k].portName = " ";
                          k++;
                          //dt.Rows.Add("External", " ");
                      }
                  }
          }
          for (i = 0; i < k; i++)
          {
              dt.Rows.Add(listOfInstPorts[i].instName, listOfInstPorts[i].portName);
          }

        }

        public string alignStr(string stringToAlign, int numOfSigns)
        {
          int i = 0;
          string res;

          res = stringToAlign;
          for (i = 0; i < (numOfSigns - stringToAlign.Length); i++)
            res = res + " ";
          return res;

        }

        private void createTopBTN_Click(object sender, EventArgs e)
        {
          int i = 0;
          int j = 0;
          int k = 0;
          int m = 0;
          int numOfExternals = 0;
          int numOfPorts = 0;
          string dir_dtype_dim;
          int externalExist = 0;
          using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\" + topNameTB.Text + ".txt"))
          {

            file.Write("module " + topNameTB.Text + "(");

            for (k = 0; k < listOfConnections.Length; k++)
              if (listOfConnections[k] != null && listOfConnections[k].external == 1)
              {
                numOfExternals++;
              }
            if (numOfExternals > 0) file.WriteLine();

            for (k = 0; k < listOfConnections.Length; k++)
              if (listOfConnections[k] != null && listOfConnections[k].external == 1)
              {
                if (listOfConnections[k].inst_1_port.data_type == "logic")
                  dir_dtype_dim = alignStr(listOfConnections[k].inst_1_port.dir,7) + " " + "logic ";
                else dir_dtype_dim = alignStr(listOfConnections[k].inst_1_port.dir, 7) + " ";

                if (listOfConnections[k].inst_1_port.dim > 1)
                  dir_dtype_dim = dir_dtype_dim + "[" + (listOfConnections[k].inst_1_port.dim - 1).ToString() + ":0]";
                
                dir_dtype_dim = alignStr (dir_dtype_dim, 30);

                file.Write (dir_dtype_dim);
                file.Write (listOfConnections[k].Name);
                if (numOfExternals > 1)
                {
                  file.WriteLine(",");
                  numOfExternals--;
                }
              }
            file.WriteLine();
            file.WriteLine(");");
            file.WriteLine();

            file.WriteLine("/*------------------------- Internal connections ---------------------------------*/");
            for (k = 0; k < listOfConnections.Length; k++)
              if (listOfConnections[k] != null && listOfConnections[k].external == 0)
              {
                externalExist = 0;
                for (m = 0; m < listOfConnections.Length; m++)
                {
                  if (m!=k && listOfConnections[m] != null && listOfConnections[m].Name == listOfConnections[k].Name && listOfConnections[m].external == 1)
                    externalExist ++;
                    
                }
                if (externalExist == 0)
                {
                  file.Write("wire ");
                  if (listOfConnections[k].inst_1_port.dim > 1)
                    file.WriteLine ("[" + (listOfConnections[k].inst_1_port.dim-1).ToString() + ":0] " + listOfConnections[k].Name + ";");
                  else file.WriteLine(listOfConnections[k].Name + ";");
                }
              }

            file.WriteLine("/*--------------------------------------------------------------------------------*/");
            file.WriteLine();
            file.WriteLine();

            
            for (i = 0; i < listOfInstances.Length; i++)
            {
              if (listOfInstances[i] != null)
              {
                file.WriteLine (listOfInstances[i].BaseModule.modName + " " + listOfInstances[i].Name + "(");
                numOfPorts = listOfInstances[i].BaseModule.getNumOfPorts();
                for (j = 0; j < listOfInstances[i].BaseModule.listOfPorts.Length; j++)
                {
                  if (listOfInstances[i].BaseModule.listOfPorts[j] != null)
                  {
                    
                    file.Write("." + listOfInstances[i].BaseModule.listOfPorts[j].name);
                    for (m = 0; m < (50 - listOfInstances[i].BaseModule.listOfPorts[j].name.Length); m++) file.Write(" ");
                    file.Write("(");
                      for (k = 0; k < listOfConnections.Length; k++)
                        if (listOfConnections[k] != null && (listOfConnections[k].inst_1_port.name == listOfInstances[i].BaseModule.listOfPorts[j].name && listOfConnections[k].inst_1.Name == listOfInstances[i].Name || listOfConnections[k].inst_2_port.name == listOfInstances[i].BaseModule.listOfPorts[j].name && listOfConnections[k].inst_2.Name == listOfInstances[i].Name))
                        {
                          file.Write(listOfConnections[k].Name);
                          break;
                        }
                    file.Write(")");
                    if (numOfPorts > 1)
                    {
                      file.Write(",");
                      numOfPorts--;
                    }
                    file.WriteLine();
                  }
                }
                file.WriteLine(");");
                file.WriteLine();
                file.WriteLine();
              }
            }
            file.WriteLine("endmodule ");
              file.Close();
          }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
          deleteConnection(listOfConnLB.SelectedItem.ToString());
        }



      private void btnCreateTest_Click(object sender, EventArgs e)
      {

        Module module;
        string mod_name;
        int numOfPorts;
        string metka = "";
        
        int i = 0;
        int j = 0;
        int k = 0;
        int m = 0;


        module = getModule(listOfModuleLB.SelectedItem.ToString());
        mod_name = module.getModName();
        numOfPorts = module.getNumOfPorts();

        string vrfFolderName = @"" + fileDirPath + mod_name + "_VRF\\";

        if (Directory.Exists(vrfFolderName) != true)
            Directory.CreateDirectory(vrfFolderName);

        string testFileName = @"" + vrfFolderName + "test.sv";
        string testCopyFileName = @"" + vrfFolderName + "test_copy.sv";
        string testTopFileName = @"" + vrfFolderName + "test_top.sv";
        string test_top_mod_name = "test_top";
        string dut_inst_name = mod_name + "_1";
        string make_do_filename = @"" + vrfFolderName + "make.do";
        string restart_do_filename = @"" + vrfFolderName + "restart.do";
        string addButton_do_filename = @"" + vrfFolderName + "addButton_" + mod_name + ".do";


        using (System.IO.StreamWriter file = new System.IO.StreamWriter(addButton_do_filename))
        {
          file.WriteLine("quit -sim");
          file.WriteLine("add button make_" + mod_name + " {do " + make_do_filename.Replace("\\", "\\\\") + "} NoDisable");
          file.WriteLine("add button restart_" + mod_name + " {do " + restart_do_filename.Replace("\\", "\\\\") + "} NoDisable");
          file.Close();
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(make_do_filename))
        {
          file.WriteLine("quit -sim");
          file.WriteLine("vlog \"" + testTopFileName.Replace("\\", "\\\\") + "\"");
          file.WriteLine();
          file.WriteLine("vsim -novopt work." + test_top_mod_name);
          file.WriteLine();
          file.WriteLine("add wave -divider -height 30 " + dut_inst_name);
          file.WriteLine("add wave -radix hex sim:/" + test_top_mod_name + "/" + dut_inst_name + "/*");
          file.WriteLine("run 500");
          //file.WriteLine("#view wave");
          file.Close();
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(restart_do_filename))
        {
          file.WriteLine("vlog \"" + testTopFileName.Replace("\\", "\\\\") + "\"");
          file.WriteLine("restart -force");
          file.WriteLine("run 500");
          //file.WriteLine("#view wave");
          file.Close();
        }



        int val = 0;
        if (File.Exists(testFileName))
        {
          //Модифицируем существующий файл с тестом, чтобы не нарушить его содержимое
          using (System.IO.StreamReader sr = new System.IO.StreamReader(testFileName))
          {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(testCopyFileName))
            {
              //Пропускаем описание портов в старом тесте
              //while (sr.Read() != ';') ;

              while (true)
              {
                  metka = sr.ReadLine();
                  if (string.Equals(metka, "/*DONT_DELETE2567*/"))
                  {
                      Console.WriteLine("Equal ****************8");
                      break;
                  }
              }


              
              // Вставляем в новый файл с тестом данные из текстбокса

              file.WriteLine(rtbAddToTest.Text);
              file.WriteLine();

                

              //Вставляем новое описание портов
              file.Write("module test (\n");
              Console.WriteLine(rtbAddToTest.Text);

              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {

                  if (module.listOfPorts[j].dir == "input") file.Write("output ");
                  else if (module.listOfPorts[j].dir == "output") file.Write("input  ");
                  else if (module.listOfPorts[j].dir == "inout") file.Write("inout  ");
                  else MessageBox.Show("Ошибка! Неизвестное направление порта !");

                  file.Write("logic ");
                  //file.Write("logic " + module.listOfPorts[j].name);
                  for (m = 0; m < (40 - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");
                  file.Write(module.listOfPorts[j].dim_str + " ");

                  file.Write(module.listOfPorts[j].name);
                  for (m = 0; m < (20 - module.listOfPorts[j].name.Length); m++) file.Write(" ");

                  if (numOfPorts > 1)
                  {
                    file.Write(",");
                    numOfPorts--;
                  }
                  file.WriteLine();
                }
              }

              file.Write(");\n\n");

              file.WriteLine("/*DONT_DELETE2567*/");

              //Вставляем в новый файл текст теста из старого файла
              while ((val = sr.Read()) > 0) file.Write((char)val);
              file.Close();
            }
            sr.Close();
          }

          //Заменяем старый файл с тестом новым
          File.Delete(testFileName);
          File.Move(testCopyFileName, testFileName);
        }
        else // Если создаем файл с тестом впервые 
          using (System.IO.StreamWriter file = new System.IO.StreamWriter(testFileName))
          {

            // Добавляем общий текст  из текстбокса
            file.WriteLine(rtbAddToTest.Text);
            file.WriteLine();

            file.Write("module test (\n");

            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {

                if (module.listOfPorts[j].dir == "input") file.Write("output ");
                else if (module.listOfPorts[j].dir == "output") file.Write("input  ");
                else if (module.listOfPorts[j].dir == "inout") file.Write("inout  ");
                else MessageBox.Show("Ошибка! Неизвестное направление порта !");

                file.Write("logic ");
                //file.Write("logic " + module.listOfPorts[j].name);
                for (m = 0; m < (40 - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");
                file.Write(module.listOfPorts[j].dim_str + " ");

                file.Write(module.listOfPorts[j].name);
                for (m = 0; m < (20 - module.listOfPorts[j].name.Length); m++) file.Write(" ");

                if (numOfPorts > 1)
                {
                  file.Write(",");
                  numOfPorts--;
                }
                file.WriteLine();
              }
            }

            file.Write(");\n\n");

            file.WriteLine("/*DONT_DELETE2567*/");
            file.WriteLine();

            file.Write("// always #10 clk = ~clk;\n\n");

            file.WriteLine("// initial           ");
            file.WriteLine("//   begin           ");
            file.WriteLine("//     rstN = 1;     ");
            file.WriteLine("//     clk = 0;      \n\n");
            file.WriteLine("//     #100 rstN = 0;");
            file.WriteLine("//     #10 rstN = 1; ");
            file.WriteLine("//   end             ");


            file.Write("\n\nendmodule\n\n");
            file.Close();
          }



        // Создаем топ для теста
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(testTopFileName))
        {

          file.WriteLine(rtbAddToTest.Text);
          file.WriteLine();

          //строчки для инклуда тестового файла и тестируемого файла
          file.WriteLine("`include \"" + fullFilePath.Replace("\\", "/") + "\"");
          file.WriteLine("`include \"" + testFileName.Replace("\\","/") + "\"");
          file.WriteLine();

          file.Write("module " + test_top_mod_name + ";\n\n");

          //Instantiate wires

          for (j = 0; j < module.listOfPorts.Length; j++)
          {
            if (module.listOfPorts[j] != null)
            {

              file.Write("wire ");
              //file.Write("logic " + module.listOfPorts[j].name);
              for (m = 0; m < (30 - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");
              file.Write(module.listOfPorts[j].dim_str + " ");

              file.Write(module.listOfPorts[j].name);
              for (m = 0; m < (20 - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write(";\n");

            }
          }


          //Instantiate DUT

          numOfPorts = module.getNumOfPorts();

          file.Write("\n\n" + mod_name + " " + dut_inst_name + " (" + "\n");

          for (j = 0; j < module.listOfPorts.Length; j++)
          {
            if (module.listOfPorts[j] != null)
            {

              file.Write("." + module.listOfPorts[j].name);
              for (m = 0; m < (40 - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write("(");
              file.Write(module.listOfPorts[j].name);
              for (m = 0; m < (20 - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write(")");
              if (numOfPorts > 1)
              {
                file.Write(",");
                numOfPorts--;
              }
              file.WriteLine();
            }
          }
          file.WriteLine(");");
          file.WriteLine();
          file.WriteLine();


          //Instantiate test

          numOfPorts = module.getNumOfPorts();
          file.Write("\n\ntest test_1 (\n");

          for (j = 0; j < module.listOfPorts.Length; j++)
          {
            if (module.listOfPorts[j] != null)
            {

              file.Write("." + module.listOfPorts[j].name);
              for (m = 0; m < (40 - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write("(");
              file.Write(module.listOfPorts[j].name);
              for (m = 0; m < (20 - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write(")");
              if (numOfPorts > 1)
              {
                file.Write(",");
                numOfPorts--;
              }
              file.WriteLine();
            }
          }
          file.WriteLine(");");
          file.WriteLine();
          file.WriteLine();


          file.Write("\n\nendmodule");

          file.Close();
        }   


      }

    }
}
