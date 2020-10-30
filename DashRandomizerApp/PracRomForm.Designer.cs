namespace DASH
{
   partial class PracRomForm
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
         this.textBoxDifficulty = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.btnGenerate = new System.Windows.Forms.Button();
         this.btnClose = new System.Windows.Forms.Button();
         this.label4 = new System.Windows.Forms.Label();
         this.comboBoxType = new System.Windows.Forms.ComboBox();
         this.labelDetails = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(8, 9);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(455, 19);
         this.label1.TabIndex = 3;
         this.label1.Text = "Generate a version of the Super Metroid Practice Hack for practicing:";
         // 
         // textBoxDifficulty
         // 
         this.textBoxDifficulty.BackColor = System.Drawing.SystemColors.Control;
         this.textBoxDifficulty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.textBoxDifficulty.Font = new System.Drawing.Font("Calibri", 12F);
         this.textBoxDifficulty.Location = new System.Drawing.Point(12, 31);
         this.textBoxDifficulty.Name = "textBoxDifficulty";
         this.textBoxDifficulty.ReadOnly = true;
         this.textBoxDifficulty.Size = new System.Drawing.Size(503, 27);
         this.textBoxDifficulty.TabIndex = 6;
         this.textBoxDifficulty.Text = "rom difficulty";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Font = new System.Drawing.Font("Calibri", 12F);
         this.label2.Location = new System.Drawing.Point(8, 71);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(59, 19);
         this.label2.TabIndex = 5;
         this.label2.Text = "Details:";
         // 
         // btnGenerate
         // 
         this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnGenerate.AutoSize = true;
         this.btnGenerate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnGenerate.Location = new System.Drawing.Point(285, 208);
         this.btnGenerate.Name = "btnGenerate";
         this.btnGenerate.Size = new System.Drawing.Size(112, 29);
         this.btnGenerate.TabIndex = 1;
         this.btnGenerate.Text = "Generate";
         this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
         // 
         // btnClose
         // 
         this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnClose.AutoSize = true;
         this.btnClose.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.btnClose.Location = new System.Drawing.Point(403, 208);
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size(112, 29);
         this.btnClose.TabIndex = 2;
         this.btnClose.Text = "Close";
         this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label4.AutoSize = true;
         this.label4.Font = new System.Drawing.Font("Calibri", 12F);
         this.label4.Location = new System.Drawing.Point(8, 187);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(43, 19);
         this.label4.TabIndex = 9;
         this.label4.Text = "Type:";
         // 
         // comboBoxType
         // 
         this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.comboBoxType.FormattingEnabled = true;
         this.comboBoxType.Location = new System.Drawing.Point(12, 209);
         this.comboBoxType.Name = "comboBoxType";
         this.comboBoxType.Size = new System.Drawing.Size(267, 27);
         this.comboBoxType.TabIndex = 10;
         // 
         // labelDetails
         // 
         this.labelDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
         this.labelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.labelDetails.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelDetails.Location = new System.Drawing.Point(12, 93);
         this.labelDetails.Name = "labelDetails";
         this.labelDetails.Size = new System.Drawing.Size(503, 81);
         this.labelDetails.TabIndex = 11;
         this.labelDetails.Text = "details";
         // 
         // PracRomForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(534, 254);
         this.ControlBox = false;
         this.Controls.Add(this.labelDetails);
         this.Controls.Add(this.comboBoxType);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.btnClose);
         this.Controls.Add(this.btnGenerate);
         this.Controls.Add(this.textBoxDifficulty);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "PracRomForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Generate Practice Hack";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox textBoxDifficulty;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button btnGenerate;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.ComboBox comboBoxType;
      private System.Windows.Forms.Label labelDetails;
      }
}