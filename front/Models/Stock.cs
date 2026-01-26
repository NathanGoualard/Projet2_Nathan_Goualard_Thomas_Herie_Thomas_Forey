namespace front.Models
{
    public class Stock
    {
        public int Id_Stock { get; set; }

        public int Nb { get; set; }

        
        public int Id_Livres { get; set; }

        public Livre? Livre { get; set; }
    }
}
