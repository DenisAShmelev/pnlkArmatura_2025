namespace PnlkArmatura
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_PanelWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PanelHeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_PanelLength = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmb_Position_Depth = new System.Windows.Forms.ComboBox();
            this.cmb_Position_Plane = new System.Windows.Forms.ComboBox();
            this.cmb_Position_Rotation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(636, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "установить рабочую плоскость button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(636, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Создать балку button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(636, 104);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 40);
            this.button3.TabIndex = 2;
            this.button3.Text = "Создать балку button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(636, 150);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 40);
            this.button4.TabIndex = 3;
            this.button4.Text = "Пользовательский атрибут button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txt_PanelWidth
            // 
            this.txt_PanelWidth.Location = new System.Drawing.Point(15, 28);
            this.txt_PanelWidth.Name = "txt_PanelWidth";
            this.txt_PanelWidth.Size = new System.Drawing.Size(100, 20);
            this.txt_PanelWidth.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Толщина:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Высота:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txt_PanelHeight
            // 
            this.txt_PanelHeight.Location = new System.Drawing.Point(125, 28);
            this.txt_PanelHeight.Name = "txt_PanelHeight";
            this.txt_PanelHeight.Size = new System.Drawing.Size(100, 20);
            this.txt_PanelHeight.TabIndex = 6;
            this.txt_PanelHeight.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Длина:";
            // 
            // txt_PanelLength
            // 
            this.txt_PanelLength.Location = new System.Drawing.Point(232, 28);
            this.txt_PanelLength.Name = "txt_PanelLength";
            this.txt_PanelLength.Size = new System.Drawing.Size(100, 20);
            this.txt_PanelLength.TabIndex = 8;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(628, 231);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(148, 66);
            this.button5.TabIndex = 10;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(15, 54);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(111, 28);
            this.button6.TabIndex = 11;
            this.button6.Text = "Заполнить button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(338, 28);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(150, 113);
            this.button8.TabIndex = 29;
            this.button8.Text = "Создать балку button3";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 186);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(317, 20);
            this.textBox1.TabIndex = 30;
            // 
            // cmb_Position_Depth
            // 
            this.cmb_Position_Depth.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmb_Position_Depth.DisplayMember = "1";
            this.cmb_Position_Depth.FormattingEnabled = true;
            this.cmb_Position_Depth.Items.AddRange(new object[] {
            "MIDDLE",
            "FRONT",
            "BEHIND"});
            this.cmb_Position_Depth.Location = new System.Drawing.Point(125, 120);
            this.cmb_Position_Depth.Name = "cmb_Position_Depth";
            this.cmb_Position_Depth.Size = new System.Drawing.Size(100, 21);
            this.cmb_Position_Depth.TabIndex = 32;
            this.cmb_Position_Depth.ValueMember = "1;2;3";
            // 
            // cmb_Position_Plane
            // 
            this.cmb_Position_Plane.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmb_Position_Plane.DisplayMember = "1";
            this.cmb_Position_Plane.FormattingEnabled = true;
            this.cmb_Position_Plane.Items.AddRange(new object[] {
            "MIDDLE",
            "LEFT",
            "RIGHT"});
            this.cmb_Position_Plane.Location = new System.Drawing.Point(15, 120);
            this.cmb_Position_Plane.Name = "cmb_Position_Plane";
            this.cmb_Position_Plane.Size = new System.Drawing.Size(100, 21);
            this.cmb_Position_Plane.TabIndex = 33;
            this.cmb_Position_Plane.ValueMember = "1;2;3";
            // 
            // cmb_Position_Rotation
            // 
            this.cmb_Position_Rotation.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmb_Position_Rotation.DisplayMember = "1";
            this.cmb_Position_Rotation.FormattingEnabled = true;
            this.cmb_Position_Rotation.Items.AddRange(new object[] {
            "TOP",
            "FRONT",
            "BELOW",
            "BACK"});
            this.cmb_Position_Rotation.Location = new System.Drawing.Point(232, 120);
            this.cmb_Position_Rotation.Name = "cmb_Position_Rotation";
            this.cmb_Position_Rotation.Size = new System.Drawing.Size(100, 21);
            this.cmb_Position_Rotation.TabIndex = 34;
            this.cmb_Position_Rotation.ValueMember = "1;2;3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "На плоскости:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Поворот:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "По глубине:";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(338, 186);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(150, 20);
            this.button9.TabIndex = 38;
            this.button9.Text = "Создать арматуру";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(628, 364);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(95, 85);
            this.button10.TabIndex = 39;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(645, 327);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(101, 68);
            this.button20.TabIndex = 51;
            this.button20.Text = "button20";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_Position_Rotation);
            this.Controls.Add(this.cmb_Position_Plane);
            this.Controls.Add(this.cmb_Position_Depth);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_PanelLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_PanelHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_PanelWidth);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_PanelWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PanelHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_PanelLength;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmb_Position_Depth;
        private System.Windows.Forms.ComboBox cmb_Position_Plane;
        private System.Windows.Forms.ComboBox cmb_Position_Rotation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button20;
    }
}

