using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DAL
{
    public interface ILike
    {
        public void like(int tweetId, int userId);
        public void disLike(int tweetId, int userId);
    }
}
