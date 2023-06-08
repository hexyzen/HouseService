namespace HouseService.Accessors.Entities.Dtos
{
    public class DogsFilterPaginationDto
    {
       public string attribute { get; set; }
       public string order { get; set; }
       public int pageNumber { get; set; }
       public int pageSize { get; set; }
    }
}
