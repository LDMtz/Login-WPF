using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //<-- Agregamos referencia al ensamblado del modelo componentes

/*
    COMMENTS:

    -> Clase abstracta para que solo se puedda usar mediante la herencia. Y que implementa la interfaz "INotifyPropertyChanged".

    -> "INotifyPropertyChanged":Esta interfaz tiene un unico evento que notifica a los clientes de enlace que un valor ha cambiado 
       y que debe volver a evaluar sus valores ("public event PropertyChangedEventHandler? PropertyChanged;").

    -> Definimos un metodo para generar el evento cuando una propiedad haya cambiado ("OnPropertyChanged")
   
*/

namespace MVVM.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        //Event
        public event PropertyChangedEventHandler? PropertyChanged;


        //Method
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
