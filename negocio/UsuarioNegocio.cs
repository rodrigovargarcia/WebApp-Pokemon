using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio1;

namespace negocio
{
    public class UsuarioNegocio
    {
        public bool Logear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("select Id, TipoUser from USUARIOS where Usuario = @user AND Pass = @pass");
                datos.setearParametro("@user", usuario.User);
                datos.setearParametro("@pass", usuario.Pass);

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.TipoUsuario = (int)(datos.Lector["TipoUser"]) == 2 ? TipoUsuario.ADMIN : TipoUsuario.NORMAL;
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
