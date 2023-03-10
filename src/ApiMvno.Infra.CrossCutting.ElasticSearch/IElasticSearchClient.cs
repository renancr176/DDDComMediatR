namespace ApiMvno.Infra.CrossCutting.ElasticSearch;

public interface IElasticSearchClient
{
    ElasticSearchOptions Config { get; }
    /// <summary>Sends the event asynchronous.</summary>
    /// <param name="event">The event.</param>
    /// <param name="channel">The channel.</param>
    /// <param name="ct">The ct.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    Task SendEventAsync<TClass>(TClass @event, string? index = null, CancellationToken cancellationToken = default)
        where TClass : class;
}