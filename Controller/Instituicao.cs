
public class Instituicao {
    private int id;
    private string nome;
    private string descricao;
    private string logoPath;
    private string link;

    public Instituicao(int id, string nome, string descricao, string logoPath, string link) {
        this.id = id;
        this.nome = nome;
        this.descricao = descricao;
        this.logoPath = logoPath;
        this.link = link;
    }

    public int getId() {
        return id;
    }

    public string getNome() {
        return nome;
    }

    public string getDescricao() {
        return descricao;
    }

    public string getLogoPath() {
        return logoPath;
    }

    public string getLink() {
        return link;
    }
}