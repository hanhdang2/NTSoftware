using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Shared.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
