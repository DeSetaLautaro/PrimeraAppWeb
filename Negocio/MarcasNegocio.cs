using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcasNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> Lista = new List<Marca>();
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearConsulta("select Id, Descripcion from MARCAS");
                Datos.EjecutarLectura();

                while (Datos.Lector.Read())
                {
                    Marca Marca = new Marca();
                    Marca.Id = (int)Datos.Lector["Id"];
                    Marca.Descripcion = (string)Datos.Lector["Descripcion"];

                    Lista.Add(Marca);
                }

                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }

    
    }
}
