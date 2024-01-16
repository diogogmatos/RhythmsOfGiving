using System.Drawing;

namespace RhythmsOfGiving.Controller{
public class LicitadorDAO{
    private static LicitadorDAO? singleton = null;
        private LicitadorDAO() { }

        public static LicitadorDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LicitadorDAO();
            }
            return singleton;
        }

        public static int size(){
            return 0; // depois usar a query necessária
        }

       public Licitador get(string email){
            //falta definir a lógica
            throw new LicitadorNaoExisteException("O licitador com o email, " + email + " não existe!");
       } 
        public void put(String email ,Object l){
            //falta definir a lógica
        }        
       public Licitador getPorId(int id){
            Licitador l;
            
            throw new LicitadorNaoExisteException("O licitador de id" + id + "não existe!");
       } 
}
}