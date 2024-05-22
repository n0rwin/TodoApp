using System.Linq.Expressions;

namespace TodoApp.Domain.Interfaces;

public interface IRepository<TEntity>
{
    Task<TDetailDto?> GetByIdAsync<TDetailDto>(int id);
    Task<IEnumerable<TListDto>> GetAsync<TListDto>(Expression<Func<TEntity, bool>> filter, int start = 0, int count = 10);
    Task<IEnumerable<TListDto>> GetAsync<TListDto>(int start = 0, int count = 10);
    Task<TListDto> AddAsync<TNewDto, TListDto>(TNewDto newReward);
    Task<bool> DeleteAsync(int id);
}