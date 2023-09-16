using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/*
    COMMENTS:

    -> Definimos una propiedad de dependencia publica, estatica y de solo lectura llamada "PasswordProperty" para posteriormente
       registrarla una vez creada la propiedad "Password"
    
    -> Creamos una propiedad SecureString llamada Password "Password" para acceder a la propiedad "PasswordProperty" con su get
       y su set. Ahora si podemos registrar la propiedad "PasswordProperty"

    -> Se crea el metodo "OnPasswordChanged" para cuando el valor de la contraseña cambie
 
    -> Se establece el valor del cuadro de contraseña estandar en la nueva propiedad de contraseña suscribiendo el evento "OnPasswordChanged"
       ya que se genera cada vez que el valor del control cambie.
*/
namespace MVVM.CustomControls
{
    
    public partial class BindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password",typeof(SecureString),typeof(BindablePasswordBox));

        public SecureString Password
        {
            get {return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
            txbContraseña.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = txbContraseña.SecurePassword;
        }
    }
}
