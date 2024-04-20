using CadastroClientes.Model;
using CadastroClientes.Util;
using System.Data.OleDb;
using System;
using System.Windows.Forms;

namespace CadastroClientes.Repository
{
    public class ClientesRepository
    {


        public ClientesRepository()
        {

        }
        public void AdicionaCliente(ClientesModel cliente, TextBox txtId)
        {
            using (OleDbConnection conexao = new OleDbConnection(Constantes.CONNECTION_STRING))
            {
                conexao.Open();

                using (OleDbCommand cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Clientes (nome, cpf_cnpj, genero, rg_ie, inscricao_municipal, estado_civil, nascimento, cep, endereco, numero, bairro, cidade, estado, celular, email, obs, situacao) " +
                                      "VALUES(@nome, @cpf_cnpj, @genero, @rg_ie,@inscricao_municipal, @estado_civil, @nascimento, @cep, @endereco, @numero, @bairro, @cidade, @estado, @celular, @email, @obs, @situacao)";

                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CpfCnpj);
                    cmd.Parameters.AddWithValue("@genero", cliente.Genero);
                    cmd.Parameters.AddWithValue("@rg_ie", cliente.RgIe);
                    cmd.Parameters.AddWithValue("@inscricao_municipal", cliente.InscricaoMunicipal);
                    cmd.Parameters.AddWithValue("@estado_civil", cliente.EstadoCivil);
                    cmd.Parameters.AddWithValue("@nascimento", cliente.Nascimento.HasValue ? (object)cliente.Nascimento : DBNull.Value);
                    cmd.Parameters.AddWithValue("@cep", cliente.Cep);
                    cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@numero", cliente.Numero);
                    cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
                    cmd.Parameters.AddWithValue("@cidade", cliente.Cidade);
                    cmd.Parameters.AddWithValue("@estado", cliente.Estado);
                    cmd.Parameters.AddWithValue("@celular", cliente.Celular);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@obs", cliente.Obs);
                    cmd.Parameters.AddWithValue("@situacao", cliente.Situacao ? "Ativo" : "Cancelado");

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY ";
                    txtId.Text = cmd.ExecuteScalar().ToString();
                }
            }
        }

        public void AtualizaCliente(ClientesModel cliente)
        {
            using (OleDbConnection conexao = new OleDbConnection(Constantes.CONNECTION_STRING))
            {
                conexao.Open();

                using (OleDbCommand cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Clientes SET nome = @nome, cpf_cnpj = @cpf_cnpj, genero = @genero, rg_ie = @rg_ie, inscricao_municipal = @inscricao_municipal, " +
                                      "estado_civil = @estado_civil, nascimento = @nascimento, cep = @cep, endereco = @endereco, numero = @numero, " +
                                      "bairro = @bairro, cidade = @cidade, estado = @estado, celular = @celular, email = @email, obs = @obs, " +
                                      "situacao = @situacao WHERE id = @id";

                    cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CpfCnpj);
                    cmd.Parameters.AddWithValue("@genero", cliente.Genero);
                    cmd.Parameters.AddWithValue("@rg_ie", cliente.RgIe);
                    cmd.Parameters.AddWithValue("@inscricao_municipal", cliente.InscricaoMunicipal);
                    cmd.Parameters.AddWithValue("@estado_civil", cliente.EstadoCivil);
                    cmd.Parameters.AddWithValue("@nascimento", cliente.Nascimento.HasValue ? (object)cliente.Nascimento : DBNull.Value);
                    cmd.Parameters.AddWithValue("@cep", cliente.Cep);
                    cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@numero", cliente.Numero);
                    cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
                    cmd.Parameters.AddWithValue("@cidade", cliente.Cidade);
                    cmd.Parameters.AddWithValue("@estado", cliente.Estado);
                    cmd.Parameters.AddWithValue("@celular", cliente.Celular);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@obs", cliente.Obs);
                    cmd.Parameters.AddWithValue("@situacao", cliente.Situacao ? "Ativo" : "Cancelado");
                    cmd.Parameters.AddWithValue("@id", cliente.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}