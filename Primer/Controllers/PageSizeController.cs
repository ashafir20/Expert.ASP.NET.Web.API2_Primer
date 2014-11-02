using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Primer.Infrastructure;

namespace Primer.Controllers
{
    public class PageSizeController : ApiController, ICustomController
    {
        private static string TargetUrl = "http://apress.com";


        //Synchronous Action Method
/*      public long GetPageSizeSync()
        {
            WebClient wc = new WebClient();
            Stopwatch sw = Stopwatch.StartNew();
            byte[] apressData = wc.DownloadData(TargetUrl);
            Debug.WriteLine("Elapsed ms: {0}", sw.ElapsedMilliseconds);
            return apressData.LongLength;
        }*/

        //Understanding Asynchronous Methods
        //Chapter 3 page 42

        //Understanding the Problem Asynchronous Methods Solve*/

        //Asynchronous Action Method

        public async Task<long> GetPageSize(CancellationToken cToken)
        {
            WebClient wc = new WebClient();
            Stopwatch sw = Stopwatch.StartNew();
            byte[] apressData = await wc.DownloadDataTaskAsync(TargetUrl);
            Debug.WriteLine("Elapsed ms: {0}", sw.ElapsedMilliseconds);
            return apressData.LongLength;
        }


        //Dealing with Cancellation
        //The CancellationToken parameter is used to signal when the request has been cancelled
        //This implementation gets the content from the Apress web site ten times and averages the result.
/*      public async Task<long> GetPageSize10TimesAsync(CancellationToken cToken)
        {
            WebClient wc = new WebClient();
            Stopwatch sw = Stopwatch.StartNew();

            List<long> results = new List<long>();

            for (int i = 0; i < 10; i++)
            {
                if (!cToken.IsCancellationRequested)
                {
                    Debug.WriteLine("Making Request: {0}", i);
                    byte[] apressData = await wc.DownloadDataTaskAsync(TargetUrl);
                    results.Add(apressData.LongLength);
                }
                else
                {
                    Debug.WriteLine("Cancelled");
                    return 0;
                }
            }

            Debug.WriteLine("Elapsed ms: {0}", sw.ElapsedMilliseconds);
            return (long)results.Average();
        }*/

        //Creating a Self-Contained Asynchronous Method Body
/*      public Task<long> GetPageSizeSelfContained(CancellationToken cToken)
        {
            return Task<long>.Factory.StartNew(() =>
            {
                WebClient wc = new WebClient();
                Stopwatch sw = Stopwatch.StartNew();

                List<long> results = new List<long>();

                for (int i = 0; i < 10; i++)
                {
                    if (!cToken.IsCancellationRequested)
                    {
                        Debug.WriteLine("Making Request: {0}", i);
                        results.Add(wc.DownloadData(TargetUrl).LongLength);
                    }
                    else
                    {
                        Debug.WriteLine("Cancelled");
                        return 0;
                    }
                }

                Debug.WriteLine("Elapsed ms: {0}", sw.ElapsedMilliseconds);
                return (long)results.Average();
            });
        }*/

/*      The new method is written so that it can be implemented asynchronously, but my implementation in the
        controller, as shown by Listing 3-10, can do its work in a single statement.*/
/*      public Task PostUrl(string newUrl, CancellationToken cToken)
        {
            TargetUrl = newUrl;
            return Task.FromResult<object>(null);
        }*/

/*      The static Task.FromResult<T> method is used to create a Task that is a wrapper around a specific value. The
        version I used in the listing is helpful when the method doesn’t return a value. If I had a similar method that returned
        an int value, for example, then I might use the following statements:
        ...
        int x = 100;
        int y = 200;
        return Task.FromResult<int>(x + y);*/

/*      Task.FromResult allows you to generate Task wrappers around results that you generated synchronously; in
        this case, the evaluation of x+y happens synchronously and is wrapped in the Task that yields this value immediately.
        There is no asynchronous work performed when you use the FromResult method.*/
    }




}
