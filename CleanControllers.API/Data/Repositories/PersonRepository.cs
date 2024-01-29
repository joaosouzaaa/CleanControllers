using CleanControllers.API.Data.DatabaseContexts;
using CleanControllers.API.Entities;
using CleanControllers.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanControllers.API.Data.Repositories;

public sealed class PersonRepository(CleanControllersDbContext dbContext) : IPersonRepository, IDisposable
{
    private readonly CleanControllersDbContext _dbContext = dbContext;
    private DbSet<Person> DbContextSet => _dbContext.Set<Person>();

    public async Task<bool> AddAsync(Person person)
    {
        await DbContextSet.AddAsync(person);

        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Person person)
    {
        _dbContext.Entry(person).State = EntityState.Modified;

        return await SaveChangesAsync();
    }
    
    public Task<bool> ExistsAsync(int id) =>
        DbContextSet.AsNoTracking().AnyAsync(p => p.Id == id);

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await DbContextSet.FirstOrDefaultAsync(p => p.Id == id);

        DbContextSet.Remove(person!);

        return await SaveChangesAsync();
    }

    public Task<Person?> GetByIdAsync(int id, bool asNoTracking)
    {
        var query = (IQueryable<Person>)DbContextSet;

        if (asNoTracking)
            query = DbContextSet.AsNoTracking();

        return query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<Person>> GetAllAsync() =>
        DbContextSet.AsNoTracking().ToListAsync();

    public void Dispose()
    {
        _dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    private async Task<bool> SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync() > 0;
}
