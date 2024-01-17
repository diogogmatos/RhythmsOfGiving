
using System;

public class InstituicaoDAO{
     private static InstituicaoDAO? singleton = null;
        private InstituicaoDAO() { }

        public static InstituicaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new InstituicaoDAO();
            }
            return singleton;
        }
        
        public static int size(){
            return 0; // depois usar a query necessária
        }

        internal  List<int> containsKeys() {
            List<int> r = new List<int>();

            return r ;
        }
        internal Instituicao get(int idLicitacao)
        {
            // A exception está mal, só para funcionar o get
            throw new NotImplementedException();
        }

        public void put (int id, Instituicao a)
       {
           //falta definir a lógica
       }

}