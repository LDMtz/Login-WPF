using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient; //<- Agregamos referencia al ensamblado de SQL

/*
    COMMENTS:

    -> Clase en publica y abstracta para que la clase solo pueda ser usada mediante la herencia

    -> Definimos un campo privado de solo lectura ("_connectionString"), que incluirá la cadena de conexion a la DB, esta cadena
       se inicializa en el constructor.

    -> Definimos un método llamado "GetConnection" que retorna una instancia de la clase "SqlConnection" y que contiene la 
       cadena de conexion a la DB.
*/

namespace MVVM.Repositories
{
    public abstract class RepositoryBase
    {
        //Properties
        private readonly string _connectionString;

        //Constructor
        public RepositoryBase()
        {
            _connectionString = "Server=DESKTOP-1145PHU\\SQLEXPRESS;Database=MVVM_DB;Integrated Security=true";
        }

        //Method
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
