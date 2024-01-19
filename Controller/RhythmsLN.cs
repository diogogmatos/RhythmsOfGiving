namespace RhythmsOfGiving.Controller;

public class RhythmsLN : IRhythmsLN
{
    private ISubUtilizadores subUtilizadores;
    private ISubLeiloes subLeiloes;
    
    //Construtor
    public RhythmsLN()
    {
        this.subUtilizadores = new SubUtilizadores();
        this.subLeiloes = new SubLeiloes();
    }

}