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

namespace EP2ATIS.vistas.paciente
{
    /// <summary>
    /// Lógica de interacción para Paciente.xaml
    /// </summary>
    public partial class Paciente : Window
    {
        private Inicio pgInicio;
        public Paciente(int idPaciente)
        {
            InitializeComponent();
            lblPaciente.Content = idPaciente.ToString();
            pgInicio = new Inicio();
            frmContenido.Content = pgInicio;
        }



        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            UsuarioCache.Id = 0;
            UsuarioCache.Usuario = "";
            UsuarioCache.IdRol = 0;

            Close();
        }

        private void doCita_Click(object sender, RoutedEventArgs e)
        {
            Recepcion pgPaciente = new Recepcion(int.Parse(lblPaciente.Content.ToString()));
            frmContenido.Navigate(pgPaciente);
        }

        private void cancelCita_Click(object sender, RoutedEventArgs e)
        {
            CancelarCita pgCita = new CancelarCita(int.Parse(lblPaciente.Content.ToString()));
            frmContenido.Navigate(pgCita);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmContenido.Content = pgInicio;
        }
    }
}
