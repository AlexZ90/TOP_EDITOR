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
          this.button2 = new System.Windows.Forms.Button();
          this.listOfModuleLB = new System.Windows.Forms.ListBox();
          this.textBox1 = new System.Windows.Forms.TextBox();
          this.browseBtn = new System.Windows.Forms.Button();
          this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
          this.delModBtn = new System.Windows.Forms.Button();
          this.dataGridView1 = new System.Windows.Forms.DataGridView();
          this.showPortsBtn = new System.Windows.Forms.Button();
          this.listOfInstLB = new System.Windows.Forms.ListBox();
          this.instNameTB = new System.Windows.Forms.TextBox();
          this.createInstBtn = new System.Windows.Forms.Button();
          this.delInstBtn = new System.Windows.Forms.Button();
          this.dataGridView2 = new System.Windows.Forms.DataGridView();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
          this.SuspendLayout();
          // 
          // button1
          // 
          this.button1.Location = new System.Drawing.Point(30, 36);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(75, 23);
          this.button1.TabIndex = 0;
          this.button1.Text = "Open";
          this.button1.UseVisualStyleBackColor = true;
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // button2
          // 
          this.button2.Location = new System.Drawing.Point(30, 7);
          this.button2.Name = "button2";
          this.button2.Size = new System.Drawing.Size(75, 23);
          this.button2.TabIndex = 1;
          this.button2.Text = "Analyze";
          this.button2.UseVisualStyleBackColor = true;
          this.button2.Click += new System.EventHandler(this.button2_Click);
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
          this.textBox1.Location = new System.Drawing.Point(227, 10);
          this.textBox1.Name = "textBox1";
          this.textBox1.Size = new System.Drawing.Size(288, 20);
          this.textBox1.TabIndex = 3;
          this.textBox1.Text = "D:\\Test.txt";
          // 
          // browseBtn
          // 
          this.browseBtn.Location = new System.Drawing.Point(534, 6);
          this.browseBtn.Name = "browseBtn";
          this.browseBtn.Size = new System.Drawing.Size(75, 23);
          this.browseBtn.TabIndex = 4;
          this.browseBtn.Text = "browse";
          this.browseBtn.UseVisualStyleBackColor = true;
          this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
          // 
          // openFileDialog1
          // 
          this.openFileDialog1.FileName = "openFileDialog1";
          // 
          // delModBtn
          // 
          this.delModBtn.Location = new System.Drawing.Point(30, 65);
          this.delModBtn.Name = "delModBtn";
          this.delModBtn.Size = new System.Drawing.Size(75, 23);
          this.delModBtn.TabIndex = 5;
          this.delModBtn.Text = "delete";
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
          this.dataGridView1.Size = new System.Drawing.Size(337, 186);
          this.dataGridView1.TabIndex = 6;
          // 
          // showPortsBtn
          // 
          this.showPortsBtn.Location = new System.Drawing.Point(30, 95);
          this.showPortsBtn.Name = "showPortsBtn";
          this.showPortsBtn.Size = new System.Drawing.Size(75, 23);
          this.showPortsBtn.TabIndex = 7;
          this.showPortsBtn.Text = "Show ports";
          this.showPortsBtn.UseVisualStyleBackColor = true;
          this.showPortsBtn.Click += new System.EventHandler(this.showPortsBtn_Click);
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
          this.createInstBtn.Location = new System.Drawing.Point(12, 261);
          this.createInstBtn.Name = "createInstBtn";
          this.createInstBtn.Size = new System.Drawing.Size(75, 23);
          this.createInstBtn.TabIndex = 10;
          this.createInstBtn.Text = "Create";
          this.createInstBtn.UseVisualStyleBackColor = true;
          this.createInstBtn.Click += new System.EventHandler(this.createInstBtn_Click);
          // 
          // delInstBtn
          // 
          this.delInstBtn.Location = new System.Drawing.Point(12, 291);
          this.delInstBtn.Name = "delInstBtn";
          this.delInstBtn.Size = new System.Drawing.Size(75, 23);
          this.delInstBtn.TabIndex = 11;
          this.delInstBtn.Text = "del Inst";
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
          this.dataGridView2.Size = new System.Drawing.Size(337, 160);
          this.dataGridView2.TabIndex = 6;
          // 
          // Form1
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(713, 408);
          this.Controls.Add(this.delInstBtn);
          this.Controls.Add(this.createInstBtn);
          this.Controls.Add(this.instNameTB);
          this.Controls.Add(this.listOfInstLB);
          this.Controls.Add(this.showPortsBtn);
          this.Controls.Add(this.dataGridView2);
          this.Controls.Add(this.dataGridView1);
          this.Controls.Add(this.delModBtn);
          this.Controls.Add(this.browseBtn);
          this.Controls.Add(this.textBox1);
          this.Controls.Add(this.listOfModuleLB);
          this.Controls.Add(this.button2);
          this.Controls.Add(this.button1);
          this.Name = "Form1";
          this.Text = "Form1";
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listOfModuleLB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button delModBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button showPortsBtn;
        private System.Windows.Forms.ListBox listOfInstLB;
        private System.Windows.Forms.TextBox instNameTB;
        private System.Windows.Forms.Button createInstBtn;
        private System.Windows.Forms.Button delInstBtn;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}

