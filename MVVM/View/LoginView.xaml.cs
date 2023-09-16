using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

/*
    COMMENTS:

    -> Clase publica y que hereda de windows y representa la ventana de inicio de sesion.

    -> En el .xaml se agrega la referencia al componente ViewModel ("xmlns:ViewModel="clr-namespace:MVVM.ViewModel"") y se
       establece el ModelView como contexto de datos de la View:
                                <Window.DataContext>
                                    <ViewModel:LoginViewModel/>
                                </Window.DataContext>

    -> Enlaces entre las propiedades del View ("LoginView") y el Model View ("LoginViewModel") en el .xaml:
       *La propiedad visibilidad de la ventana estará vinculada a la propiedad "IsViewVisible" ya que si el inicio de sesión es
       exitoso ocultaremos la vista:
                                <Window.Visibility>
                                    <Binding Path="IsViewVisible" Mode="TwoWay" Converter="{StaticResource BooleanToVisibility}"/>
                                </Window.Visibility>
        El mode se debe actualizar en ambos sentidos, tambien se necesita un convertidor ya que la propiedad "IsViewVisible" es del
        tipo bool y la propiedad de la visibilidad de la ventana es de tipo numeración,entonces en los recursos de la ventana instanciamos
        la clase "BooleanToVisibilityConverter" y se hace la convercion en la Visibility de la ventana.

        *Se establece el enlace de la propiedad del TexBox de nombre de usuario ("txtUsuario") de la View y la propiedad "Username" de la 
        LoginViewModel ("Text="{Binding Username, UpdateSourceTrigger=PropertyChanged}"") y se actualiza el origen del enlace cada vez que cambia.
        
        *Se establece el enlace de la propiedad del TexBox de la contraseña de la View y la propiedad "Password" de la LoginViewModel, 
        para esto creamos previamente un control personalizado ("BindablePasswordBox") y fue guardado en la carpeta "CustomControls", 
        ya que el control "PasswordBox" no permite establecer bindings entre propiedades. Y tambien se acutaliza el origen del enlace.
                   (BindablePasswordBox Password="{Binding Password, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}").

        *Se establece el enlace entre el "btnLogin" y la propiedad "LoginCommand" de la LoginViewModel ("Command="{Binding LoginCommand}"").
        
        *Se establece el enlace entre "txtError" y la propiedad "ErrorMessage" de la LoginViewModel ("Text="{Binding ErrorMessage}"").
*/
namespace MVVM.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private MediaPlayer mediaPlayer;
        public LoginView()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("C:\\Users\\PC\\Desktop\\Codigo\\WPF_INTERFACES_GRAFICAS\\MVVM\\Sounds\\kickfx.wav"));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) //Para arrastrar la ventana
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; //Minimizar aplicacion
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //Cerrar aplicacion
        }

        private void btnLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            mediaPlayer.Volume = 0.80;
            mediaPlayer.Stop();  // Detener la reproducción actual
            mediaPlayer.Position = TimeSpan.Zero;  // Reiniciar la posición al inicio
            mediaPlayer.Play();

        }
    }
}
