namespace MIDI_Konverter
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox midiBeVal;
        private System.Windows.Forms.ComboBox midiKiVal;
        private System.Windows.Forms.ComboBox ComVal;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.RichTextBox textBoxLog;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            midiBeVal = new ComboBox();
            midiKiVal = new ComboBox();
            ComVal = new ComboBox();
            buttonStartStop = new Button();
            textBoxLog = new RichTextBox();
            labelStatus = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            fileGomb = new Button();
            kimenetValaszt = new ComboBox();
            kimenetiTanit = new Button();
            kimenetValasztLBL = new Label();
            kimenetBeallitasok = new RichTextBox();
            numericCC = new NumericUpDown();
            logBe = new CheckBox();
            eszkozTorlese = new Button();
            tanulas = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numericCC).BeginInit();
            SuspendLayout();
            // 
            // midiBeVal
            // 
            midiBeVal.BackColor = Color.FromArgb(0, 4, 16);
            midiBeVal.DropDownStyle = ComboBoxStyle.DropDownList;
            midiBeVal.ForeColor = Color.FromArgb(254, 254, 254);
            midiBeVal.FormattingEnabled = true;
            midiBeVal.Location = new Point(535, 134);
            midiBeVal.Name = "midiBeVal";
            midiBeVal.Size = new Size(182, 33);
            midiBeVal.TabIndex = 0;
            // 
            // midiKiVal
            // 
            midiKiVal.BackColor = Color.FromArgb(0, 4, 16);
            midiKiVal.DropDownStyle = ComboBoxStyle.DropDownList;
            midiKiVal.ForeColor = Color.FromArgb(254, 254, 254);
            midiKiVal.FormattingEnabled = true;
            midiKiVal.Location = new Point(535, 55);
            midiKiVal.Name = "midiKiVal";
            midiKiVal.Size = new Size(182, 33);
            midiKiVal.TabIndex = 1;
            // 
            // ComVal
            // 
            ComVal.BackColor = Color.FromArgb(0, 4, 16);
            ComVal.DropDownStyle = ComboBoxStyle.DropDownList;
            ComVal.ForeColor = Color.FromArgb(254, 254, 254);
            ComVal.FormattingEnabled = true;
            ComVal.Location = new Point(46, 111);
            ComVal.Name = "ComVal";
            ComVal.Size = new Size(318, 33);
            ComVal.TabIndex = 2;
            // 
            // buttonStartStop
            // 
            buttonStartStop.Location = new Point(46, 150);
            buttonStartStop.Name = "buttonStartStop";
            buttonStartStop.Size = new Size(112, 34);
            buttonStartStop.TabIndex = 3;
            buttonStartStop.Text = "Start";
            buttonStartStop.UseVisualStyleBackColor = false;
            buttonStartStop.Click += StartStop;
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = Color.FromArgb(0, 4, 14);
            textBoxLog.ForeColor = Color.FromArgb(254, 254, 254);
            textBoxLog.Location = new Point(12, 199);
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.Size = new Size(776, 251);
            textBoxLog.TabIndex = 4;
            textBoxLog.Text = "";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(193, 159);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(80, 25);
            labelStatus.TabIndex = 5;
            labelStatus.Text = "Stopped";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 83);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 6;
            label1.Text = "Serial port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(535, 27);
            label2.Name = "label2";
            label2.Size = new Size(86, 25);
            label2.TabIndex = 7;
            label2.Text = "MIDI Out";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(535, 106);
            label3.Name = "label3";
            label3.Size = new Size(71, 25);
            label3.TabIndex = 8;
            label3.Text = "MIDI In";
            // 
            // fileGomb
            // 
            fileGomb.BackColor = Color.FromArgb(0, 4, 16);
            fileGomb.Location = new Point(0, 0);
            fileGomb.Name = "fileGomb";
            fileGomb.Size = new Size(53, 34);
            fileGomb.TabIndex = 9;
            fileGomb.Text = "file";
            fileGomb.UseVisualStyleBackColor = false;
            fileGomb.Click += beallitasokNyit;
            // 
            // kimenetValaszt
            // 
            kimenetValaszt.BackColor = Color.FromArgb(0, 4, 16);
            kimenetValaszt.DropDownStyle = ComboBoxStyle.DropDownList;
            kimenetValaszt.ForeColor = Color.FromArgb(254, 254, 254);
            kimenetValaszt.FormattingEnabled = true;
            kimenetValaszt.Location = new Point(821, 159);
            kimenetValaszt.Name = "kimenetValaszt";
            kimenetValaszt.Size = new Size(243, 33);
            kimenetValaszt.TabIndex = 10;
            // 
            // kimenetiTanit
            // 
            kimenetiTanit.Location = new Point(819, 74);
            kimenetiTanit.Name = "kimenetiTanit";
            kimenetiTanit.Size = new Size(112, 34);
            kimenetiTanit.TabIndex = 11;
            kimenetiTanit.Text = "Tanítás";
            kimenetiTanit.UseVisualStyleBackColor = false;
            kimenetiTanit.Click += kimenetiTanitKatt;
            // 
            // kimenetValasztLBL
            // 
            kimenetValasztLBL.AutoSize = true;
            kimenetValasztLBL.Location = new Point(821, 119);
            kimenetValasztLBL.Name = "kimenetValasztLBL";
            kimenetValasztLBL.Size = new Size(222, 25);
            kimenetValasztLBL.TabIndex = 12;
            kimenetValasztLBL.Text = "Kimeneti eszköz választása";
            // 
            // kimenetBeallitasok
            // 
            kimenetBeallitasok.BackColor = Color.FromArgb(0, 4, 14);
            kimenetBeallitasok.ForeColor = Color.FromArgb(254, 254, 254);
            kimenetBeallitasok.Location = new Point(819, 230);
            kimenetBeallitasok.Name = "kimenetBeallitasok";
            kimenetBeallitasok.ReadOnly = true;
            kimenetBeallitasok.Size = new Size(245, 220);
            kimenetBeallitasok.TabIndex = 13;
            kimenetBeallitasok.Text = "";
            // 
            // numericCC
            // 
            numericCC.BackColor = Color.FromArgb(0, 4, 16);
            numericCC.ForeColor = Color.FromArgb(254, 254, 254);
            numericCC.Location = new Point(922, 28);
            numericCC.Maximum = new decimal(new int[] { 127, 0, 0, 0 });
            numericCC.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericCC.Name = "numericCC";
            numericCC.Size = new Size(142, 31);
            numericCC.TabIndex = 14;
            // 
            // logBe
            // 
            logBe.AutoSize = true;
            logBe.Location = new Point(334, 161);
            logBe.Name = "logBe";
            logBe.Size = new Size(100, 29);
            logBe.TabIndex = 15;
            logBe.Text = "Logolás";
            logBe.UseVisualStyleBackColor = true;
            // 
            // eszkozTorlese
            // 
            eszkozTorlese.Location = new Point(952, 74);
            eszkozTorlese.Name = "eszkozTorlese";
            eszkozTorlese.Size = new Size(112, 34);
            eszkozTorlese.TabIndex = 16;
            eszkozTorlese.Text = "Törlés";
            eszkozTorlese.UseVisualStyleBackColor = false;
            eszkozTorlese.Click += eszkozTorleseKatt;
            // 
            // tanulas
            // 
            tanulas.AutoSize = true;
            tanulas.Location = new Point(821, 30);
            tanulas.Name = "tanulas";
            tanulas.Size = new Size(95, 29);
            tanulas.TabIndex = 17;
            tanulas.Text = "Tanulás";
            tanulas.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 4, 16);
            ClientSize = new Size(1083, 465);
            Controls.Add(tanulas);
            Controls.Add(eszkozTorlese);
            Controls.Add(logBe);
            Controls.Add(numericCC);
            Controls.Add(kimenetBeallitasok);
            Controls.Add(kimenetValasztLBL);
            Controls.Add(kimenetiTanit);
            Controls.Add(kimenetValaszt);
            Controls.Add(fileGomb);
            Controls.Add(midiBeVal);
            Controls.Add(midiKiVal);
            Controls.Add(ComVal);
            Controls.Add(buttonStartStop);
            Controls.Add(textBoxLog);
            Controls.Add(labelStatus);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = Color.FromArgb(254, 254, 254);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "MIDI Konverter";
            ((System.ComponentModel.ISupportInitialize)numericCC).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button fileGomb;
        private ComboBox kimenetValaszt;
        private Button kimenetiTanit;
        private Label kimenetValasztLBL;
        private RichTextBox kimenetBeallitasok;
        private NumericUpDown numericCC;
        private CheckBox logBe;
        private Button eszkozTorlese;
        private CheckBox tanulas;
    }
}
