namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdProgInfoBinaryAsync()
    {
        return await Task.FromResult(RdProgInfo());
    }

    public async Task<dynamic> RdProgInfoAsciiAsync()
    {
        return await Task.FromResult(RdProgInfo(1, 31, 2));
    }

    public dynamic RdProgInfo(short type = 0, short length = 12, short format = 1)
    {
        dynamic proginfo;

        if (format == 1)
            proginfo = new Focas.ODBNC_1();
        else if (format == 2)
            proginfo = new Focas.ODBNC_2();
        else
            proginfo = new Focas.ODBNC_1();

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdproginfo(_handle, type, length, proginfo);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdproginfo",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_rdproginfo",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdproginfo = new {type, length, format}},
            response = new {cnc_rdproginfo = new {proginfo}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}