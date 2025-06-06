﻿using Entities;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repositories
{
    public class VoucherRepository(Context context) : BaseRepository<Voucher>(context), IVoucherRepository
    {
        private readonly Context _context = context;

        public async Task<IEnumerable<Voucher>> GetVoucherListAsync(bool tracking = false)
        {
            if (tracking)
            {
                return await _context.Vouchers
                    .Where(v => v.Quantity >= 0)
                    .ToListAsync();
            }

            return await _context.Vouchers
                .AsNoTracking()
                .Where(v => v.Quantity >= 0)
                .ToListAsync();
        }

        public async Task<bool> CheckCodeVoucherAsync(string code)
        {
            return await _context.Vouchers
                .AnyAsync(v => v.Code.ToUpper() == code.ToUpper());
        }

        public async Task<bool> CheckCodeVoucherByIdAsync(Guid id, string code)
        {
            return await _context.Vouchers
                .AnyAsync(v => v.Code.ToUpper() == code.ToUpper()
                                && v.Id != id);
        }

        public async Task<Voucher?> GetVoucherByCodeAsync(string code, bool tracking = false)
        {
            IQueryable<Voucher> query = tracking ? _context.Vouchers : _context.Vouchers.AsNoTracking();

            return await query.FirstOrDefaultAsync(v => v.Code.ToUpper() == code.ToUpper());
        }

        public async Task<bool> IsValidVoucherByIdAsync(Guid? id, DateOnly dateOnly, 
            decimal? minOrderValue)
        {
            var voucher = await _context.Vouchers
                .AsNoTracking()
                .Where(v => v.Id == id
                        && v.ExpiryDate > dateOnly
                        && v.MinOrderValue <= minOrderValue
                        && v.Quantity > 0)
                .FirstOrDefaultAsync();

            return voucher != null; // if != null -> return true;
        }
    }
}
