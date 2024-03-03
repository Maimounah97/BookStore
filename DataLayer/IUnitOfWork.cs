using DataLayer.Entites;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get; }
        IBooksRepository Books { get; }

        int Complete();
    }
}
