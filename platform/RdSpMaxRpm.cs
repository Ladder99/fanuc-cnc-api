namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdSpMaxRpmAsync(short sp_no = 1)
    {
        return await Task.FromResult(RdSpMaxRpm(sp_no));
    }

    public dynamic RdSpMaxRpm(short sp_no = 1)
    {
        var serialspindle = new Focas.ODBSPN();

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdspmaxrpm(_handle, sp_no, serialspindle);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdspmaxrpm",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/position/cnc_rdspmaxrpm",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdspmaxrpm = new {sp_no}},
            response = new {cnc_rdspmaxrpm = new {serialspindle}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}