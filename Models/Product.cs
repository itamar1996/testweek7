namespace testweek7.Models
{
    public class Product
    {
        public Product()
        { 
            rating = new Rating();
        }
        public int Id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public Rating rating { get; set; }
        
    }
}
