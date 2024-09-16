using BookLibraryApi.Models;

namespace BookLibraryApi.Base
{
    public static class StaticDB
    {


        public static List<Book> Books = new List<Book>()
        {
            new Book(){Title = "The Dream of a Ridiculous Man",Author = "Fyodor Dostoyevsky",},
            new Book(){Title = "Migrations", Author= "Milos Crnjanski" },
            new Book(){Title = "The Picture of Dorian Gray", Author ="Oscar Wilde",},
        };



    }
}
