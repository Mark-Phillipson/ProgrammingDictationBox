﻿namespace ControlWSR
{
	partial class AvailableCommandsForm
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
			this.richTextBoxAvailableCommands = new System.Windows.Forms.RichTextBox();
			this.textBoxResults = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// richTextBoxAvailableCommands
			// 
			this.richTextBoxAvailableCommands.Location = new System.Drawing.Point(1, 21);
			this.richTextBoxAvailableCommands.Name = "richTextBoxAvailableCommands";
			this.richTextBoxAvailableCommands.Size = new System.Drawing.Size(385, 427);
			this.richTextBoxAvailableCommands.TabIndex = 0;
			this.richTextBoxAvailableCommands.Text = "";
			// 
			// textBoxResults
			// 
			this.textBoxResults.Location = new System.Drawing.Point(393, 20);
			this.textBoxResults.Multiline = true;
			this.textBoxResults.Name = "textBoxResults";
			this.textBoxResults.Size = new System.Drawing.Size(399, 427);
			this.textBoxResults.TabIndex = 1;
			// 
			// AvailableCommandsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.textBoxResults);
			this.Controls.Add(this.richTextBoxAvailableCommands);
			this.Name = "AvailableCommandsForm";
			this.Text = "Available Commands";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxAvailableCommands;
		private System.Windows.Forms.TextBox textBoxResults;
	}
}
