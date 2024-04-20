using System.Windows.Forms;

public static class Mensagens
{
    public static void Sucesso(string mensagem)
    {
        MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void Erro(string mensagem)
    {
        MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void Aviso(string mensagem)
    {
        MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void Info(string mensagem)
    {
        MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static DialogResult Pergunta(string mensagem)
    {
        return MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }

}
 