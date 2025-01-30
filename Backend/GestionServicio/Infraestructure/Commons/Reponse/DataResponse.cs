namespace Infraestructure.Commons.Reponse
{
    public class DataResponse<T>
    {
        public int? TotalRecords { get; set; }
        public List<T>? Items { get; set; }
    }
}
