﻿namespace ApiMvno.Domain.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}