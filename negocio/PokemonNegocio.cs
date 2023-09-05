﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio1;
using negocio;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar(string id = "")
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server= .\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = P.IdTipo and D.id = P.IdDebilidad ";
                if(id != "")
                {
                    comando.CommandText += " and P.Id=    " + id;
                }
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();                    
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0); // una forma
                    aux.Nombre = (string)lector["Nombre"]; // otra forma, mejor porque no hace falta especificar el indice 
                    aux.Descripcion = (string)lector["Descripcion"];


                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    
                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    aux.Activo = (bool)lector["Activo"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;

            }
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }
        public List<Pokemon> listarConSP()
        {
            {
                List<Pokemon> lista = new List<Pokemon>();
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    //string consulta = ("select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = P.IdTipo and D.id = P.IdDebilidad and p.Activo = 1");                    
                    //datos.SetearConsulta(consulta);
                    datos.SetearProcedimiento("storedListar");
                    datos.EjecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Pokemon aux = new Pokemon();
                        aux.Id = (int)datos.Lector["Id"];
                        aux.Numero = datos.Lector.GetInt32(0); // una forma
                        aux.Nombre = (string)datos.Lector["Nombre"]; // otra forma, mejor porque no hace falta especificar el indice 
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                        aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            aux.UrlImagen = (string)datos.Lector["UrlImagen"];


                        aux.Tipo = new Elemento();
                        aux.Tipo.id = (int)datos.Lector["IdTipo"];
                        aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                        aux.Debilidad = new Elemento();
                        aux.Debilidad.id = (int)datos.Lector["IdDebilidad"];
                        aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                        lista.Add(aux);
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<Pokemon> listarConSPDefault()
        {
            {
                List<Pokemon> lista = new List<Pokemon>();
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    //string consulta = ("select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = P.IdTipo and D.id = P.IdDebilidad and p.Activo = 1");                    
                    //datos.SetearConsulta(consulta);
                    datos.SetearProcedimiento("storedListarDefault");
                    datos.EjecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Pokemon aux = new Pokemon();
                        aux.Id = (int)datos.Lector["Id"];
                        aux.Numero = datos.Lector.GetInt32(0); // una forma
                        aux.Nombre = (string)datos.Lector["Nombre"]; // otra forma, mejor porque no hace falta especificar el indice 
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                        //aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            aux.UrlImagen = (string)datos.Lector["UrlImagen"];


                        aux.Tipo = new Elemento();
                        aux.Tipo.id = (int)datos.Lector["IdTipo"];
                        aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                        aux.Debilidad = new Elemento();
                        aux.Debilidad.id = (int)datos.Lector["IdDebilidad"];
                        aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                        lista.Add(aux);
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen) values (" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', 1, @IdTipo, @IdDebilidad, @UrlImagen)");
                datos.setearParametro("IdTipo", nuevo.Tipo.id);
                datos.setearParametro("IdDebilidad", nuevo.Debilidad.id);
                datos.setearParametro("UrlImagen", nuevo.UrlImagen);
                //datos.EjecutarLectura(); ---> en el metodo setear consulta, realizamos un insert, NO una lectura. Por lo tanto necesitamos una ejecucion de tipo No consulta (executeNonQuery)
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void agregarConSP(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("storedAltaPokemon");
                datos.setearParametro("@numero", nuevo.Numero);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@img", nuevo.UrlImagen);
                datos.setearParametro("@idTipo", nuevo.Tipo.id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.id);
                //datos.setearParametro("@evolucion", null);
                //datos.EjecutarLectura(); ---> en el metodo setear consulta, realizamos un insert, NO una lectura. Por lo tanto necesitamos una ejecucion de tipo No consulta (executeNonQuery)
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void modificar(Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update POKEMONS set Numero = @Numero, Nombre = @Nombre, Descripcion = @Desc, UrlImagen = @Img, IdTipo = @IdTipo, IdDebilidad = @IdDebilidad where id = @Id");
                datos.setearParametro("@Numero", poke.Numero);
                datos.setearParametro("@Nombre", poke.Nombre);
                datos.setearParametro("@Desc", poke.Descripcion);
                datos.setearParametro("@Img", poke.UrlImagen);
                datos.setearParametro("@IdTipo", poke.Tipo.id);
                datos.setearParametro("@IdDebilidad", poke.Debilidad.id);
                datos.setearParametro("@Id", poke.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void modificarConSP(Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("storedModificarPokemon"); //update POKEMONS set Numero = @Numero, Nombre = @Nombre, Descripcion = @Desc, UrlImagen = @Img, IdTipo = @IdTipo, IdDebilidad = @IdDebilidad where id = @Id
                datos.setearParametro("@Numero", poke.Numero);
                datos.setearParametro("@Nombre", poke.Nombre);
                datos.setearParametro("@descripcion", poke.Descripcion);
                datos.setearParametro("@Img", poke.UrlImagen);
                datos.setearParametro("@IdTipo", poke.Tipo.id);
                datos.setearParametro("@IdDebilidad", poke.Debilidad.id);
                datos.setearParametro("@Id", poke.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearConsulta("delete from pokemons where id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarLogico(int id, bool activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearConsulta("update POKEMONS set Activo = @activo where id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }

        public object filtrar(string campo, string criterio, string filtro, string activo)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = ("select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.id = P.IdTipo and D.id = P.IdDebilidad and ");
                if(campo == "Número")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;                           
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }                
                else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "E.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "E.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                if (activo == "Activo")
                {
                    consulta += " and P.Activo = 1";
                }
                else if(activo == "Inactivo")
                {
                    consulta += " and P.Activo = 0";
                }
                                     
                
                
                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0); // una forma
                    aux.Nombre = (string)datos.Lector["Nombre"]; // otra forma, mejor porque no hace falta especificar el indice 
                    aux.Descripcion = (string)datos.Lector["Descripcion"];


                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];


                    aux.Tipo = new Elemento();
                    aux.Tipo.id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
