using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Helpers
{
    public static class RetryHandler
    {
        public static T Retry<T>(Func<T> action, int retryCount = 0)
        {
            PolicyResult<T> policyResult = Policy
             .Handle<Exception>()
             .Retry(retryCount)
             .ExecuteAndCapture<T>(action);

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                throw policyResult.FinalException;
            }

            return policyResult.Result;
        }

        public static TEnum GetRandomEnum<TEnum>()
        {

            Random rand = new Random();

            var type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException("Not an enum type");

            var values = Enum.GetValues(type).Cast<TEnum>();

            //TEnum color = (TEnum)rand.Next(0, Enum.GetNames(typeof(TEnum)).Length - 2);
            int randomIndex = (int)(rand.NextDouble() * values.Count());
            return values.ElementAt(randomIndex);
        }
    }


}
