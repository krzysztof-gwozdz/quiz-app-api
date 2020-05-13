using Newtonsoft.Json;
using System;

namespace QuizApp.Infrastructure.Entities
{
    public abstract class Entity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
