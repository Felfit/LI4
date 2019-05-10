using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Tag
    {
        public Tag()
        {
            Dislikes = new HashSet<Dislikes>();
            Likes = new HashSet<Likes>();
            TagReceita = new HashSet<TagReceita>();
        }

        public int Id { get; set; }
        public string Tag1 { get; set; }
        [JsonIgnore]
        public ICollection<Dislikes> Dislikes { get; set; }
        [JsonIgnore]
        public ICollection<Likes> Likes { get; set; }
        [JsonIgnore]
        public ICollection<TagReceita> TagReceita { get; set; }
    }
}
