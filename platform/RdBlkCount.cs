namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdBlkCountAsync()
    {
        return await Task.FromResult(RdBlkCount());
    }

    public dynamic RdBlkCount()
    {
        var prog_bc = 0;

        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_rdblkcount(_handle, out prog_bc); });

        var nr = new
        {
            @null = false,
            method = "cnc_rdblkcount",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_rdblkcount",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdblkcount = new { }},
            response = new {cnc_rdblkcount = new {prog_bc}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}