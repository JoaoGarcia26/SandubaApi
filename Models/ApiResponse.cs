namespace SandubaApi.Models
{
    public class ApiResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool sucess, string message, T data)
        {
            this.Success = sucess;
            this.Message = message;
            this.Data = data;
        }
    }
}
