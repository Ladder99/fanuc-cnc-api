namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> ExePrgName2Async()
    {
        return await Task.FromResult(ExePrgName2());
    }

    public dynamic ExePrgName2()
    {
        var path_name = new char[256];

        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_exeprgname2(_handle, path_name); });

        var nr = new
        {
            @null = false,
            method = "cnc_exeprgname2",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_exeprgname2",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_exeprgname2 = new { }},
            response = new {cnc_exeprgname2 = new {path_name}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}