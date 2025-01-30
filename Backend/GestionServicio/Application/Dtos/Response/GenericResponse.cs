using FluentValidation.Results;

namespace Application.Dtos.Response
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public IEnumerable<ValidationFailure>? Erros { get; set; }
    }
}
