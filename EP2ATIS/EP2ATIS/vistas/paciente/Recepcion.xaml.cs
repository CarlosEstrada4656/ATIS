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
using EP2ATIS.model;
using EP2ATIS.service.dao;

namespace EP2ATIS.vistas.paciente
{
    /// <summary>
    /// Lógica de interacción para Recepcion.xaml
    /// </summary>
    public partial class Recepcion : Page
    {
        public Recepcion(int idPaciente)
        {
            InitializeComponent();
            
            cargarDoctores();
            lblPaciente.Content = idPaciente.ToString();
        }

        void cargarHorarios()
        {
            DAOcita daoCita = new DAOcita();
            
            if (this.dpFecha.SelectedDate != null && this.cmbDoctor.SelectedItem != null)
            {
                cmbHorario.ItemsSource = null;
                cmbHorario.ItemsSource = daoCita.listarDisponible(dpFecha.SelectedDate.Value.Date, int.Parse(cmbDoctor.SelectedValue.ToString()));
                cmbHorario.DisplayMemberPath = "hora";
                cmbHorario.SelectedValuePath = "id_Horario";
            }
            //cmbHorario.ItemsSource = DAOcita.MuestraHorarios();
            //cmbHorario.DisplayMemberPath = "hora";
            //cmbHorario.SelectedValuePath = "id_Horario";

        }

        void cargarDoctores()
        {
            cmbDoctor.ItemsSource = DAOdoctor.MuestraDoctores();
            cmbDoctor.DisplayMemberPath = "Nombres";
            cmbDoctor.SelectedValuePath = "IdDoctor";

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                //int val = DAOcita.validarCita(int.Parse(cmbHorario.SelectedValue.ToString()), int.Parse(cmbDoctor.SelectedValue.ToString()), dpFecha.SelectedDate.Value.Date);
                //if (val > 0)
                //{
                    //MessageBox.Show("Ya existe una cita programada para la fecha y hora seleccionada.");
                //}
                //else
               // {
                    if (int.Parse(txtCupo.Text) < 10)
                    {
                        int res = 0;
                        Cita ci = new Cita();

                        ci.Fecha = dpFecha.SelectedDate.Value.Date;
                        ci.Estado = 0;
                        ci.Confirmada = 0;
                        ci.IdHorario = int.Parse(cmbHorario.SelectedValue.ToString());
                        ci.IdDoctor = int.Parse(cmbDoctor.SelectedValue.ToString());
                        ci.IdPaciente = int.Parse(lblPaciente.Content.ToString());

                        res = DAOcita.InsertarCita(ci);
                        MessageBox.Show("Cita realizada con éxito","Éxito");
                        limpiar();

                    }
                    else
                    {
                        MessageBox.Show("No se puede registrar la cita por falta de cupo.");
                    }
                //}

            }

            catch(Exception ex)
            {
                MessageBox.Show("Asegúrese de rellenar todos los campos.");
            }
            

        }

        private void dpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime fechaP = new DateTime();

            fechaP = DateTime.Parse(dpFecha.Text);

            txtCupo.Text = DAOcita.MuestraCupos(fechaP).ToString();
            cargarHorarios();
        }

        private void cmbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cargarHorarios();
        }

        private void limpiar()
        {
            
            cmbHorario.SelectedIndex = -1;
            cmbDoctor.SelectedIndex = -1;
            txtCupo.Clear();

            try
            {
                dpFecha.Text = "";
            }
            catch
            {
            }          
            
        }
    }
}
