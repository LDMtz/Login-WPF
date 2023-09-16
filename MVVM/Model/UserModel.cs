using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    COMMENTS:

    -> Clase publica para el modelo de Usuario o User.

    -> Definimos las propiedades correspondientes del usuario (los mismos a los campos de la tabla)
   
*/

namespace MVVM.Model
{
    public class UserModel
    {
        //Properties
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

    }
}
