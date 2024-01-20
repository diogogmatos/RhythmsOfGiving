namespace RhythmsOfGiving.Controller.UI;

public class InstituicaoUi
{
    private string nome;
    private string descricao;
    private string link;
    private string logoPath;

    //Construtor para o get
    public InstituicaoUi(string nome, string descricao, string logoPath, string link)
    {
        this.nome = nome;
        this.descricao = descricao;
        this.link = link;
        this.logoPath = logoPath;
    }

    public InstituicaoUi(Instituicao instituicao)
    {
        this.nome = instituicao.GetNome();
        this.descricao = instituicao.GetDescricao();
        this.link = instituicao.GetLink();
        this.logoPath = instituicao.GetLogoPath();
    }

    public string GetNome()
    {
        return nome;
    }

    public string GetDescricao()
    {
        return descricao;
    }

    public string GetLogoPath()
    {
        return logoPath;
    }

    public string GetLink()
    {
        return link;
    }
}