

using System.Text;

namespace RhythmsOfGiving.Controller{
public class Artista{

    private int idArtista;
    private string nome;
    private byte[] imagem;
    private int idAdmin;

    private static int contadorArtistas = ArtistaDAO.size();

    //Construtor para fazer get
    public Artista (int idArtista, string nome, byte[] imagem, int idAdmin){
        this.idArtista = idArtista;
        this.nome = nome;
        this.imagem = imagem;
        this.idAdmin = idAdmin;
    }

    //Construtor para criar o Licitador
    public Artista(string nome, byte[] imagem, int idAdmin){
        this.idArtista = ++contadorArtistas;
        this.nome = nome;
        this.imagem = imagem;
        this.idAdmin = idAdmin;
    }

    public int getIdArtista(){
        return idArtista;
    }

    public string getNome(){
        return nome;
    }

    public byte[] getImagem(){
        return imagem;
    }

    public int getIdAdmin(){
        return idAdmin;
    }

    public void setIdArtista(int idArtista){
        this.idArtista = idArtista;
    }

    public void setNome(string nome){
        this.nome = nome;
    }

    public void setImagem(byte[] imagem){
        this.imagem = imagem;
    }

    public void setIdAdmin(int idAdmin){
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
    return (this.idArtista == other.getIdArtista() &&
            this.nome.Equals(other.getNome()) &&
            this.imagem.Equals(other.getImagem()) &&
            this.idAdmin == other.getIdAdmin()
            );
    }

}




}