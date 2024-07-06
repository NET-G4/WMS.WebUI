namespace WMS.WebUI.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
}
