namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdPrgNumAsync()
    {
        return await Task.FromResult(RdPrgNum());
    }

    public dynamic RdPrgNum()
    {
        var prgnum = new Focas.ODBPRO();

        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_rdprgnum(_handle, prgnum); });

        var nr = new
        {
            @null = false,
            method = "cnc_rdprgnum",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_rdprgnum",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdprgnum = new { }},
            response = new {cnc_rdprgnum = new {prgnum}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}