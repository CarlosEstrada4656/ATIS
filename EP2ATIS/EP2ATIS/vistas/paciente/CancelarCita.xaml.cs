using EP2ATIS.model;
using EP2ATIS.service.dao;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EP2ATIS.vistas.paciente
{
    /// <summary>
    /// Lógica de interacción para CancelarCita.xaml
    /// </summary>
    public partial class CancelarCita : Page
    {
        public CancelarCita(int idPaciente)
        {
            InitializeComponent();
            txtPaciente.Text = idPaciente.ToString();
            cargarCitas();
            cargarPaciente();
        }

        void cargarCitas()
        {
            dgCita.ItemsSource = DAOcita.MuestraCitas(int.Parse(txtPaciente.Text));

        }

        void cargarPaciente()
        {
            lblNombreP.Content += DAOpaciente.MuestraPacientesFiltro(int.Parse(txtPaciente.Text)).FirstOrDefault().nombre;
            lblNombreP.Content += " " + DAOpaciente.MuestraPacientesFiltro(int.Parse(txtPaciente.Text)).FirstOrDefault().apellido;
        }

        private void dgCita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CitaPaciente da = (CitaPaciente)dgCita.SelectedItem;
            if (da == null)
                return;
            this.txtCita.Text = da.IdCita.ToString();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtCita.Text);
                DAOcita.ModificarCita(id);
                MessageBox.Show("La cita ha sido cancelada con éxito.", "Cancelación");
                cargarCitas();
            }

            catch(Exception ex)
            {
                MessageBox.Show("No se ha seleccionado una cita a cancelar.");
            }
            
        }
    }
}
