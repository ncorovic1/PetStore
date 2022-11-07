using MediatR;
using PetStore.DataContracts.Toys;

namespace PetStore.BLL.Toys.Queries
{
    public class SearchToysQuery : SearchToysRequest, IRequest<ICollection<GetToyResult>>
    {
    }
}
