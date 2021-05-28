using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Data;
using PFSoftware.FinancesApi.Models.Api.Requests;
using PFSoftware.FinancesApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PFSoftware.FinancesApi.Services
{
    public class MajorCategoryService
    {
        private readonly AppDbContext _context;

        public MajorCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateMajorCategory(MajorCategory majorCategory)
        {
            if (majorCategory == null)
                throw new ArgumentNullException(nameof(majorCategory));

            _context.MajorCategories.Add(majorCategory);
            _context.SaveChanges();
        }

        public void DeleteMajorCategory(MajorCategory majorCategory)
        {
            if (majorCategory == null)
                throw new ArgumentNullException(nameof(majorCategory));

            _context.MajorCategories.Remove(majorCategory);
            _context.SaveChanges();
        }

        public IEnumerable<MajorCategory> GetAllMajorCategories()
        {
            return _context
                .MajorCategories
                .Include(x => x.MinorCategories)
                .ToList();
        }

        public MajorCategory GetMajorCategoryById(int id)
        {
            return _context
                .MajorCategories
                .Include(x => x.MinorCategories)
                .FirstOrDefault(v => v.Id == id);
        }

        public void UpdateMajorCategory(CreateEditMajorCategoryRequest request, MajorCategory majorCategory)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                majorCategory.Name = request.Name;
            if (request.MinorCategories != null && request.MinorCategories.Count > 0)
                majorCategory.MinorCategories = new List<MinorCategory>(request.MinorCategories);
            _context.SaveChanges();
        }
    }
}