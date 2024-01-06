using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVVM.View; //<-- Agregamos referencia al componente "View".

/*
    COMMENTS:

    -> Definimos el método "ApplicationStart", para abrir y cerrar el inicio de sesion y mostrar la ventana principal:
       Instanciamos la View de inicio de sesion y mostramos la View, suscribimos el evento IsVisibleChanged que si la 
       View de inicio de sesion no esta visible y está cargado creamos la instancia de la View principal, lo mostramos 
       y finalmente cerramos la vista de Inicio de sesion.

       El evento y la propiedad "IsVisible" dependen de la propiedad "IsViewVisible" de la "LoginView", es decir, del 
       componente "View" que dicha propiedad se establece en falso si el inicio de sesion del usuario es valido, esa
       propiedad tambien está enlazada a la propiedad "Visibility" de la ventana, lo que hará que se genere el evento
       "IsVisibleChanged" cuando la propiedad del "ViewModel" cambie.

    -> Falta implementar el metodo para cerrar sesion

                                                          
*/

namespace MVVM
{
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) => {  
                if(loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    loginView.Close();
                }
            };

            //Esto es para cuando necesite iniciar sin Iniciar Sesion previamente
            /*
            var borrarEsto = new MainWindow();
            borrarEsto.Show();*/
        }
    }
}
