namespace BeastiaryQuery
{
    partial class DisplayForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCombat = new System.Windows.Forms.TextBox();
            this.tbLore = new System.Windows.Forms.TextBox();
            this.tbSurvival = new System.Windows.Forms.TextBox();
            this.tbMagic = new System.Windows.Forms.TextBox();
            this.Priority = new System.Windows.Forms.Label();
            this.cbPriority = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CombatScript";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "LoreScripts";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "SurvivalScripts";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "MagicScripts";
            // 
            // tbCombat
            // 
            this.tbCombat.Location = new System.Drawing.Point(127, 5);
            this.tbCombat.Name = "tbCombat";
            this.tbCombat.Size = new System.Drawing.Size(418, 20);
            this.tbCombat.TabIndex = 4;
            // 
            // tbLore
            // 
            this.tbLore.Location = new System.Drawing.Point(127, 39);
            this.tbLore.Name = "tbLore";
            this.tbLore.Size = new System.Drawing.Size(418, 20);
            this.tbLore.TabIndex = 5;
            // 
            // tbSurvival
            // 
            this.tbSurvival.Location = new System.Drawing.Point(127, 71);
            this.tbSurvival.Name = "tbSurvival";
            this.tbSurvival.Size = new System.Drawing.Size(418, 20);
            this.tbSurvival.TabIndex = 6;
            // 
            // tbMagic
            // 
            this.tbMagic.Location = new System.Drawing.Point(127, 105);
            this.tbMagic.Name = "tbMagic";
            this.tbMagic.Size = new System.Drawing.Size(418, 20);
            this.tbMagic.TabIndex = 7;
            // 
            // Priority
            // 
            this.Priority.AutoSize = true;
            this.Priority.Location = new System.Drawing.Point(16, 140);
            this.Priority.Name = "Priority";
            this.Priority.Size = new System.Drawing.Size(44, 13);
            this.Priority.TabIndex = 8;
            this.Priority.Text = "Prioprity";
            // 
            // cbPriority
            // 
            this.cbPriority.FormattingEnabled = true;
            this.cbPriority.Items.AddRange(new object[] {
            "Combats",
            "Lores",
            "Survivals",
            "Magics"});
            this.cbPriority.Location = new System.Drawing.Point(66, 140);
            this.cbPriority.Name = "cbPriority";
            this.cbPriority.Size = new System.Drawing.Size(84, 64);
            this.cbPriority.TabIndex = 9;
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 258);
            this.Controls.Add(this.cbPriority);
            this.Controls.Add(this.Priority);
            this.Controls.Add(this.tbMagic);
            this.Controls.Add(this.tbSurvival);
            this.Controls.Add(this.tbLore);
            this.Controls.Add(this.tbCombat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DisplayForm";
            this.Text = "DisplayForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCombat;
        private System.Windows.Forms.TextBox tbLore;
        private System.Windows.Forms.TextBox tbSurvival;
        private System.Windows.Forms.TextBox tbMagic;
        private System.Windows.Forms.Label Priority;
        private System.Windows.Forms.CheckedListBox cbPriority;
    }
}