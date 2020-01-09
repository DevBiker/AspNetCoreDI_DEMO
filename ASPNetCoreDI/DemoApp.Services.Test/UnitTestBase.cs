using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Telerik.JustMock;

using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DemoApp.Services.Test.TestServices;

namespace DemoApp.Services.Test
{
    public abstract class UnitTestBase
        {

            [SetUp]
            public void SetupDependencies()
            {
                this.TestLogger = new TestLogger(TestContext.Out);
            }

            
            
            protected TestLogger TestLogger { get; set; }


            protected ILoggerFactory TestLoggerFactory
            {
                get
                {
                    var mock = Mock.Create<ILoggerFactory>();
                    Mock.Arrange(() => mock.CreateLogger(Arg.IsAny<string>())).Returns(TestLogger);
                    return mock;
                }
            }

            /// <summary>
            /// Gets a random items from a Queryable list of items
            /// </summary>
            /// <typeparam name="TEntity">The type of the entity.</typeparam>
            /// <param name="sourceQuery">The source query.</param>
            /// <returns></returns>
            protected TEntity GetRandom<TEntity>(IQueryable<TEntity> sourceQuery)
            {
                var count = sourceQuery.Count();
                switch (count)
                {
                    case 0:
                        throw new InvalidOperationException("No items to select");
                        
                    case 1:
                        return sourceQuery.First();
                    default:
                        var randomIndex = new Random().Next(1, count);
                        var list = sourceQuery.Take(randomIndex).ToList();
                        return list.Last();
                }
            }

            protected void ExecuteWithStopwatch(Action action, [CallerMemberName] string description = null)
            {
                var sw = Stopwatch.StartNew();
                action();
                sw.Stop();
                Console.WriteLine("Execution time for: " + description);
                Console.WriteLine(sw.Elapsed.TotalSeconds.ToString("F4") + " seconds");

            }
            protected async Task ExecuteWithStopwatchAsync(Func<Task> action, [CallerMemberName] string description = null)
            {
                var sw = Stopwatch.StartNew();
                await action().ContinueWith(t =>
                {
                    sw.Stop();
                    Console.WriteLine("Execution time for: " + description);
                    Console.WriteLine(sw.Elapsed.TotalSeconds.ToString("F4") + " seconds");

                });
            }
            protected async Task<TResult> ExecuteWithStopwatchAsync<TResult>(Func<Task<TResult>> action, [CallerMemberName] string description = null)
            {
                var sw = Stopwatch.StartNew();
                var result = await action();

                sw.Stop();
                Console.WriteLine("Execution time for: " + description);
                Console.WriteLine(sw.Elapsed.TotalSeconds.ToString("F4") + " seconds");
                return result;

            }

            protected virtual TResult ExecuteWithStopwatch<TResult>(Func<TResult> func, [CallerMemberName] string description = null)
            {
                var sw = Stopwatch.StartNew();
                TResult result = func();
                sw.Stop();
                Console.WriteLine("Execution time for: " + description);
                Console.WriteLine(sw.Elapsed.TotalSeconds.ToString("F4") + " seconds");
                return result;
            }

            /// <summary>
            /// Deserializes the file resource.
            /// </summary>
            /// <typeparam name="TResult">The type of the result.</typeparam>
            /// <param name="sourceType">Type from the source assembly.</param>
            /// <param name="resourceFileName">Name of the resource file.</param>
            /// <returns></returns>
            public static TResult DeserializeFileResource<TResult>(Type sourceType, string resourceFileName, Action<TResult> afterDeserialize = null)
            {
                var assembly = sourceType.Assembly;
                var resourceNames = assembly.GetManifestResourceNames();

                //Get the correct resource with "Ends With" 
                var resName = resourceNames.FirstOrDefault(e =>
                    e.EndsWith(resourceFileName, StringComparison.InvariantCultureIgnoreCase));
                if (String.IsNullOrWhiteSpace(resName))
                {
                    Debug.WriteLine("Resource file {0} not found.", resName);
                    throw new InvalidOperationException($"The specified resource entry [{resourceFileName}] was not found in the assembly {assembly.GetName().Name}.");
                }
                TResult result;
                using (Stream str = sourceType.Assembly.GetManifestResourceStream(resName))
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        var jsonTextReader = new JsonTextReader(sr);
                        JsonSerializer ser = new JsonSerializer();
                        result = ser.Deserialize<TResult>(jsonTextReader);
                    }
                }

                afterDeserialize?.Invoke(result);

                return result;
            }

            /// <summary>
            /// Deserializes the file resource.
            /// </summary>
            /// <typeparam name="TResult">The type of the result.</typeparam>
            /// <param name="sourceType">Type from the source assembly.</param>
            /// <param name="resourceFileName">Name of the resource file.</param>
            /// <returns></returns>s
            public TResult DeserializeFileResource<TResult>(string resourceFileName, Action<TResult> afterDeserialize = null)
            {
                return DeserializeFileResource(GetType(), resourceFileName, afterDeserialize);
            }

            public async Task<TResult> DeserializeFileResourceAsync<TResult>(string resourceFileName, Action<TResult> afterDeserialize = null)
            {
                return DeserializeFileResource(GetType(), resourceFileName, afterDeserialize);
            }


            /// <summary>
            /// Deserializes the file resource.
            /// </summary>
            /// <typeparam name="TResult">The type of the result.</typeparam>
            /// <param name="sourceType">Type from the source assembly.</param>
            /// <param name="resourceFileName">Name of the resource file.</param>
            /// <returns></returns>
            public async Task<string> GetFileResourceAsStringAsync(string resourceFileName, Action<string> afterDeserialize = null)
            {
                var assembly = this.GetType().Assembly;
                var resourceNames = assembly.GetManifestResourceNames();

                //Get the correct resource with "Ends With" 
                var resName = resourceNames.FirstOrDefault(e =>
                    e.EndsWith(resourceFileName, StringComparison.InvariantCultureIgnoreCase));
                if (String.IsNullOrWhiteSpace(resName))
                {
                    Debug.WriteLine("Resource file {0} not found.", resName);
                }
                string result = string.Empty;
                using (Stream str = assembly.GetManifestResourceStream(resName))
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        result = await sr.ReadToEndAsync();
                    }
                }

                afterDeserialize?.Invoke(result);

                return result;
            }

        }
    }


