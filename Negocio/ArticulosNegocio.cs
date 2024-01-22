using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;



namespace Negocio
{
    public class ArticulosNegocio
    {
        AccesoADatos datos = new AccesoADatos();
        
        public List<Articulos> Listar()
        {
            List<Articulos> Lista = new List<Articulos>();
            SqlConnection Conexion = new SqlConnection();
            SqlCommand Comando = new SqlCommand();



            try
            {
                Conexion.ConnectionString = "Data Source=DESKTOP-JIQQSHD\\SQLEXPRESS; Initial Catalog=CATALOGO_DB; Integrated Security=True;";
                datos.SetearConsulta("select Codigo, Nombre, A.Descripcion, M.Descripcion as Marcas, C.Descripcion as Categoria, ImagenUrl, Precio, A.Id, M.Id, C.Id, A.IdMarca, A.IdCategoria from dbo.ARTICULOS A, dbo.CATEGORIAS C, dbo.MARCAS M where A.IdCategoria = C.Id AND A.IdMarca= M.Id \r\n");
                datos.EjecutarLectura();

                while (datos.Lector.Read())

                {
                    Marca Marca = new Marca();
                    Categoria Categoria = new Categoria();
                    Articulos Aux = new Articulos(Marca, Categoria);
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo"))))
                        Aux.CodigoDeProducto = (string)datos.Lector["Codigo"];
                    Aux.Nombre = (string)datos.Lector["Nombre"];
                    Aux.Descripcion = (string)datos.Lector["Descripcion"];
                    Aux.Id = (int)datos.Lector["id"];
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl"))))
                    {
                        Aux.Imagen = (string)datos.Lector["ImagenUrl"];
                    }
                    Aux.Precio = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));
                    Aux.Marca.Descripcion = (string)datos.Lector["Marcas"];
                    Aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    Aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    Aux.Categoria.Id = (int)datos.Lector["IdCategoria"];




                    Lista.Add(Aux);
                }
                Conexion.Close();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Agregar(Articulos Nuevo)
        {
            AccesoADatos datos = new AccesoADatos();
            datos.SetearConsulta("Insert into ARTICULOS (Nombre, Descripcion, Precio, IdMarca, IdCategoria, ImagenUrl, Codigo) values('" + Nuevo.Nombre + "','" + Nuevo.Descripcion + "', " + Nuevo.Precio + ", @IdMarca, @IdCategoria, @ImagenUrl, @Codigo)");
            datos.SetearParametro("idMarca", Nuevo.Marca.Id);
            datos.SetearParametro("idCategoria", Nuevo.Categoria.Id);
            datos.SetearParametro("ImagenUrl", Nuevo.Imagen);
            datos.SetearParametro("Codigo", Nuevo.CodigoDeProducto);
            datos.EjecutarAccion();
        }
        public void AgregarMarca(Marca Nuevo)
        {
            AccesoADatos Datos = new AccesoADatos();
            datos.SetearConsulta("INSERT into MARCAS (Descripcion) values(@Descripcion)");
            datos.SetearParametro("Descripcion", Nuevo.Descripcion);
            datos.EjecutarAccion();
        }

        public void AgregarCategoria(Categoria Nuevo)
        {
            AccesoADatos Datos = new AccesoADatos();
            datos.SetearConsulta("INSERT into CATEGORIAS (Descripcion) values(@Descripcion)");
            datos.SetearParametro("Descripcion", Nuevo.Descripcion);
            datos.EjecutarAccion();
        }
        public void Modificar(Articulos Art)
        {
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.SetearConsulta("update ARTICULOS set Nombre = @Nombre, Codigo=@Codigo, Descripcion= @Desc, IdMarca= @IdMarca, IdCategoria= @IdCategoria, ImagenUrl= @ImagenUrl, Precio= @Precio where Id=@Id");
                datos.SetearParametro("@Nombre", Art.Nombre);
                datos.SetearParametro("@Desc", Art.Descripcion);
                datos.SetearParametro("@IdMarca", Art.Marca.Id);
                datos.SetearParametro("@IdCategoria", Art.Categoria.Id);
                datos.SetearParametro("@ImagenUrl", Art.Imagen);
                datos.SetearParametro("@Precio", Art.Precio);
                datos.SetearParametro("@Id", Art.Id);
                datos.SetearParametro("@Codigo", Art.CodigoDeProducto);
                datos.EjecutarAccion();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void Eliminar(int Id)
        {
            try
            {
                AccesoADatos datos = new AccesoADatos();
                datos.SetearConsulta("delete from ARTICULOS where Id=@Id");
                datos.SetearParametro("@Id", Id);
                datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulos> Filtrar( string Precio, string Categorias, string Marcas, string Nombre, int FPrecio, string FNombre)
        {
            List<Articulos> ListaDeArticulos = new List<Articulos>();
            AccesoADatos datos = new AccesoADatos();

            

            try
            {
                string Consulta = "select Codigo, Nombre, A.Descripcion, M.Descripcion as Marcas, C.Descripcion as Categoria , ImagenUrl, Precio, A.Id, M.Id, C.Id, A.IdMarca, A.IdCategoria from dbo.ARTICULOS A, dbo.CATEGORIAS C, dbo.MARCAS M where A.IdCategoria = C.Id AND A.IdMarca= M.Id ";
                if (!string.IsNullOrEmpty(Precio))
                {


                    switch (Precio)
                    {
                        case "mayor a":
                            Consulta += " AND Precio > " + FPrecio;
                            break;
                        case "menor a":
                            Consulta += " AND Precio < " + FPrecio;
                            break;
                        case "igual a":
                            Consulta += " AND Precio = " + FPrecio;
                            break;
                    }
                }


                if ((!string.IsNullOrEmpty(Categorias)) || Categorias=="") 
                {
                    Consulta += "AND C.Descripcion = '" + Categorias + "'";
                }


                if (!(string.IsNullOrEmpty(Marcas)) || Marcas=="")
                {

                    Consulta += "AND M.Descripcion = '" + Marcas + "'";

                }

                if (!(string.IsNullOrEmpty(FNombre)) || FNombre =="") 
                {
                    switch (Nombre)
                    {
                        case "termina con":
                            Consulta += " AND Nombre LIKE '" + FNombre + "%'";
                            break;
                        case "empieza con":
                            Consulta += " AND Nombre LIKE '%" + FNombre + "'";
                            break;
                        case "contiene":
                            Consulta += " AND Nombre LIKE '%" + FNombre + "%'";
                            break;
                    }
                }
               
                datos.SetearConsulta(Consulta);
                Console.WriteLine(Consulta);
                
                datos.EjecutarLectura();


                while (datos.Lector.Read())

                {
                    Marca Marca = new Marca();


                    Categoria Categoria = new Categoria();
                    Articulos Aux = new Articulos(Marca, Categoria);
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo"))))
                        Aux.CodigoDeProducto = (string)datos.Lector["Codigo"];
                    Aux.Nombre = (string)datos.Lector["Nombre"];
                    Aux.Descripcion = (string)datos.Lector["Descripcion"];
                    Aux.Id = (int)datos.Lector["id"];
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl"))))
                    {
                        Aux.Imagen = (string)datos.Lector["ImagenUrl"];
                    }
                    Aux.Precio = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("Precio"));
                    Aux.Marca.Descripcion = (string)datos.Lector["Marcas"];
                    Aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    Aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    Aux.Categoria.Id = (int)datos.Lector["IdCategoria"];


                    ListaDeArticulos.Add(Aux);
                }
            return ListaDeArticulos;
             
            }
            
            catch (Exception ex)
            {

                throw new Exception("no se pudo filtrar", ex);
            }

           
           

        }
    
    
        
    } 



 }
        

       
    


        
    

