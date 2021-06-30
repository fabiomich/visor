using PdfSharp.Drawing;
using PdfSharp.Pdf;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using VISOR.Clases;

namespace VISOR
{
    public partial class FrmVisorImagenes : Form
    {

        private const string rutaImg = @"C:\imagenes_scan\";

        
        List<Image> ListaImagenes;
        List<Image> ListaImagenesAtencion;
        int pageCount;
        int curPage;
        string NumHistoria = "";


        public FrmVisorImagenes()
        {
            InitializeComponent();
        }

        private void BuscarAtenciones(string NumHistoria)
        {
            try
            {
                this.dgvAtenciones.DataSource = null;
                this.dgvAtenciones.Rows.Clear();

                Utils.SqlDatos = "SELECT Id, NroAtencion AS [No Atencion], FechaAtencion AS [Fecha Atencion] " +
                                 "FROM [BDDIGITAEL].[dbo].[Datos Historia Escaneadas] " +
                                 "WHERE HistorPaci = @HistorPaci ";

                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@HistorPaci", SqlDbType.VarChar, 50) { Value = NumHistoria }
                    };

                DataSet ds = Conexion.SQLDataSet(Utils.SqlDatos, parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dgvAtenciones.DataSource = ds.Tables[0];

                    this.dgvAtenciones.Columns[0].Visible = false;
                    this.dgvAtenciones.Columns[2].Width = 105;
                }
                else
                {
                    Utils.Titulo01 = "Control de ejecucion programa";
                    Utils.Informa = "Lo siento pero no se encontraron atenciones" + "\r";
                    Utils.Informa += "para el numero de historia clinica." + "\r";
                    Utils.Informa += "Por tanto no se puede adjuntar una imagen." + "\r";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en el metodo BuscarAtenciones." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarImagenes()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;               

                Conexion.conexionACCESS = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SIIGHOSPLUS\SIIGPLUS.SIP;Jet OLEDB:Database Password=SIIGHOS33";

                Utils.SqlDatos = "SELECT * FROM [Datos ejecutar siighosplus]";

                OleDbDataReader drOle = Conexion.AccessDataReaderOLEDB(Utils.SqlDatos);

                if (drOle.HasRows)
                {
                    drOle.Read();
                    NumHistoria = drOle["HistoTempo"].ToString();
                }
                else
                {
                    return;
                }

                drOle.Close();



                Utils.SqlDatos = "SELECT Imagen FROM [BDDIGITAEL].[dbo].[Datos Historia Escaneadas] WHERE HistorPaci = " + NumHistoria;
                SqlDataReader dr = Conexion.SQLDataReader(Utils.SqlDatos);

                if (dr.HasRows)
                {
                    pageCount = 0;
                    ListaImagenes = new List<Image>();

                    while (dr.Read())
                    {
                        byte[] bytesImg = (byte[])(dr["Imagen"]);
                        MemoryStream stream = new MemoryStream(bytesImg);
                        Bitmap tifImg = new Bitmap(stream);
                        Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                        int count = bitmap.GetFrameCount(FrameDimension.Page);

                        for (int idx = 0; idx < count; idx++)
                        {
                            // save each frame to a bytestream
                            bitmap.SelectActiveFrame(FrameDimension.Page, idx);
                            System.IO.MemoryStream byteStream = new System.IO.MemoryStream();
                            bitmap.Save(byteStream, ImageFormat.Tiff);

                            // and then create a new Image from it
                            ListaImagenes.Add(Image.FromStream(byteStream));
                        }

                        pageCount += count;
                    }

                    //pageCount = count;
                    curPage = 0;

                    this.pbListaImagenes.Image = ListaImagenes[curPage];
                    this.pbListaImagenes.Refresh();

                    this.lblPaginacion.Text = "Pagina " + Convert.ToString(curPage + 1) + " de " + Convert.ToString(pageCount);

                    //org = new PictureBox();
                    //org.Image = this.pbListaImagenes.Image;

                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en el metodo CargarImagenes." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void RecargarImagenes()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                curPage = 0;
                pageCount = ListaImagenes.Count;

                this.pbListaImagenes.Image = ListaImagenes[curPage];
                this.pbListaImagenes.Refresh();

                this.lblPaginacion.Text = "Pagina " + Convert.ToString(curPage + 1) + " de " + Convert.ToString(pageCount);
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en el metodo RecargarImagenes." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FrmVisorImagenes_Load(object sender, EventArgs e)
        {
            try
            {
                Conexion.conexionACCESS = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SIIGHOSPLUS\LogPlus.LogSip;Jet OLEDB:Database Password=SIIGHOS33";

                Utils.SqlDatos = "SELECT * FROM [Local registro del usuario]";

                OleDbDataReader dr = Conexion.AccessDataReaderOLEDB(Utils.SqlDatos);

                if (dr.HasRows)
                {
                    dr.Read();

                    // Se procede a validar las credenciales de acceso al Servidor SQL Server
                    // Y verificar el tipo de cliente de SQL Server

                    Conexion.servidor = dr["NomServi"].ToString();
                    Conexion.username = dr["NomUsar"].ToString();
                    Conexion.password = dr["PassWusa"].ToString();
                    //Conexion.nativeclient = dr["VerNatiClien"].ToString();

                    Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                           "Initial Catalog = BDDIGITAEL; " +
                                           "User ID = " + Conexion.username + "; " +
                                           "Password = " + Conexion.password;

                    Utils.codUsuario = dr["CodigUsar"].ToString();
                    Utils.nomUsuario = dr["NombreUsar"].ToString();
                    Utils.nivelPermiso = dr["NivelPermiso"].ToString();
                    Utils.codUnicoEmpresa = dr["CodRegEn"].ToString(); // CodEnti
                    Utils.CodAplicacion = dr["CodApli"].ToString();
                }
                else
                {
                    this.Close();
                }

                CargarImagenes();

                BuscarAtenciones(NumHistoria);
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al abrir el formulario principal" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnImgAtras_Click(object sender, EventArgs e)
        {
            try
            {
                if (curPage - 1 >= 0)
                {
                    curPage--;

                    this.pbListaImagenes.Image = ListaImagenes[curPage]; // tifPages[curPage];
                    this.pbListaImagenes.Refresh();
                    this.lblPaginacion.Text = "Pagina " + Convert.ToString(curPage + 1) + " de " + Convert.ToString(pageCount);

                    //org = new PictureBox();
                    //org.Image = this.pictureBox1.Image;
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al dar clic en el boton anterior." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImgAdelante_Click(object sender, EventArgs e)
        {
            try
            {
                if (curPage + 1 < pageCount)
                {
                    curPage++;

                    this.pbListaImagenes.Image = ListaImagenes[curPage]; // tifPages[curPage];
                    this.pbListaImagenes.Refresh();
                    this.lblPaginacion.Text = "Pagina " + Convert.ToString(curPage + 1) + " de " + Convert.ToString(pageCount);

                    //org = new PictureBox();
                    //org.Image = this.pictureBox1.Image;
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al dar clic en el boton siguiente." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCargarAtenciones_Click(object sender, EventArgs e)
        {
            try
            {
                RecargarImagenes();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al dar clic en el boton Cargar todas las Atenciones." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvAtenciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int idAtencion = 0;
                idAtencion = Convert.ToInt32(dgvAtenciones.Rows[dgvAtenciones.CurrentRow.Index].Cells[0].Value);

                Utils.SqlDatos = "SELECT Imagen FROM [BDDIGITAEL].[dbo].[Datos Historia Escaneadas] WHERE Id = " + idAtencion;
                SqlDataReader dr = Conexion.SQLDataReader(Utils.SqlDatos);

                if (dr.HasRows)
                {
                    dr.Read();

                    byte[] bytesImg = (byte[])(dr["Imagen"]);
                    MemoryStream stream = new MemoryStream(bytesImg);
                    Bitmap tifImg = new Bitmap(stream);

                    ListaImagenesAtencion = new List<Image>();

                    Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                    int count = bitmap.GetFrameCount(FrameDimension.Page);
                    for (int idx = 0; idx < count; idx++)
                    {
                        // save each frame to a bytestream
                        bitmap.SelectActiveFrame(FrameDimension.Page, idx);
                        System.IO.MemoryStream byteStream = new System.IO.MemoryStream();
                        bitmap.Save(byteStream, ImageFormat.Tiff);

                        // and then create a new Image from it
                        ListaImagenesAtencion.Add(Image.FromStream(byteStream));
                    }

                    pageCount = count;
                    curPage = 0;

                    this.pbListaImagenes.Image = ListaImagenesAtencion[curPage];
                    this.pbListaImagenes.Refresh();

                    this.lblPaginacion.Text = "Pagina " + Convert.ToString(curPage + 1) + " de " + Convert.ToString(pageCount);

                   
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al dar clic en la tabla de atenciones." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
          
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvAtenciones.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                string nomPDF;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Pdf|*.pdf";
                saveFileDialog1.Title = "Guardar Imagenes como un documento PDF";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    this.Cursor = Cursors.WaitCursor;

                    List<Image> images = null;
                    int count = 0;

                    Utils.Titulo01 = "Control de ejecucion programa";

                    //int idAtencion = Convert.ToInt32(dgvAtenciones.Rows[dgvAtenciones.CurrentRow.Index].Cells[0].Value);

                    //Utils.SqlDatos = "SELECT Imagen " +
                    //                 "FROM [BDDIGITAEL].[dbo].[Datos Historia Escaneadas] " +
                    //                 "WHERE Id = " + idAtencion;

                    Utils.SqlDatos = "SELECT Imagen FROM [BDDIGITAEL].[dbo].[Datos Historia Escaneadas] WHERE HistorPaci = " + NumHistoria;


                    SqlDataReader dr1 = Conexion.SQLDataReader(Utils.SqlDatos);

                    if (dr1.HasRows)
                    {
                        images = new List<Image>();

                        while (dr1.Read())
                        {
                            byte[] bytesImg = (byte[])(dr1["Imagen"]);

                            MemoryStream stream = new MemoryStream(bytesImg);

                            Bitmap tifImg = new Bitmap(stream);

                            Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                            count = bitmap.GetFrameCount(FrameDimension.Page);

                            for (int idx = 0; idx < count; idx++)
                            {
                                // save each frame to a bytestream
                                bitmap.SelectActiveFrame(FrameDimension.Page, idx);
                                System.IO.MemoryStream byteStream = new System.IO.MemoryStream();
                                bitmap.Save(byteStream, ImageFormat.Tiff);

                                // and then create a new Image from it
                                images.Add(Image.FromStream(byteStream));
                            }
                        }

                    }

                    dr1.Close();

                    nomPDF = saveFileDialog1.FileName;

                    PdfDocument doc = new PdfDocument();
                    int i = 0;

                    foreach (var item in images)
                    {

                        PdfPage page = new PdfPage();
                        XImage img = XImage.FromGdiPlusImage(item);

                        page.Width = img.PointWidth;
                        page.Height = img.PointHeight;
                        doc.Pages.Add(page);

                        XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[i]);

                        xgr.DrawImage(img, 0, 0);

                        i++;
                    }

                    doc.Save(nomPDF);

                    doc.Close();

                    this.Cursor = Cursors.Default;

                    Utils.Informa = "Se exportaron las imagenes a  PDF";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al dar clic en el botón Exportar a PDF." + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
