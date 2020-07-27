# CodingChallenge
Paymentsense coding challenge

## Setup
* Please download latest dotnet core (3+) -> https://dotnet.microsoft.com/download/dotnet-core

## Implementation notes
* For the Task 3 (**Cache the call to the data source(https://restcountries.eu/)**) I choose to implement caching using Response Caching Middleware. It's a zero-coding solution that works well across both the server-side and client-side and generates proper HTTP 1.1 Caching-compliant headers, reducing both the need for the API to call the rest countries service, and for the web page to call the API. The alternatives would be to cache only client-side (with the drawback of every new visitor causing a call to Rest Countries API) or use in-memory cache within controller (requires coding), or the combination of the two.
* I've added some Polly policies for retry and circuit breaker around the Rest Countries API to reflect the real-world usage, but I refrained from testing them for the sake of this exercise (they're already tested as part of Polly code anyway)
* I assumed only client-side paging for the countries dataset is needed; server-side paging would add complexity that's probably not needed for a dataset with a capped size.
