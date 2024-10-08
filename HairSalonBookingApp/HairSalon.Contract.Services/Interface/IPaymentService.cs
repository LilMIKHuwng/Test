﻿using HairSalon.Core;
using HairSalon.ModelViews.PaymentModelViews;

namespace HairSalon.Contract.Services.Interface
{
    public interface IPaymentService
    {
        Task<BasePaginatedList<PaymentModelView>> GetAllPaymentAsync(int pageNumber, int pageSize, string id, string AppointmentId, string PaymentMethod);

        Task<string> AddPaymentAsync(CreatePaymentModelView model);

        Task<string> UpdatePaymentAsync(string id, UpdatedPaymentModelView model);

        Task<string> DeletePaymentpAsync(string id);
    }
}
