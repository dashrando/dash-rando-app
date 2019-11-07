namespace SuperMetroidRandoApp
{
   partial class NewRomForm
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
         this.label1 = new System.Windows.Forms.Label();
         this.textBoxROM = new System.Windows.Forms.TextBox();
         this.textBoxDifficulty = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.textBoxType = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.btnCopyFile = new System.Windows.Forms.Button();
         this.btnOpenFolder = new System.Windows.Forms.Button();
         this.btnClose = new System.Windows.Forms.Button();
         this.textBoxSeed = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(8, 9);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(118, 19);
         this.label1.TabIndex = 3;
         this.label1.Text = "Generated ROM:";
         // 
         // textBoxROM
         // 
         this.textBoxROM.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxROM.Font = new System.Drawing.Font("Calibri", 12F);
         this.textBoxROM.Location = new System.Drawing.Point(12, 31);
         this.textBoxROM.Name = "textBoxROM";
         this.textBoxROM.ReadOnly = true;
         this.textBoxROM.Size = new System.Drawing.Size(503, 27);
         this.textBoxROM.TabIndex = 4;
         this.textBoxROM.Text = "rom path";
         // 
         // textBoxDifficulty
         // 
         this.textBoxDifficulty.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDifficulty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxDifficulty.Font = new System.Drawing.Font("Calibri", 12F);
         this.textBoxDifficulty.Location = new System.Drawing.Point(12, 93);
         this.textBoxDifficulty.Name = "textBoxDifficulty";
         this.textBoxDifficulty.ReadOnly = true;
         this.textBoxDifficulty.Size = new System.Drawing.Size(267, 27);
         this.textBoxDifficulty.TabIndex = 6;
         this.textBoxDifficulty.Text = "rom difficulty";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Font = new System.Drawing.Font("Calibri", 12F);
         this.label2.Location = new System.Drawing.Point(8, 71);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(92, 19);
         this.label2.TabIndex = 5;
         this.label2.Text = "Game Mode:";
         // 
         // textBoxType
         // 
         this.textBoxType.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxType.Font = new System.Drawing.Font("Calibri", 12F);
         this.textBoxType.Location = new System.Drawing.Point(285, 93);
         this.textBoxType.Name = "textBoxType";
         this.textBoxType.ReadOnly = true;
         this.textBoxType.Size = new System.Drawing.Size(230, 27);
         this.textBoxType.TabIndex = 8;
         this.textBoxType.Text = "rando type";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Font = new System.Drawing.Font("Calibri", 12F);
         this.label3.Location = new System.Drawing.Point(281, 71);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(109, 19);
         this.label3.TabIndex = 7;
         this.label3.Text = "Randomization:";
         // 
         // btnCopyFile
         // 
         this.btnCopyFile.AutoSize = true;
         this.btnCopyFile.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnCopyFile.Location = new System.Drawing.Point(167, 154);
         this.btnCopyFile.Name = "btnCopyFile";
         this.btnCopyFile.Size = new System.Drawing.Size(112, 30);
         this.btnCopyFile.TabIndex = 0;
         this.btnCopyFile.Text = "Copy File";
         this.btnCopyFile.Click += new System.EventHandler(this.btnCopyFile_Click);
         // 
         // btnOpenFolder
         // 
         this.btnOpenFolder.AutoSize = true;
         this.btnOpenFolder.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnOpenFolder.Location = new System.Drawing.Point(285, 154);
         this.btnOpenFolder.Name = "btnOpenFolder";
         this.btnOpenFolder.Size = new System.Drawing.Size(112, 30);
         this.btnOpenFolder.TabIndex = 1;
         this.btnOpenFolder.Text = "Open Folder";
         this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
         // 
         // btnClose
         // 
         this.btnClose.AutoSize = true;
         this.btnClose.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnClose.Location = new System.Drawing.Point(403, 154);
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size(112, 30);
         this.btnClose.TabIndex = 2;
         this.btnClose.Text = "Close";
         this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
         // 
         // textBoxSeed
         // 
         this.textBoxSeed.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxSeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxSeed.Font = new System.Drawing.Font("Calibri", 12F);
         this.textBoxSeed.Location = new System.Drawing.Point(12, 155);
         this.textBoxSeed.Name = "textBoxSeed";
         this.textBoxSeed.ReadOnly = true;
         this.textBoxSeed.Size = new System.Drawing.Size(110, 27);
         this.textBoxSeed.TabIndex = 10;
         this.textBoxSeed.Text = "rom seed";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Font = new System.Drawing.Font("Calibri", 12F);
         this.label4.Location = new System.Drawing.Point(8, 133);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(44, 19);
         this.label4.TabIndex = 9;
         this.label4.Text = "Seed:";
         // 
         // NewRomForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(534, 200);
         this.ControlBox = false;
         this.Controls.Add(this.textBoxSeed);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.btnClose);
         this.Controls.Add(this.btnOpenFolder);
         this.Controls.Add(this.btnCopyFile);
         this.Controls.Add(this.textBoxType);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.textBoxDifficulty);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.textBoxROM);
         this.Controls.Add(this.label1);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "NewRomForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Randomization Complete!";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox textBoxROM;
      private System.Windows.Forms.TextBox textBoxDifficulty;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox textBoxType;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button btnCopyFile;
      private System.Windows.Forms.Button btnOpenFolder;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.TextBox textBoxSeed;
      private System.Windows.Forms.Label label4;
   }
}