namespace PFSoftware.FinancesApi.Models.Api.Requests
{
    public class CreateEditMinorCategoryRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int MajorCategoryId { get; set; }
    }
}