using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppCredenciales.Data;


namespace AppCredenciales
{
    public partial class Form2 : Form
    {
        private List<EmployeeData> credentialsList;
        public Form2(List<EmployeeData> templist)
        {
            InitializeComponent();
            this.credentialsList = templist;
            foreach (var item in credentialsList)
            {
                listBoxCtemp.Items.Add($"Nombre: {item.Name}          CURP: {item.CURP}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCan_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void listBoxCtemp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        // Método para actualizar la vista del ListBox
        private void UpdateListBox()
        {
            listBoxCtemp.Items.Clear();
            foreach (var item in credentialsList)
            {
                listBoxCtemp.Items.Add(item.Name); // Ajustar según las propiedades del objeto
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay un elemento seleccionado
                if (listBoxCtemp.SelectedItem != null)
                {
                    // Validar que no sea el último elemento
                    if (credentialsList.Count == 1)
                    {
                        MessageBox.Show("No se puede eliminar el último usuario de la lista.",
                                        "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Obtener el índice del elemento seleccionado
                    int selectedIndex = listBoxCtemp.SelectedIndex;

                    // Confirmar la eliminación del usuario
                    var result = MessageBox.Show($"¿Estás seguro de que deseas eliminar el usuario seleccionado?",
                        "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Eliminar el elemento de la lista credentialsList
                        credentialsList.RemoveAt(selectedIndex);

                        // Actualizar el ListBox
                        UpdateListBox();

                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un usuario para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al eliminar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
