using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelDB;

namespace TestLevelDb
{
    class Program
    {
        static void Main(string[] args)
        {
			//// Open a connection to a new DB and create if not found
			var options = new Options { CreateIfMissing = true };
			string path = "testdb";

			using (var db = new DB(options, path))
			{
				db.Put("New York", "blue");

				// Create a batch to set key2 and delete key1
				using (var batch = new WriteBatch())
				{
					var keyValue = db.Get("New York");
					batch.Put("Tampa", keyValue);
					batch.Delete("New York");

					// Write the batch
					var writeOptions = new WriteOptions { Sync = true };
					db.Write(batch, writeOptions);
				}
			}
		}
    }
}
