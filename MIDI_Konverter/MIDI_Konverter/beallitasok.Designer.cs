namespace MIDI_Konverter
{
    partial class beallitasok
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
            serialSett = new GroupBox();
            comboBox5 = new ComboBox();
            StopBit = new ComboBox();
            Patity = new ComboBox();
            DataBits = new ComboBox();
            buildRat = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            serialSett.SuspendLayout();
            SuspendLayout();
            // 
            // serialSett
            // 
            serialSett.Controls.Add(comboBox5);
            serialSett.Controls.Add(StopBit);
            serialSett.Controls.Add(Patity);
            serialSett.Controls.Add(DataBits);
            serialSett.Controls.Add(buildRat);
            serialSett.Controls.Add(label5);
            serialSett.Controls.Add(label4);
            serialSett.Controls.Add(label3);
            serialSett.Controls.Add(label2);
            serialSett.Controls.Add(label1);
            serialSett.Location = new Point(12, 12);
            serialSett.Name = "serialSett";
            serialSett.Size = new Size(300, 261);
            serialSett.TabIndex = 0;
            serialSett.TabStop = false;
            serialSett.Text = "serialSett";
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(113, 198);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(165, 33);
            comboBox5.TabIndex = 9;
            // 
            // StopBit
            // 
            StopBit.FormattingEnabled = true;
            StopBit.Location = new Point(112, 157);
            StopBit.Name = "StopBit";
            StopBit.Size = new Size(165, 33);
            StopBit.TabIndex = 8;
            // 
            // Patity
            // 
            Patity.FormattingEnabled = true;
            Patity.Location = new Point(112, 115);
            Patity.Name = "Patity";
            Patity.Size = new Size(165, 33);
            Patity.TabIndex = 7;
            // 
            // DataBits
            // 
            DataBits.FormattingEnabled = true;
            DataBits.Location = new Point(112, 74);
            DataBits.Name = "DataBits";
            DataBits.Size = new Size(165, 33);
            DataBits.TabIndex = 6;
            // 
            // buildRat
            // 
            buildRat.FormattingEnabled = true;
            buildRat.Location = new Point(112, 30);
            buildRat.Name = "buildRat";
            buildRat.Size = new Size(165, 33);
            buildRat.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 201);
            label5.Name = "label5";
            label5.Size = new Size(59, 25);
            label5.TabIndex = 4;
            label5.Text = "Flow Control";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 157);
            label4.Name = "label4";
            label4.Size = new Size(59, 25);
            label4.TabIndex = 3;
            label4.Text = "stop Bit(s)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 115);
            label3.Name = "label3";
            label3.Size = new Size(59, 25);
            label3.TabIndex = 2;
            label3.Text = "Parity";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 77);
            label2.Name = "label2";
            label2.Size = new Size(59, 25);
            label2.TabIndex = 1;
            label2.Text = "Data Bits";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 38);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 0;
            label1.Text = "Baud rate";
            // 
            // beallitasok
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 292);
            Controls.Add(serialSett);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "beallitasok";
            ShowIcon = false;
            Text = "beallitasok";
            serialSett.ResumeLayout(false);
            serialSett.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox serialSett;
        private ComboBox comboBox5;
        private ComboBox StopBit;
        private ComboBox Patity;
        private ComboBox DataBits;
        private ComboBox buildRat;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}