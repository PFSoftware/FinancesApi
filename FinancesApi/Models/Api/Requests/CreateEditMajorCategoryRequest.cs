using PFSoftware.FinancesApi.Models.Domain;
using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Models.Api.Requests
{
    public class CreateEditMajorCategoryRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<MinorCategory> MinorCategories { get; set; }
    }
}