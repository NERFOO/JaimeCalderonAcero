using JaimeCalderonAcero.Models;
using JaimeCalderonAcero.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JaimeCalderonAcero
{
    public partial class FormPractica : Form
    {

        RepositoryPracticaado repo;

        public FormPractica()
        {
            InitializeComponent();
            this.repo = new RepositoryPracticaado();
            this.PintarClientes();
        }

        public void PintarClientes()
        {
            foreach (string cli in repo.GetClientes())
            {
                this.cmbclientes.Items.Add(cli);
            }
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombre = this.cmbclientes.SelectedItem.ToString();

            foreach (Cliente emp in repo.CargarClientes(nombre))
            {
                this.txtempresa.Text = emp.Empresa.ToString();
                this.txtcontacto.Text = emp.Contacto.ToString();
                this.txtcargo.Text = emp.Cargo.ToString();
                this.txtciudad.Text = emp.Ciudad.ToString();
                this.txttelefono.Text = emp.Telefono.ToString();
            }
            this.PintarPedidos();
        }

        public void PintarPedidos()
        {
            string nombre = this.cmbclientes.SelectedItem.ToString();

            foreach (string pedi in repo.GetPedidos(nombre))
            {
                this.lstpedidos.Items.Add(pedi);
            }
        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nomPedido = this.lstpedidos.SelectedItem.ToString();

            foreach (Pedido ped in repo.CargarDatosPedidos(nomPedido))
            {
                this.txtcodigopedido.Text = ped.CodPedido.ToString();
                this.txtfechaentrega.Text = ped.FechEntrega.ToString();
                this.txtformaenvio.Text = ped.FormaEnvio.ToString();
                this.txtimporte.Text = ped.Importe.ToString();
            }
        }

        private void btneliminarpedido_Click(object sender, EventArgs e)
        {
            if (this.lstpedidos.SelectedItems.Count != 0)
            {
                this.repo.DeletePedido(this.lstpedidos.SelectedItem.ToString());

                MessageBox.Show("Se ha eliminado un registros");
            }
        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {

        }
    }
}
