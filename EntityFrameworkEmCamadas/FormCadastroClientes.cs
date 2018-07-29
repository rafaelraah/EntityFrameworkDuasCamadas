using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkEmCamadas
{
    public partial class FormCadastroClientes : Form
    {
        Cliente cliente;
        ClienteDAL clienteDal;

        public FormCadastroClientes()
        {
            InitializeComponent();
        }

        private void FormCadastroClientes_Load(object sender, EventArgs e)
        {
            btnSalvar.Visible = false;
            Ajudante.CamposHabilitados(this, false);

            cliente = new Cliente();
            clienteDal = new ClienteDAL();

            preencherDataGridView();

            int codigo = Convert.ToInt32(dataGridViewClientes.Rows[0].Cells[0].Value);
            if(codigo > 0)
            {
                Cliente cli = clienteDal.ObterRegistroPorCodigo(codigo);
                preencherControles(cli);
            }
        }

        void preencherDataGridView()
        {
            List<Cliente> listaClientes = clienteDal.ObterTodos();
            dataGridViewClientes.DataSource = listaClientes;
        }

        private void preencherControles(Cliente cli)
        {
            txtCodigo.Text = cli.id.ToString();
            txtEmail1.Text = cli.email1;
            txtEmail2.Text = cli.email2;
            txtEndereco.Text = cli.endereco;
            txtNome.Text = cli.nome;
            txtObs.Text = cli.obs;
        }

        private void dataGridViewClientes_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells[0].Value);
            try { 
                int codigoSelecionado = Convert.ToInt32(dataGridViewClientes.Rows[e.RowIndex].Cells[0].Value); //Convert.ToInt32(dataGridViewClientes.SelectedRows[0].Cells[0].Value);
                cliente = clienteDal.ObterRegistroPorCodigo(codigoSelecionado);
                preencherControles(cliente);
                Ajudante.CamposHabilitados(this, true);
                btnSalvar.Visible = true;
            }
            catch(Exception)
            {

            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            clienteDal = new ClienteDAL();
            cliente = new Cliente();

            //Validação
            if (true) {
                PreencherEntidade(cliente);
                int retorno = clienteDal.SalvarRegistro(cliente);
                if (retorno > 0)
                {
                    MessageBox.Show("Cliente adicionado com sucesso");
                }
                else
                {
                    MessageBox.Show("Erro ao adicionar o cliente");
                }
            }
            else
            {
                MessageBox.Show("Dados inválidos");
            }
            preencherDataGridView();
            btnSalvar.Visible = false;
            Ajudante.CamposHabilitados(this, false);
            Ajudante.limparControlesEFocar(this);
        }

        public void PreencherEntidade(Cliente clienteEntrada)
        {
            clienteEntrada.nome = txtNome.Text;
            clienteEntrada.email1 = txtEmail1.Text;
            clienteEntrada.email2 = txtEmail1.Text;
            clienteEntrada.endereco = txtEndereco.Text;
            clienteEntrada.obs = txtObs.Text;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            Ajudante.CamposHabilitados(this, true);
            Ajudante.limparControlesEFocar(this, txtNome);
            btnSalvar.Visible = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Gostaria de excluir o cliente?", "Confirmação", MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    cliente = new Cliente();
                    clienteDal = new ClienteDAL();
                    cliente.id = Convert.ToInt32(txtCodigo.Text);
                    if (clienteDal.DeletarRegistro(cliente))
                    {
                        MessageBox.Show("Cliente excluído com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente foi excluído!");
                    }
                    break;
                case DialogResult.No:
                    break;
            }
            preencherDataGridView();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            cliente = new Cliente();
            clienteDal = new ClienteDAL();

            PreencherEntidade(cliente);

            if (clienteDal.ObterRegistroPorCodigo(cliente.id) != null)
            {
                if (clienteDal.AtualizarRegistro(cliente))
                {
                    MessageBox.Show("Cliente atualizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Não foi possível atualizar o cliente!");
                }
            }
            preencherDataGridView();
            Ajudante.limparControlesEFocar(this);
            Ajudante.CamposHabilitados(this, false);
            btnSalvar.Visible = false;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Ajudante.limparControlesEFocar(this);
        }
    }
}
