using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Data;
using PFSoftware.FinancesApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PFSoftware.FinancesApi.Services
{
    public class MinorCategoryService
    {
        private readonly AppDbContext _context;

        public MinorCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateMinorCategory(MinorCategory minorCategory)
        {
            if (minorCategory == null)
                throw new ArgumentNullException(nameof(minorCategory));

            _context.MinorCategories.Add(minorCategory);
            _context.SaveChanges();
        }

        public void DeleteMinorCategory(MinorCategory minorCategory)
        {
            if (minorCategory == null)
                throw new ArgumentNullException(nameof(minorCategory));

            _context.MinorCategories.Remove(minorCategory);
            _context.SaveChanges();
        }

        public IEnumerable<MinorCategory> GetAllMinorCategories()
        {
            return _context
                .MinorCategories
                .Include(x => x.MajorCategory)
                .ToList();
        }

        public MinorCategory GetMinorCategoryById(int id)
        {
            return _context
                .MinorCategories
                .Include(x => x.MajorCategory)
                .FirstOrDefault(v => v.Id == id);
        }

        public void UpdateMinorCategory(int id, MinorCategory minorCategory)
        {
            //Nothing
        }
    }
}