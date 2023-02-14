using EP2ATIS.utilities;
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

namespace EP2ATIS.vistas.secretaria
{
    /// <summary>
    /// Lógica de interacción para Secretaria.xaml
    /// </summary>
    public partial class Secretaria : Window
    {
        private GestionCitas pgGestion;
        private Inicio pgInicio;
        private ConfirmacionCitas pgConfirmar;

        public Secretaria()
        {
            InitializeComponent();
            pgGestion = new GestionCitas();
            pgInicio = new Inicio();
            pgConfirmar = new ConfirmacionCitas();

            frmContenido.Content = pgInicio;
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            frmContenido.Content = pgInicio;
        }

        private void btnGestionar_Click(object sender, RoutedEventArgs e)
        {
            frmContenido.Content = pgGestion;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            frmContenido.Content = pgConfirmar;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            UsuarioCache.Id = 0;
            UsuarioCache.Usuario = "";
            UsuarioCache.IdRol = 0;

            Close();
        }
    }
}
