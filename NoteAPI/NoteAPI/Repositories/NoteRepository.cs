using NoteAPI.Abstractions;

namespace NoteAPI.Repositories
{
    public class NoteRepository : INoteRepository
    {

        private readonly string _conectionString;

        public NoteRepository(string conectionString)
        {
            _conectionString = conectionString; 
        }

        
    }
}
