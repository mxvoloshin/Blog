using System.Text.Json.Serialization;

namespace Blog.Application.Common
{
    public abstract class BaseResult
    {
        protected BaseResult(ResultType status)
        {
            Status = status;
        }

        [JsonIgnore]
        public ResultType Status { get; }
        [JsonIgnore]
        public string Message { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
}