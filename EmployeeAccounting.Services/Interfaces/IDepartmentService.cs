﻿using EmployeeAccounting.Services.Models;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface IDepartmentService<T> : ICoreService, ICoreCrud<T> where T : BaseModel
    {

    }
}