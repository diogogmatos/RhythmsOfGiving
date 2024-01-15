
using System.Text;

namespace RhythmsOfGiving.Controller{
public class Administrador{

    private int idAdmin;
    private string email;
    private string palavraPasse;

    //Construtor parametrizado
    public Administrador (int idAdmin, string email, string palavraPasse){
        this.idAdmin = idAdmin;
        this.email = email;
        this.palavraPasse = palavraPasse;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Administrador:: {");
        sb.Append(" IdAdmin: ").Append(this.idAdmin);
        sb.Append(" Email: ").Append(this.email);
        sb.Append(" Palavra Passe: ").Append(this.palavraPasse).Append(" }");

        return sb.ToString();
    }

   public override bool Equals(object obj){
    // Verifica se o objeto é o próprio objeto
    if (obj == this)
        return true;

    // Verifica se o objeto é nulo ou pertence a uma classe diferente
    if (obj == null || obj.GetType() != this.GetType())
        return false;

    // Faz a conversão segura do objeto para a classe Licitador
    Administrador other = (Administrador)obj;

    // Verifica a igualdade dos atributos
    return (this.idAdmin == other.idAdmin &&
            this.email == other.email &&
            this.palavraPasse == other.palavraPasse);
    }

}
}