namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> DwnStart3Async(short type)
    {
        return await Task.FromResult(DwnStart(type));
    }

    public dynamic DwnStart(short type)
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_dwnstart3(_handle, type); });

        var nr = new
        {
            @null = false,
            method = "cnc_dwnstart3",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_dwnstart3",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_dwnstart3 = new {type}},
            response = new {cnc_dwnstart3 = new { }}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}