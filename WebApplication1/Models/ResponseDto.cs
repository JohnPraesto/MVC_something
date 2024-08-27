namespace WebApplication1.Models
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public Object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
