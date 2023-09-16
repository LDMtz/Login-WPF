using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; //<-- Agregamos esta referencia para usar la clase "NetworCredential"
/*
    COMMENTS:

    -> Interfaz publica para declarar los métodos del repositorio de usuario.

    -> Methods.
       "AuthenticateUser": Para la auntentificación del usuario y que recibe como parametro Usuario y Contraseña
       pero dado a que en la View y en el ViewModel son de tipo "SecureString" usamos la clase NetworCredential.

       "Add": Agrega un nuevo usuario

       "Edit": Modifica los datos de un usuario
         
       "Remove": Elimina un usuario

       "GetById": Obtiene un usuario por mediante el Id

       "GetByUsername": Obtiene un usuario por mediante el Username

       "GetByAll":Obtiene una colección de todos los registros de usuarios.
*/

namespace MVVM.Model
{
    public interface IUserRepository
    {
        //Methods
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel ?GetByUsername(string username);
        IEnumerable<UserModel> GetByAll();
    }
}
