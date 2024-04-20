using System;
using System.IO;

namespace CadastroClientes.Util
{
    public static class Constantes
    {

        public static string CAMINHO_BANCO  = $@"{AppDomain.CurrentDomain.BaseDirectory}\database.mdb";
        public static string CAMINHO_IMG_PERFIL = $@"{AppDomain.CurrentDomain.BaseDirectory}\Profile\";
        public static string CONNECTION_STRING = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={CAMINHO_BANCO}";
        
    }
}
