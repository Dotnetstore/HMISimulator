using System.Collections;
using HMISimulator.API.SharedKernel.Repositories;

namespace HMISimulator.API.Oven.Services;

internal sealed class OvenUnitOfWork(OvenDataContext context) : IOvenUnitOfWork, IDisposable
{
    private readonly OvenDataContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly Hashtable _repositories = new();

    IGenericRepository<T> IUnitOfWork.Repository<T>()
    {
        var type = typeof(T).Name;

        if (_repositories.ContainsKey(type)) return (IGenericRepository<T>)_repositories[type]!;
        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
        _repositories.Add(type, repositoryInstance);

        return (IGenericRepository<T>) _repositories[type]!;
    }
    
    async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    void IUnitOfWork.Rollback()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
    }
    
    public void Dispose()
    {
        _context.Dispose();
        _repositories.Clear();
    }
}