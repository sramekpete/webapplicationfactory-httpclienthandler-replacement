namespace DropoutCoder.HttpClientHandlerReplacement.SendGrid
{
    public interface ISendGridClient
    {
        public Task<HttpResponseMessage> SendAsync();
    }
}
