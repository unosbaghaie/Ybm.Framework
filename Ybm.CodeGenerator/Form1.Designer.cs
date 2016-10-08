namespace Ybm.CodeGenerator
{
    partial class Form1
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
            this.textBoxModelPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxEntityDLLPath = new System.Windows.Forms.TextBox();
            this.buttonSetpath = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonSetDllPath = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxModelPath
            // 
            this.textBoxModelPath.Location = new System.Drawing.Point(157, 18);
            this.textBoxModelPath.Name = "textBoxModelPath";
            this.textBoxModelPath.Size = new System.Drawing.Size(571, 20);
            this.textBoxModelPath.TabIndex = 0;
            this.textBoxModelPath.Text = "C:\\WebProject\\Ybm\\Ybm\\Ybm.Common\\Models";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source Model path : ";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(27, 142);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 4;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "EntityDLL path : ";
            // 
            // textBoxEntityDLLPath
            // 
            this.textBoxEntityDLLPath.Location = new System.Drawing.Point(157, 44);
            this.textBoxEntityDLLPath.Name = "textBoxEntityDLLPath";
            this.textBoxEntityDLLPath.Size = new System.Drawing.Size(571, 20);
            this.textBoxEntityDLLPath.TabIndex = 11;
            this.textBoxEntityDLLPath.Text = "C:\\WebProject\\Ybm\\Ybm\\Ybm.Common\\bin\\Debug\\Ybm.Common.dll";
            // 
            // buttonSetpath
            // 
            this.buttonSetpath.Location = new System.Drawing.Point(734, 16);
            this.buttonSetpath.Name = "buttonSetpath";
            this.buttonSetpath.Size = new System.Drawing.Size(28, 23);
            this.buttonSetpath.TabIndex = 13;
            this.buttonSetpath.Text = "...";
            this.buttonSetpath.UseVisualStyleBackColor = true;
            this.buttonSetpath.Click += new System.EventHandler(this.buttonSetpath_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(5, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 206);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonSetDllPath);
            this.tabPage1.Controls.Add(this.buttonRun);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.buttonSetpath);
            this.tabPage1.Controls.Add(this.textBoxEntityDLLPath);
            this.tabPage1.Controls.Add(this.textBoxModelPath);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(782, 180);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Models";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonSetDllPath
            // 
            this.buttonSetDllPath.Location = new System.Drawing.Point(734, 41);
            this.buttonSetDllPath.Name = "buttonSetDllPath";
            this.buttonSetDllPath.Size = new System.Drawing.Size(28, 23);
            this.buttonSetDllPath.TabIndex = 14;
            this.buttonSetDllPath.Text = "...";
            this.buttonSetDllPath.UseVisualStyleBackColor = true;
            this.buttonSetDllPath.Click += new System.EventHandler(this.buttonSetDllPath_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 216);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxModelPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxEntityDLLPath;
        private System.Windows.Forms.Button buttonSetpath;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonSetDllPath;
    }
}

