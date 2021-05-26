namespace Abyssal_Quest
{
    partial class LoadShip
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
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.shipNameDisplay = new System.Windows.Forms.ComboBox();
            this.combatButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(13, 39);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(401, 587);
            this.outputTextBox.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(270, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Load";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // shipNameDisplay
            // 
            this.shipNameDisplay.FormattingEnabled = true;
            this.shipNameDisplay.Location = new System.Drawing.Point(13, 13);
            this.shipNameDisplay.Name = "shipNameDisplay";
            this.shipNameDisplay.Size = new System.Drawing.Size(251, 21);
            this.shipNameDisplay.TabIndex = 3;
            // 
            // combatButton
            // 
            this.combatButton.Location = new System.Drawing.Point(351, 12);
            this.combatButton.Name = "combatButton";
            this.combatButton.Size = new System.Drawing.Size(63, 23);
            this.combatButton.TabIndex = 4;
            this.combatButton.Text = "Combat";
            this.combatButton.UseVisualStyleBackColor = true;
            this.combatButton.Click += new System.EventHandler(this.combatButton_Click);
            // 
            // LoadShip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(426, 638);
            this.Controls.Add(this.combatButton);
            this.Controls.Add(this.shipNameDisplay);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.outputTextBox);
            this.Name = "LoadShip";
            this.Text = "Load Ship";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox shipNameDisplay;
        private System.Windows.Forms.Button combatButton;
    }
}

