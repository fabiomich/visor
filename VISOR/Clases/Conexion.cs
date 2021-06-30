﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VISOR.Clases
{
    class Conexion
    {
        #region DECLARACION PROPIEDADES

        public static string servidor { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string nativeclient { get; set; }

        public static string conexionSQL { get; set; }
        public static string conexionACCESS { get; set; }

        public static OleDbConnection conexionOleDb;
        public static SqlConnection sqlConnection;

        #endregion


        public static SqlDataReader SQLDataReader(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                sqlConnection = new SqlConnection(conexionSQL);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                SqlDataReader reader = command.ExecuteReader();

                return reader;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLDataReader" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool SQLUpdate(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(conexionSQL))
                {
                    SqlCommand command = new SqlCommand(sqlString, sqlCon);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    sqlCon.Open();

                    command.CommandTimeout = 180;
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                //Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLUpdate" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static int SQLStoreProcedure(string sqlString)
        {
            try
            {
                int rowAffected;

                using (SqlConnection sqlConnection1 = new SqlConnection(conexionSQL))
                {
                    SqlCommand command = new SqlCommand(sqlString, sqlConnection1);
                    command.CommandType = CommandType.Text;

                    sqlConnection1.Open();
                    rowAffected = command.ExecuteNonQuery();
                }

                return rowAffected;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLStoreProcedure" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }


        public static DataSet SQLDataSet(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (SqlConnection sqlConnection1 = new SqlConnection(conexionSQL))
                {
                    sqlConnection1.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlString, sqlConnection1);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            sqlDataAdapter.SelectCommand.Parameters.Add(parameter);
                        }
                    }

                    DataSet dataSet = new DataSet();

                    sqlDataAdapter.Fill(dataSet);

                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLDataSet" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable SQLDataTable(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (SqlConnection sqlConnection1 = new SqlConnection(conexionSQL))
                {
                    sqlConnection1.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlString, sqlConnection1);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            sqlDataAdapter.SelectCommand.Parameters.Add(parameter);
                        }
                    }

                    DataTable dataTable = new DataTable();

                    sqlDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLDataSet" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static OleDbDataReader AccessDataReaderOLEDB(string sqlString)
        {
            try
            {
                // No se puede cerrar el objeto conexion antes de usar el Data reader
                // Se necesita abierto para que funcione el Data reader

                conexionOleDb = new OleDbConnection();
                conexionOleDb.ConnectionString = conexionACCESS;
                conexionOleDb.Open();

                OleDbCommand cmd = new OleDbCommand(sqlString, conexionOleDb);

                OleDbDataReader reader = cmd.ExecuteReader();

                return reader;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion AccessDataReaderOLEDB" + "\r\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}
