﻿namespace Matt.Paginated;

public class PaginatedParams : IPaginated
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}