using backend.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public interface ISearch
    {
        public List<searchDto> searchPeople(string searchTerm, int userId);
        public List<tweetDto> searchTweet(string searchTerm);
    }
}
