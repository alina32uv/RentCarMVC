﻿using CarApp.Entities;

namespace CarApp.Interfaces
{
    public interface IBody
    {
        Task<List<CarBodyType>> GetAll();
        Task AddNew(string name);
        Task Update(CarBodyType body);
        Task Delete(CarBodyType body);
        Task<CarBodyType> GetById(int id);
    }
}
