using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security; //<-- Agregamos la referencia al ensamblado para las campos seguros
using System.Windows.Input; //<-- Agregamos referencia al ensamblado de entrada de windows para ICommand
using MVVM.Model; //<-- Agregamos referencia al componente "Model"
using MVVM.Repositories; //<-- Agregamos una referencia los repositorios
using System.Net; //<-- Agregamos referencia para la clase NetworkCredential
using System.Threading; //< --Agregamos referencia para la clase Thread
using System.Security.Principal;

/*
    COMMENTS:

    -> Clase publica y que hereda la clase base del ViewModel.

    -> Definimos los campos de clase privados para establecer el enlace entre la View y la ViewModel (los campos: _username,
       _password, _errorMessage y _isViewVisible).

       Para el campo _password usamos un tipo de dato seguro , se usa para el acceso de datos confidenciales
       mediante el teclado.

    -> Definimos un campo para el repositorio del usuario del tipo "IUserRepository" llamado "userRepository" y lo inicializamos en 
       el constructor con una nueva instancia de la clase "UserRepository"

    -> Definimos las properties con su get y set de sus respectivos campos de clase

       Cada vez que se establezca un valor a la propiedad (set) debemos notificar que la propiedad ha cambiado, es
       decir generar un evento el cual esta defenido en la clase base "OnPropertyChanged()".

    -> Definimos propiedades del tipo ICommand para ejecutar las acciones del usuario: "LoginComand", "RecoverPasswordCommand",
       "ShowPasswordCommand" y "RememberPasswordCommand".
       
       En estos comandos solo definimos el metodo de acceso "get" y no el "set" ya que debe ser privado y solo la propia clase deberia poder inicializar
       o establecer un valor, ya que no tendria sentido que la View inicializara o estableciera la accion del comando ya que romperia
       el principio del patron MVVM

    -> Inicializamos los comandos en el constructor mediante la clase "ViewModelCommand" de la ViewModel, uno de los constructores de 
       esa clase requiere dos delegados como parametro: 

       "Action<object> executeAction" El primero un delegado Action para ejecutar el comando
       "Predicate<object> canExecuteAction": El segundo un delegado predicado para ver si se puede ejecutar el comando.

       y el otro constructor solo requiere el delegado Action que ejecuta el comando, sin necesidad de validar nada.

    -> Creamos los metodos de clase que se delegan a los comandos para ser pasados como parametros al constructor, dependiendo de las 
       necesidades se crearon el "Execute" y el "CanExecute" o solo el "Execute" para cada comando.
       
       En los metodos "CanExecute..." debemos hacer las validaciones correspondientes antes de ejecutar el comando y en los metodos "Execute..."
       debemos ejecutar los comandos.
       
                                                          
*/

namespace MVVM.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields    
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible=true;

        private IUserRepository userRepository;


        //Properties
        public string Username
        {
            get { return _username; }
            set {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get { return _password; }
            set { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }


        //Commands
        public ICommand LoginCommand { get;}
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }


        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand,CanExecuteLoginCommand);
            //RecoverPasswordCommand = new ViewModelCommand(p=>ExecuteRecoverPassCommand("",""));
        }

        //Methods
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;

            if(string.IsNullOrWhiteSpace(Username)||Username.Length<3||Password==null||Password.Length<3)
                validData = false;
            else 
                validData = true;

            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username,Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Usuario o contraseña invalido *";
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

    }
}
