﻿using Entities;

namespace Repository.Contracts
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        Task<IEnumerable<Voucher>> GetVoucherListAsync(bool tracking = false);
        Task<bool> CheckCodeVoucherAsync(string code);
        Task<bool> CheckCodeVoucherByIdAsync(Guid id, string code);
        Task<Voucher?> GetVoucherByCodeAsync(string code, bool tracking = false);
        Task<bool> IsValidVoucherByIdAsync(Guid? id, DateOnly dateOnly,
            decimal? minOrderValue);
    }
}
