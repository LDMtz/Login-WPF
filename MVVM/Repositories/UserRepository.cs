using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MVVM.Model; //<-- Agregamos la referencia al componente Model
using System.Data.SqlClient; //<- Agregamos referencia al ensamblado para conectarse a la BD
using System.Data;
/*
COMMENTS:

    -> Clase en publica que hereda de la clase base "RepositoryBase" y que implementa la interfaz "IUserRepository" (del "Model"), 
    y que se encargará de separar la logica de acceso a datos de la "View" y la "ViewModel"

    -> Methods de la Interfaz:

    Add: Agrega un usuario a la DB

    AuthenticateUser: Aquí hacemos la consulta correspondiente a la BD y devolvemos un bool si la auntentificacion de usuario es
    valida o no.
    
    Edit:
    
    GetByAll:
    
    GetById:

    GetByUsername: Hace una consulta a la DB, y recupera los datos del nombre de usuario pasado como parametro.
    
    Remove:
    
*/

namespace MVVM.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        //Methods de la interfaz "IUserRepository"
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where Username=@username and [Password]=@password";
                command.Parameters.Add("@username",SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel? GetByUsername(string username)
        {
            UserModel? user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where Username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using(var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
