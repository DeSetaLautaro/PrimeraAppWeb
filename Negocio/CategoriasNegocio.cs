using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriasNegocio
    {
        List<Categoria> Lista = new List<Categoria>();
        AccesoADatos Datos = new AccesoADatos();

        public List<Categoria> Listar()
        {
            try
            {
                Datos.SetearConsulta("select Id, Descripcion from CATEGORIAS");
                Datos.EjecutarLectura();

                while (Datos.Lector.Read())
                {
                    Categoria Categoria = new Categoria();
                    Categoria.Id = (int)Datos.Lector["Id"];
                    Categoria.Descripcion = (string)Datos.Lector["Descripcion"];

                    Lista.Add(Categoria);
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