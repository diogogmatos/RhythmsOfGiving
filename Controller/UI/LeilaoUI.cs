using RhythmsOfGiving.Controller.Leiloes;

namespace RhythmsOfGiving.Controller.UI;

public class LeilaoUi
{
    private static readonly int shortDescSize = 300;
    
    private readonly int id;
    private readonly string artista;
    private readonly string title;
    private readonly string localizacao;
    private readonly string genero;
    private readonly string tipo;
    private readonly DateTime fim;
    private readonly string shortDescricao;
    private readonly string descricao;
    private readonly string imagePath;
    private readonly string authorImagePath;
    private readonly bool asCegas;
    private readonly float valorBase;
    private readonly float valorFinal;
    private readonly int instituicao;

    public LeilaoUi(int id, string artista, string title, string localizacao, string genero, string tipo, DateTime fim,
        string shortDescricao, string descricao, string imagePath, string authorImagePath, bool asCegas, float valorBase)
    {
        this.id = id;
        this.artista = artista;
        this.title = title;
        this.localizacao = localizacao;
        this.genero = genero;
        this.tipo = tipo;
        this.fim = fim;
        this.shortDescricao = shortDescricao;
        this.descricao = descricao;
        this.imagePath = imagePath;
        this.authorImagePath = authorImagePath;
        this.asCegas = asCegas;
        this.valorBase = valorBase;
    }

    public LeilaoUi(Leilao leilao, Artista artista)
    {
        this.id = leilao.IdLeilao;
        this.artista = artista.GetNome();
        this.title = leilao.Titulo;
        this.localizacao = leilao.Experiencia.GetLocalizacao();
        this.genero = leilao.Experiencia.GetGeneroMusical().GetNome();
        this.tipo = leilao.GetTipo() == 0 ? "Ás Cegas" : "Inglês";
        this.fim = leilao.DataHoraFinal;
        this.shortDescricao = leilao.Experiencia.GetDescricao().Substring(0,
            Math.Min(leilao.Experiencia.GetDescricao().Length, shortDescSize)) + "...";
        this.descricao = leilao.Experiencia.GetDescricao();
        this.imagePath = leilao.Experiencia.GetImagem();
        this.authorImagePath = artista.GetImagem();
        this.asCegas = leilao.GetTipo() == 0;
        this.valorBase = leilao.ValorBase;
        try
        {
            this.valorFinal = leilao.GetValorUltimaLicitacao();
        }
        catch
        {
            this.valorFinal = 0;
        }
        this.instituicao = leilao.IdInstituicao;
    }

    public int GetId() {
        return id;
    }

    public string GetArtista() {
        return artista;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetLocalizacao() {
        return localizacao;
    }

    public string GetGenero() {
        return genero;
    }

    public string GetTipo() {
        return tipo;
    }

    public DateTime GetFim() {
        return fim;
    }

    public string GetDescricao() {
        return descricao;
    }

    public string GetShortDescricao() {
        return shortDescricao;
    }

    public string GetImagePath() {
        return imagePath;
    }

    public string GetAuthorImagePath() {
        return authorImagePath;
    }

    public bool IsAsCegas() {
        return asCegas;
    }
    
    public float GetValorBase() {
        return valorBase;
    }
    
    public float GetValorFinal() {
        return valorFinal;
    }
    
    public int GetInstituicao() {
        return instituicao;
    }
}