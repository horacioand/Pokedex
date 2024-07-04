using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        //constructor para acceder al lector
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        // Establecer el acceso a los datos
        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS01; database=POKEDEX_DB; integrated security=true");
            comando = new SqlCommand();
        }

        // Setear consulta
        public void SetQuery(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        // Conectar y ejecutar lectura
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            { 
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public void cerrarConexion()
        {
            //Si el lector realizo una lectura cerrar el lector
            if(lector != null)
            {
                lector.Close();
            }

            conexion.Close();
        }
    }
}
