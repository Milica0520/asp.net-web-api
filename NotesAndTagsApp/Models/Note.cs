using NotesAndTagsApp.Models.Base;

namespace NotesAndTagsApp.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }    
        public Enum Priority { get; set; }

        public User User {  get; set; } 

        public int UserId { get; set; }

        public List<Tag> Tags {  get; set; }      
    }
}
