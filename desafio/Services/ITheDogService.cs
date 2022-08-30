using System.Collections.Generic;
using System.Threading.Tasks;
using desafio_api.Services.Models;

namespace desafio_api.Services
{
    public interface ITheDogService
    {
        Task<List<Breeds>> GetBreeds();

        Task<List<Breeds>> GetBreedByName(string name);
    }
}