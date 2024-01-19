namespace RhythmsOfGiving.Controller.UI;

public class LeilaoUI
{
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

    public LeilaoUI(int id, string artista, string title, string localizacao, string genero, string tipo, DateTime fim,
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
}