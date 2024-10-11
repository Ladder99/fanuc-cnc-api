namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> Download3Async(int length, char[] data)
    {
        return await Task.FromResult(Download3(length, data));
    }

    public dynamic Download3(int length, char[] data)
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_download3(_handle, ref length, data); });

        var nr = new
        {
            @null = false,
            method = "cnc_download3",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_download3",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_download3 = new {length, data}},
            response = new {cnc_download3 = new {}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}