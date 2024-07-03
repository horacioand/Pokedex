using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class PokemonDatabase
    {
        //necesito una funcion que me devuelva un conjunto de datos, por lo que uso una funcion que me devuelva una lista de los datos
        public List<Pokemon> listar()
        {
            List<Pokemon> listaPokemons = new List<Pokemon>();

            SqlConnection conexion = new SqlConnection();   //Objeto para establecer la conexion a la base de datos
            SqlCommand comando = new SqlCommand();          //Para realizar acciones dentro de la base de datos
            SqlDataReader lector;
            //manejo de excepciones, dentro del try todo lo que pueda fallar(lectura de datos, conexion a db, etc)
            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS01; database=POKEDEX_DB; integrated security=true"; // Donde debe conectarse, que base de datos utilizar y con que tipo de seguridad hacerlo(credenciales)
                // (server=//url) (database=//base de datos) (integrated security=credenciales de windows o SQL, en caso de necesitar usar credenciales se agrega ;user= y ;password=)
                
                comando.CommandType = System.Data.CommandType.Text;    //Que tipo de comando que voy a inyectar en sql, con text se pasa la consulta sql

                comando.CommandText = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE P.IdTipo = E.Id AND D.Id = P.IdDebilidad";   //CONSULTA SQL

                comando.Connection = conexion; //establezco que la conexion del comando que voy a mandar va a ser mediante la conexion configurada

                conexion.Open();    //abro la conexion

                lector = comando.ExecuteReader();    //funcion que me devuelve un objeto sqldatareader con los datos, la cual voy a guardar en lector
                
                //para guardar todos los datos, voy a transformarlos en una coleccion de objetos
                while (lector.Read())       //lector.Read  ==== mientras el lector pueda leer un objeto se va a ejecutar el bloque de codigo
                {
                    Pokemon aux = new Pokemon();        //me creo un objeto auxiliar para guardar los dato apuntado
                    aux.Numero = lector.GetInt32(0);    //forma 1 de capturar datos, mediante indice
                    aux.Nombre = (string)lector["Nombre"];      // forma 2 de capturar datos, mediante nombre de columna, es necesario casteo explicito
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Imagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    listaPokemons.Add(aux);     //agrego el pokemon a la lista
                }

                conexion.Close();   //cierro la conexion a la db
                return listaPokemons;   //retorno la lista
            }
            catch (Exception)
            {

                throw;
            }




            
        }
    }
}
