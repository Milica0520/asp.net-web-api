﻿using NotesAndTagsApp.Models.Base;

namespace NotesAndTagsApp.Models
{
    public class User : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string  Password { get; set; }

        public List<Note> Notes { get; set;}

    }
}
