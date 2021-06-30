
namespace VISOR
{
    partial class FrmVisorImagenes
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCargarAtenciones = new System.Windows.Forms.Button();
            this.pbListaImagenes = new System.Windows.Forms.PictureBox();
            this.btnImgAtras = new System.Windows.Forms.Button();
            this.btnImgAdelante = new System.Windows.Forms.Button();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.lblPaginacion = new System.Windows.Forms.Label();
            this.dgvAtenciones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pbListaImagenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtenciones)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCargarAtenciones
            // 
            this.btnCargarAtenciones.Location = new System.Drawing.Point(12, 12);
            this.btnCargarAtenciones.Name = "btnCargarAtenciones";
            this.btnCargarAtenciones.Size = new System.Drawing.Size(120, 54);
            this.btnCargarAtenciones.TabIndex = 0;
            this.btnCargarAtenciones.Text = "Cargar todas las atenciones";
            this.btnCargarAtenciones.UseVisualStyleBackColor = true;
            this.btnCargarAtenciones.Click += new System.EventHandler(this.btnCargarAtenciones_Click);
            // 
            // pbListaImagenes
            // 
            this.pbListaImagenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbListaImagenes.Location = new System.Drawing.Point(12, 109);
            this.pbListaImagenes.Name = "pbListaImagenes";
            this.pbListaImagenes.Size = new System.Drawing.Size(918, 810);
            this.pbListaImagenes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbListaImagenes.TabIndex = 1;
            this.pbListaImagenes.TabStop = false;
            // 
            // btnImgAtras
            // 
            this.btnImgAtras.Location = new System.Drawing.Point(247, 12);
            this.btnImgAtras.Name = "btnImgAtras";
            this.btnImgAtras.Size = new System.Drawing.Size(75, 54);
            this.btnImgAtras.TabIndex = 2;
            this.btnImgAtras.Text = "<<";
            this.btnImgAtras.UseVisualStyleBackColor = true;
            this.btnImgAtras.Click += new System.EventHandler(this.btnImgAtras_Click);
            // 
            // btnImgAdelante
            // 
            this.btnImgAdelante.Location = new System.Drawing.Point(328, 12);
            this.btnImgAdelante.Name = "btnImgAdelante";
            this.btnImgAdelante.Size = new System.Drawing.Size(75, 54);
            this.btnImgAdelante.TabIndex = 3;
            this.btnImgAdelante.Text = ">>";
            this.btnImgAdelante.UseVisualStyleBackColor = true;
            this.btnImgAdelante.Click += new System.EventHandler(this.btnImgAdelante_Click);
            // 
            // btnExportarPdf
            // 
            this.btnExportarPdf.Location = new System.Drawing.Point(138, 12);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(103, 54);
            this.btnExportarPdf.TabIndex = 4;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.UseVisualStyleBackColor = true;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            // 
            // lblPaginacion
            // 
            this.lblPaginacion.Location = new System.Drawing.Point(247, 73);
            this.lblPaginacion.Name = "lblPaginacion";
            this.lblPaginacion.Size = new System.Drawing.Size(156, 30);
            this.lblPaginacion.TabIndex = 5;
            this.lblPaginacion.Text = "Pagina 0 de 0";
            this.lblPaginacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvAtenciones
            // 
            this.dgvAtenciones.AllowUserToAddRows = false;
            this.dgvAtenciones.AllowUserToDeleteRows = false;
            this.dgvAtenciones.AllowUserToResizeColumns = false;
            this.dgvAtenciones.AllowUserToResizeRows = false;
            this.dgvAtenciones.ColumnHeadersHeight = 29;
            this.dgvAtenciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAtenciones.Location = new System.Drawing.Point(409, 12);
            this.dgvAtenciones.MultiSelect = false;
            this.dgvAtenciones.Name = "dgvAtenciones";
            this.dgvAtenciones.ReadOnly = true;
            this.dgvAtenciones.RowHeadersVisible = false;
            this.dgvAtenciones.RowHeadersWidth = 51;
            this.dgvAtenciones.RowTemplate.Height = 24;
            this.dgvAtenciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAtenciones.Size = new System.Drawing.Size(521, 91);
            this.dgvAtenciones.TabIndex = 7;
            this.dgvAtenciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAtenciones_CellClick);
            // 
            // FrmVisorImagenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 943);
            this.Controls.Add(this.dgvAtenciones);
            this.Controls.Add(this.lblPaginacion);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.btnImgAdelante);
            this.Controls.Add(this.btnImgAtras);
            this.Controls.Add(this.pbListaImagenes);
            this.Controls.Add(this.btnCargarAtenciones);
            this.MaximizeBox = false;
            this.Name = "FrmVisorImagenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HISTORIAS DIGITALIZADAS SIIGHOS PLUS  Ver. 0.0.1 (29-JUN-2021)";
            this.Load += new System.EventHandler(this.FrmVisorImagenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbListaImagenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtenciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCargarAtenciones;
        private System.Windows.Forms.PictureBox pbListaImagenes;
        private System.Windows.Forms.Button btnImgAtras;
        private System.Windows.Forms.Button btnImgAdelante;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Label lblPaginacion;
        private System.Windows.Forms.DataGridView dgvAtenciones;
    }
}

