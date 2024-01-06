using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading; //<-- Agregamos referencia al ensamblado para la clase Thread"
using MVVM.Model; //<-- Agregamos referencia al componente "Model"
using MVVM.Repositories; //<-- Referencia a los "Repositories" del proyecto
using FontAwesome.Sharp;
using System.Windows.Input;
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
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;

        //Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

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

        public ViewModelBase CurrentChildView
        {
            get
            { 
                return _currentChildView; 
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            { 
                return _caption;
            }
            set
            { 
                _caption = value;
                OnPropertyChanged(nameof(Caption)); 
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //Constructor
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }


        //Methods
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Inicio";
            Icon = IconChar.Home;
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customer / Cambiar a User";
            Icon = IconChar.UserGroup;
        }

        private void LoadCurrentUserData()
        {
            //TODO: arreglar aqui
            if (Thread.CurrentPrincipal?.Identity?.Name != null) //<-- Condicion para evitar advertencias de posible asignacion a valor NULL
            {
                var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Usuario inválido, sin iniciar sesión";
                //Hide childs view
                //Aqui tengo que destruir todas las ventanas hijas por motivos de seguridad.
            }

        }
        
        
    }
}
