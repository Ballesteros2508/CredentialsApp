namespace AppCredenciales
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxCtemp = new ListBox();
            buttonCont = new Button();
            buttonCan = new Button();
            buttonDelete = new Button();
            SuspendLayout();
            // 
            // listBoxCtemp
            // 
            listBoxCtemp.FormattingEnabled = true;
            listBoxCtemp.ItemHeight = 15;
            listBoxCtemp.Location = new Point(94, 45);
            listBoxCtemp.Name = "listBoxCtemp";
            listBoxCtemp.Size = new Size(612, 199);
            listBoxCtemp.TabIndex = 0;
            // 
            // buttonCont
            // 
            buttonCont.Location = new Point(351, 334);
            buttonCont.Name = "buttonCont";
            buttonCont.Size = new Size(75, 23);
            buttonCont.TabIndex = 1;
            buttonCont.Text = "Continuar";
            buttonCont.UseVisualStyleBackColor = true;
            buttonCont.Click += button1_Click;
            // 
            // buttonCan
            // 
            buttonCan.Location = new Point(465, 334);
            buttonCan.Name = "buttonCan";
            buttonCan.Size = new Size(75, 23);
            buttonCan.TabIndex = 2;
            buttonCan.Text = "Cancelar";
            buttonCan.UseVisualStyleBackColor = true;
            buttonCan.Click += buttonCan_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(245, 334);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Eliminar";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click_1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonDelete);
            Controls.Add(buttonCan);
            Controls.Add(buttonCont);
            Controls.Add(listBoxCtemp);
            Name = "Form2";
            Text = "Credenciales Videsa";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxCtemp;
        private Button buttonCont;
        private Button buttonCan;
        private Button buttonDelete;
    }
}