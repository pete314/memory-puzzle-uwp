using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using memory_puzzle_uwp.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace memory_puzzle_uwp.Helpers
{
    /// <summary>
    /// DatabaseHelper holds a collection of database methods for local sqlite
    /// Followed the example at https://github.com/oysteinkrog/SQLite.Net-PCL/blob/master/examples/Stocks/Stocks.cs
    /// </summary>
    public class DatabaseHelper : SQLiteConnection
    {
        const string DB_NAME = "mplocal.sqlite";

        public DatabaseHelper() : base(new SQLitePlatformWinRT(), Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, DB_NAME)) {
            CreateTable<ScoreModel>();
        }

        /// <summary>
        /// Get scores by collection
        /// </summary>
        /// <param name="collectionName">The name to look for</param>
        /// <param name="limit">The amount results to return</param>
        /// <returns></returns>
        public IEnumerable<ScoreModel> QueryLocalScoreCollection(string collectionName, int limit)
        {
            return Table<ScoreModel>().Where(x => x.Collection.Equals(collectionName)).Take(limit);
        }


        /// <summary>
        /// Get all scores in score, collection name desc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScoreModel> QueryScores() {
            return from sm in Table<ScoreModel>()
                   orderby sm.Collection descending, sm.Score descending
                   select sm;
        }

        /// <summary>
        /// Insert local score
        /// </summary>
        /// <param name="score">The ScoreModel as per</param>
        /// <returns></returns>
        public int InsertScore(ScoreModel score) {
            return Insert(score);
        }

        /// <summary>
        /// Insert scores for 
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public int InsertScores(IEnumerable<ScoreModel> scores) {
            return InsertOrReplaceAll(scores);
        }

    }
}
