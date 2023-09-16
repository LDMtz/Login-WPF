﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    COMMENTS:

    -> Clase publica para el modelo del Usuario actual.

    -> Definimos las propiedades del Usuario actual
   
*/

namespace MVVM.Model
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
