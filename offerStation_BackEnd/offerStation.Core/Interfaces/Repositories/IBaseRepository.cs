using offerStation.Core.Constants;
using offerStation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace offerStation.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Find(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);
        Task<object> FindWithSelectAsync(Expression<Func<T, bool>> criteria,
           Expression<Func<T, object>> selects = null, List<Expression<Func<T, object>>> includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, List<Expression<Func<T, object>>> includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<object>> FindAllWithSelectAsync(Expression<Func<T, bool>> criteria,
           Expression<Func<T, object>> selects = null, List<Expression<Func<T, object>>> includes = null);
        Task<T> Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<OwnerOffer>> FindAllAsync(Func<OwnerOffer, bool> value, List<Expression<Func<OwnerOfferProduct, object>>> list);
    }
}
