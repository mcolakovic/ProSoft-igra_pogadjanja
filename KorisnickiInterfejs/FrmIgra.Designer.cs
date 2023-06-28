
namespace KorisnickiInterfejs
{
    partial class FrmIgra
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
            this.dgvIgra = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgra)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvIgra
            // 
            this.dgvIgra.AllowUserToAddRows = false;
            this.dgvIgra.AllowUserToDeleteRows = false;
            this.dgvIgra.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dgvIgra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIgra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIgra.ColumnHeadersVisible = false;
            this.dgvIgra.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvIgra.Location = new System.Drawing.Point(50, 50);
            this.dgvIgra.Name = "dgvIgra";
            this.dgvIgra.ReadOnly = true;
            this.dgvIgra.RowHeadersVisible = false;
            this.dgvIgra.RowHeadersWidth = 51;
            this.dgvIgra.RowTemplate.Height = 24;
            this.dgvIgra.Size = new System.Drawing.Size(300, 200);
            this.dgvIgra.TabIndex = 0;
            this.dgvIgra.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIgra_CellClick);
            // 
            // FrmIgra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 299);
            this.Controls.Add(this.dgvIgra);
            this.Name = "FrmIgra";
            this.Text = "FrmIgra";
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgra)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIgra;
    }
}