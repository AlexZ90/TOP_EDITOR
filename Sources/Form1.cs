﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Office.Interop;

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
        Connection[] listOfConnections = new Connection[500];
        List<Connection_> listOfConnections_ = new List<Connection_>();



    string safeFileName = ""; //!Добавил
        string fileDirPath = ""; //!Добавил
        string fullFilePath = ""; //!Добавил
    string vrfFolderNameGlbl = "";


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
            dt2.Columns.Add("Size string"); //!Добавил
            dataGridView2.DataSource = dt2;
            listOfInstLB.Click += new EventHandler(listOfInstLB_Click);

            dt3.Columns.Add("Dir");
            dt3.Columns.Add("Data type");
            dt3.Columns.Add("Size");
            dt3.Columns.Add("Name");
            dt3.Columns.Add("Size string"); //!Добавил
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


            string[] separators_ = { "\\" };
            string[] substrings_;


            fullFilePath = textBox1.Text;

            // Удаляем имя файла из пути к файлу
            fileDirPath = "";
            substrings_ = textBox1.Text.Split(separators_, 10, StringSplitOptions.RemoveEmptyEntries);
            fileDirPath = substrings_[0];
            for (i = 1; i < substrings_.Length - 1; i++)
            {
              fileDirPath = fileDirPath + "\\" + substrings_[i];
            }
            fileDirPath = fileDirPath + "\\";
            Console.WriteLine("fileDirPath = {0}", fileDirPath);
            //


            funcRes = testAnalyzer.analizeFile(fullFilePath, fileDirPath, ref newlistOfModules, cbOnlyForTest.Checked);
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

            if (funcRes == 1)
            {
                //Поиск комментариев с назначением портов
                string line;
                string[] substrings;
                string[] temp_substrings;
                string[] separators_1 = { "<pCom>" };
                string[] separators_2 = { " " };
                string modName;
                string portName;
                string comment;
                int mod_exists;
                int port_exists;

                mod_exists = 0;
                port_exists = 0;


                if (File.Exists(textBox1.Text))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(textBox1.Text))
                    {
                        while ((line = sr.ReadLine()) != null)
                            if (line.Contains("<pCom>"))
                            {

                                substrings = line.Split(separators_1, 10, StringSplitOptions.RemoveEmptyEntries);
                                if (substrings.Length < 4)
                                {
                                    MessageBox.Show("Неверный формат комментария для порта!\n Верный формат:\n// <pCom>mod_name<pCom>port_name<pCom>your_comment<pCom>");
                                    return;

                                }

                                modName = substrings[1];
                                temp_substrings = substrings[1].Split(separators_2, 10, StringSplitOptions.RemoveEmptyEntries);
                                modName = temp_substrings[0];

                                portName = substrings[2];
                                temp_substrings = substrings[2].Split(separators_2, 10, StringSplitOptions.RemoveEmptyEntries);
                                portName = temp_substrings[0];

                                comment = substrings[3];

                                mod_exists = 0;
                                port_exists = 0;

                                for (i = 0; i < newlistOfModules.Length; i++)
                                    if (newlistOfModules[i] != null)
                                    {
                                        if (newlistOfModules[i].getModName() == modName)
                                        {
                                            mod_exists = 1;
                                            for (j = 0; j < newlistOfModules[i].listOfPorts.Length; j++)
                                                if (newlistOfModules[i].listOfPorts[j] != null)
                                                {
                                                    if (newlistOfModules[i].listOfPorts[j].name == portName)
                                                    {
                                                        port_exists = 1;
                                                        newlistOfModules[i].listOfPorts[j].comment = comment;

                                                    }
                                                }
                                        }
                                    }
                                if (mod_exists == 0)
                                {
                                    MessageBox.Show("Приведен комметарий для порта несуществующего модуля!\n modName: " + modName + " \n portName: " + portName);
                                    return;
                                }

                                if (port_exists == 0)
                                {
                                    MessageBox.Show("Приведен комметарий для несуществующего порта!\n modName: " + modName + " \n portName: " + portName);
                                    return;
                                }
                            }
                    }
                }
            }
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



            //fullFilePath = openFileDialog1.FileName;//!Добавил
            textBox1.Text = openFileDialog1.FileName;//!Добавил
            //safeFileName = openFileDialog1.SafeFileName;//!Добавил
            //fileDirPath = fullFilePath.Replace(safeFileName, "");//!Добавил
            //MessageBox.Show(fileDirPath);

            

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
          Connection_ conn_;
      bool conn1_exists = false;
      bool conn2_exists = false;
      int connAlrdyExist_ = 0;
      Connection_ existsConn_;

      inst1 = this.getInstance(inst1_name);
          inst1_port = inst1.BaseModule.getPort(inst1_port_name);
          inst2 = this.getInstance(inst2_name);
          inst2_port = inst2.BaseModule.getPort(inst2_port_name);

          Console.WriteLine(inst1.Name);

          if (extConnCHBX.Checked == false)
            ext = 0;
          else
            ext = 1;
/*          if (ext == 0)
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
          }*/

      //******************************************** Connection_ begin

      foreach (Connection_ conn in listOfConnections_)
      {
        if (conn != null )
        {
          Console.WriteLine(conn.Name);
          conn.printInstPorts();
        }
      }

      existsConn_ = null;
      foreach (Connection_ conn in listOfConnections_)
      {
        if (conn != null && connName == conn.Name)
        {
          connAlrdyExist_++;
          existsConn_ = conn;
          break;
        }
      }


      if (connAlrdyExist_ > 0 && existsConn_ != null)
        {
          conn1_exists = existsConn_.InstPortExists(inst1, inst1_port);
          conn2_exists = existsConn_.InstPortExists(inst2, inst2_port);
          if (conn1_exists && conn2_exists)
            MessageBox.Show("Связь " + connName + " уже существует!");
          else if (conn1_exists)
          {
          existsConn_.addInstPort(inst2, inst2_port);
            if (ext == 1)
            {
              existsConn_.external = ext;
            }
          }
          else if (conn2_exists)
          {
            existsConn_.addInstPort(inst1, inst1_port);
            if (ext == 1)
            {
              existsConn_.external = ext;
            }
        }
          else MessageBox.Show("Ошибка при добавлении связи.");
        }
        else
        {

          conn_ = new Connection_(connName, ext);
          listOfConnections_.Add(conn_);
          conn_.addInstPort(inst1, inst1_port);
          conn_.addInstPort(inst2, inst2_port);
          conn_.external = ext;
        }


      //******************************************** Connection_ end



 /*     if (connAlrdyExist > 0)
            MessageBox.Show("Связь " + connName + " уже существует.");
          else
          {
            if (extConnCHBX.Checked == false)
              listOfConnections[curConntNumber] = new  Connection (connName, inst1, inst1_port, inst2, inst2_port, 0);
            else
              listOfConnections[curConntNumber] = new Connection(connName, inst1, inst1_port, inst1, inst1_port, 1);
            Console.WriteLine(listOfConnections[curConntNumber].inst_1.Name);
            curConntNumber++;
          }*/

          this.updateListOfConnections(listOfConnections, listOfConnLB);
        }

        private void updateListOfConnections(Connection[] listOfConnections, ListBox lb)
        {
          int i = 0;
          int j = 0;
          string selectedConn = "";
          lb.Items.Clear();
          /*
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
            */
      foreach (Connection_ conn in listOfConnections_)
      {
        if (conn != null)
        {
          lb.Items.Add(conn.Name);
        }
      }
      if (lb.Items.Count != 0)
      {
        lb.SelectedIndex = 0;
        showConnections(listOfConnLB.SelectedItem.ToString(), dt4);
      }
      else
      {
        showConnections("", dt4);
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

      Connection_ existsConn_;
      /*int i = 0;
      for (i = 0; i < listOfConnections.Length; i++)
        if (listOfConnections[i] != null && listOfConnections[i].Name == ConnName)
        {
          listOfConnections[i] = null;
          this.updateListOfConnections(listOfConnections, listOfConnLB);
        }
        */
      existsConn_ = null;
      foreach (Connection_ conn in listOfConnections_)
        if (conn != null && conn.Name == ConnName)
        {
          existsConn_ = conn;
        }
      if (existsConn_ != null)
      {
        listOfConnections_.Remove(existsConn_);
      }
      this.updateListOfConnections(listOfConnections, listOfConnLB);


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

      Connection_ existsConn_;

      //********** Connection_ Begin
      dt.Clear();
      if (connName != "")
      {
        foreach (Connection_ conn in listOfConnections_)
        {
          if (conn != null && connName == conn.Name)
          {
            foreach (InstPort instPort in conn.listOfInstPort)
            {
              dt.Rows.Add(instPort.inst.Name, instPort.port.name);
            }
            if (conn.external == 1) dt.Rows.Add("External", " ");
          }
        }
      }


      //********** Connection_ End

      /*
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
          */
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

        /*foreach (Connection_ conn in listOfConnections_)
        {
          if (conn.external == 1){
        }*/



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
        /*
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
              */

        foreach (Connection_ conn in listOfConnections_)
        {
          if (conn != null && conn.external == 0)
          {
            file.Write("wire ");
            file.Write(conn.listOfInstPort[1].port.dim_str); //!!!!!!!!!!!!!!!!!!!!!! Исправить (разрядности могут не совпадать)
            file.WriteLine(conn.Name + ";");
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

                foreach (Connection_ conn in listOfConnections_)
                {
                    foreach (InstPort instPort in conn.listOfInstPort)
                    {
                      if ((instPort.port.name == listOfInstances[i].BaseModule.listOfPorts[j].name) && (instPort.inst.Name == listOfInstances[i].Name))
                      {
                        file.Write(conn.Name);
                      }
                      //file.Write("wire ");
                      //file.WriteLine(conn.listOfInstPort[1].port.dim_str); //!!!!!!!!!!!!!!!!!!!!!! Исправить (разрядности могут не совпадать)
                      //file.WriteLine(conn.Name + ";");
                    }
                }
                /*
                      for (k = 0; k < listOfConnections.Length; k++)
                        if (listOfConnections[k] != null && (listOfConnections[k].inst_1_port.name == listOfInstances[i].BaseModule.listOfPorts[j].name && listOfConnections[k].inst_1.Name == listOfInstances[i].Name || listOfConnections[k].inst_2_port.name == listOfInstances[i].BaseModule.listOfPorts[j].name && listOfConnections[k].inst_2.Name == listOfInstances[i].Name))
                        {
                          file.Write(listOfConnections[k].Name);
                          break;
                        }
                        */
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
      try
      {
        deleteConnection(listOfConnLB.SelectedItem.ToString());
      }
      catch
      {
        MessageBox.Show("Ошибка при удалении связи!");
      }
        }



      private void btnCreateTest_Click(object sender, EventArgs e)
      {

        Module module;
        string mod_name;
        int numOfPorts;
        string metka = "";

      int maxDataTypeLength = 0;
      int maxDirLength = 0;
      int maxNameLength = 0;


      int i = 0;
      int j = 0;
      int k = 0;
      int m = 0;

        module = getModule(listOfModuleLB.SelectedItem.ToString());
        mod_name = module.getModName();
        numOfPorts = module.getNumOfPorts();

        string vrfFolderName = @"" + fileDirPath + "VRF\\" + mod_name + "_VRF\\";

      vrfFolderNameGlbl = vrfFolderName;
      module.vrfFolderPath = vrfFolderName;


      maxDataTypeLength = 0;
      for (j = 0; j < module.listOfPorts.Length; j++)
      {
        if (module.listOfPorts[j] != null)
        {
          if (module.listOfPorts[j].data_type.Length > maxDataTypeLength) maxDataTypeLength = module.listOfPorts[j].data_type.Length;
        }
      }

      maxDataTypeLength = maxDataTypeLength + 2;

      maxDirLength = 0;
        for (j = 0; j < module.listOfPorts.Length; j++)
        {
          if (module.listOfPorts[j] != null)
          {
            if (module.listOfPorts[j].dim_str.Length > maxDirLength) maxDirLength = module.listOfPorts[j].dim_str.Length;
          }
        }

        maxDirLength = maxDirLength + 2;

        maxNameLength = 0;
        for (j = 0; j < module.listOfPorts.Length; j++)
        {
          if (module.listOfPorts[j] != null)
          {
            if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
          }
        }

        maxNameLength = maxNameLength + 2;

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
        string addwave_do_filename = @"" + vrfFolderName + "addwave" + ".do";
        string readme_filename = @"" + vrfFolderName + "readme" + ".txt";

            string trigModName = mod_name + "_trig";
      string trigModFilename = @"" + vrfFolderName + trigModName + ".sv";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(readme_filename))
            {
                file.WriteLine("* Create new project in ModelSim");
                file.WriteLine("* Add file addButton_... to project");
                file.WriteLine("* Right click on addButton_... and click Execute. Two buttons (make_... and restart_...) should appear in top panel");
                file.WriteLine("* Button make_... runs test");
                file.WriteLine("* Button restart_... restarts test");
                file.Close();
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(addButton_do_filename))
        {
          file.WriteLine("# project addfile \"" + addButton_do_filename.Replace("\\", "/") + "");
          file.WriteLine("quit -sim");
          file.WriteLine("add button make_" + mod_name + " {do " + make_do_filename.Replace("\\", "\\\\") + "} NoDisable");
          file.WriteLine("add button restart_" + mod_name + " {do " + restart_do_filename.Replace("\\", "\\\\") + "} NoDisable");
          file.Close();
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(addwave_do_filename))
        {
                file.WriteLine("#first you mast run simulate without optimization                                           ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("proc add_wave_breadthwiserecursive { instance_name prev_group_option } {                    ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  set breadthwise_instances [find instances $instance_name/*]                               ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("# IF there are items itterate through them breadthwise                                      ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  foreach inst $breadthwise_instances {                                                     ");
                file.WriteLine("        # Separate \"/top/inst\"  from \"(MOD1)\"                                           ");
                file.WriteLine("        # echo $inst                                                                        ");
                file.WriteLine("        set inst_path [lindex [split $inst \" \"] 0]                                        ");
                file.WriteLine("        echo $inst_path                                                                     ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("        # Get just the end word after last \"/\"                                            ");
                file.WriteLine("        set gname     [lrange [split $inst_path \"/\"] end end]                             ");
                file.WriteLine("        # echo $gname                                                                       ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("        add_wave_breadthwiserecursive  \"$inst_path\"  \"$prev_group_option -group $gname\" ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  }                                                                                         ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  # Avoid including your top level /* as we already have /top/*                             ");
                file.WriteLine("  if { $instance_name != \"\" } {                                                           ");
                file.WriteLine("      # Echo the wave add command, but you can turn this off                                ");
                file.WriteLine("      echo add wave -noupdate $prev_group_option \"$instance_name/*\"                       ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("      set CMD \"add wave -noupdate -radix hex $prev_group_option $instance_name/*\"         ");
                file.WriteLine("      eval $CMD                                                                             ");
                file.WriteLine("  }                                                                                         ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  # Return up the recursing stack                                                           ");
                file.WriteLine("  return                                                                                    ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("}                                                                                           ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("proc add_wave_groupedrecursive { } {                                                        ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  set inst_path_1 [lindex [find instances /*] 0]                                            ");
                file.WriteLine("  set inst_path_2 [lindex [split $inst_path_1 \" \"] 0]                                     ");
                file.WriteLine("  add_wave_breadthwiserecursive $inst_path_2 \"\"                                           ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("  # Added all signals, now trigger a wave window update                                     ");
                file.WriteLine("  wave refresh                                                                              ");
                file.WriteLine("}                                                                                           ");
                file.WriteLine("                                                                                            ");
                file.WriteLine("add_wave_groupedrecursive                                                                   ");
                file.Close();
        }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(make_do_filename))
        {
          file.WriteLine("quit -sim");
          file.Write("vlog \"" + testTopFileName.Replace("\\", "\\\\") + "\"");
          foreach (object inclDir in includeDirLB.Items)
          {
          file.Write(" +incdir+" + inclDir.ToString().Replace("\\", "/") + " ");
          }
          file.WriteLine();
          file.WriteLine("vsim -novopt work." + test_top_mod_name);
          file.WriteLine();
          file.WriteLine("# add wave -divider -height 30 " + dut_inst_name);
          file.WriteLine("# add wave -radix hex sim:/" + test_top_mod_name + "/" + dut_inst_name + "/*");
          file.WriteLine("do " + addwave_do_filename.Replace("\\", "\\\\"));
          
          file.WriteLine("run 500");
          //file.WriteLine("#view wave");
          file.Close();
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(restart_do_filename))
        {
        file.Write("vlog \"" + testTopFileName.Replace("\\", "\\\\") + "\"");
        foreach (object inclDir in includeDirLB.Items)
        {
          file.Write(" +incdir+" + inclDir.ToString().Replace("\\", "/") + " ");
        }
        file.WriteLine();
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

            //Копируем содержимое тестового файла до описания портов в новый файл
            while (true)
            {
              metka = sr.ReadLine();
              if (string.Equals(metka, "/*DONT_DELETE2568*/") || metka.Contains("module test"))
              {
                Console.WriteLine("Equal ****************8");
                break;
              }
              else
              {
                file.WriteLine(metka);
              }
            }

            // Вставляем в новый файл с тестом данные из текстбокса
              if (rtbAddToTest.Text != "")
              {
                file.WriteLine(rtbAddToTest.Text);
                file.WriteLine();
              }
              file.WriteLine("/*DONT_DELETE2568*/");

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

                 

                  if (module.listOfPorts[j].data_type == "")
                  {
                    file.Write("logic ");
                    for (m = 0; m < maxDataTypeLength - 5; m++) file.Write(" ");
                  }
                  else if (module.listOfPorts[j].data_type != "reg")
                  {
                    file.Write(module.listOfPorts[j].data_type);
                    for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) file.Write(" ");
                  }
                  else
                  {
                    file.Write("logic ");
                    for (m = 0; m < maxDataTypeLength - 5; m++) file.Write(" ");
                  }

                //file.Write("logic " + module.listOfPorts[j].name);
                file.Write(module.listOfPorts[j].dim_str + " ");
                for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");


                  file.Write(module.listOfPorts[j].name);
                  for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");

                  if (numOfPorts > 1)
                  {
                    file.Write(",");
                    numOfPorts--;
                  }
                  file.WriteLine();
                }
              }

              file.Write(");\n\n");


            file.WriteLine("// initial           ");
            file.WriteLine("//   begin           ");
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].dir == "input")
                    {
                        file.Write("//     " + module.listOfPorts[j].name + " = '0;\n");
                    }
                }
            }

                        file.WriteLine("/*DONT_DELETE2567*/");

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
          if (rtbAddToTest.Text != "")
          {
            file.WriteLine(rtbAddToTest.Text);
            file.WriteLine();
          }

          file.WriteLine("/*DONT_DELETE2568*/");


            file.Write("module test (\n");

            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {

                if (module.listOfPorts[j].dir == "input") file.Write("output ");
                else if (module.listOfPorts[j].dir == "output") file.Write("input  ");
                else if (module.listOfPorts[j].dir == "inout") file.Write("inout  ");
                else MessageBox.Show("Ошибка! Неизвестное направление порта !");

                if (module.listOfPorts[j].data_type == "")
                {
                  file.Write("logic ");
                  for (m = 0; m < maxDataTypeLength - 5; m++) file.Write(" ");
                }
                else if (module.listOfPorts[j].data_type != "reg")
                {
                  file.Write(module.listOfPorts[j].data_type);
                  for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) file.Write(" ");
                }
                else
                {
                  file.Write("logic ");
                  for (m = 0; m < maxDataTypeLength - 5; m++) file.Write(" ");
                }
                  
              //file.Write("logic " + module.listOfPorts[j].name);
              file.Write(module.listOfPorts[j].dim_str + " ");
              for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");


                file.Write(module.listOfPorts[j].name);
                for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");

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
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {
                if (module.listOfPorts[j].dir == "input")
                {
                  file.Write("//     " + module.listOfPorts[j].name + " = '0;\n");
                }
              }
            }
            file.WriteLine("//     #100 rstN = 0;");
            file.WriteLine("//     #10 rstN = 1; ");
            file.WriteLine("//   end             ");


            file.Write("\n\nendmodule\n\n");
            file.Close();
          }



        // Создаем топ для теста
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(testTopFileName))
        {

        if (rtbAddToTest.Text != "")
        {
          file.WriteLine(rtbAddToTest.Text);
          file.WriteLine();
        }

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
            if (module.listOfPorts[j].data_type != "reg" && module.listOfPorts[j].data_type != "logic")
            {
              file.Write(module.listOfPorts[j].data_type);
              for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) file.Write(" ");
            }
            else
            {
              for (m = 0; m < maxDataTypeLength; m++) file.Write(" ");
            }

            //file.Write("logic " + module.listOfPorts[j].name);
            file.Write(module.listOfPorts[j].dim_str + " ");
            for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");


              file.Write(module.listOfPorts[j].name);
              for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
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
              for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write("(");
              file.Write(module.listOfPorts[j].name);
              for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
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
              for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
              file.Write("(");
              file.Write(module.listOfPorts[j].name);
              for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
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

      // *************** Создаем файл с триггерами для оценки частоты ***********************
        string clkForTriggerName = "clkForTrigger2785";

      using (System.IO.StreamWriter file = new System.IO.StreamWriter(trigModFilename))
      {

        if (rtbAddToTest.Text != "")
        {
          file.WriteLine(rtbAddToTest.Text);
          file.WriteLine();
        }

        //строчки для инклуда тестируемого файла
        file.WriteLine("`include \"" + fullFilePath.Replace("\\", "/") + "\"");
        file.WriteLine();


        numOfPorts = module.getNumOfPorts();
        file.Write("module " + trigModName + " (\n");

        file.WriteLine("input  logic  " + clkForTriggerName + ",");

        for (j = 0; j < module.listOfPorts.Length; j++)
        {
          if (module.listOfPorts[j] != null)
          {

            if (module.listOfPorts[j].dir == "input") file.Write("input  ");
            else if (module.listOfPorts[j].dir == "output") file.Write("output ");
            else if (module.listOfPorts[j].dir == "inout") file.Write("inout  ");
            else MessageBox.Show("Ошибка! Неизвестное направление порта !");

            file.Write(module.listOfPorts[j].data_type);
            for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) file.Write(" ");
            //file.Write("logic " + module.listOfPorts[j].name);
            file.Write(module.listOfPorts[j].dim_str + " ");
            for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");
            file.Write(module.listOfPorts[j].name);
            for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");

            if (numOfPorts > 1)
            {
              file.Write(",");
              numOfPorts--;
            }
            file.WriteLine();
          }
        }

        file.Write(");\n\n");


        //Instantiate wires

        for (j = 0; j < module.listOfPorts.Length; j++)
        {
          if (module.listOfPorts[j] != null)
          {

            file.Write("wire ");
            if (module.listOfPorts[j].data_type != "reg" && module.listOfPorts[j].data_type != "logic")
            {
              file.Write(module.listOfPorts[j].data_type);
              for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) file.Write(" ");
            }
            else
            {
              for (m = 0; m < maxDataTypeLength; m++) file.Write(" ");
            }
            //file.Write("logic " + module.listOfPorts[j].name);
            file.Write(module.listOfPorts[j].dim_str + " ");
            for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) file.Write(" ");

            if (module.listOfPorts[j].dir == "input")
              file.Write("tr_" + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "output")
              file.Write("cb_" + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "inout")
              file.Write(module.listOfPorts[j].name);
            else 
              MessageBox.Show("Ошибка! Неизвестное направление порта !");

            for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
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
            for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
            file.Write("(");

            if (module.listOfPorts[j].dir == "input")
              file.Write("tr_" + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "output")
              file.Write("cb_" + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "inout")
              file.Write(module.listOfPorts[j].name);
            else
              MessageBox.Show("Ошибка! Неизвестное направление порта !");


            for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");
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

        file.WriteLine("always_ff @(posedge " + clkForTriggerName + ")");
        file.WriteLine("begin");

        for (j = 0; j < module.listOfPorts.Length; j++)
        {
          if (module.listOfPorts[j] != null)
          {

            if (module.listOfPorts[j].dir == "input")
              file.Write("  tr_" + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "output")
              file.Write("  " + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "inout")
              file.Write("");
            else
              MessageBox.Show("Ошибка! Неизвестное направление порта !");

            for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");

            file.Write("<= ");

            if (module.listOfPorts[j].dir == "input")
              file.Write(module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "output")
              file.Write("cb_" + module.listOfPorts[j].name);
            else if (module.listOfPorts[j].dir == "inout")
              file.Write("");
            else
              MessageBox.Show("Ошибка! Неизвестное направление порта !");

            for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) file.Write(" ");

            file.Write(";\n");

          }
        }


        file.Write("end");


        file.Write("\n\nendmodule");

        



        file.Close();
      }


    }




        private void getOutputsBtn_Click(object sender, EventArgs e)
        {
            //******************************
            Module module;
            string mod_name;
            int numOfPorts;
            string metka = "";
            int maxDataTypeLength = 0;
            int maxDirLength = 0;
            int maxNameLength = 0;

            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;
            int outputs_exists = 0;


            module = getModule(listOfModuleLB.SelectedItem.ToString());
            mod_name = module.getModName();
            numOfPorts = module.getNumOfPorts();

            outputRTB.Clear();

            maxDataTypeLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {
                if (module.listOfPorts[j].data_type.Length > maxDataTypeLength) maxDataTypeLength = module.listOfPorts[j].data_type.Length;
              }
            }

            maxDataTypeLength = maxDataTypeLength + 2;

            maxDirLength = 0;
              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {
                  if (module.listOfPorts[j].dim_str.Length > maxDirLength) maxDirLength = module.listOfPorts[j].dim_str.Length;
                }
              }

              maxDirLength = maxDirLength + 2;

              maxNameLength = 0;
              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {
                  if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
                }
              }

              maxNameLength = maxNameLength + 2;

            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].dir == "output")
                    {
                        outputs_exists = 1;
                        outputRTB.AppendText("output ");
                        outputRTB.AppendText(module.listOfPorts[j].data_type);
                        for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) outputRTB.AppendText(" ");
                        //file.Write("logic " + module.listOfPorts[j].name);
                        outputRTB.AppendText(module.listOfPorts[j].dim_str + " ");
                        for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) outputRTB.AppendText(" ");
                        outputRTB.AppendText(module.listOfPorts[j].name);
                        for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) outputRTB.AppendText(" ");
                        outputRTB.AppendText("\n");
                    }
                }
            }
            if (outputs_exists == 1) Clipboard.SetText(outputRTB.Text);
            //******************
            
            if (createDocCHB.Checked)
            {

              Microsoft.Office.Interop.Word._Application objApp;
              Microsoft.Office.Interop.Word._Document objDoc;
              objApp = new Microsoft.Office.Interop.Word.Application();
              objApp.Visible = true;
              objDoc = objApp.Documents.Add();

              object objMiss = System.Reflection.Missing.Value;
              object objEndOfDocFlag = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

              Microsoft.Office.Interop.Word.Paragraph objPara1; //define paragraph object
              object oRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
              objPara1 = objDoc.Content.Paragraphs.Add(ref oRng); //add paragraph at end of document
              objPara1.Range.Text = "Test Table Caption"; //add some text in paragraph
              objPara1.Format.SpaceAfter = 10; //define some style
              objPara1.Range.InsertParagraphAfter(); //insert paragraph

              Microsoft.Office.Interop.Word.Table objTab1;
              Microsoft.Office.Interop.Word.Range objWordRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;


              //Add text after the table.
              //objWordRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;
              //objWordRng.InsertParagraphAfter();
              //objWordRng.InsertAfter("THE END.");

              int numOfOutPorts = module.getNumOfOutPorts();
              
              
              objTab1 = objDoc.Tables.Add(objWordRng, numOfOutPorts + 1, 4, ref objMiss, ref objMiss);
              objTab1.Range.ParagraphFormat.SpaceAfter = 6;
              int iRow, iCols;
              string strText;
              iCols = 1;

              objTab1.Cell(1, 1).Range.Text = "Наименование";
              objTab1.Cell(1, 2).Range.Text = "Направление";
              objTab1.Cell(1, 3).Range.Text = "Разрядность";
              objTab1.Cell(1, 4).Range.Text = "Назначение";

              iRow = 2;


              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {

                  if (module.listOfPorts[j].dir == "output")
                  {
                    objTab1.Cell(iRow, 1).Range.Text = module.listOfPorts[j].name;
                    objTab1.Cell(iRow, 3).Range.Text = module.listOfPorts[j].dim.ToString();
                    objTab1.Cell(iRow, 4).Range.Text = module.listOfPorts[j].comment;
                    iRow++;
                  }
                }
              }

              objTab1.Rows[1].Range.Font.Bold = 1;
              objTab1.Rows[1].Range.Font.Italic = 0;
            }
            
            
        }

        private void getInputsBtn_Click(object sender, EventArgs e)
        {
            //******************************
            Module module;
            string mod_name;
            int numOfPorts;
            string metka = "";
            int maxDataTypeLength = 0;
            int maxDirLength = 0;
            int maxNameLength = 0;

            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;


            module = getModule(listOfModuleLB.SelectedItem.ToString());
            mod_name = module.getModName();
            numOfPorts = module.getNumOfPorts();

            outputRTB.Clear();

            maxDataTypeLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {
                if (module.listOfPorts[j].data_type.Length > maxDataTypeLength) maxDataTypeLength = module.listOfPorts[j].data_type.Length;
              }
            }

            maxDataTypeLength = maxDataTypeLength + 2;

            maxDirLength = 0;
              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {
                  if (module.listOfPorts[j].dim_str.Length > maxDirLength) maxDirLength = module.listOfPorts[j].dim_str.Length;
                }
              }

              maxDirLength = maxDirLength + 2;

              maxNameLength = 0;
              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {
                  if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
                }
              }

              maxNameLength = maxNameLength + 2;

            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].dir == "input")
                    {
                        outputRTB.AppendText("input ");
                        outputRTB.AppendText(module.listOfPorts[j].data_type);
                        for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) outputRTB.AppendText(" ");
                        //file.Write("logic " + module.listOfPorts[j].name);
                        outputRTB.AppendText(module.listOfPorts[j].dim_str + " ");
                        for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) outputRTB.AppendText(" ");
                        outputRTB.AppendText(module.listOfPorts[j].name);
                        for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) outputRTB.AppendText(" ");
                        outputRTB.AppendText("\n");
                    }
                }
            }
            Clipboard.SetText(outputRTB.Text);
            //******************
            
            if (createDocCHB.Checked)
            {

              Microsoft.Office.Interop.Word._Application objApp;
              Microsoft.Office.Interop.Word._Document objDoc;
              objApp = new Microsoft.Office.Interop.Word.Application();
              objApp.Visible = true;
              objDoc = objApp.Documents.Add();

              object objMiss = System.Reflection.Missing.Value;
              object objEndOfDocFlag = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

              Microsoft.Office.Interop.Word.Paragraph objPara1; //define paragraph object
              object oRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
              objPara1 = objDoc.Content.Paragraphs.Add(ref oRng); //add paragraph at end of document
              objPara1.Range.Text = "Test Table Caption"; //add some text in paragraph
              objPara1.Format.SpaceAfter = 10; //define some style
              objPara1.Range.InsertParagraphAfter(); //insert paragraph

              Microsoft.Office.Interop.Word.Table objTab1;
              Microsoft.Office.Interop.Word.Range objWordRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;


              //Add text after the table.
              //objWordRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;
              //objWordRng.InsertParagraphAfter();
              //objWordRng.InsertAfter("THE END.");

              int numOfInPorts = module.getNumOfInPorts();
              
              objTab1 = objDoc.Tables.Add(objWordRng, numOfInPorts + 1, 4, ref objMiss, ref objMiss);
              objTab1.Range.ParagraphFormat.SpaceAfter = 6;
              int iRow, iCols;
              string strText;
              iCols = 1;

              objTab1.Cell(1, 1).Range.Text = "Наименование";
              objTab1.Cell(1, 2).Range.Text = "Направление";
              objTab1.Cell(1, 3).Range.Text = "Разрядность";
              objTab1.Cell(1, 4).Range.Text = "Назначение";

              iRow = 2;


              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {

                  if (module.listOfPorts[j].dir == "input")
                  {
                    objTab1.Cell(iRow, 1).Range.Text = module.listOfPorts[j].name;
                    objTab1.Cell(iRow, 3).Range.Text = module.listOfPorts[j].dim.ToString();
                    objTab1.Cell(iRow, 4).Range.Text = module.listOfPorts[j].comment;
                    iRow++;
                  }
                  
                }
              }

              objTab1.Rows[1].Range.Font.Bold = 1;
              objTab1.Rows[1].Range.Font.Italic = 0;
            }
            
            
        }

        private void genInstBtn_Click(object sender, EventArgs e)
        {
            //******************************
            Module module;
            string mod_name;
            int numOfPorts;
            string metka = "";
            int maxNameLength = 0;

            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;


            module = getModule(listOfModuleLB.SelectedItem.ToString());
            mod_name = module.getModName();
            numOfPorts = module.getNumOfPorts();

            outputRTB.Clear();

            maxNameLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
                }
            }

            maxNameLength = maxNameLength + 2;

            outputRTB.AppendText(module.getModName() + " block_" + module.getModName() + "\n");
            outputRTB.AppendText("(\n");

            numOfPorts = module.getNumOfPorts();

            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    outputRTB.AppendText("  ." + module.listOfPorts[j].name + " ");
                    for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) outputRTB.AppendText(" ");
                    outputRTB.AppendText("()");
                    if (numOfPorts > 1)
                    {
                        outputRTB.AppendText(",");
                        numOfPorts--;
                    }
                    outputRTB.AppendText("\n");
                }
            }
            outputRTB.AppendText(");\n");
            Clipboard.SetText(outputRTB.Text);
            //******************
        }

        private void genPortsDef_Click(object sender, EventArgs e)
        {
            //******************************
            Module module;
            string mod_name;
            int numOfPorts;
            string metka = "";
            int maxDataTypeLength = 0;
            int maxDirLength = 0;
            int maxNameLength = 0;

            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;


            module = getModule(listOfModuleLB.SelectedItem.ToString());
            mod_name = module.getModName();
            numOfPorts = module.getNumOfPorts();

            outputRTB.Clear();

            maxDataTypeLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {
                if (module.listOfPorts[j].data_type.Length > maxDataTypeLength) maxDataTypeLength = module.listOfPorts[j].data_type.Length;
              }
            }

            maxDataTypeLength = maxDataTypeLength + 2;
            
            
            maxDirLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].dim_str.Length > maxDirLength) maxDirLength = module.listOfPorts[j].dim_str.Length;
                }
            }

            maxDirLength = maxDirLength + 2;

            maxNameLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
                }
            }

            maxNameLength = maxNameLength + 2;


            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    outputRTB.AppendText(module.listOfPorts[j].data_type);
                    for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) outputRTB.AppendText(" ");
                    //file.Write("logic " + module.listOfPorts[j].name);
                    outputRTB.AppendText(module.listOfPorts[j].dim_str + " ");
                    for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) outputRTB.AppendText(" ");
                    outputRTB.AppendText(module.listOfPorts[j].name);
                    for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) outputRTB.AppendText(" ");
                    outputRTB.AppendText(";\n");
                }
            }
            Clipboard.SetText(outputRTB.Text);
            //******************
        }

        private void getPortsBtn_Click(object sender, EventArgs e)
        {
            //******************************
            Module module;
            string mod_name;
            int numOfPorts;
            string metka = "";
            int maxDataTypeLength = 0;
            int maxDirLength = 0;
            int maxNameLength = 0;

            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;
            
            module = getModule(listOfModuleLB.SelectedItem.ToString());
            mod_name = module.getModName();
            numOfPorts = module.getNumOfPorts();
            numOfPortsValLbl.Text = numOfPorts.ToString() + ", I:" + module.getNumOfInPorts() + ", O:" + module.getNumOfOutPorts() + ", IO:" + module.getNumOfInOutPorts();

            outputRTB.Clear();

            maxDataTypeLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {
                if (module.listOfPorts[j].data_type.Length > maxDataTypeLength) maxDataTypeLength = module.listOfPorts[j].data_type.Length;
              }
            }

            maxDataTypeLength = maxDataTypeLength + 2;
            
            
            maxDirLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].dim_str.Length > maxDirLength) maxDirLength = module.listOfPorts[j].dim_str.Length;
                }
            }

            maxDirLength = maxDirLength + 2;

            maxNameLength = 0;
            for (j = 0; j < module.listOfPorts.Length; j++)
            {
              if (module.listOfPorts[j] != null)
              {
                if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
              }
            }

            maxNameLength = maxNameLength + 2;


            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {
                    if (module.listOfPorts[j].dir == "output")
                    {
                        if (invertPortsChb.Checked)
                            outputRTB.AppendText("input  ");
                        else
                            outputRTB.AppendText("output ");
                    }
                    else if (module.listOfPorts[j].dir == "input")
                    {
                        if (invertPortsChb.Checked)
                            outputRTB.AppendText("output ");
                        else
                            outputRTB.AppendText("input  ");
                    }
                    else if (module.listOfPorts[j].dir == "inout") outputRTB.AppendText("inout  ");
                    else MessageBox.Show("Ошибка! Неизвестное направление порта !");

                    outputRTB.AppendText(module.listOfPorts[j].data_type);
                    for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) outputRTB.AppendText(" ");
                    //file.Write("logic " + module.listOfPorts[j].name);
                    outputRTB.AppendText(module.listOfPorts[j].dim_str + " ");
                    for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) outputRTB.AppendText(" ");
                    outputRTB.AppendText(module.listOfPorts[j].name);
                    for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) outputRTB.AppendText(" ");
                    outputRTB.AppendText("\n");
                }
            }
            Clipboard.SetText(outputRTB.Text);

            if (createDocCHB.Checked)
            {

              Microsoft.Office.Interop.Word._Application objApp;
              Microsoft.Office.Interop.Word._Document objDoc;
              objApp = new Microsoft.Office.Interop.Word.Application();
              objApp.Visible = true;
              objDoc = objApp.Documents.Add();

              object objMiss = System.Reflection.Missing.Value;
              object objEndOfDocFlag = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

              Microsoft.Office.Interop.Word.Paragraph objPara1; //define paragraph object
              object oRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range; //go to end of the page
              objPara1 = objDoc.Content.Paragraphs.Add(ref oRng); //add paragraph at end of document
              objPara1.Range.Text = "Test Table Caption"; //add some text in paragraph
              objPara1.Format.SpaceAfter = 10; //define some style
              objPara1.Range.InsertParagraphAfter(); //insert paragraph

              Microsoft.Office.Interop.Word.Table objTab1;
              Microsoft.Office.Interop.Word.Range objWordRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;


              //Add text after the table.
              //objWordRng = objDoc.Bookmarks.get_Item(ref objEndOfDocFlag).Range;
              //objWordRng.InsertParagraphAfter();
              //objWordRng.InsertAfter("THE END.");


              objTab1 = objDoc.Tables.Add(objWordRng, numOfPorts + 1, 4, ref objMiss, ref objMiss);
              objTab1.Range.ParagraphFormat.SpaceAfter = 6;
              int iRow, iCols;
              string strText;
              iCols = 1;

              objTab1.Cell(1, 1).Range.Text = "Наименование";
              objTab1.Cell(1, 2).Range.Text = "Направление";
              objTab1.Cell(1, 3).Range.Text = "Разрядность";
              objTab1.Cell(1, 4).Range.Text = "Назначение";

              iRow = 2;


              for (j = 0; j < module.listOfPorts.Length; j++)
              {
                if (module.listOfPorts[j] != null)
                {
                  strText = module.listOfPorts[j].name;
                  objTab1.Cell(iRow, 1).Range.Text = strText;

                  if (module.listOfPorts[j].dir == "output")
                  {
                    objTab1.Cell(iRow, 2).Range.Text = "Выход";
                  }
                  else if (module.listOfPorts[j].dir == "input")
                  {
                    objTab1.Cell(iRow, 2).Range.Text = "Вход";
                  }
                  else if (module.listOfPorts[j].dir == "inout")
                  {
                    objTab1.Cell(iRow, 2).Range.Text = "Вх/Вых";
                  }
                  else MessageBox.Show("Ошибка! Неизвестное направление порта !");

                  objTab1.Cell(iRow, 3).Range.Text = module.listOfPorts[j].dim.ToString();
                  objTab1.Cell(iRow, 4).Range.Text = module.listOfPorts[j].comment;

                  iRow++;

                }
              }

              objTab1.Rows[1].Range.Font.Bold = 1;
              objTab1.Rows[1].Range.Font.Italic = 0;
            }

            

            //******************
        }

    private void connectionTB_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter) { this.createConnBtn_Click(sender, e); }
    }

    private void instNameTB_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter) { this.createInstBtn_Click(sender, e); }
    }

    private void openVrfFldrBtn_Click(object sender, EventArgs e)
    {

      Module module;

      string vrfFolderName;

      try
      {
        module = getModule(listOfModuleLB.SelectedItem.ToString());

        vrfFolderName= @"" + module.fileFolderPath + "VRF\\" + module.getModName() + "_VRF\\";
        if (Directory.Exists(vrfFolderName))
          Process.Start(vrfFolderName);
        else
        {
          MessageBox.Show("Ошибка при открытии папки! \n Сначала нужно нажать кнопки Analize и Create test!");
        }
      }
      catch
      {
        MessageBox.Show("Ошибка при попытке открыть папку с тестом!");
      }
    }

    private void getFormatBtn_Click(object sender, EventArgs e)
    {
      //******************************
      Module module;
      string mod_name;
      int numOfPorts;
      int maxDataTypeLength = 0;
      int maxDirLength = 0;
      int maxNameLength = 0;
      int i = 0;
      int j = 0;
      int k = 0;
      int m = 0;


      module = getModule(listOfModuleLB.SelectedItem.ToString());
      mod_name = module.getModName();
      numOfPorts = module.getNumOfPorts();

      outputRTB.Clear();


      outputRTB.AppendText("module " + mod_name + "\n");
      outputRTB.AppendText("(\n");

      maxDataTypeLength = 0;
      for (j = 0; j < module.listOfPorts.Length; j++)
      {
        if (module.listOfPorts[j] != null)
        {
          if (module.listOfPorts[j].data_type.Length > maxDataTypeLength) maxDataTypeLength = module.listOfPorts[j].data_type.Length;
        }
      }

      maxDataTypeLength = maxDataTypeLength + 2;     
      
      
      maxDirLength = 0;
      for (j = 0; j < module.listOfPorts.Length; j++)
      {
        if (module.listOfPorts[j] != null)
        {
          if (module.listOfPorts[j].dim_str.Length > maxDirLength) maxDirLength = module.listOfPorts[j].dim_str.Length;
        }
      }

      maxDirLength = maxDirLength + 2;


      maxNameLength = 0;
      for (j = 0; j < module.listOfPorts.Length; j++)
      {
        if (module.listOfPorts[j] != null)
        {
          if (module.listOfPorts[j].name.Length > maxNameLength) maxNameLength = module.listOfPorts[j].name.Length;
        }
      }

      maxNameLength = maxNameLength + 2;


      i = 0;
      for (j = 0; j < module.listOfPorts.Length; j++)
      {
        if (module.listOfPorts[j] != null)
        {
          i++;
          outputRTB.AppendText("  ");
          if (module.listOfPorts[j].dir == "output")
          {
            if (invertPortsChb.Checked)
              outputRTB.AppendText("input  ");
            else
              outputRTB.AppendText("output ");
          }
          else if (module.listOfPorts[j].dir == "input")
          {
            if (invertPortsChb.Checked)
              outputRTB.AppendText("output ");
            else
              outputRTB.AppendText("input  ");
          }
          else if (module.listOfPorts[j].dir == "inout") outputRTB.AppendText("inout  ");
          else MessageBox.Show("Ошибка! Неизвестное направление порта !");

          outputRTB.AppendText(module.listOfPorts[j].data_type);
          for (m = 0; m < (maxDataTypeLength - module.listOfPorts[j].data_type.Length); m++) outputRTB.AppendText(" ");
          //file.Write("logic " + module.listOfPorts[j].name);
          outputRTB.AppendText(module.listOfPorts[j].dim_str + " ");
          for (m = 0; m < (maxDirLength - module.listOfPorts[j].dim_str.Length); m++) outputRTB.AppendText(" ");
          outputRTB.AppendText(module.listOfPorts[j].name);
          for (m = 0; m < (maxNameLength - module.listOfPorts[j].name.Length); m++) outputRTB.AppendText(" ");
          if (i != numOfPorts)
            outputRTB.AppendText(",");
          outputRTB.AppendText("\n");
        }
      }

      outputRTB.AppendText(");\n");

      Clipboard.SetText(outputRTB.Text);
      //******************
    }

    private void openInFileFldrBtn_Click(object sender, EventArgs e)
    {

      Module module;

      try
      {
        module = getModule(listOfModuleLB.SelectedItem.ToString());
        Process.Start(module.fileFolderPath);
      }
      catch
      {
        MessageBox.Show("Какая-то непонятная ошибка при попытке открыть папку с исходным файлом!");
      }
    }

    private void openInFileBtn_Click(object sender, EventArgs e)
    {
      Module module;

      try
      {
        module = getModule(listOfModuleLB.SelectedItem.ToString());
        Process.Start(module.filePath);
      }
      catch
      {
        MessageBox.Show("Какая-то непонятная ошибка при попытке открыть исходный файл!");
      }
    }

        private void genVisPortsBtn_Click(object sender, EventArgs e)
        {
            Module module;
            string mod_name;
            int numOfPorts;
            int i = 0;
            int j = 0;
            int k = 0;
            int m = 0;
            int height = 5;
            int gap = 2;
            int initY = 50;
            int direction = 0; // input - 0, output - 1 
            VisDrawer Vis = new VisDrawer();

            module = getModule(listOfModuleLB.SelectedItem.ToString());
            mod_name = module.getModName();
            numOfPorts = module.getNumOfPorts();

            for (j = 0; j < module.listOfPorts.Length; j++)
            {
                if (module.listOfPorts[j] != null)
                {

                    if (module.listOfPorts[j].dir == "output")
                    {
                        direction = 1;
                    }
                    else if (module.listOfPorts[j].dir == "input")
                    {
                        direction = 0;
                    }
                    else if (module.listOfPorts[j].dir == "inout")
                    {
                        direction = 0;
                    }
                    else MessageBox.Show("Ошибка! Неизвестное направление порта !");

                    Vis.DropShape(module.listOfPorts[j].name, 0.0, initY, height, direction);
                    initY = initY - height - gap;
                }
            }

        }

    private void addIncludeBtn_Click(object sender, EventArgs e)
    {

      string folder;
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        folder = folderBrowserDialog1.SelectedPath;
        includeDirLB.Items.Add(folder);
      }
    }

    private void inclDirDelBtn_Click(object sender, EventArgs e)
    {
      includeDirLB.Items.Remove(includeDirLB.SelectedItem);
    }

    private void inclDirClearBtn_Click(object sender, EventArgs e)
    {
      includeDirLB.Items.Clear();
    }

    private void includeDirLB_DoubleClick(object sender, EventArgs e)
    {
      includeDirLB.Items.Add(Clipboard.GetText());
    }

    private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      string filePath = "";
      if (Clipboard.ContainsText())
      {
        filePath = Clipboard.GetText();
        if (filePath.Contains("\""))
          filePath = filePath.Replace("\"", "");
        textBox1.Text = filePath;
      }
      else
      {
        MessageBox.Show("В буфере обмена нет текста");
      }

    }
  }
}
