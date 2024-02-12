namespace RhythmsOfGiving.Controller.UI;

public class UserUI
{
    private string email;
    private string nome;
    private string role;
    
    public UserUI(string email, string nome, string role)
    {
        this.email = email;
        this.nome = nome;
        this.role = role;
    }
    
    public string GetEmail()
    {
        return email;
    }
    
    public string GetNome()
    {
        return nome;
    }
    
    public string GetRole()
    {
        return role;
    }
}
