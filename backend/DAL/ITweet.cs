using backend.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public interface ITweet
    {
        public void addTweet(Models.Tweet tweet);
        public List<tweetDto> getAllTweets(int userId);
        public void editTweet(Models.Tweet tweet);
        public void deleteTweet(tweetDto tweet);
        public tweetDto getTweetById(int id);

    }
}
