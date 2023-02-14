using EP2ATIS.model;
using EP2ATIS.service.daoimp;
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

namespace EP2ATIS.vistas.secretaria
{
    /// <summary>
    /// Lógica de interacción para ConfirmacionCitas.xaml
    /// </summary>
    public partial class ConfirmacionCitas : Page
    {
        public ConfirmacionCitas()
        {
            InitializeComponent();
        }

        private void Recargar()
        {
            CitaDAOImpl citasDAOImpl = new CitaDAOImpl();
            DoctorDAOImpl doctorDAOImpl = new DoctorDAOImpl();
            PacienteDAOImpl pacienteDAOImpl = new PacienteDAOImpl();
            HorarioDAOImpl horarioDAOImpl = new HorarioDAOImpl();

            List<Cita> citas = citasDAOImpl.listarNoConfirmadas();
            List<dynamic> dynamicList = new List<dynamic>();

            string nombrePaciente, nombreDoctor, hora;

            if (citas != null)
                foreach (Cita c in citas)
                {
                    nombrePaciente = pacienteDAOImpl.listarNombrePorId(c.IdPaciente);
                    nombreDoctor = doctorDAOImpl.listarNombrePorId(c.IdDoctor);
                    hora = horarioDAOImpl.listarHoraPorId(c.IdHorario);

                    dynamicList.Add(new
                    {
                        IdCita = c.IdCita,
                        Fecha = c.Fecha.ToString("dd/MM/yyyy"),
                        Estado = c.Estado,
                        Confirmada = c.Confirmada,
                        Hora = hora,
                        Doctor = nombreDoctor,
                        Paciente = nombrePaciente
                    });
                }

            dgCitasConfirmar.ItemsSource = dynamicList;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Recargar();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            CitaDAOImpl citaDAOImpl = new CitaDAOImpl();

            dynamic d = dgCitasConfirmar.SelectedItem;

            if (d != null)
            {
                if (MessageBox.Show("¿Esta segura de confirma la cita?", "Advertencia",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (citaDAOImpl.confirmar(d.IdCita))
                    {
                        MessageBox.Show("Cita confirmada correctamente", "Informacion",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        Recargar();
                    }                       
                    else
                        MessageBox.Show("Ocurrio un problema al confirmar la cita", "Error", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }                    
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
            }               
        }
    }
}
