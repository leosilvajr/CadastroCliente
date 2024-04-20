using System;

namespace CadastroClientes.Model
{
    public class ClientesModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Genero { get; set; }
        public string RgIe { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string EstadoCivil { get; set; }
        public DateTime? Nascimento { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Obs { get; set; }
        public bool Situacao { get; set; }
    }
}
