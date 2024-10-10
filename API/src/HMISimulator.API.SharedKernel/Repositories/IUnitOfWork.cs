using HMISimulator.API.SharedKernel.Models;

namespace HMISimulator.API.SharedKernel.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;

    Task SaveChangesAsync(CancellationToken cancellationToken);

    void Rollback();
}