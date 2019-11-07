﻿namespace SuperMetroidRandoApp
{
   partial class MainForm
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
         this.btnRandomize = new System.Windows.Forms.Button();
         this.labelVanilla = new System.Windows.Forms.Label();
         this.btnBrowse = new System.Windows.Forms.Button();
         this.labelGameMode = new System.Windows.Forms.Label();
         this.comboBoxMode = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.textBoxBrowse = new System.Windows.Forms.TextBox();
         this.comboBoxDifficulty = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
         this.radioButtonRandom = new System.Windows.Forms.RadioButton();
         this.radioButtonManual = new System.Windows.Forms.RadioButton();
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
         this.SuspendLayout();
         // 
         // btnRandomize
         // 
         this.btnRandomize.Font = new System.Drawing.Font("Calibri", 12F);
         this.btnRandomize.Location = new System.Drawing.Point(589, 53);
         this.btnRandomize.Name = "btnRandomize";
         this.btnRandomize.Size = new System.Drawing.Size(112, 30);
         this.btnRandomize.TabIndex = 3;
         this.btnRandomize.Text = "Randomize!";
         this.btnRandomize.Click += new System.EventHandler(this.btnRandomize_Click);
         // 
         // labelVanilla
         // 
         this.labelVanilla.AutoSize = true;
         this.labelVanilla.Font = new System.Drawing.Font("Calibri", 12F);
         this.labelVanilla.Location = new System.Drawing.Point(12, 21);
         this.labelVanilla.Name = "labelVanilla";
         this.labelVanilla.Size = new System.Drawing.Size(94, 19);
         this.labelVanilla.TabIndex = 8;
         this.labelVanilla.Text = "Vanilla ROM:";
         // 
         // btnBrowse
         // 
         this.btnBrowse.AutoSize = true;
         this.btnBrowse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnBrowse.Location = new System.Drawing.Point(589, 15);
         this.btnBrowse.Name = "btnBrowse";
         this.btnBrowse.Size = new System.Drawing.Size(112, 30);
         this.btnBrowse.TabIndex = 10;
         this.btnBrowse.Text = "Browse...";
         this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
         // 
         // labelGameMode
         // 
         this.labelGameMode.AutoSize = true;
         this.labelGameMode.Font = new System.Drawing.Font("Calibri", 12F);
         this.labelGameMode.Location = new System.Drawing.Point(12, 57);
         this.labelGameMode.Name = "labelGameMode";
         this.labelGameMode.Size = new System.Drawing.Size(92, 19);
         this.labelGameMode.TabIndex = 11;
         this.labelGameMode.Text = "Game Mode:";
         // 
         // comboBoxMode
         // 
         this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxMode.Font = new System.Drawing.Font("Calibri", 12F);
         this.comboBoxMode.FormattingEnabled = true;
         this.comboBoxMode.Items.AddRange(new object[] {
            "Standard"});
         this.comboBoxMode.Location = new System.Drawing.Point(127, 54);
         this.comboBoxMode.Name = "comboBoxMode";
         this.comboBoxMode.Size = new System.Drawing.Size(172, 27);
         this.comboBoxMode.TabIndex = 12;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label3.Location = new System.Drawing.Point(13, 137);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(84, 14);
         this.label3.TabIndex = 4;
         this.label3.Text = "Developed by:";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label4.Location = new System.Drawing.Point(103, 137);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(154, 14);
         this.label4.TabIndex = 5;
         this.label4.Text = "Kipp, Smiley, MassHesteria";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Font = new System.Drawing.Font("Calibri", 9F);
         this.label5.Location = new System.Drawing.Point(399, 137);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(184, 14);
         this.label5.TabIndex = 7;
         this.label5.Text = "Dessyreqt, Total, Foosda, Leodox";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Underline);
         this.label6.Location = new System.Drawing.Point(288, 137);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(105, 14);
         this.label6.TabIndex = 6;
         this.label6.Text = "Based on work by:";
         // 
         // textBoxBrowse
         // 
         this.textBoxBrowse.Font = new System.Drawing.Font("Calibri", 12F);
         this.textBoxBrowse.Location = new System.Drawing.Point(112, 18);
         this.textBoxBrowse.Name = "textBoxBrowse";
         this.textBoxBrowse.Size = new System.Drawing.Size(471, 27);
         this.textBoxBrowse.TabIndex = 9;
         // 
         // comboBoxDifficulty
         // 
         this.comboBoxDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxDifficulty.Font = new System.Drawing.Font("Calibri", 12F);
         this.comboBoxDifficulty.FormattingEnabled = true;
         this.comboBoxDifficulty.Location = new System.Drawing.Point(127, 91);
         this.comboBoxDifficulty.Name = "comboBoxDifficulty";
         this.comboBoxDifficulty.Size = new System.Drawing.Size(170, 27);
         this.comboBoxDifficulty.TabIndex = 14;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Calibri", 12F);
         this.label1.Location = new System.Drawing.Point(12, 94);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(109, 19);
         this.label1.TabIndex = 13;
         this.label1.Text = "Randomization:";
         // 
         // numericUpDown1
         // 
         this.numericUpDown1.Enabled = false;
         this.numericUpDown1.Font = new System.Drawing.Font("Calibri", 12F);
         this.numericUpDown1.Location = new System.Drawing.Point(427, 92);
         this.numericUpDown1.Name = "numericUpDown1";
         this.numericUpDown1.Size = new System.Drawing.Size(120, 27);
         this.numericUpDown1.TabIndex = 2;
         this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         // 
         // radioButtonRandom
         // 
         this.radioButtonRandom.AutoSize = true;
         this.radioButtonRandom.Checked = true;
         this.radioButtonRandom.Font = new System.Drawing.Font("Calibri", 12F);
         this.radioButtonRandom.Location = new System.Drawing.Point(330, 55);
         this.radioButtonRandom.Name = "radioButtonRandom";
         this.radioButtonRandom.Size = new System.Drawing.Size(179, 23);
         this.radioButtonRandom.TabIndex = 0;
         this.radioButtonRandom.TabStop = true;
         this.radioButtonRandom.Text = "Generate Random Seed";
         this.radioButtonRandom.UseVisualStyleBackColor = true;
         // 
         // radioButtonManual
         // 
         this.radioButtonManual.AutoSize = true;
         this.radioButtonManual.Enabled = false;
         this.radioButtonManual.Font = new System.Drawing.Font("Calibri", 12F);
         this.radioButtonManual.Location = new System.Drawing.Point(330, 92);
         this.radioButtonManual.Name = "radioButtonManual";
         this.radioButtonManual.Size = new System.Drawing.Size(91, 23);
         this.radioButtonManual.TabIndex = 1;
         this.radioButtonManual.Text = "Use Seed:";
         this.radioButtonManual.UseVisualStyleBackColor = true;
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(714, 169);
         this.Controls.Add(this.radioButtonManual);
         this.Controls.Add(this.radioButtonRandom);
         this.Controls.Add(this.numericUpDown1);
         this.Controls.Add(this.comboBoxDifficulty);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.textBoxBrowse);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.comboBoxMode);
         this.Controls.Add(this.labelGameMode);
         this.Controls.Add(this.btnBrowse);
         this.Controls.Add(this.labelVanilla);
         this.Controls.Add(this.btnRandomize);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.MinimumSize = new System.Drawing.Size(656, 166);
         this.Name = "MainForm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Super Metroid DASH Randomizer! [v6.0]";
         ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnRandomize;
      private System.Windows.Forms.Label labelVanilla;
      private System.Windows.Forms.Button btnBrowse;
      private System.Windows.Forms.Label labelGameMode;
      private System.Windows.Forms.ComboBox comboBoxMode;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.TextBox textBoxBrowse;
      private System.Windows.Forms.ComboBox comboBoxDifficulty;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.NumericUpDown numericUpDown1;
      private System.Windows.Forms.RadioButton radioButtonRandom;
      private System.Windows.Forms.RadioButton radioButtonManual;
   }
}
