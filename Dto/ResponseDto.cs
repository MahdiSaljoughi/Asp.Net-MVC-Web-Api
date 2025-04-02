namespace MvcApi.Dto;

public class ResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    
    public object Data { get; set; }
    
    public int StatusCode { get; set; }
}