﻿using Facehook.Core.EFRepository;
using Facehook.DAL.Abstracts;
using Facehook.DAL.Context;
using Facehook.Entity.Entites;

namespace Facehook.DAL.Implementations;

public class SavePostRepositoryDal : EFEntityRepositoryBase<SavePost, AppDbContext>, ISavePostDal
{
    public SavePostRepositoryDal(AppDbContext context) : base(context) { }
}
