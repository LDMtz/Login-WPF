﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Runtime.InteropServices; //<Agregramos referencia a los servicios integrables en tiempo de ejecucion de WINDOWS
using System.Windows.Interop;

/*
    COMMENTS:

    -> Clase publica .cs para la ventana principal de la app .

    -> En el .xaml se agrega la referencia al componente ViewModel ("xmlns:ViewModel="clr-namespace:MVVM.ViewModel"") y se
       establece el ModelView como contexto de datos de la View:
                                <Window.DataContext>
                                    <ViewModel:MainViewModel/>
                                </Window.DataContext>
    ->
*/

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

    }
}