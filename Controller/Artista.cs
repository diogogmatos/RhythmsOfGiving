

using System.Text;
using RhythmsOfGiving.Controller.Dados;

namespace RhythmsOfGiving.Controller{
public class Artista{

    private int idArtista;
    private string nome;
    private string imagem;
    private int idAdmin;

    private static int _contadorArtistas = ArtistaDao.Size();

    //Construtor para fazer get
    public Artista (int idArtista, string nome, string imagem, int idAdmin){
        this.idArtista = idArtista;
        this.nome = nome;
        this.imagem = imagem;
        this.idAdmin = idAdmin;
    }

    //Construtor para criar o Licitador
    public Artista(string nome, string imagem, int idAdmin){
        this.idArtista = ++_contadorArtistas;
        this.nome = nome;
        this.imagem = imagem;
        this.idAdmin = idAdmin;
    }

    public int GetIdArtista(){
        return idArtista;
    }

    public string GetNome(){
        return nome;
    }

    public string GetImagem(){
        return imagem;
    }

    public int GetIdAdmin(){
        return idAdmin;
    }

    public void SetIdArtista(int idArtista){
        this.idArtista = idArtista;
    }

    public void SetNome(string nome){
        this.nome = nome;
    }

    public void SetImagem(string imagem){
        this.imagem = imagem;
    }

    public void SetIdAdmin(int idAdmin){
        this.idAdmin = idAdmin;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Artista:: {");
        sb.Append(" IdArtista: ").Append(this.idArtista);
        sb.Append(" Nome: ").Append(this.nome);
        sb.Append(" Id Admin: ").Append(this.idAdmin).Append(" }");

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
    Artista other = (Artista)obj;

    // Verifica a igualdade dos atributos
    return (this.idArtista == other.GetIdArtista() &&
            this.nome.Equals(other.GetNome()) &&
            this.imagem.Equals(other.GetImagem()) &&
            this.idAdmin == other.GetIdAdmin()
            );
    }

}




}