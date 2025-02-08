
namespace AppCredenciales
{
    partial class InitialForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonAdd = new Button();
            menuStrip1 = new MenuStrip();
            fillDataToolStripMenuItem1 = new ToolStripMenuItem();
            uploadDataToolStripMenuItem2 = new ToolStripMenuItem();
            labelName = new Label();
            labelCURP = new Label();
            comboBoxName = new ComboBox();
            labelNSS = new Label();
            labelImage = new Label();
            buttonGenerate = new Button();
            textBoxCURP = new TextBox();
            textBoxNSS = new TextBox();
            buttonBrowse = new Button();
            textBoxPath = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(148, 254);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Agregar Credencial";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fillDataToolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(458, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // fillDataToolStripMenuItem1
            // 
            fillDataToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { uploadDataToolStripMenuItem2 });
            fillDataToolStripMenuItem1.Name = "fillDataToolStripMenuItem1";
            fillDataToolStripMenuItem1.Size = new Size(50, 20);
            fillDataToolStripMenuItem1.Text = "Menu";
            // 
            // uploadDataToolStripMenuItem2
            // 
            uploadDataToolStripMenuItem2.Name = "uploadDataToolStripMenuItem2";
            uploadDataToolStripMenuItem2.Size = new Size(139, 22);
            uploadDataToolStripMenuItem2.Text = "Update Data";
            uploadDataToolStripMenuItem2.Click += uploadDataToolStripMenuItem2_Click;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(58, 86);
            labelName.Name = "labelName";
            labelName.Size = new Size(51, 15);
            labelName.TabIndex = 5;
            labelName.Text = "Nombre";
            // 
            // labelCURP
            // 
            labelCURP.AutoSize = true;
            labelCURP.Location = new Point(58, 127);
            labelCURP.Name = "labelCURP";
            labelCURP.Size = new Size(37, 15);
            labelCURP.TabIndex = 6;
            labelCURP.Text = "CURP";
            labelCURP.Click += label1_Click;
            // 
            // comboBoxName
            // 
            comboBoxName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxName.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxName.FormattingEnabled = true;
            comboBoxName.Location = new Point(117, 83);
            comboBoxName.Name = "comboBoxName";
            comboBoxName.Size = new Size(284, 23);
            comboBoxName.TabIndex = 7;
            comboBoxName.SelectedIndexChanged += comboBoxName_SelectedIndexChanged;
            // 
            // labelNSS
            // 
            labelNSS.AutoSize = true;
            labelNSS.Location = new Point(58, 166);
            labelNSS.Name = "labelNSS";
            labelNSS.Size = new Size(28, 15);
            labelNSS.TabIndex = 9;
            labelNSS.Text = "NSS";
            // 
            // labelImage
            // 
            labelImage.AutoSize = true;
            labelImage.Location = new Point(58, 206);
            labelImage.Name = "labelImage";
            labelImage.Size = new Size(47, 15);
            labelImage.TabIndex = 11;
            labelImage.Text = "Imagen";
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(248, 254);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(75, 23);
            buttonGenerate.TabIndex = 14;
            buttonGenerate.Text = "Generar";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += button1_Click;
            // 
            // textBoxCURP
            // 
            textBoxCURP.Location = new Point(117, 124);
            textBoxCURP.Name = "textBoxCURP";
            textBoxCURP.Size = new Size(284, 23);
            textBoxCURP.TabIndex = 15;
            // 
            // textBoxNSS
            // 
            textBoxNSS.Location = new Point(117, 163);
            textBoxNSS.Name = "textBoxNSS";
            textBoxNSS.Size = new Size(284, 23);
            textBoxNSS.TabIndex = 16;
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(349, 203);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(52, 24);
            buttonBrowse.TabIndex = 18;
            buttonBrowse.Text = "Buscar";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Location = new Point(117, 203);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(226, 23);
            textBoxPath.TabIndex = 19;
            // 
            // InitialForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 356);
            Controls.Add(textBoxPath);
            Controls.Add(buttonBrowse);
            Controls.Add(textBoxNSS);
            Controls.Add(textBoxCURP);
            Controls.Add(buttonGenerate);
            Controls.Add(labelImage);
            Controls.Add(labelNSS);
            Controls.Add(comboBoxName);
            Controls.Add(labelCURP);
            Controls.Add(labelName);
            Controls.Add(menuStrip1);
            Controls.Add(buttonAdd);
            Name = "InitialForm";
            Text = "Credenciales Videsa";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void comboBoxCURP_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void uploadDataToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Title = "Select File";
            ofg.InitialDirectory = @"C:\Downloads";
            ofg.ShowDialog();
            ReadExcelFile(ofg.FileName);
        }

        private void fillDataToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Button buttonAdd;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fillDataToolStripMenuItem1;
        private ToolStripMenuItem uploadDataToolStripMenuItem2;
        private Label labelName;
        private Label labelCURP;
        private ComboBox comboBoxName;
        private Label labelNSS;
        private Label labelImage;
        private Button buttonGenerate;
        private TextBox textBoxCURP;
        private TextBox textBoxNSS;
        private Button buttonBrowse;
        private TextBox textBoxPath;
    }
}
