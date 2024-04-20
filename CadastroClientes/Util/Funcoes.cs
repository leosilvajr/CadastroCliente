using System.Data;
using System.Data.OleDb;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace CadastroClientes.Util

{
    public class Funcoes
    {
        public static DataTable BuscaSQL(string comandoSQL)
        {
            DataTable dataTable = new DataTable();
            using (OleDbConnection conexao = new OleDbConnection(Constantes.CONNECTION_STRING))

            {
                conexao.Open();
                using(OleDbCommand cmd = conexao.CreateCommand())
                { 
                    cmd.CommandText = comandoSQL;
                    using(OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd))
                    { 
                        dataAdapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;   
        }

        public static void CarregarComboBox(ComboBox cb, string tabela, string campo)
        {
            cb.DataSource = Funcoes.BuscaSQL($"SELECT DISTINCT {campo} FROM {tabela} WHERE endereco <> '' ");
            cb.DisplayMember = $"{campo}";
            cb.SelectedIndex = -1;
        }
         
    }
}
