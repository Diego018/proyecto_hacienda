namespace Bib_Hacienda.Interfaces {

    public interface IVentaRes {
    
        //Metodo para vender res
        string vender_res(string id_potrero, string nombre, uint monto);
        
    }
    
}