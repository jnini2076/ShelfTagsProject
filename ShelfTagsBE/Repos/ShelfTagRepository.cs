using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using ShelfTagsBE.Data;
using ShelfTagsBE.Models;

namespace ShelfTagsBE.Repos;

public class ShelfTagRepository: IShelfTagInterface
{
    private readonly DBmodel context;

    public ShelfTagRepository(DBmodel context)
    {
        this.context= context;
    }

    public async Task<ShelfTag> CreateShelfTagAsync(ShelfTag shelfTag)
    {
        await context.ShelfTags.AddAsync(shelfTag);
        await context.SaveChangesAsync();
        return shelfTag;
    }

    public async Task <List<ShelfTag>> GetAllShelfTagsAsync()
    {
        return await context.ShelfTags.ToListAsync();

    }
}
