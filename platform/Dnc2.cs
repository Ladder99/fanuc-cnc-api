namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> Dnc2Async(int length, char[] data)
    {
        return await Task.FromResult(Dnc2(length, data));
    }

    public dynamic Dnc2(int length, char[] data)
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_dnc2(_handle, ref length, data); });

        var nr = new
        {
            @null = false,
            method = "cnc_dnc2",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_dnc2",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_dnc2 = new {length, data}},
            response = new {cnc_dnc2 = new {}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}