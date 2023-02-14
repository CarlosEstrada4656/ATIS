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
    /// Lógica de interacción para GestionCitas.xaml
    /// </summary>
    public partial class GestionCitas : Page
    {
        private DateTime? fecha = null;
        private int? idDoctor = null;

        public GestionCitas()
        {
            InitializeComponent();
        }

        private void Recargar()
        {
            CitaDAOImpl citasDAOImpl = new CitaDAOImpl();
            DoctorDAOImpl doctorDAOImpl = new DoctorDAOImpl();
            PacienteDAOImpl pacienteDAOImpl = new PacienteDAOImpl();
            HorarioDAOImpl horarioDAOImpl = new HorarioDAOImpl();

            List<Cita> citas = citasDAOImpl.listar();
            List<dynamic> dynamicList = new List<dynamic>();

            string nombrePaciente, nombreDoctor, hora;

            if (citas != null)
            {
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
            }

            dgCitas.ItemsSource = dynamicList;
        }

        private void cargarPacientes()
        {
            PacienteDAOImpl pacienteDAOImpl = new PacienteDAOImpl();

            cmbPaciente.ItemsSource = pacienteDAOImpl.listar();
            cmbPaciente.DisplayMemberPath = "nombre";
            cmbPaciente.SelectedValuePath = "id_Paciente";
        }

        private void cargarDoctores()
        {
            DoctorDAOImpl doctorDAOImpl = new DoctorDAOImpl();

            cmbDoctor.ItemsSource = doctorDAOImpl.listar();
            cmbDoctor.DisplayMemberPath = "Nombres";
            cmbDoctor.SelectedValuePath = "IdDoctor";
        }

        private void cargarHorarios()
        {
            HorarioDAOImpl horarioDAOImpl = new HorarioDAOImpl();

            if (fecha != null && idDoctor != null)
            {
                cmbHorario.ItemsSource = null;
                cmbHorario.ItemsSource = horarioDAOImpl.listarDisponible(fecha, idDoctor);
                cmbHorario.DisplayMemberPath = "hora";
                cmbHorario.SelectedValuePath = "id_Horario";
            }           
        }

        private void limpiar()
        {
            txtId.Text = "";
            dpFecha.Text = "";
            cmbEstado.SelectedIndex = -1;
            cmbConfirmada.SelectedIndex = -1;
            cmbHorario.SelectedIndex = -1;
            cmbDoctor.SelectedIndex = -1;
            cmbPaciente.SelectedIndex = -1;
            fecha = null;
            idDoctor = null;
        }

        private bool validar()
        {
            string mensaje = "Todos los campos son requeridos. Revise los campos: \n\n";

            if (dpFecha.Text != "" && cmbEstado.Text != "" && cmbConfirmada.Text != "" && 
                (cmbHorario.Items.Count > 0 || cmbHorario.Text != "") && 
                cmbDoctor.Text != "" && cmbPaciente.Text != "")
            {
                return true;
            }
            else
            {
                mensaje += dpFecha.Text == "" ? "Fecha\n" : "";
                mensaje += cmbEstado.Text == "" ? "Estado\n" : "";
                mensaje += cmbConfirmada.Text == "" ? "Confirmada\n" : "";
                mensaje += cmbHorario.Items.Count < 1 ? "Sin horarios disponibles (seleccione una fecha)\n" : "";
                mensaje += cmbDoctor.Text == "" ? "Doctor\n" : "";
                mensaje += cmbPaciente.Text == "" ? "Paciente\n" : "";

                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Recargar();
            cargarDoctores();
            cargarPacientes();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            CitaDAOImpl citaDAOImpl = new CitaDAOImpl();
            HorarioDAOImpl horarioDAOImpl = new HorarioDAOImpl();

            DateTime fecha;
            int estado, confirmada, idHorario, idDoctor;

            if (validar())
            {
                fecha = DateTime.Parse(dpFecha.Text);
                estado = cmbEstado.Text == "Cancelada" ? 1 : 0;
                confirmada = cmbConfirmada.Text == "Confirmada" ? 1 : 0;

                if (cmbHorario.SelectedValue != null)
                    idHorario = int.Parse(cmbHorario.SelectedValue.ToString());

                else
                    idHorario = horarioDAOImpl.buscarIdPorHora(cmbHorario.Text);

                idDoctor = int.Parse(cmbDoctor.SelectedValue.ToString());


                Cita cita = new Cita()
                {
                    Fecha = fecha,
                    Estado = estado,
                    Confirmada = confirmada,
                    IdHorario = idHorario,
                    IdDoctor = idDoctor
                };

                if (cmbPaciente.SelectedValue != null)
                    cita.IdPaciente = int.Parse(cmbPaciente.SelectedValue.ToString());

                if (txtId.Text == "")
                {
                    if (citaDAOImpl.insetar(cita))
                        MessageBox.Show("Registro agregado exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                    else
                        MessageBox.Show("Ocurrio un error al agregar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    cita.IdCita = int.Parse(txtId.Text);

                    if (citaDAOImpl.editar(cita))
                        MessageBox.Show("Registro editado exitosamente", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);

                    else
                        MessageBox.Show("Ocurrio un error al editar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                limpiar();
                Recargar();
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            CitaDAOImpl citaDAOImpl = new CitaDAOImpl();

            dynamic d = dgCitas.SelectedItem;

            if (d != null)
            {
                txtId.Text = d.IdCita + string.Empty;
                dpFecha.Text = d.Fecha + string.Empty;
                cmbEstado.Text = d.Estado == 1 ? "Cancelada" : "No cancelada";
                cmbConfirmada.Text = d.Confirmada == 1 ? "Confirmada" : "No confirmada";
                cmbHorario.Text = d.Hora;
                cmbDoctor.Text = d.Doctor + string.Empty;
                cmbPaciente.Text = d.Paciente + string.Empty;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro primero para poder editarlo!!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            CitaDAOImpl citaDAOImpl = new CitaDAOImpl();

            dynamic data = dgCitas.SelectedItem;

            bool result;

            if (data != null)
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el registro?", "Advertencia",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    result = citaDAOImpl.eliminar(data.IdCita);

                    if (result)
                        MessageBox.Show("Registro eliminado exitosamente", "Mensaje",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    else
                        MessageBox.Show("Ocurrio un problema al eliminar!!", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Recargar();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro primero para poder eliminarlo!!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void dpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFecha.Text != "")
            {
                fecha = DateTime.Parse(dpFecha.Text);
                cargarHorarios();
            }           
        }

        private void cmbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDoctor.SelectedValue != null)
            {
                idDoctor = int.Parse(cmbDoctor.SelectedValue.ToString());
                cargarHorarios();
            }
        }

        private void cmbEstado_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbEstado.Text != "")
            {
                cmbConfirmada.Text = "No confirmada";
            }
        }
    }
}
