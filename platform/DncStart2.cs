namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> DncStart2Async(string filename)
    {
        return await Task.FromResult(DncStart2(filename));
    }

    public dynamic DncStart2(string filename)
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_dncstart2(_handle, filename); });

        var nr = new
        {
            @null = false,
            method = "cnc_dncstart2",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_dncstart2",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_dncstart2 = new {filename}},
            response = new {cnc_dncstart2 = new { }}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}