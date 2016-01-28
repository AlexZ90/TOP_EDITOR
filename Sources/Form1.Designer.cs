namespace TopEditor
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.listOfModuleLB = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.delModBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listOfInstLB = new System.Windows.Forms.ListBox();
            this.instNameTB = new System.Windows.Forms.TextBox();
            this.createInstBtn = new System.Windows.Forms.Button();
            this.delInstBtn = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.listOfInstLB2 = new System.Windows.Forms.ListBox();
            this.connectionTB = new System.Windows.Forms.TextBox();
            this.listOfConnLB = new System.Windows.Forms.ListBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.createConnBtn = new System.Windows.Forms.Button();
            this.createTopBTN = new System.Windows.Forms.Button();
            this.topNameTB = new System.Windows.Forms.TextBox();
            this.extConnCHBX = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCreateTest = new System.Windows.Forms.Button();
            this.cbOnlyForTest = new System.Windows.Forms.CheckBox();
            this.rtbAddToTest = new System.Windows.Forms.RichTextBox();
            this.lblAddToTestFiles = new System.Windows.Forms.Label();
            this.outputRTB = new System.Windows.Forms.RichTextBox();
            this.getOutputsBtn = new System.Windows.Forms.Button();
            this.getInputsBtn = new System.Windows.Forms.Button();
            this.genInstBtn = new System.Windows.Forms.Button();
            this.genPortsDefBtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.invertPortsChb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Analize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listOfModuleLB
            // 
            this.listOfModuleLB.FormattingEnabled = true;
            this.listOfModuleLB.Location = new System.Drawing.Point(227, 36);
            this.listOfModuleLB.Name = "listOfModuleLB";
            this.listOfModuleLB.Size = new System.Drawing.Size(120, 186);
            this.listOfModuleLB.TabIndex = 2;
            this.listOfModuleLB.Tag = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "D:\\Downloads\\Test.txt";
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(13, 61);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(77, 23);
            this.browseBtn.TabIndex = 4;
            this.browseBtn.Text = "Add file";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // delModBtn
            // 
            this.delModBtn.Location = new System.Drawing.Point(13, 118);
            this.delModBtn.Name = "delModBtn";
            this.delModBtn.Size = new System.Drawing.Size(77, 23);
            this.delModBtn.TabIndex = 5;
            this.delModBtn.Text = "Del mod";
            this.delModBtn.UseVisualStyleBackColor = true;
            this.delModBtn.Click += new System.EventHandler(this.delModBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(353, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(580, 186);
            this.dataGridView1.TabIndex = 6;
            // 
            // listOfInstLB
            // 
            this.listOfInstLB.FormattingEnabled = true;
            this.listOfInstLB.Location = new System.Drawing.Point(227, 229);
            this.listOfInstLB.Name = "listOfInstLB";
            this.listOfInstLB.Size = new System.Drawing.Size(120, 160);
            this.listOfInstLB.TabIndex = 8;
            // 
            // instNameTB
            // 
            this.instNameTB.Location = new System.Drawing.Point(12, 229);
            this.instNameTB.Name = "instNameTB";
            this.instNameTB.Size = new System.Drawing.Size(209, 20);
            this.instNameTB.TabIndex = 9;
            // 
            // createInstBtn
            // 
            this.createInstBtn.Location = new System.Drawing.Point(13, 261);
            this.createInstBtn.Name = "createInstBtn";
            this.createInstBtn.Size = new System.Drawing.Size(77, 23);
            this.createInstBtn.TabIndex = 10;
            this.createInstBtn.Text = "Create Inst";
            this.createInstBtn.UseVisualStyleBackColor = true;
            this.createInstBtn.Click += new System.EventHandler(this.createInstBtn_Click);
            // 
            // delInstBtn
            // 
            this.delInstBtn.Location = new System.Drawing.Point(13, 291);
            this.delInstBtn.Name = "delInstBtn";
            this.delInstBtn.Size = new System.Drawing.Size(77, 23);
            this.delInstBtn.TabIndex = 11;
            this.delInstBtn.Text = "Del Inst";
            this.delInstBtn.UseVisualStyleBackColor = true;
            this.delInstBtn.Click += new System.EventHandler(this.delInstBtn_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(353, 229);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(287, 160);
            this.dataGridView2.TabIndex = 6;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(646, 229);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(287, 160);
            this.dataGridView3.TabIndex = 6;
            // 
            // listOfInstLB2
            // 
            this.listOfInstLB2.FormattingEnabled = true;
            this.listOfInstLB2.Location = new System.Drawing.Point(939, 229);
            this.listOfInstLB2.Name = "listOfInstLB2";
            this.listOfInstLB2.Size = new System.Drawing.Size(120, 160);
            this.listOfInstLB2.TabIndex = 8;
            // 
            // connectionTB
            // 
            this.connectionTB.Location = new System.Drawing.Point(12, 427);
            this.connectionTB.Name = "connectionTB";
            this.connectionTB.Size = new System.Drawing.Size(209, 20);
            this.connectionTB.TabIndex = 9;
            // 
            // listOfConnLB
            // 
            this.listOfConnLB.FormattingEnabled = true;
            this.listOfConnLB.Location = new System.Drawing.Point(227, 427);
            this.listOfConnLB.Name = "listOfConnLB";
            this.listOfConnLB.Size = new System.Drawing.Size(120, 160);
            this.listOfConnLB.TabIndex = 8;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(353, 427);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(339, 160);
            this.dataGridView4.TabIndex = 6;
            // 
            // createConnBtn
            // 
            this.createConnBtn.Location = new System.Drawing.Point(13, 460);
            this.createConnBtn.Name = "createConnBtn";
            this.createConnBtn.Size = new System.Drawing.Size(77, 23);
            this.createConnBtn.TabIndex = 12;
            this.createConnBtn.Text = "Add conn";
            this.createConnBtn.UseVisualStyleBackColor = true;
            this.createConnBtn.Click += new System.EventHandler(this.createConnBtn_Click);
            // 
            // createTopBTN
            // 
            this.createTopBTN.Location = new System.Drawing.Point(13, 640);
            this.createTopBTN.Name = "createTopBTN";
            this.createTopBTN.Size = new System.Drawing.Size(77, 23);
            this.createTopBTN.TabIndex = 13;
            this.createTopBTN.Text = "Create top";
            this.createTopBTN.UseVisualStyleBackColor = true;
            this.createTopBTN.Click += new System.EventHandler(this.createTopBTN_Click);
            // 
            // topNameTB
            // 
            this.topNameTB.Location = new System.Drawing.Point(13, 614);
            this.topNameTB.Name = "topNameTB";
            this.topNameTB.Size = new System.Drawing.Size(208, 20);
            this.topNameTB.TabIndex = 14;
            this.topNameTB.Text = "top";
            // 
            // extConnCHBX
            // 
            this.extConnCHBX.AutoSize = true;
            this.extConnCHBX.Location = new System.Drawing.Point(95, 465);
            this.extConnCHBX.Name = "extConnCHBX";
            this.extConnCHBX.Size = new System.Drawing.Size(63, 17);
            this.extConnCHBX.TabIndex = 15;
            this.extConnCHBX.Text = "external";
            this.extConnCHBX.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 595);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Top name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 408);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Connection name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Instance name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Input file name";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 489);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Del conn";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnCreateTest
            // 
            this.btnCreateTest.Location = new System.Drawing.Point(940, 35);
            this.btnCreateTest.Name = "btnCreateTest";
            this.btnCreateTest.Size = new System.Drawing.Size(119, 23);
            this.btnCreateTest.TabIndex = 21;
            this.btnCreateTest.Text = "Create test";
            this.btnCreateTest.UseVisualStyleBackColor = true;
            this.btnCreateTest.Click += new System.EventHandler(this.btnCreateTest_Click);
            // 
            // cbOnlyForTest
            // 
            this.cbOnlyForTest.AutoSize = true;
            this.cbOnlyForTest.Location = new System.Drawing.Point(16, 148);
            this.cbOnlyForTest.Name = "cbOnlyForTest";
            this.cbOnlyForTest.Size = new System.Drawing.Size(115, 17);
            this.cbOnlyForTest.TabIndex = 22;
            this.cbOnlyForTest.Text = "Только для теста";
            this.cbOnlyForTest.UseVisualStyleBackColor = true;
            // 
            // rtbAddToTest
            // 
            this.rtbAddToTest.Location = new System.Drawing.Point(698, 427);
            this.rtbAddToTest.Name = "rtbAddToTest";
            this.rtbAddToTest.Size = new System.Drawing.Size(361, 160);
            this.rtbAddToTest.TabIndex = 23;
            this.rtbAddToTest.Text = "";
            // 
            // lblAddToTestFiles
            // 
            this.lblAddToTestFiles.AutoSize = true;
            this.lblAddToTestFiles.Location = new System.Drawing.Point(698, 408);
            this.lblAddToTestFiles.Name = "lblAddToTestFiles";
            this.lblAddToTestFiles.Size = new System.Drawing.Size(154, 13);
            this.lblAddToTestFiles.TabIndex = 24;
            this.lblAddToTestFiles.Text = "Добавить в тестовые файлы";
            // 
            // outputRTB
            // 
            this.outputRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputRTB.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outputRTB.Location = new System.Drawing.Point(227, 595);
            this.outputRTB.Name = "outputRTB";
            this.outputRTB.Size = new System.Drawing.Size(832, 134);
            this.outputRTB.TabIndex = 25;
            this.outputRTB.Text = "";
            // 
            // getOutputsBtn
            // 
            this.getOutputsBtn.Location = new System.Drawing.Point(941, 93);
            this.getOutputsBtn.Name = "getOutputsBtn";
            this.getOutputsBtn.Size = new System.Drawing.Size(119, 23);
            this.getOutputsBtn.TabIndex = 26;
            this.getOutputsBtn.Text = "Get outputs";
            this.getOutputsBtn.UseVisualStyleBackColor = true;
            this.getOutputsBtn.Click += new System.EventHandler(this.getOutputsBtn_Click);
            // 
            // getInputsBtn
            // 
            this.getInputsBtn.Location = new System.Drawing.Point(941, 122);
            this.getInputsBtn.Name = "getInputsBtn";
            this.getInputsBtn.Size = new System.Drawing.Size(119, 23);
            this.getInputsBtn.TabIndex = 26;
            this.getInputsBtn.Text = "Get inputs";
            this.getInputsBtn.UseVisualStyleBackColor = true;
            this.getInputsBtn.Click += new System.EventHandler(this.getInputsBtn_Click);
            // 
            // genInstBtn
            // 
            this.genInstBtn.Location = new System.Drawing.Point(941, 151);
            this.genInstBtn.Name = "genInstBtn";
            this.genInstBtn.Size = new System.Drawing.Size(119, 23);
            this.genInstBtn.TabIndex = 27;
            this.genInstBtn.Text = "Gen instance";
            this.genInstBtn.UseVisualStyleBackColor = true;
            this.genInstBtn.Click += new System.EventHandler(this.genInstBtn_Click);
            // 
            // genPortsDefBtn
            // 
            this.genPortsDefBtn.Location = new System.Drawing.Point(941, 180);
            this.genPortsDefBtn.Name = "genPortsDefBtn";
            this.genPortsDefBtn.Size = new System.Drawing.Size(119, 23);
            this.genPortsDefBtn.TabIndex = 27;
            this.genPortsDefBtn.Text = "Gen ports def";
            this.genPortsDefBtn.UseVisualStyleBackColor = true;
            this.genPortsDefBtn.Click += new System.EventHandler(this.genPortsDef_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(941, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 26;
            this.button3.Text = "Get ports";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.getPortsBtn_Click);
            // 
            // invertPortsChb
            // 
            this.invertPortsChb.AutoSize = true;
            this.invertPortsChb.Location = new System.Drawing.Point(1066, 70);
            this.invertPortsChb.Name = "invertPortsChb";
            this.invertPortsChb.Size = new System.Drawing.Size(79, 17);
            this.invertPortsChb.TabIndex = 28;
            this.invertPortsChb.Text = "Invert ports";
            this.invertPortsChb.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 741);
            this.Controls.Add(this.invertPortsChb);
            this.Controls.Add(this.genPortsDefBtn);
            this.Controls.Add(this.genInstBtn);
            this.Controls.Add(this.getInputsBtn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.getOutputsBtn);
            this.Controls.Add(this.outputRTB);
            this.Controls.Add(this.lblAddToTestFiles);
            this.Controls.Add(this.rtbAddToTest);
            this.Controls.Add(this.cbOnlyForTest);
            this.Controls.Add(this.btnCreateTest);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.extConnCHBX);
            this.Controls.Add(this.topNameTB);
            this.Controls.Add(this.createTopBTN);
            this.Controls.Add(this.createConnBtn);
            this.Controls.Add(this.delInstBtn);
            this.Controls.Add(this.connectionTB);
            this.Controls.Add(this.createInstBtn);
            this.Controls.Add(this.instNameTB);
            this.Controls.Add(this.listOfInstLB2);
            this.Controls.Add(this.listOfConnLB);
            this.Controls.Add(this.listOfInstLB);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.delModBtn);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listOfModuleLB);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listOfModuleLB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button delModBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox listOfInstLB;
        private System.Windows.Forms.TextBox instNameTB;
        private System.Windows.Forms.Button createInstBtn;
        private System.Windows.Forms.Button delInstBtn;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.ListBox listOfInstLB2;
        private System.Windows.Forms.TextBox connectionTB;
        private System.Windows.Forms.ListBox listOfConnLB;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Button createConnBtn;
        private System.Windows.Forms.Button createTopBTN;
        private System.Windows.Forms.TextBox topNameTB;
        private System.Windows.Forms.CheckBox extConnCHBX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCreateTest;
        private System.Windows.Forms.CheckBox cbOnlyForTest;
        private System.Windows.Forms.RichTextBox rtbAddToTest;
        private System.Windows.Forms.Label lblAddToTestFiles;
        private System.Windows.Forms.RichTextBox outputRTB;
        private System.Windows.Forms.Button getOutputsBtn;
        private System.Windows.Forms.Button getInputsBtn;
        private System.Windows.Forms.Button genInstBtn;
        private System.Windows.Forms.Button genPortsDefBtn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox invertPortsChb;
    }
}

