using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio1;

namespace negocio
{
    public class TraineeNegocio
    {
        //id
        //email         ----> ya los tenemos
        //pass
        //admin false

        //nombre, apellido, fecha, imagen

        public int insertarNuevo(Trainee nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("insertarNuevo");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);

                return datos.ejecutarAccionScalar();
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

        public bool Login(Trainee trainee)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("Select id, email, pass, admin from USERS where email = @email AND pass = @pass");
                datos.setearParametro("@email", trainee.Email);
                datos.setearParametro("@pass", trainee.Pass);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    trainee.Id = (int)datos.Lector["id"];
                    trainee.Admin = (bool)datos.Lector["admin"];
                    return true;
                }
                return false;
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
    }
}
