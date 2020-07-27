using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace YourFinances.SpendingMirror.Domain.Core.DTOs.Object
{
    public class ValidateModel
    {
        public ValidateModel(bool success = true)
        {
            Messages = new List<string>();
            Success = success;

        }

        [JsonProperty]
        public bool Success { get; private set; }

        [JsonProperty]
        public IEnumerable<string> Messages { get; private set; }

        public void SetMessages(IEnumerable<string> messages) => Messages = Messages.Concat(messages);
        public void SetMessages(string message) => Messages = Messages.Concat(new string[] { message });
        public void NotValid() => Success = false;
        public void IsValid() => Success = true;

        public void NotValid(string message)
        {
            Success = false;
            SetMessages(message);
        }
    }
}
