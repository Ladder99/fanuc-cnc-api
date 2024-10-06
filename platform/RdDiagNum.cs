namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdDiagNumAsync()
    {
        return await Task.FromResult(RdDiagNum());
    }

    public dynamic RdDiagNum()
    {
        var diagnum = new Focas.ODBDIAGNUM();

        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_rddiagnum(_handle, diagnum); });

        var nr = new
        {
            @null = false,
            method = "cnc_rddiagnum",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/misc/cnc_rddiagnum",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rddiagnum = new { }},
            response = new {cnc_rddiagnum = new {diagnum}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}