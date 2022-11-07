namespace PetStore.DataContracts.Toys;
public class GetToyResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public int TypeId { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double? Rating { get; set; }
}