namespace HttpClientHandlerReplacement.SendGrid;
public interface ISendGridClient {
    public Task<HttpResponseMessage> SendAsync();
}
