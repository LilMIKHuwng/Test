﻿using HairSalon.Core;
using HairSalon.ModelViews.RoleModelViews;
using HairSalon.ModelViews.ShopModelViews;

namespace HairSalon.Contract.Services.Interface
{
    public interface IRoleService
    {
        Task<BasePaginatedList<RoleModelView>> GetAllRoleAsync(int pageNumber, int pageSize);
		Task<RoleModelView> AddRoleAsync(CreateRoleModelView model);
		Task<RoleModelView> UpdateRoleAsync(string id, UpdatedRoleModelView model);
		Task<string> DeleteRoleAsync(string id);
		Task<RoleModelView> GetRoleAsync(string id);
	}
}