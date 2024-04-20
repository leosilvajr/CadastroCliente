using CadastroClientes.Services;
using CadastroClientes.Util;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class frmMenuClientes : Form
    {
        public frmMenuClientes()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frmCadCliente frm = new frmCadCliente();

            //Abri o formulario de uma forma que voce nao consegue fazer nada no formulario de tras
            frm.ShowDialog();
        }

        private void FrmMenuClientes_Load(object sender, EventArgs e)
        {
            dgLista.DataSource = Funcoes.BuscaSQL("SELECT id, nome, cpf_cnpj, rg_ie, endereco, numero, bairro, cep, cidade, estado, celular, email, situacao FROM Clientes");
            dgLista.ClearSelection();//Limpar seleçoes
            ReorganizarGrid();

        }

        private void DgLista_Sorted(object sender, EventArgs e) //Evento quando é clicado em alguma coluna no grid
        {
            ReorganizarGrid();

        }



        private void ReorganizarGrid()
        {
            foreach (DataGridViewRow linha in dgLista.Rows)
            {
                // Pintando de vermelho os clientes com status cancelado
                if (linha.Cells["situacao"].Value.ToString() == "Cancelado")
                    linha.DefaultCellStyle.ForeColor = Color.Crimson;

                try
                {
                    string imagePath = $"{Constantes.CAMINHO_IMG_PERFIL}{linha.Cells["id"].Value}.png";
                    if (File.Exists(imagePath))
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            linha.Cells["foto"].Value = Image.FromStream(fs);
                        }
                    }
                    else
                    {
                        linha.Cells["foto"].Value = Properties.Resources.profile;
                    }
                }
                catch (Exception ex)
                {
                    // Lidar com a exceção aqui, por exemplo, registrar ou exibir uma mensagem de erro
                    Console.WriteLine($"Erro ao carregar imagem: {ex.Message}");
                    linha.Cells["foto"].Value = Properties.Resources.profile; // Pode definir uma imagem padrão
                }
            }

            dgLista.ClearSelection();
            btnEditar.Enabled = false;
        }

        private void DgLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmCadCliente frm = new frmCadCliente();
            frm.txtId.Text = dgLista.CurrentRow.Cells["id"].Value.ToString();   
            frm.ShowDialog();
            AtualizarDataGrid();
        }

        private void AtualizarDataGrid()
        {
            dgLista.DataSource = Funcoes.BuscaSQL("SELECT id, nome, cpf_cnpj, rg_ie, endereco, numero, bairro, cep, cidade, estado, celular, email, situacao FROM Clientes");
            dgLista.ClearSelection();//Limpar seleçoes
            ReorganizarGrid();
        }
    }
}
