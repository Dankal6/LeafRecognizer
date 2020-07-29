namespace LeafRecognizer
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.leafPicture = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.RecognizeButton = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.logLoss = new System.Windows.Forms.TextBox();
            this.pcLogLoss = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leafPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // leafPicture
            // 
            this.leafPicture.Location = new System.Drawing.Point(12, 12);
            this.leafPicture.Name = "leafPicture";
            this.leafPicture.Size = new System.Drawing.Size(560, 560);
            this.leafPicture.TabIndex = 0;
            this.leafPicture.TabStop = false;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(117, 601);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // RecognizeButton
            // 
            this.RecognizeButton.Location = new System.Drawing.Point(198, 601);
            this.RecognizeButton.Name = "RecognizeButton";
            this.RecognizeButton.Size = new System.Drawing.Size(75, 23);
            this.RecognizeButton.TabIndex = 2;
            this.RecognizeButton.Text = "Recognize";
            this.RecognizeButton.UseVisualStyleBackColor = true;
            this.RecognizeButton.Click += new System.EventHandler(this.RecognizeButton_Click);
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(314, 601);
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.Size = new System.Drawing.Size(191, 20);
            this.Output.TabIndex = 3;
            // 
            // logLoss
            // 
            this.logLoss.Location = new System.Drawing.Point(61, 629);
            this.logLoss.Name = "logLoss";
            this.logLoss.ReadOnly = true;
            this.logLoss.Size = new System.Drawing.Size(225, 20);
            this.logLoss.TabIndex = 4;
            // 
            // pcLogLoss
            // 
            this.pcLogLoss.Location = new System.Drawing.Point(390, 629);
            this.pcLogLoss.Name = "pcLogLoss";
            this.pcLogLoss.ReadOnly = true;
            this.pcLogLoss.Size = new System.Drawing.Size(182, 20);
            this.pcLogLoss.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 632);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Log loss";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 632);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Per class Log loss";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcLogLoss);
            this.Controls.Add(this.logLoss);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.RecognizeButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.leafPicture);
            this.Name = "Form1";
            this.Text = "LeafRecognizer";
            ((System.ComponentModel.ISupportInitialize)(this.leafPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox leafPicture;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button RecognizeButton;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.TextBox logLoss;
        private System.Windows.Forms.TextBox pcLogLoss;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

