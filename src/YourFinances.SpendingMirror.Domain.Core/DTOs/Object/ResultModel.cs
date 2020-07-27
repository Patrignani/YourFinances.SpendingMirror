using Newtonsoft.Json;

namespace YourFinances.SpendingMirror.Domain.Core.DTOs.Object
{
    public class ResultModel<T> : ValidateModel
    {
        public ResultModel(bool sucess = true) : base(sucess)
        {

        }

        [JsonProperty]
        public T Data { get; private set; }

        public void SetData(T data) => Data = data;
    }
}
