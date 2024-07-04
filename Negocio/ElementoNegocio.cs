﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class ElementoNegocio
    {
        public List<Elemento> listar()
        {
			List<Elemento> listaElementos = new List<Elemento>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.SetQuery("SELECT Id, Descripcion FROM ELEMENTOS");
				datos.ejecutarLectura();
				while (datos.Lector.Read())
				{
					Elemento aux = new Elemento();
					aux.Id = (int)datos.Lector["Id"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];
					listaElementos.Add(aux);
				}
				return listaElementos;
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				datos.cerrarConexion();
			}
        }
    }
}
