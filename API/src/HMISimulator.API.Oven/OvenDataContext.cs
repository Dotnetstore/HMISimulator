﻿using HMISimulator.API.Oven.Recipes;
using HMISimulator.API.SharedKernel.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HMISimulator.API.Oven;

internal sealed class OvenDataContext(DbContextOptions<OvenDataContext> options) : ApiContext<OvenDataContext>(options)
{
    internal DbSet<Recipe> Recipes => Set<Recipe>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //modelBuilder.HasDefaultSchema("Oven");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IOvenAssemblyMarker).Assembly);
    }
}