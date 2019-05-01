using Microsoft.JSInterop;
using System.Threading.Tasks;

public class ExampleJsInterop
{
    private readonly IJSRuntime _jsRuntime;

    public ExampleJsInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public Task CallHelloHelperSayHello(string name)
    {
        return _jsRuntime.InvokeAsync<object>(
            "exampleJsFunctions.sayHello",
            new DotNetObjectRef(new HelloHelper(name)));
    }

    public Task<string> CallHelloHelperShowPrompt()
    {
        return _jsRuntime.InvokeAsync<string>(
            "exampleJsFunctions.showPrompt");
    }
}