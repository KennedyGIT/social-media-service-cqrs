using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace post.query.domain.Entities
{
    [Table("Comments")]
    public class CommentEntity
    {
        [Key]
        public Guid CommentId { get; set; }
        public string Username { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }
        public bool Edited { get; set; }
        public Guid PostId { get; set; }

        [JsonIgnore]
        public virtual PostEntity Post { get; set; }


    }
}