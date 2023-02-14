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
using System.Windows.Shapes;

namespace EP2ATIS.vistas
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            string nombres, apellidos, genero, direccion, telefono, 
                dui, tipoSangre, correo, clave, confirmacion;

            DateTime nacimiento;

            nombres = txtNombres.Text;
            apellidos = txtApellidos.Text;

            if (dpNacimiento.Text != "")
                nacimiento = DateTime.Parse(dpNacimiento.Text);

            else
                nacimiento = DateTime.Now;

            genero = cmbGenero.Text;
            direccion = txtDireccion.Text;
            telefono = txtTelefono.Text;
            dui = txtDUI.Text;
            tipoSangre = cmbTipoSangre.Text;
            correo = txtCorreo.Text;
            clave = txtClave.Password;
            confirmacion = txtConfirmar.Password;

            if (validar())
            {
                if (clave == confirmacion)
                {
                    UsuarioDAOImpl usuarioDAOImpl = new UsuarioDAOImpl();
                    RolDAOImpl rolDAOImpl = new RolDAOImpl();
                    PacienteDAOImpl pacienteDAOImp = new PacienteDAOImpl();

                    Usuario usuario = new Usuario()
                    {
                        Correo = correo,
                        Clave = clave,
                        IdRol = rolDAOImpl.buscarIdPorNombre("Paciente")
                    };

                    int idUsuario = usuarioDAOImpl.insertar(usuario);

                    if (idUsuario > 0)
                    {
                        Paciente paciente = new Paciente()
                        {
                            nombre = nombres,
                            apellido = apellidos,
                            genero = genero,
                            fecha_Nacimiento = nacimiento,
                            direccion = direccion,
                            telefono = telefono,
                            dui = dui,
                            tipo_Sangre = tipoSangre,
                            id_Usuario = idUsuario
                        };

                        if (pacienteDAOImp.insertar(paciente))
                        {
                            MessageBox.Show("Ya puedes iniciar sesión con tu cuenta.", "Información", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un problema al registrarse. Intentelo nuevamente mas tarde.", 
                                "Error", MessageBoxButton.OK, MessageBoxImage.Information);

                            usuarioDAOImpl.eliminar(idUsuario);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al registrarse.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                txtNombres.Clear();
                txtApellidos.Clear();
                cmbGenero.SelectedIndex = -1;
                dpNacimiento.Text = "";
                txtDireccion.Text = "";
                txtTelefono.Text = "";
                txtDUI.Text = "";
                cmbTipoSangre.SelectedIndex = -1;
                txtCorreo.Clear();
                txtClave.Clear();
                txtConfirmar.Clear();
            }
        }

        private bool validar()
        {
            string mensaje = "Todos los campos son requeridos. Revise los campos: \n\n";

            if (txtNombres.Text != "" && txtApellidos.Text != "" && cmbGenero.Text != "" && 
                dpNacimiento.Text != "" && txtDireccion.Text != "" && txtTelefono.Text != "" && 
                txtDUI.Text != "" && cmbTipoSangre.Text != "" && txtCorreo.Text != "" && 
                txtClave.Password != "" && txtConfirmar.Password != "")
            {
                return true;
            }
            else
            {
                mensaje += txtNombres.Text == "" ? "Nombres\n" : "";
                mensaje += txtApellidos.Text == "" ? "Apellidos\n" : "";
                mensaje += cmbGenero.Text == "" ? "Genero\n" : "";
                mensaje += dpNacimiento.Text == "" ? "Nacimiento\n" : "";
                mensaje += txtDireccion.Text == "" ? "Direccion\n" : "";
                mensaje += txtTelefono.Text == "" ? "Telefono\n" : "";
                mensaje += txtDUI.Text == "" ? "DUI\n" : "";
                mensaje += cmbTipoSangre.Text == "" ? "Sangre\n" : "";
                mensaje += txtCorreo.Text == "" ? "Correo\n" : "";
                mensaje += txtClave.Password == "" ? "Clave\n" : "";
                mensaje += txtConfirmar.Password == "" ? "Confimar Clave\n" : "";

                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }
    }
}
