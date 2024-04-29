using Agoda.IoC.Core;
using efatoora.service.Data.Entities;

namespace efatoora.service.Data
{
    public interface IKeyRepository
    {
        Task Create(Key key);
        Task<IEnumerable<Key>> GetKeys();
        Task Update(Key key);
    }
    [RegisterPerRequest]

    public class KeyRepository : IKeyRepository
    {
        private readonly Repository repository;

        public KeyRepository(Repository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Key>> GetKeys()
        {
            return repository.Keys.ToList();
        }

        public async Task Create(Key key)
        {
            repository.Keys.Add(key);
            await repository.SaveChangesAsync();
        }

        public async Task Update(Key key)
        {
            repository.Keys.Update(key);
            await repository.SaveChangesAsync();
        }


    }
}
