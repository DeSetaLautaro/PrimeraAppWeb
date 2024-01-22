using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulos
    {
        public Articulos() 
        { }
        public Articulos(Marca M, Categoria C) 
        {
            this.Marca = M;
            this.Categoria = C;
        }
        public string CodigoDeProducto {  get; set; }

        public int Id { get; set; }
        public string Nombre {  get; set; }
        public Categoria Categoria {  get; set; }
        public Marca Marca { get; set; }

        public string Descripcion { get; set; }

        public string Imagen {  get; set; }

        public decimal Precio { get; set; }
    }
}
