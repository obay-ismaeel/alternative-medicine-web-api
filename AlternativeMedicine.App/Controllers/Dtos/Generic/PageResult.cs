namespace AlternativeMedicine.App.Controllers.Dtos.Generic;

public record PageResult<T> : Result<List<T>>
{
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int ResultCount { get; set; }
    public int ResultsPerPage { get; set; }
}
