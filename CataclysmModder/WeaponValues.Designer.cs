namespace CataclysmModder
{
    partial class WeaponValues
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.shortDescTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.classNumeric = new System.Windows.Forms.NumericUpDown();
            this.textureFileWarn = new System.Windows.Forms.PictureBox();
            this.textureFileTextBox = new System.Windows.Forms.TextBox();
            this.labelb = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textureHNumeric = new System.Windows.Forms.NumericUpDown();
            this.textureWNumeric = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.autoFireCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bindsControlNumeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.overrideAngleNumeric = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textureFileWarn)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textureHNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textureWNumeric)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindsControlNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overrideAngleNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.textureFileWarn);
            this.groupBox1.Controls.Add(this.textureFileTextBox);
            this.groupBox1.Controls.Add(this.labelb);
            this.groupBox1.Controls.Add(this.classNumeric);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.idTextBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.shortDescTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nameTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 293);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Weapon Properties";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(75, 19);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(139, 20);
            this.idTextBox.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Id";
            // 
            // shortDescTextBox
            // 
            this.shortDescTextBox.Location = new System.Drawing.Point(75, 71);
            this.shortDescTextBox.Multiline = true;
            this.shortDescTextBox.Name = "shortDescTextBox";
            this.shortDescTextBox.Size = new System.Drawing.Size(139, 46);
            this.shortDescTextBox.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Short Desc";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(75, 45);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(139, 20);
            this.nameTextBox.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Class";
            // 
            // classNumeric
            // 
            this.classNumeric.Location = new System.Drawing.Point(76, 124);
            this.classNumeric.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.classNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.classNumeric.Name = "classNumeric";
            this.classNumeric.Size = new System.Drawing.Size(64, 20);
            this.classNumeric.TabIndex = 32;
            this.classNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textureFileWarn
            // 
            this.textureFileWarn.InitialImage = null;
            this.textureFileWarn.Location = new System.Drawing.Point(490, 16);
            this.textureFileWarn.Name = "textureFileWarn";
            this.textureFileWarn.Size = new System.Drawing.Size(20, 20);
            this.textureFileWarn.TabIndex = 35;
            this.textureFileWarn.TabStop = false;
            // 
            // textureFileTextBox
            // 
            this.textureFileTextBox.Location = new System.Drawing.Point(298, 16);
            this.textureFileTextBox.Name = "textureFileTextBox";
            this.textureFileTextBox.Size = new System.Drawing.Size(185, 20);
            this.textureFileTextBox.TabIndex = 34;
            // 
            // labelb
            // 
            this.labelb.AutoSize = true;
            this.labelb.Location = new System.Drawing.Point(230, 16);
            this.labelb.Name = "labelb";
            this.labelb.Size = new System.Drawing.Size(62, 13);
            this.labelb.TabIndex = 33;
            this.labelb.Text = "Texture File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textureHNumeric);
            this.groupBox2.Controls.Add(this.textureWNumeric);
            this.groupBox2.Location = new System.Drawing.Point(233, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(94, 69);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Texture Size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "w";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "h";
            // 
            // textureHNumeric
            // 
            this.textureHNumeric.Enabled = false;
            this.textureHNumeric.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.textureHNumeric.Location = new System.Drawing.Point(27, 41);
            this.textureHNumeric.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.textureHNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textureHNumeric.Name = "textureHNumeric";
            this.textureHNumeric.Size = new System.Drawing.Size(58, 20);
            this.textureHNumeric.TabIndex = 18;
            this.textureHNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textureWNumeric
            // 
            this.textureWNumeric.Enabled = false;
            this.textureWNumeric.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.textureWNumeric.Location = new System.Drawing.Point(27, 15);
            this.textureWNumeric.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.textureWNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textureWNumeric.Name = "textureWNumeric";
            this.textureWNumeric.Size = new System.Drawing.Size(58, 20);
            this.textureWNumeric.TabIndex = 16;
            this.textureWNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.overrideAngleNumeric);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.bindsControlNumeric);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.autoFireCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(6, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(607, 137);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stat";
            // 
            // autoFireCheckBox
            // 
            this.autoFireCheckBox.AutoSize = true;
            this.autoFireCheckBox.Location = new System.Drawing.Point(7, 20);
            this.autoFireCheckBox.Name = "autoFireCheckBox";
            this.autoFireCheckBox.Size = new System.Drawing.Size(68, 17);
            this.autoFireCheckBox.TabIndex = 0;
            this.autoFireCheckBox.Text = "Auto Fire";
            this.autoFireCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Control";
            // 
            // bindsControlNumeric
            // 
            this.bindsControlNumeric.Location = new System.Drawing.Point(94, 44);
            this.bindsControlNumeric.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.bindsControlNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.bindsControlNumeric.Name = "bindsControlNumeric";
            this.bindsControlNumeric.Size = new System.Drawing.Size(72, 20);
            this.bindsControlNumeric.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Angle Override";
            // 
            // overrideAngleNumeric
            // 
            this.overrideAngleNumeric.DecimalPlaces = 2;
            this.overrideAngleNumeric.Increment = new decimal(new int[] {
            157,
            0,
            0,
            131072});
            this.overrideAngleNumeric.Location = new System.Drawing.Point(94, 70);
            this.overrideAngleNumeric.Maximum = new decimal(new int[] {
            628,
            0,
            0,
            131072});
            this.overrideAngleNumeric.Minimum = new decimal(new int[] {
            628,
            0,
            0,
            -2147352576});
            this.overrideAngleNumeric.Name = "overrideAngleNumeric";
            this.overrideAngleNumeric.Size = new System.Drawing.Size(120, 20);
            this.overrideAngleNumeric.TabIndex = 4;
            // 
            // WeaponValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "WeaponValues";
            this.Size = new System.Drawing.Size(626, 300);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textureFileWarn)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textureHNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textureWNumeric)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindsControlNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overrideAngleNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox shortDescTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown classNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox textureFileWarn;
        private System.Windows.Forms.TextBox textureFileTextBox;
        private System.Windows.Forms.Label labelb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown textureHNumeric;
        private System.Windows.Forms.NumericUpDown textureWNumeric;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown overrideAngleNumeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown bindsControlNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox autoFireCheckBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
