using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;//<-- Agregamos referencia al ensamblado de entrada de windows

/*
    COMMENTS:

    -> Clase publica y que implementa la interfaz "ICommand".

    -> "ICommand":Permite definir comandos que pueden ser ejecutados por la interfaz de usuario, cuenta con el evento "CanExecuteChanged" y
        los metodos "CanExecute" y Execute

    -> Definimos un campo de tipo action para ejecutar los comandos, el delegado "Action" se utiliza para encapsular un método, es decir,
       podemos pasar un metodo como parametro ( "private readonly Action <object> _executeAction;"). 
        
    -> Definimos despues un campo de tipo "Predicate" para determinar si la accion se puede ejecutar o no. Este delegado predicado permite
       que el control se habilite o deshabilite en funcion de si se puede ejecutar su comando ("private readonly Predicate<object> _canExecuteAction;").

    -> Creamos una sobrecarga de constructores: Uno con los campos _executeAction y _canExecuteAction. Y otro solo con executeAction, ya que no
       todos los comandos tienen que ser validados para determinar si se debe ejecutar la accion.

    -> Events:
        "CanExecuteChanged": Suscribimos o damos de baja el evento ("public event EventHandler? CanExecuteChanged;") segun se requiera, para ello usamos 
        el administrador de comandos.
                                                add { CommandManager.RequerySuggested += value; }
                                                remove { CommandManager.RequerySuggested -= value; }
        El evento "RequerySuggested" se produce cuando el administrador de comandos detecta si la condicion de ejecucion del comando ha cambiado.

    -> Methods:
        "CanExecute": Si el campo _canExecuteAction es null retornavos verdadero ya que no se ha establecido el predicado de validacion, caso
        contrario retornamos el valor del delegado, es decir el metodo que se va a definir y delegar en el ViewModel.

        "Execute": Simplemente ejecutamos la accion, del mismo modo esto va a ejecutar el metodo que se va a delegar en el ViewModel
                                                          
*/

namespace MVVM.ViewModel
{
    public class ViewModelCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;


        //Constructors
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }


        //Event
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        //Methods
        public bool CanExecute(object? parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }
        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }
    }
}
