using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using OrdersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersServices.Clients
{
    class DBClient
    {

        IMongoClient _client;
        IMongoDatabase _database;
        public List<DBOrder> dbOrders;
        Thread dbWriter;
        Thread dbReader;
        //SshClient client;
        //ForwardedPortLocal port;


        public DBClient()
        {

            ConnectToDB();
            // vendors =GetVendors();

            //dbWriter = new Thread(dbWrite);
            //dbWriter.Start();

            //dbReader = new Thread(dbRead);
            //dbReader.Start();
        }

        void ConnectToDB()
        {

            // Start("hiefficiency.srmtechsol.com", "root", "Welcome@321", 22);
            //_client = new MongoClient();
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("Hi-Eff");
            //_database = _client.GetDatabase("hiefficiency");
            //Stop();

        }

        public void WriteOrders(string json)
        {
            //var connectionString = "mongodb://localhost";
            //var client = new MongoClient(connectionString);
            //var database = client.GetDatabase("test");
            //string text = System.IO.File.ReadAllText(@"records.JSON");
            try
            {

                var collection = _database.GetCollection<BsonDocument>("Orders");

                var ordersObj = BsonSerializer.Deserialize<BsonDocument>(json);
                var orders = ordersObj.Values.Where(x => x.IsBsonArray);
                if (orders != null)
                {
                    foreach (var order in orders.First().AsBsonArray)
                    {
                        BsonDocument order_document = new BsonDocument(order.AsBsonDocument);
                        var filter = new BsonDocument("id", order["id"].AsInt32);

                        //insert if not exists
                        if (collection.CountDocuments(filter) == 0)
                        {
                           collection.InsertOne(order_document);
                        }

                        ////insert if new, update if exists
                        //BsonDocument order_document = new BsonDocument(order.AsBsonDocument);
                        //var filter = new BsonDocument("id", order["id"].AsInt32);
                        //var result = collection.ReplaceOne(
                        //        filter,
                        //        order_document,
                        //        new UpdateOptions { IsUpsert = true });
                        //var info = result.MatchedCount; //1 - update; 0 - insert
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
 
        }

        //public List<PLCInput> GetPLCInputs()
        //{
        //    List<PLCInput> _plcInputs = new List<PLCInput>();
        //    var collection = _database.GetCollection<BsonDocument>("PLC_inputs");
        //    var filter = new BsonDocument();
        //    var results = collection.Find(filter).Limit(100).ToList();
        //    if (results.Count > 0)
        //    {
        //        foreach (var result in results)
        //        {
        //            PLCInput plcInput = new PLCInput(result);
        //            _plcInputs.Add(plcInput);
        //        }
        //    }
        //    return _plcInputs;
        //}

        //public List<PLCOutput> GetPLCOutputs()
        //{
        //    List<PLCOutput> _plcOutputs = new List<PLCOutput>();
        //    var collection = _database.GetCollection<BsonDocument>("PLC_outputs");
        //    var filter = new BsonDocument();
        //    var results = collection.Find(filter).Limit(100).ToList();
        //    if (results.Count > 0)
        //    {
        //        foreach (var result in results)
        //        {
        //            PLCOutput plcOutput = new PLCOutput(result);
        //            _plcOutputs.Add(plcOutput);
        //        }
        //    }
        //    return _plcOutputs;
        //}

        //public void updatePLCOutput(PLCOutput plcOutput)
        //{
        //    var collection = _database.GetCollection<BsonDocument>("PLC_outputs");
        //    var filter = Builders<BsonDocument>.Filter.Eq("id", plcOutput.id);
        //    var update = Builders<BsonDocument>.Update.Set("iPLC_STATUS", plcOutput.iPlc_Status);
        //    //  .Set("output_int", testOutput.output_int)
        //    //  .Set("output_random", testOutput.output_random);
        //    //var update = Builders<BsonDocument>.Update.Set("order_status", order.order_status);
        //    var result = collection.UpdateMany(filter, update);
        //}

        //public void updatePLCInput(PLCInput plcInput)
        //{
        //    var collection = _database.GetCollection<BsonDocument>("PLC_inputs");
        //    var filter = Builders<BsonDocument>.Filter.Eq("id", plcInput.id);
        //}

        //void dbWrite()
        //{
        //    try
        //    {
        //        var connectionString = "mongodb://localhost";
        //        var client = new MongoClient(connectionString);
        //        var database = client.GetDatabase("test");
        //        string text = System.IO.File.ReadAllText(@"records.JSON");
        //        var document = BsonSerializer.Deserialize<BsonDocument>(text);
        //        var collection = database.GetCollection<BsonDocument>("Orders");
        //        await collection.InsertOneAsync(document);
        //        //while (true)
        //        //{
        //        //    Thread.Sleep(200);
        //        //    if (Workflow.plcOutputs != null)
        //        //    {
        //        //        for (int i = 0; i < Workflow.plcOutputs.Count; i++)
        //        //        {
        //        //            updatePLCOutput(Workflow.plcOutputs[i]);
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //        throw;
        //    }

        //}

        //void dbRead()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            Thread.Sleep(200);
        //            Workflow.plcInputs = GetPLCInputs();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //        throw;
        //    }

        //}


    }

}
