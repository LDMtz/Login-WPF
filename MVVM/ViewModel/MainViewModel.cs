using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading; //<-- Agregamos referencia al ensamblado para la clase Thread"
using MVVM.Model; //<-- Agregamos referencia al componente "Model"
using MVVM.Repositories; //<-- Referencia a los "Repositories" del proyecto
/*
    COMMENTS:

    -> Clase publica para la ventana principal del y que hereda de la clase base "ViewModelBase".

    -> Definimos los campos de  clase
    
    -> En el constructor creamos una instancia de la clase "UserRepository" y cargamos los datos del
       usuario actual mediante el metodo "LoadCurrentUserData".

    -> Methods:
       
       LoadCurrentUserData: Cargar los datos del usuario actual autenticado y los almacena en un objeto "UserAccountModel"
*/

namespace MVVM.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        //Properties
        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set 
            { 
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        //Constructor
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        //Methods
        private void LoadCurrentUserData()
        {
            if (Thread.CurrentPrincipal?.Identity?.Name != null) //<-- Condicion para evitar advertencias de posible asignacion a valor NULL
            {
                var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
                if (user != null)
                {
                    CurrentUserAccount.Username = user.Username;
                    CurrentUserAccount.DisplayName = $"Bienvenido {user.Name} {user.LastName} :)";
                    CurrentUserAccount.ProfilePicture = null;
                }
                else
                {
                    CurrentUserAccount.DisplayName = "Usuario inválido, sin iniciar sesión";
                    //Hide childs view
                }
            }

        }

        
    }
}
