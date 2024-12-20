﻿using FluentValidation.Results;
using Hope.Domain.Model;

namespace Hope.Core.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        IBaseRepo<T> Repository<T>() where T : class;

        Task<int> SaveAsync();
    }
}
