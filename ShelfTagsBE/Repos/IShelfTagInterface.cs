using System;
using ShelfTagsBE.Models;

namespace ShelfTagsBE.Repos;

public interface IShelfTagInterface
{
        Task<ShelfTag> CreateShelfTagAsync(ShelfTag shelfTag);

        Task <List<ShelfTag>> GetAllShelfTagsAsync();
}
