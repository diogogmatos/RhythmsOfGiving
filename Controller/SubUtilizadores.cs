
using System.Reflection.Metadata;

namespace RhythmsOfGiving.Controller{
public class SubUtilizadores: ISubUtilizadores {
    private LicitadorDAO licitadores;
    private  Dictionary<int, Administrador> administradores;


    //Construtor
    public SubUtilizadores(){
        this.licitadores = LicitadorDAO.getInstance();
        this.administradores = new Dictionary<int, Administrador>();
        //preencher o map administradores
        //ver classe SubServicos no trabalho DSS para ajudar
    }

        public bool validarAutenticacao(string email, string palavraPasse)
        {
            try
            {
                Licitador l = this.licitadores.get(email);

                if (l.getPalavraPasse().Equals(palavraPasse))
                {
                    return true;
                }
                return false;
            }
            catch (LicitadorNaoExisteException ex)
            {
                throw;
            }
        }

        
        public bool alterarInfosPessoais(string email,string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse){
            // Tenho de pensar o que pode faltar, visto que não estou a verificar os dados
            try{
            Licitador l = this.licitadores.get(email);
            l.setNome(novoNome);
            l.setDataNascimento(novaDataNascimento);
            l.setNcc(novoNumeroCC);
            l.setPalavraPasse(novaPalavraPasse); 
            return true;
            }catch (LicitadorNaoExisteException ex)
            {
            
                throw;
            }
            return false;         
            
        }

        public void AdicionarLicitacao(int idLicitacao, int idLicitador)
        {
            Licitador l  = this.licitadores.get(idLicitacao);
            l.getMinhasLicitacoes().Add(idLicitacao);
            licitadores.put(l.getEmail(),l);

        }   
    }
}