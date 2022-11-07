using PetStore.Common.Models;

namespace PetStore.DataContracts.Toys
{
    public class PatchToyRequest
    {
        public PatchProperty<string> Name { get; set; }
        public PatchProperty<int> CategoryId { get; set; }
        public PatchProperty<int> TypeId { get; set; }
        public PatchProperty<decimal> Price { get; set; }
        public PatchProperty<int> Quantity { get; set; }
    }
}
