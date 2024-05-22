using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Interfaces;
using TodoApp.Model.Configuration;
using TodoApp.Model.Entities;

namespace TodoApp.Domain.Implementations;

public class ARepository<TEntity> : IRepository<TEntity>
    where TEntity : IdEntity, new()
{
    protected readonly TodoDbContext DbContext;
    protected readonly DbSet<TEntity> Table;
    protected readonly IMapper Mapper;

    protected ARepository(TodoDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Table = dbContext.Set<TEntity>();
        Mapper = mapper;
    }
    
    public async Task<TDetailDto?> GetByIdAsync<TDetailDto>(int id)
    {
        return await Table
            .Where(r => r.Id == id)
            .ProjectTo<TDetailDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TListDto>> GetAsync<TListDto>(Expression<Func<TEntity, bool>> filter, int start = 0, int count = 10)
    {
        return await Table
            .Where(filter)
            .Skip(start)
            .Take(count)
            .ProjectTo<TListDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<TListDto>> GetAsync<TListDto>(int start = 0, int count = 10)
    {
        return await Table
            .Skip(start)
            .Take(count)
            .ProjectTo<TListDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TListDto> AddAsync<TNewDto, TListDto>(TNewDto newReward)
    {
        var entity = Mapper.Map<TEntity>(newReward);
        var addedEntry = await Table.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return Mapper.Map<TListDto>(addedEntry.Entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var nOfDeletedRows = await Table.Where(e => e.Id == id).ExecuteDeleteAsync();
        return nOfDeletedRows > 0;
    }
}