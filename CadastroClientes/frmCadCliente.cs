using CadastroClientes.Model;
using CadastroClientes.Repository;
using CadastroClientes.Services;
using CadastroClientes.Util;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class frmCadCliente : Form
    {
        private readonly ClientesService _clientesService;
        public frmCadCliente()
        {
            InitializeComponent();
            var clientesRepository = new ClientesRepository();
            _clientesService = new ClientesService(clientesRepository);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Validacoes())
                return;

            ClientesModel cliente = new ClientesModel();
            cliente.Nome = txtNomeCliente.Text;
            cliente.CpfCnpj = mskCpfOrCnpj.Text;
            cliente.Genero = opMasculino.Checked ? "Masculino" : opOutros.Checked ? "Outros" : opFeminino.Checked ? "Feminino" : string.Empty;
            cliente.RgIe = txtRgIe.Text;
            cliente.InscricaoMunicipal = txtInscricaoMunicipal.Text;
            cliente.EstadoCivil = cbEstadoCivil.Text;
            cliente.Nascimento = mskNascimento.Text == "  /  /" ? (DateTime?)null : Convert.ToDateTime(mskNascimento.Text);
            cliente.Cep = mskCep.Text;
            cliente.Endereco = cbEndereco.Text;
            cliente.Numero = txtNumero.Text;
            cliente.Bairro = cbBairro.Text;
            cliente.Cidade = cbCidade.Text;
            cliente.Estado = cbEstado.Text;
            cliente.Celular = mskCelular.Text;
            cliente.Email = txtEmail.Text;
            cliente.Obs = txtObs.Text;
            cliente.Situacao = ckSituacao.Checked;

            if (txtId.Text == string.Empty)
            {
                _clientesService.AdicionaCliente(cliente, txtId);
                Mensagens.Info("Operação realizada com sucesso.");
            }
            else
            {
                cliente.Id = int.Parse(txtId.Text);
                _clientesService.AtualizaCliente(cliente);
                Mensagens.Info("Operação realizada com sucesso.");
            }
        }

        private void frmCadCliente_KeyDown(object sender, KeyEventArgs e)
        {
            //Comando para a  tecla enter funcionar como tab
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;

            }
        }

        private bool Validacoes()
        {

            //Validar se marcou CPF ou CNPJ
            if (!opCpf.Checked && !opCnpj.Checked)
            {
                MessageBox.Show("Selecione o tipo de documentação CPF ou CNPJ");
                return true;
            }

            //Validar Campo CPF ou CNPJ
            if (mskCpfOrCnpj.Text == "")
            {
                MessageBox.Show("O campo CPF/CNPJ é obrigatório.");
                mskCpfOrCnpj.Focus();
                return true;
            }

            //Validar Data 
            if (mskNascimento.Text != "  /  /")
            {
                try
                {
                    Convert.ToDateTime(mskNascimento.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Data de Nascimento inválida.");
                    return true;
                }
            }

            return false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (Mensagens.Pergunta("Deseja limpar todos os campos ? ") == DialogResult.No)
                return;

            txtId.Text = string.Empty;
            txtNomeCliente.Text = string.Empty;
            opCpf.Checked = false;
            opCnpj.Checked = false;
            opMasculino.Checked = false;
            opFeminino.Checked = false;
            opOutros.Checked = false;
            mskCpfOrCnpj.Text = string.Empty;
            txtRgIe.Text = string.Empty;
            txtInscricaoMunicipal.Text = string.Empty;
            cbEstadoCivil.Text = string.Empty;
            mskNascimento.Text = string.Empty;
            mskCep.Text = string.Empty;
            cbEndereco.Text = string.Empty;
            txtNumero.Text = string.Empty;
            cbBairro.Text = string.Empty;
            cbCidade.Text = string.Empty;
            cbEstado.Text = string.Empty;
            mskCelular.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtObs.Text = string.Empty;
            ckSituacao.Checked = true;

            btnSalvar.Text = "Salvar";
            imgCliente.Image = Properties.Resources.profile;
            txtNomeCliente.Focus();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void opCpf_CheckedChanged(object sender, EventArgs e)
        {
            if (opCpf.Checked)
            {
                mskCpfOrCnpj.Mask = "000,000,000-00";
                lblRgIe.Text = "RG";
                gpBoxGenero.Enabled = true;
                cbEstadoCivil.Enabled = true;
                mskNascimento.Enabled = true;
                lblCpfCnpj.Text = "CPF";
                txtInscricaoMunicipal.Enabled = false;
                lblInscricaoMunicipal.Enabled = false;
                lblNomeRazao.Text = "Nome Completo";

            }
            txtNomeCliente.Focus();
        }

        private void opCnpj_CheckedChanged(object sender, EventArgs e)
        {
            if (opCnpj.Checked)
            {
                mskCpfOrCnpj.Mask = "00,000,000/0000-00";
                lblRgIe.Text = "Inscrição Estadual";
                gpBoxGenero.Enabled = false;
                cbEstadoCivil.Enabled = false;
                mskNascimento.Enabled = false;
                lblNomeRazao.Text = "Razão Social";
                lblCpfCnpj.Text = "CNPJ";
                lblInscricaoMunicipal.Enabled = true;
                txtInscricaoMunicipal.Enabled = true;
            }
            txtNomeCliente.Focus();
        }

        private void opMasculino_CheckedChanged(object sender, EventArgs e)
        {
            cbEstadoCivil.Focus();
        }

        private void opFeminino_CheckedChanged(object sender, EventArgs e)
        {
            cbEstadoCivil.Focus();
        }

        private void opOutros_CheckedChanged(object sender, EventArgs e)
        {
            cbEstadoCivil.Focus();
        }

        private void mskNascimento_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mskNascimento.Text == "  /  /")
                return;

            try
            {
                Convert.ToDateTime(mskNascimento.Text).ToString();
            }
            catch (Exception)
            {
                Mensagens.Aviso("Data Inválida");
                e.Cancel = true;
            }
        }

        private void cbEstadoCivil_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbEstadoCivil.Text == "")
                return;
            if (cbEstadoCivil.SelectedIndex == -1)
            {
                Mensagens.Info("Selecione um Estado Civil.");
                cbEstadoCivil.Focus();
            }
        }

        private void cbEstado_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cbEstado.Text == "")
                return;
            if (cbEstado.SelectedIndex == -1)
            {
                Mensagens.Info("Selecione um Estado da lista.");
                cbEstado.Focus();
            }
        }

        private void frmCadCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void mskCep_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mskCep.Text.Replace(" ", "").Length == 0)
                return;
            if (mskCep.Text.Replace(" ", "").Length < 8)
            {
                Mensagens.Aviso("CEP Inválido.");
                e.Cancel = true;
            }

        }

        private void mskCpfOrCnpj_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mskCpfOrCnpj.Text.Length == 0)
                return;
            if (opCpf.Checked == true && mskCpfOrCnpj.Text.Replace(" ", "").Length < 11)
            {
                Mensagens.Aviso("CPF Inválido.");
                e.Cancel = true;
            }

            if (opCnpj.Checked == true && mskCpfOrCnpj.Text.Replace(" ", "").Length < 14)
            {
                Mensagens.Aviso("CNPJ Inválido.");
                e.Cancel = true;
            }
        }

        private void txtRgIe_TextChanged(object sender, EventArgs e)
        {
            if (txtRgIe.Text.Length > 14)
            {
                MessageBox.Show("O campo deve ter no máximo 14 dígitos.");
                txtRgIe.Focus();
            }
        }

        private void btnAddFoto_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                Mensagens.Aviso("Salve os dados do cliente primeiro.");
                return;
            }

            OpenFileDialog caixa = new OpenFileDialog();
            caixa.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (caixa.ShowDialog() == DialogResult.OK)
            {
                using (var image = Image.FromFile(caixa.FileName))
                {
                    imgCliente.Image = (Image)image.Clone();
                    File.Copy(caixa.FileName, $@"{Constantes.CAMINHO_IMG_PERFIL}{txtId.Text}.png");
                }
            }

            Mensagens.Info("Foto salva com sucesso.");

        }

        private void btnRemoveFoto_Click(object sender, EventArgs e)
        {
            if (Mensagens.Pergunta("Deseja remover a foto de perfil ?") == DialogResult.No)
                return;

            imgCliente.Image = Properties.Resources.profile;

            string filePath = $"{Constantes.CAMINHO_IMG_PERFIL}{txtId.Text}.png";

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Mensagens.Info("Foto removida com sucesso.");
                }
                catch (IOException ex)
                {
                    Mensagens.Erro($"O arquivo está sendo usado por outro processo. {ex.Message}");
                }
            }
            else
            {
                Mensagens.Erro("O arquivo não foi encontrado.");
            }
        }

        private void frmCadCliente_Load(object sender, EventArgs e)
        {
            Funcoes.CarregarComboBox(cbEndereco, "Clientes", "endereco");
            Funcoes.CarregarComboBox(cbBairro, "Clientes", "bairro");
            Funcoes.CarregarComboBox(cbCidade, "Clientes", "cidade");

            if (txtId.Text == "")
                return;
            btnSalvar.Text = "Atualizar";

            DataTable dataTable = Funcoes.BuscaSQL($"SELECT * FROM Clientes WHERE id = {txtId.Text}");

            //Fisica ou Juridica
            if (dataTable.Rows[0]["cpf_cnpj"].ToString().Length == 11)
                opCpf.Checked = true;
            else
                opCnpj.Checked = true;

            txtNomeCliente.Text = dataTable.Rows[0]["nome"].ToString();
            mskCpfOrCnpj.Text = dataTable.Rows[0]["cpf_cnpj"].ToString();
            txtRgIe.Text = dataTable.Rows[0]["rg_ie"].ToString();
            txtInscricaoMunicipal.Text = dataTable.Rows[0]["inscricao_municipal"].ToString();

            //Genero
            switch (dataTable.Rows[0]["genero"].ToString())
            {
                case "Masculino":
                    opMasculino.Checked = true;
                    break;
                case "Feminino":
                    opFeminino.Checked = true;
                    break;
                case "Outros":
                    opOutros.Checked = true;
                    break;
            }

            cbEstadoCivil.Text = dataTable.Rows[0]["estado_civil"].ToString();
            mskNascimento.Text = dataTable.Rows[0]["nascimento"].ToString();
            cbEndereco.Text = dataTable.Rows[0]["endereco"].ToString();
            cbBairro.Text = dataTable.Rows[0]["bairro"].ToString();
            mskCep.Text = dataTable.Rows[0]["cep"].ToString();
            txtNumero.Text = dataTable.Rows[0]["numero"].ToString();
            cbCidade.Text = dataTable.Rows[0]["cidade"].ToString();
            cbEstado.Text = dataTable.Rows[0]["estado"].ToString();
            mskCelular.Text = dataTable.Rows[0]["celular"].ToString();
            txtEmail.Text = dataTable.Rows[0]["email"].ToString();
            txtObs.Text = dataTable.Rows[0]["obs"].ToString();

            if (dataTable.Rows[0]["situacao"].ToString() == "Ativo")
                ckSituacao.Checked = true;
            else
                ckSituacao.Checked = false;

            if (File.Exists($"{Constantes.CAMINHO_IMG_PERFIL}{txtId.Text}.png"))
            {
                using (var imgStream = new FileStream($"{Constantes.CAMINHO_IMG_PERFIL}{txtId.Text}.png", FileMode.Open))
                {
                    imgCliente.Image = Image.FromStream(imgStream);
                }
            }

            else
            {
                imgCliente.Image = Properties.Resources.profile;
            }

        }

    }
}