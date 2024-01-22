using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoADatos
    {
        private SqlCommand Comando;
        private SqlConnection Conexion;
        public SqlDataReader Lector;
        

        public SqlDataReader Lector_
        {
            get { return Lector; }
        }
    
        public AccesoADatos()
        {
            Conexion = new SqlConnection("Data Source=DESKTOP-JIQQSHD\\SQLEXPRESS; Initial Catalog=CATALOGO_DB; Integrated Security=True;");
            Comando = new SqlCommand();

        }

        public void SetearConsulta(string Consulta)
        {
            Comando.CommandType = System.Data.CommandType.Text;
            Comando.CommandText = Consulta;

        }

        public void EjecutarLectura ()
        {
            Comando.Connection= Conexion;
            try
            {
                Conexion.Open();
                Lector = Comando.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void EjectutarAccion()
        {
            Comando.Connection = Conexion;
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EjecutarAccion()
        {
            Comando.Connection = Conexion;
            try
            {
                Conexion.Open ();
                Comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetearParametro(string parametro, object valor)
        {
            Comando.Parameters.AddWithValue(parametro,valor);


        }

        public void CerrarConexion()
        {
            if (Lector != null) Lector.Close();

            Conexion.Close();
            
        }
    
    }
}
