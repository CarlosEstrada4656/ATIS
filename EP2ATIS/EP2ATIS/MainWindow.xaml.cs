using EP2ATIS.service.daoimp;
using EP2ATIS.utilities;
using EP2ATIS.vistas;
using EP2ATIS.vistas.paciente;
using EP2ATIS.vistas.secretaria;
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

namespace EP2ATIS
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Logout(object sender, EventArgs e)
        {
            txtCorreo.Clear();
            txtClave.Clear();

            Show();
        }

        private bool Validar()
        {
            string mensaje = "Todos los campos son requeridos. Revise los campos: \n\n";

            if (txtCorreo.Text != "" && txtClave.Password != "")
            {
                return true;
            }
            else
            {
                mensaje += txtCorreo.Text == "" ? "Correo\n" : "";
                mensaje += txtClave.Password == "" ? "Clave\n" : "";

                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            string correo, clave;

            UsuarioDAOImpl usuarioDAOImpl = new UsuarioDAOImpl();

            if (Validar())
            {
                correo = txtCorreo.Text;
                clave = txtClave.Password;

                result = usuarioDAOImpl.login(correo, clave);

                RolDAOImpl rolDAOImpl = new RolDAOImpl();
                string nombreRol = rolDAOImpl.buscarNombreporID(UsuarioCache.IdRol);

                if (result)
                {
                    Hide();

                    switch (nombreRol)
                    {
                        case "Secretaria":
                            
                            Secretaria frmSecretaria = new Secretaria();
                            frmSecretaria.Closed += Logout;
                            frmSecretaria.Show();

                            break;

                        case "Paciente":

                            int idPaciente = usuarioDAOImpl.idPaciente(correo, clave);
                            Paciente frmPaciente = new Paciente(idPaciente);
                            frmPaciente.Closed += Logout;
                            frmPaciente.Show();

                            break;

                        case "Doctor":

                            MessageBox.Show("En Desarrollo", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                            Show();

                            break;

                    }
                }
                else
                {
                    MessageBox.Show("¡Credenciales inválidas!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
              
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            Registro registro = new Registro();
            registro.Closed += Registro_Closed;
            registro.Show();
        }

        private void Registro_Closed(object sender, EventArgs e)
        {
            Show();
        }
    }
}
